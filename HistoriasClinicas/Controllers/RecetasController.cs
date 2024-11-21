using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.DTOs;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using StatusResponse = HistoriasClinicas.Models.StatusResponse;

namespace HistoriasClinicas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetasController : ControllerBase
    {
        private readonly IBL_Recetas _blRecetas;
        private readonly IBL_Medicamentos _blMedicamentos;
        private readonly IBL_HistoriasClinicas _blHistoriasClinicas;
        private readonly ILogger<RecetasController> _logger;

        public RecetasController(
            IBL_Recetas blRecetas,
            IBL_Medicamentos blMedicamentos,
            IBL_HistoriasClinicas blHistoriasClinicas,
            ILogger<RecetasController> logger)
        {
            _blRecetas = blRecetas;
            _blMedicamentos = blMedicamentos;
            _blHistoriasClinicas = blHistoriasClinicas;
            _logger = logger;
        }

        // GET: api/Recetas
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(List<Receta>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            try
            {
                var recetas = _blRecetas.GetAll();
                return Ok(recetas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener recetas.");
                return BadRequest(new StatusDTO(false, "Error al obtener recetas."));
            }
        }

        // GET: api/Recetas/5
        [HttpGet("{id:long}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Receta), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        public IActionResult Get(long id)
        {
            try
            {
                var receta = _blRecetas.Get(id);
                if (receta == null)
                {
                    return NotFound(new StatusDTO(false, "Receta no encontrada."));
                }
                return Ok(receta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener receta.");
                return BadRequest(new StatusDTO(false, "Error al obtener receta."));
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Receta), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Post([FromBody] RecetaDTO recetaDTO)
        {
            try
            {
                if (recetaDTO == null)
                {
                    return BadRequest(new StatusDTO(false, "La receta no puede ser nula."));
                }

                // Verificar si existe la HistoriaClinica
                var historiaClinica = _blHistoriasClinicas.Get(recetaDTO.HistoriaClinicaId);
                if (historiaClinica == null)
                {
                    return NotFound(new StatusDTO(false, "Historia clínica no encontrada."));
                }

                // Obtener los medicamentos por los IDs
                List<Medicamento> medicamentos = null;
                if (recetaDTO.MedicamentoIds != null && recetaDTO.MedicamentoIds.Count > 0)
                {
                    medicamentos = _blMedicamentos.GetByIds(recetaDTO.MedicamentoIds);
                    if (medicamentos.Count != recetaDTO.MedicamentoIds.Count)
                    {
                        return BadRequest(new StatusDTO(false, "Uno o más medicamentos no existen."));
                    }
                }

                // Crear la entidad Receta
                var receta = new Receta
                {
                    Fecha = recetaDTO.Fecha,
                    Descripcion = recetaDTO.Descripcion,
                    Tipo = recetaDTO.Tipo,
                    HistoriaClinicaId = recetaDTO.HistoriaClinicaId,
                    Medicamentos = medicamentos ?? new List<Medicamento>()
                };

                // Guardar la receta
                var createdReceta = _blRecetas.Add(receta);

                return CreatedAtAction(nameof(Get), new { id = createdReceta.Id }, createdReceta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar receta.");
                return BadRequest(new StatusDTO(false, "Error al guardar receta."));
            }
        }


        // PUT: api/Recetas/5
        [HttpPut("{id:long}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Receta), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Put(long id, [FromBody] RecetaDTO recetaDTO)
        {
            try
            {
                if (recetaDTO == null || id != recetaDTO.Id)
                {
                    return BadRequest(new StatusDTO(false, "Datos de la receta no son válidos."));
                }

                var receta = _blRecetas.Get(id);
                if (receta == null)
                {
                    return NotFound(new StatusDTO(false, "Receta no encontrada para actualizar."));
                }

                receta.Fecha = recetaDTO.Fecha;
                receta.Descripcion = recetaDTO.Descripcion;
                receta.Tipo = recetaDTO.Tipo;

                // Actualizar medicamentos si se proporcionan IDs
                if (recetaDTO.MedicamentoIds.Any())
                {
                    var medicamentos = _blMedicamentos.GetByIds(recetaDTO.MedicamentoIds);
                    if (medicamentos.Count != recetaDTO.MedicamentoIds.Count)
                    {
                        return BadRequest(new StatusDTO(false, "Algunos medicamentos no existen."));
                    }
                    receta.Medicamentos = medicamentos;
                }

                var updatedReceta = _blRecetas.Update(receta);
                return Ok(updatedReceta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar receta.");
                return BadRequest(new StatusDTO(false, "Error al actualizar receta."));
            }
        }

        // DELETE: api/Recetas/5
        [HttpDelete("{id:long}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Delete(long id)
        {
            try
            {
                var receta = _blRecetas.Get(id);
                if (receta == null)
                {
                    return NotFound(new StatusDTO(false, "Receta no encontrada para eliminar."));
                }

                _blRecetas.Delete(id);
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = "Receta eliminada correctamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar receta.");
                return BadRequest(new StatusDTO(false, "Error al eliminar receta."));
            }
        }
    }
}
