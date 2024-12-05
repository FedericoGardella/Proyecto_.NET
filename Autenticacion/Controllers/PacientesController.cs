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
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(Paciente), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(_bl.Get(Id, null));
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
