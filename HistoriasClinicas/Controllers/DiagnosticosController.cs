using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = HistoriasClinicas.Models.StatusResponse;

namespace HistoriasClinicas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticosController : ControllerBase
    {
        private readonly IBL_Diagnosticos _bl;
        private readonly IBL_HistoriasClinicas blHistoriasClinicas;
        private readonly ILogger<DiagnosticosController> _logger;

        public DiagnosticosController(IBL_Diagnosticos bl, IBL_HistoriasClinicas _blHistoriasClinicas, ILogger<DiagnosticosController> logger)
        {
            _bl = bl;
            blHistoriasClinicas = _blHistoriasClinicas;
            _logger = logger;
        }

        // GET: api/Diagnosticos
        [HttpGet]
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(List<Diagnostico>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            try
            {
                var diagnosticos = _bl.GetAll();
                return Ok(diagnosticos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener diagnósticos.");
                return BadRequest(new StatusDTO(false, "Error al obtener diagnósticos."));
            }
        }

        // GET: api/Diagnosticos/5
        [HttpGet("{id:long}")]
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(Diagnostico), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        public IActionResult Get(long id)
        {
            try
            {
                var diagnostico = _bl.Get(id);
                if (diagnostico == null)
                {
                    return NotFound(new StatusDTO(false, "Diagnóstico no encontrado."));
                }
                return Ok(diagnostico);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener diagnóstico.");
                return BadRequest(new StatusDTO(false, "Error al obtener diagnóstico."));
            }
        }

        // POST: api/Diagnosticos
        [HttpPost]
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(Diagnostico), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Post([FromBody] DiagnosticoDTO diagnosticoDTO)
        {
            try
            {
                if (diagnosticoDTO == null)
                {
                    return BadRequest(new StatusDTO(false, "El diagnóstico no puede ser nulo."));
                }

                // Buscar la HistoriaClinica asociada
                var historiaClinica = blHistoriasClinicas.Get(diagnosticoDTO.HistoriaClinicaId);


                if (historiaClinica == null)
                {
                    return NotFound(new StatusDTO(false, "Historia clínica no encontrada."));
                }

                // Crear el Diagnostico basado en el DTO
                var diagnostico = new Diagnostico
                {
                    Descripcion = diagnosticoDTO.Descripcion,
                    Fecha = diagnosticoDTO.Fecha,
                    HistoriaClinicaId = diagnosticoDTO.HistoriaClinicaId,
                    HistoriaClinica = historiaClinica
                };

                // Agregar el nuevo Diagnostico a la lista de Diagnosticos de la HistoriaClinica
                if (historiaClinica.Diagnosticos == null)
                {
                    historiaClinica.Diagnosticos = new List<Diagnostico>();
                }
                historiaClinica.Diagnosticos.Add(diagnostico);

                var createdDiagnostico = _bl.Add(diagnostico);

                return Ok(createdDiagnostico);
            }
            catch (Exception ex)
            {
                return BadRequest(new StatusDTO(false, "Error al guardar diagnóstico."));
            }
        }


        // PUT: api/Diagnosticos/5
        [HttpPut("{id:long}")]
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(Diagnostico), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Put(long id, [FromBody] Diagnostico diagnostico)
        {
            try
            {
                if (diagnostico == null || id != diagnostico.Id)
                {
                    return BadRequest(new StatusDTO(false, "Datos del diagnóstico no son válidos."));
                }

                var updatedDiagnostico = _bl.Update(diagnostico);
                if (updatedDiagnostico == null)
                {
                    return NotFound(new StatusDTO(false, "Diagnóstico no encontrado para actualizar."));
                }

                return Ok(updatedDiagnostico);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar diagnóstico.");
                return BadRequest(new StatusDTO(false, "Error al actualizar diagnóstico."));
            }
        }

        // DELETE: api/Diagnosticos/5
        [HttpDelete("{id:long}")]
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Delete(long id)
        {
            try
            {
                var diagnostico = _bl.Get(id);
                if (diagnostico == null)
                {
                    return NotFound(new StatusDTO(false, "Diagnóstico no encontrado para eliminar."));
                }

                _bl.Delete(id);
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = "Diagnóstico eliminado correctamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar diagnóstico.");
                return BadRequest(new StatusDTO(false, "Error al eliminar diagnóstico."));
            }
        }
    }
}
