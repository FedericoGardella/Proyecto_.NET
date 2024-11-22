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
        private readonly ILogger<DiagnosticosController> _logger;

        public HandlerCitasController(IBL_GruposCitas _blGruposCitas, ILogger<DiagnosticosController> logger)
        {
            blGruposCitas = _blGruposCitas;
            _logger = logger;
        }

        [HttpGet("CitasHoy")]
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(GrupoCita), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult GetCitasHoy(long medicoId, string fecha)
        {
            // Validar el parámetro fecha
            if (!DateTime.TryParse(fecha, out DateTime fechaConvertida))
            {
                return BadRequest(new StatusDTO(false, "El formato de la fecha no es válido. Se esperaba YYYY-MM-DD."));
            }

            // Lógica para obtener el grupo de citas
            var grupoCitas = blGruposCitas.GetGrupoCitasMedico(medicoId, fechaConvertida.Date);
            if (grupoCitas == null)
            {
                return NotFound(new StatusDTO(false, "No hay citas para la fecha especificada."));
            }

            // Devolver el resultado
            return Ok(grupoCitas);
        }

    }
}
