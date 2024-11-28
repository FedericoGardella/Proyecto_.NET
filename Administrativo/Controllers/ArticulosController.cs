using BL.BLs;
using BL.IBLs;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = Administrativo.Models.StatusResponse;

namespace Administrativo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly IBL_Articulos bl;
        private readonly IBL_TiposSeguros blTiposSeguros;
        private readonly IBL_PreciosEspecialidades blPreciosEspecialidades;
        private readonly ILogger<ArticulosController> logger;

        public ArticulosController(IBL_Articulos _bl, IBL_TiposSeguros _blTiposSeguros, IBL_PreciosEspecialidades _blPreciosEspecialidades, ILogger<ArticulosController> _logger)
        {
            bl = _bl;
            blTiposSeguros = _blTiposSeguros;
            blPreciosEspecialidades = _blPreciosEspecialidades;
            logger = _logger;
        }


        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<Articulo>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener articulos");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener articulos"));
            }
        }


        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(Articulo), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener articulo");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener articulo"));
            }
        }


        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Articulos), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Post([FromBody] ArticuloDTO articuloDTO)
        {
            try
            {
                if (articuloDTO == null)
                {
                    return BadRequest(new StatusDTO(false, "El artículo no puede ser nulo."));
                }

                DAL.Models.TiposSeguros? tipoSeguros = null;
                // Validar la existencia del TipoSeguro si se especifica
                if (articuloDTO.TipoSeguroId.HasValue)
                {
                    var tipoSeguro = blTiposSeguros.Get(articuloDTO.TipoSeguroId.Value);
                    if (tipoSeguro == null)
                    {
                        return NotFound(new StatusDTO(false, "Tipo de seguro no encontrado."));
                    }
                    else
                    {
                        logger.LogInformation("TipoSeguro encontrado:");
                        logger.LogInformation($"Nombre: {tipoSeguro.Nombre}");
                    }
                }

                // Validar la existencia del PrecioEspecialidad si se especifica
                if (articuloDTO.PrecioEspecialidadId.HasValue)
                {
                    var precioEspecialidad = blPreciosEspecialidades.Get(articuloDTO.PrecioEspecialidadId.Value);
                    if (precioEspecialidad == null)
                    {
                        return NotFound(new StatusDTO(false, "Precio de especialidad no encontrado."));
                    }
                    else
                    {
                        logger.LogInformation("PrecioEspecialidad encontrado:");
                        logger.LogInformation($"Id: {precioEspecialidad.Id}");
                    }
                }

                // Crear el Articulo basado en el DTO
                var articulo = new Articulo
                {
                    Fecha = articuloDTO.Fecha,
                    Costo = articuloDTO.Costo,
                    TipoSeguroId = articuloDTO.TipoSeguroId.HasValue ? articuloDTO.TipoSeguroId.Value : 0,
                    PrecioEspecialidadId = articuloDTO.PrecioEspecialidadId.HasValue ? articuloDTO.PrecioEspecialidadId.Value : 0
                };

                // Guardar el Articulo
                var createdArticulo = bl.Add(articulo);

                return Ok(createdArticulo);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar el artículo.");
                return BadRequest(new StatusDTO(false, "Error al guardar el artículo."));
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Articulos), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Put(long id, [FromBody] ArticuloDTO articuloDTO)
        {
            try
            {
                // Validar la existencia del artículo a actualizar
                var existingArticulo = bl.Get(id);
                if (existingArticulo == null)
                {
                    return NotFound(new StatusDTO(false, "Artículo no encontrado."));
                }

                // Actualizar los valores recibidos en el DTO (solo los que no sean nulos o tengan valores válidos)
                if (articuloDTO.Fecha != default(DateTime))
                {
                    existingArticulo.Fecha = articuloDTO.Fecha;
                }

                if (articuloDTO.Costo > 0)
                {
                    existingArticulo.Costo = articuloDTO.Costo;
                }

                if (articuloDTO.TipoSeguroId > 0 && articuloDTO.TipoSeguroId != existingArticulo.TipoSeguroId)
                {
                    var tipoSeguro = blTiposSeguros.Get(articuloDTO.TipoSeguroId.Value);
                    if (tipoSeguro == null)
                    {
                        return NotFound(new StatusDTO(false, "Tipo de seguro no encontrado."));
                    }
                    existingArticulo.TipoSeguroId = articuloDTO.TipoSeguroId.HasValue ? articuloDTO.TipoSeguroId.Value : 0;
                }

                if (articuloDTO.PrecioEspecialidadId > 0 && articuloDTO.PrecioEspecialidadId != existingArticulo.PrecioEspecialidadId)
                {
                    var precioEspecialidad = blPreciosEspecialidades.Get(articuloDTO.PrecioEspecialidadId.Value);
                    if (precioEspecialidad == null)
                    {
                        return NotFound(new StatusDTO(false, "Precio de especialidad no encontrado."));
                    }
                    existingArticulo.PrecioEspecialidadId = articuloDTO.PrecioEspecialidadId.HasValue ? articuloDTO.PrecioEspecialidadId.Value : 0;
                }

                // Guardar los cambios
                var updatedArticulo = bl.Update(existingArticulo);

                return Ok(updatedArticulo);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar el artículo.");
                return BadRequest(new StatusDTO(false, "Error al actualizar el artículo."));
            }
        }


        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(StatusResponse), 200)]
        [HttpDelete("{Id}")]
        public IActionResult Delete(long Id)
        {
            try
            {
                bl.Delete(Id);
                return Ok(new StatusResponse() { StatusOk = true, StatusMessage = "" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al eliminar articulo");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar articulo"));
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




    }
}
