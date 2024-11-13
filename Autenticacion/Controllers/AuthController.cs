using Autenticacion.Models;
using BL.BLs;
using BL.IBLs;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.DTOs;
using Shared.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Autenticacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<AuthController> customLogger;
        private readonly IBL_Pacientes blPacientes;
        private readonly DBContext db;

        public AuthController(
                UserManager<Users> _userManager,
                RoleManager<IdentityRole> _roleManager,
                IBL_Pacientes _blPacientes,
                ILogger<AuthController> _customLogger,
                DBContext _db)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            blPacientes = _blPacientes;
            customLogger = _customLogger;
            db = _db;
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            customLogger.LogInformation("Iniciando proceso de autenticación para el usuario.");

            try
            {
                Users user = null;
                try
                {
                    user = await userManager.FindByNameAsync(model.Username);
                    customLogger.LogInformation($"Usuario encontrado: {user?.UserName ?? "No encontrado"}");
                }
                catch (Exception ex)
                {
                    customLogger.LogError(new Exception("Error al buscar usuario", ex), ex.Message);
                    return StatusCode(StatusCodes.Status400BadRequest, "Error al buscar usuario");
                }

                bool isUserActive = false;
                try
                {
                    isUserActive = user != null && user.Activo;
                    customLogger.LogInformation($"Usuario activo: {isUserActive}");
                }
                catch (Exception ex)
                {
                    customLogger.LogError(new Exception("Error al verificar si el usuario está activo", ex), ex.Message);
                    return StatusCode(StatusCodes.Status400BadRequest, "Error al verificar si el usuario está activo");
                }

                bool isUserLockedOut = false;
                if (isUserActive)
                {
                    try
                    {
                        isUserLockedOut = !await userManager.IsLockedOutAsync(user);
                        customLogger.LogInformation($"Usuario bloqueado: {!isUserLockedOut}");
                    }
                    catch (Exception ex)
                    {
                        customLogger.LogError(new Exception("Error al verificar si el usuario está bloqueado", ex), ex.Message);
                        return StatusCode(StatusCodes.Status400BadRequest, "Error al verificar si el usuario está bloqueado");
                    }
                }

                bool isPasswordCorrect = false;
                if (isUserLockedOut)
                {
                    try
                    {
                        isPasswordCorrect = await userManager.CheckPasswordAsync(user, model.Password);
                        customLogger.LogInformation($"Contraseña correcta: {isPasswordCorrect}");
                    }
                    catch (Exception ex)
                    {
                        customLogger.LogError(new Exception("Error al verificar la contraseña del usuario", ex), ex.Message);
                        return StatusCode(StatusCodes.Status400BadRequest, "Error al verificar la contraseña del usuario");
                    }
                }

                if (isUserActive && isUserLockedOut && isPasswordCorrect)
                {
                    List<string> userRoles = new List<string>();
                    try
                    {
                        userRoles = (await userManager.GetRolesAsync(user)).ToList();
                        customLogger.LogInformation("Roles obtenidos correctamente.");
                    }
                    catch (Exception ex)
                    {
                        customLogger.LogError(new Exception("Error al obtener roles del usuario", ex), ex.Message);
                        return StatusCode(StatusCodes.Status400BadRequest, "Error al obtener roles del usuario");
                    }

                    var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    SecurityToken token = null;
                    try
                    {
                        token = GetToken(authClaims);
                        customLogger.LogInformation("Token generado correctamente.");
                    }
                    catch (Exception ex)
                    {
                        customLogger.LogError(new Exception("Error al generar el token JWT", ex), ex.Message);
                        return StatusCode(StatusCodes.Status400BadRequest, "Error al generar el token JWT");
                    }

                    Persona persona = null;
                    try
                    {
                        persona = blPacientes.Get(user.PersonasId);
                        await userManager.ResetAccessFailedCountAsync(user);
                        customLogger.LogInformation("Información de la persona obtenida correctamente.");
                    }
                    catch (Exception ex)
                    {
                        customLogger.LogError(new Exception("Error al obtener información del usuario", ex), ex.Message);
                        return StatusCode(StatusCodes.Status400BadRequest, "Error al obtener información del usuario");
                    }

                    return Ok(new LoginResponse
                    {
                        StatusOk = true,
                        StatusMessage = "Usuario logueado correctamente!",
                        IdUsuario = user.Id,
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo,
                        Email = user.Email,
                        ExpirationMinutes = Convert.ToInt32((token.ValidTo - DateTime.UtcNow).TotalMinutes),
                        Nombre = $"{persona.Apellidos}, {persona.Nombres}",
                        Documento = persona.Documento,
                        Roles = userRoles
                    });
                }

                string errorMessage = user == null ? "Usuario o contraseña incorrecta" :
                                      !user.Activo ? $"El usuario {user.Email} no está habilitado." :
                                      await userManager.IsLockedOutAsync(user) ? "Cuenta bloqueada." :
                                      "Usuario o contraseña incorrecta";

                if (user != null)
                {
                    try
                    {
                        await userManager.AccessFailedAsync(user);
                    }
                    catch (Exception ex)
                    {
                        customLogger.LogError(new Exception("Error al incrementar el contador de fallos", ex), ex.Message);
                    }
                }

                customLogger.LogInformation("Autenticación fallida.");
                return Unauthorized(new LoginResponse { StatusOk = false, StatusMessage = errorMessage });
            }
            catch (Exception ex)
            {
                customLogger.LogError(new Exception("Error en Login", ex), ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, $"Error en Login: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("RegisterPaciente")]
        [ProducesResponseType(typeof(StatusDTO), 200)]
        public async Task<IActionResult> RegisterPaciente([FromBody] RegisterModel model)
        {
            try
            {
                var userExists = await userManager.FindByNameAsync(model.Email);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "El usuario ya existe!"));

                // Busca o crea un nuevo Paciente sin agregarlo aún al contexto
                Paciente paciente = blPacientes.GetPacienteByDocumento(model.Documento) ?? new Paciente
                {
                    Documento = model.Documento,
                    Nombres = model.Nombres,
                    Apellidos = model.Apellidos,
                    Telefono = model.Telefono
                };

                // Solo añade el paciente si es nuevo
                if (paciente.Id == 0)
                {
                    paciente = blPacientes.Add(paciente);
                }

                // Obtén el Id de la persona y desasocia la entidad para evitar problemas de seguimiento
                var personaId = paciente.Id;

                Users user = new Users
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email,
                    Activo = model.Activo,
                    PersonasId = personaId // Asigna solo el Id, no la entidad completa
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    string errors = string.Join(". ", result.Errors.Select(e => e.Description));
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new Models.StatusResponse
                        {
                            StatusOk = false,
                            StatusMessage = "Error al crear usuario! Revisar los datos ingresados y probar nuevamente. Errores: " + errors
                        });
                }

                await userManager.AddToRoleAsync(user, "USER");

                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var param = new Dictionary<string, string?>
                {
                    { "token", token },
                    { "email", user.Email }
                };

                return Ok(new Models.StatusResponse
                {
                    StatusOk = true,
                    StatusMessage = "Usuario creado correctamente!"
                });
            }
            catch (Exception ex)
            {
                customLogger.LogError(new Exception("Error al registrar usuario", ex), ex.Message);
                return BadRequest(new StatusDTO(false, $"Error al registrar usuario: {ex.Message}"));
            }
        }



        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            string? JWT_SECRET = Environment.GetEnvironmentVariable("JWT_SECRET") ?? throw new Exception("JWT Secret no definido.");
            string? JWT_ISSUER = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new Exception("JWT Issuer no definido.");
            string? JWT_AUDIENCE = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? throw new Exception("JWT Audience no definido.");

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_SECRET));
            return new JwtSecurityToken(
                issuer: JWT_ISSUER,
                audience: JWT_AUDIENCE,
                expires: DateTime.Now.AddHours(8),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
