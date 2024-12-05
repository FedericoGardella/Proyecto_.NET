using BL.BLs;
using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;

namespace HistoriasClinicas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandlerCitasController : ControllerBase
    {
        private readonly IBL_GruposCitas blGruposCitas;
        private readonly IBL_Pacientes blPacientes;
        private readonly ILogger<DiagnosticosController> _logger;

        public HandlerCitasController(IBL_GruposCitas _blGruposCitas, IBL_Pacientes _blPacientes, ILogger<DiagnosticosController> logger)
        {
            blGruposCitas = _blGruposCitas;
            blPacientes = _blPacientes;
            _logger = logger;
        }

        [HttpGet("CitasHoy")]
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(GrupoCita), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult GetCitasHoy(long medicoId)
        {

            // Obtén el token del encabezado de autorización
            var token = HttpContext.Request.Headers["Authorization"].ToString();

            var fechaHoy = DateTime.Today;

            // Lógica para obtener el grupo de citas
            var grupoCitas = blGruposCitas.GetGrupoCitasMedico(medicoId, fechaHoy, token);
            if (grupoCitas == null)
            {
                return NotFound(new StatusDTO(false, "No hay citas para la fecha especificada."));
            }

            // Devolver el resultado
            return Ok(grupoCitas);
        }

        [HttpGet("{id}/GetPaciente")]
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(Paciente), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult GetPaciente(long id)
        {
            // Obtén el token del encabezado de autorización
            var token = HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new StatusDTO(false, "No se proporcionó un token de autenticación."));
            }

            try
            {
                var paciente = blPacientes.Get(id, token);
                if (paciente == null)
                {
                    return NotFound(new StatusDTO(false, "Paciente no encontrado."));
                }
                return Ok(paciente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener paciente.");
                return BadRequest(new StatusDTO(false, "Error al obtener paciente."));
            }
        }
    }
}
