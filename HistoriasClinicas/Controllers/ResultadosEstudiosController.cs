using BL.IBLs;
using DAL.Models;
using HistoriasClinicas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;

namespace HistoriasClinicas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultadosEstudiosController : ControllerBase
    {
        private readonly IBL_ResultadosEstudios _bl;
        private readonly IBL_HistoriasClinicas blHistoriasClinicas;
        private readonly ILogger<ResultadosEstudiosController> _logger;

        public ResultadosEstudiosController(IBL_ResultadosEstudios bl, IBL_HistoriasClinicas _blHistoriasClinicas, ILogger<ResultadosEstudiosController> logger)
        {
            _bl = bl;
            blHistoriasClinicas = _blHistoriasClinicas;
            _logger = logger;
        }

        // GET: api/ResultadosEstudios
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(List<ResultadoEstudio>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            try
            {
                var resultadosEstudios = _bl.GetAll();
                return Ok(resultadosEstudios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener resultados de estudios.");
                return BadRequest(new StatusDTO(false, "Error al obtener resultados de estudios."));
            }
        }

        // GET: api/ResultadosEstudios/5
        [HttpGet("{id:long}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ResultadoEstudio), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        public IActionResult Get(long id)
        {
            try
            {
                var resultadoEstudio = _bl.Get(id);
                if (resultadoEstudio == null)
                {
                    return NotFound(new StatusDTO(false, "Resultado de estudio no encontrado."));
                }
                return Ok(resultadoEstudio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener resultado de estudio.");
                return BadRequest(new StatusDTO(false, "Error al obtener resultado de estudio."));
            }
        }

        // POST: api/ResultadosEstudios
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ResultadoEstudio), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Post([FromBody] ResultadoEstudioDTO resultadoEstudioDTO)
        {
            try
            {
                if (resultadoEstudioDTO == null)
                {
                    return BadRequest(new StatusDTO(false, "El resultado de estudio no puede ser nulo."));
                }

                // Buscar la HistoriaClinica asociada
                var historiaClinica = blHistoriasClinicas.Get(resultadoEstudioDTO.HistoriaClinicaId);


                if (historiaClinica == null)
                {
                    return NotFound(new StatusDTO(false, "Historia clínica no encontrada."));
                }

                // Crear el ResultadoEstudio basado en el DTO
                var resultadoEstudio = new ResultadoEstudio
                {
                    Descripcion = resultadoEstudioDTO.Descripcion,
                    Fecha = resultadoEstudioDTO.Fecha,
                    HistoriaClinicaId = resultadoEstudioDTO.HistoriaClinicaId,
                    HistoriaClinica = historiaClinica
                };

                // Agregar el nuevo ResultadoEstudio a la lista de ResultadosEstudios de la HistoriaClinica
                if (historiaClinica.ResultadosEstudios == null)
                {
                    historiaClinica.ResultadosEstudios = new List<ResultadoEstudio>();
                }
                historiaClinica.ResultadosEstudios.Add(resultadoEstudio);

                var createdResultadoEstudio = _bl.Add(resultadoEstudio);

                return Ok(createdResultadoEstudio);
            }
            catch (Exception ex)
            {
                return BadRequest(new StatusDTO(false, "Error al guardar resultado de estudio."));
            }
        }


        // PUT: api/ResultadosEstudios/5
        [HttpPut("{id:long}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ResultadoEstudio), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Put(long id, [FromBody] ResultadoEstudio resultadoEstudio)
        {
            try
            {
                if (resultadoEstudio == null || id != resultadoEstudio.Id)
                {
                    return BadRequest(new StatusDTO(false, "Datos del resultado de estudio no son válidos."));
                }

                var updatedResultadoEstudio = _bl.Update(resultadoEstudio);
                if (updatedResultadoEstudio == null)
                {
                    return NotFound(new StatusDTO(false, "Resultado de estudio no encontrado para actualizar."));
                }

                return Ok(updatedResultadoEstudio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar resultado de estudio.");
                return BadRequest(new StatusDTO(false, "Error al actualizar resultado de estudio."));
            }
        }

        // DELETE: api/ResultadosEstudios/5
        [HttpDelete("{id:long}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Delete(long id)
        {
            try
            {
                var resultadoEstudio = _bl.Get(id);
                if (resultadoEstudio == null)
                {
                    return NotFound(new StatusDTO(false, "Resultado de estudio no encontrado para eliminar."));
                }

                _bl.Delete(id);
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = "Resultado de estudio eliminado correctamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar resultado de estudio.");
                return BadRequest(new StatusDTO(false, "Error al eliminar resultado de estudio."));
            }
        }
    }
}
