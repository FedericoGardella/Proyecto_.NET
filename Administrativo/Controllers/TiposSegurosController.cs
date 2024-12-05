using BL.BLs;
using BL.IBLs;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using System.Collections.Generic;

namespace Administrativo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposSegurosController : ControllerBase
    {
        private readonly IBL_TiposSeguros bl;
        private readonly IBL_Articulos blArticulos;
        private readonly ILogger<TiposSegurosController> logger;

        public TiposSegurosController(IBL_TiposSeguros _bl, IBL_Articulos _blArticulos, ILogger<TiposSegurosController> _logger)
        {
            bl = _bl;
            blArticulos = _blArticulos;
            logger = _logger;
        }

        // GET: api/TiposSeguros
        [HttpGet]
        [Authorize(Roles = "ADMIN, PACIENTE")]
        [ProducesResponseType(typeof(List<TipoSeguro>), 200)]
        public IActionResult Get()
        {
            try
            {
                var tiposSeguros = bl.GetAll();
                if (tiposSeguros == null || !tiposSeguros.Any())
                {
                    logger.LogWarning("No se encontraron tipos de seguro.");
                    return NotFound(new StatusDTO(false, "No se encontraron tipos de seguro."));
                }

                logger.LogInformation("Se obtuvieron {Count} tipos de seguro.", tiposSeguros.Count);
                return Ok(tiposSeguros);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener tipos de seguro");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al obtener tipos de seguro"));
            }
        }

        // GET: api/TiposSeguros/5
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN, PACIENTE")]
        [ProducesResponseType(typeof(TipoSeguro), 200)]
        public IActionResult Get(long id)
        {
            try
            {
                var tipoSeguro = bl.Get(id);
                if (tipoSeguro == null)
                    return NotFound(new StatusDTO(false, "Tipo de seguro no encontrado"));

                return Ok(tipoSeguro);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener el tipo de seguro");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al obtener el tipo de seguro"));
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(TiposSeguros), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Post([FromBody] TipoSeguroDTO tiposSegurosDTO)
        {
            try
            {
                if (tiposSegurosDTO == null)
                {
                    return BadRequest(new StatusDTO(false, "El tipo de seguro no puede ser nulo."));
                }

                // Crear un nuevo artículo con precio y fecha actual para este tipo de seguro
                var nuevoArticulo = new Articulo
                {
                    Fecha = DateTime.UtcNow,
                    Costo = tiposSegurosDTO.Costo
                };
                var articuloCreado = blArticulos.Add(nuevoArticulo);

                logger.LogInformation("Nuevo artículo creado con ID: {ArticuloId}", articuloCreado.Id);

                // Crear el TipoSeguro basado en el DTO
                var tipoSeguro = new TipoSeguro
                {
                    Nombre = tiposSegurosDTO.Nombre,
                    Descripcion = tiposSegurosDTO.Descripcion,
                    ArticuloId = articuloCreado.Id
                };
                var tipoSeguroCreado = bl.Add(tipoSeguro);

                logger.LogInformation("Tipo de seguro creado con ID: {TipoSeguroId}", tipoSeguroCreado.Id);

                // Actualizar el artículo creado con el TipoSeguroId
                articuloCreado.TipoSeguroId = tipoSeguroCreado.Id;
                blArticulos.Update(articuloCreado);

                logger.LogInformation("Artículo actualizado con TipoSeguroId: {TipoSeguroId}", tipoSeguroCreado.Id);

                return Ok(tipoSeguroCreado);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar el tipo de seguro.");
                return BadRequest(new StatusDTO(false, "Error al guardar el tipo de seguro."));
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(TipoSeguro), 200)]
        public IActionResult Put(long id, [FromBody] TipoSeguroUpdateDTO tipoSeguroUpdate)
        {
            try
            {
                var existingTipoSeguro = bl.Get(id);
                if (existingTipoSeguro == null)
                {
                    return NotFound(new StatusDTO(false, "Tipo de seguro no encontrado"));
                }

                // Actualizamos solo los campos permitidos
                if (!string.IsNullOrEmpty(tipoSeguroUpdate.Nombre))
                {
                    existingTipoSeguro.Nombre = tipoSeguroUpdate.Nombre;
                }

                if (!string.IsNullOrEmpty(tipoSeguroUpdate.Descripcion))
                {
                    existingTipoSeguro.Descripcion = tipoSeguroUpdate.Descripcion;
                }

                // ArticuloId NO se modifica aquí

                var updatedTipoSeguro = bl.Update(existingTipoSeguro);
                return Ok(updatedTipoSeguro);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar el tipo de seguro");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al actualizar el tipo de seguro"));
            }
        }


        // DELETE: api/TiposSeguros/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(StatusDTO), 200)]
        public IActionResult Delete(long id)
        {
            try
            {
                var existingTipoSeguro = bl.Get(id);
                if (existingTipoSeguro == null)
                    return NotFound(new StatusDTO(false, "Tipo de seguro no encontrado"));

                bl.Delete(id);
                return Ok(new StatusDTO(true, "Tipo de seguro eliminado correctamente"));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al eliminar el tipo de seguro");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al eliminar el tipo de seguro"));
            }
        }


        [HttpPut("updateCosto/{tipoSeguroId}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Articulo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult UpdateCosto(long tipoSeguroId, [FromBody] decimal nuevoCosto)
        {
            try
            {
                logger.LogInformation("Solicitud recibida para actualizar el costo del TipoSeguro con ID: {TipoSeguroId}", tipoSeguroId);
                logger.LogInformation("Nuevo costo recibido: {NuevoCosto}", nuevoCosto);

                // Llamar al BL para actualizar el costo
                var nuevoArticulo = bl.UpdateCosto(tipoSeguroId, nuevoCosto);

                logger.LogInformation("Costo actualizado exitosamente. Nuevo artículo creado con ID: {ArticuloId}, Fecha: {Fecha}, Costo: {Costo}",
                    nuevoArticulo.Id, nuevoArticulo.Fecha, nuevoArticulo.Costo);

                return Ok(nuevoArticulo);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar el costo para TipoSeguro con ID: {TipoSeguroId}", tipoSeguroId);
                return BadRequest(new StatusDTO(false, ex.Message));
            }
        }


        [HttpGet("{id}/CostoArticulo")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult GetCostoArticulo(long id)
        {
            try
            {
                // Llamar al BL para obtener el costo del artículo asociado
                var costo = bl.GetCostoArticulo(id);
                return Ok(costo);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener el costo del artículo para el TipoSeguro con ID: {TipoSeguroId}", id);
                return NotFound(new StatusDTO(false, ex.Message));
            }
        }

    }
}
