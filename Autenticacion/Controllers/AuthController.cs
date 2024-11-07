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
            try
            {
                // 1. Buscar el usuario por nombre de usuario
                Users user = await userManager.FindByNameAsync(model.Username);

                // 2. Verificar si el usuario cumple con los requisitos de autenticación
                if (user != null && user.Activo && !await userManager.IsLockedOutAsync(user) &&
                    await userManager.CheckPasswordAsync(user, model.Password))
                {
                    // 3. Obtener los roles del usuario y crear los claims para el token
                    var userRoles = await userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    // 4. Generar el token JWT
                    var token = GetToken(authClaims);

                    // 5. Preparar la respuesta exitosa con información del usuario
                    Persona persona = blPacientes.Get(user.PersonasId);
                    await userManager.ResetAccessFailedCountAsync(user);

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
                        Roles = userRoles.ToList()
                    });
                }

                // 6. Manejo de errores de autenticación
                string errorMessage = user == null ? "Usuario o contraseña incorrecta" :
                                      !user.Activo ? $"El usuario {user.Email} no está habilitado." :
                                      await userManager.IsLockedOutAsync(user) ? "Cuenta bloqueada." :
                                      "Usuario o contraseña incorrecta";

                // Incrementar contador de fallos si el usuario existe
                if (user != null)
                {
                    await userManager.AccessFailedAsync(user);
                }

                return Unauthorized(new LoginResponse { StatusOk = false, StatusMessage = errorMessage });
            }
            catch (Exception ex)
            {
                // 7. Registrar y retornar el error en caso de excepción
                customLogger.LogError(new Exception("Error en Login", ex), ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "Error en Login");
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
