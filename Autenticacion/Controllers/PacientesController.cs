using Autenticacion.Models;
using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.DTOs;
using Shared.Entities;

namespace Autenticacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IBL_Pacientes _bl;
        private readonly IBL_HistoriasClinicas blHistoriasClinicas;
        private readonly ILogger<PacientesController> _logger;

        public PacientesController(IBL_Pacientes bl, ILogger<PacientesController> logger)
        {
            _bl = bl;
            _logger = logger;
        }

        // GET: api/Pacientes
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(List<Paciente>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_bl.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pacientes");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener pacientes"));
            }
        }

        // GET: api/Pacientes/documento/{documento}
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(Paciente), 200)]
        [HttpGet("documento/{documento}")]
        public IActionResult GetPacienteByDocumento(string documento)
        {
            try
            {
                return Ok(_bl.GetPacienteByDocumento(documento));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener paciente por documento");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener paciente por documento"));
            }
        }


        // GET api/<PacientesController>/5
        [Authorize(Roles = "ADMIN, MEDICO, PACIENTE")]
        [ProducesResponseType(typeof(Paciente), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                // Obtener el token del encabezado de autorización
                var token = HttpContext.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new StatusDTO(false, "Token no proporcionado."));
                }

                // Decodificar el token para obtener los claims
                var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // Extraer el email desde los claims del token
                var emailFromToken = jwtToken.Claims
                    .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                    ?.Value;

                if (string.IsNullOrEmpty(emailFromToken))
                {
                    return Unauthorized(new StatusDTO(false, "No se pudo determinar el email del usuario."));
                }

                // Obtener el email del paciente desde el DAL
                var emailFromPaciente = _bl.GetPacienteEmail(Id);

                if (string.IsNullOrEmpty(emailFromPaciente))
                {
                    return NotFound(new StatusDTO(false, $"No se encontró el paciente con ID {Id}."));
                }

                // Verificar que el rol del usuario sea PACIENTE y que el email coincida
                var userRole = jwtToken.Claims
                    .FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                    ?.Value;

                if (userRole == "PACIENTE" && !emailFromToken.Equals(emailFromPaciente, StringComparison.OrdinalIgnoreCase))
                {
                    return Forbid(); // Responder con 403 Forbidden si no coincide
                }

                // Obtener y devolver la información del paciente
                var paciente = _bl.Get(Id, null);
                return Ok(paciente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener paciente");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener paciente"));
            }
        }

        // PUT api/<PacientesController>/5
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(PacienteDTO), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] PacienteDTO x)
        {
            try
            {
                return Ok(_bl.Update(x));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar paciente");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar paciente"));
            }
        }

        // DELETE: api/Pacientes/5
        [HttpDelete("{id:long}")]
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Delete(long id)
        {
            try
            {
                var paciente = _bl.Get(id, null);
                if (paciente == null)
                {
                    return NotFound(new StatusDTO(false, "Paciente no encontrado para eliminar."));
                }

                _bl.Delete(id);
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = "Paciente eliminado correctamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar paciente.");
                return BadRequest(new StatusDTO(false, "Error al eliminar paciente."));
            }
        }

        // GET: api/Pacientes/{id}/email
        [HttpGet("{id:long}/email")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        public IActionResult GetEmail(long id)
        {
            try
            {
                var email = _bl.GetPacienteEmail(id);
                if (string.IsNullOrEmpty(email))
                {
                    return NotFound(new StatusDTO(false, "Email no encontrado para el paciente proporcionado."));
                }

                return Ok(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el email del paciente.");
                return BadRequest(new StatusDTO(false, "Error al obtener el email del paciente."));
            }
        }

    }
}
