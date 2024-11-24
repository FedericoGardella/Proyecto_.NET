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
        private readonly IBL_Especialidades blEspecialidades;
        private readonly ILogger<ArticulosController> logger;

        public ArticulosController(IBL_Articulos _bl, IBL_TiposSeguros _blTiposSeguros, IBL_Especialidades _blEspecialidades, ILogger<ArticulosController> _logger)
        {
            bl = _bl;
            blTiposSeguros = _blTiposSeguros;
            blEspecialidades = _blEspecialidades;
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

                // Validar la existencia del TipoSeguro
                var tipoSeguro = blTiposSeguros.Get(articuloDTO.TipoSeguroId);
                if (tipoSeguro == null)
                {
                    return NotFound(new StatusDTO(false, "Tipo de seguro no encontrado."));
                }
                else
                {
                    logger.LogInformation("TipoSeguro encontrado:");
                    logger.LogInformation($"Nombre: {tipoSeguro.Nombre}");
                }

                // Validar la existencia de la Especialidad
                var especialidad = blEspecialidades.Get(articuloDTO.EspecialidadId);
                if (especialidad == null)
                {
                    return NotFound(new StatusDTO(false, "Especialidad no encontrada."));
                }
                else
                {
                    logger.LogInformation("Especialidad encontrada:");
                    logger.LogInformation($"Nombre: {especialidad.Nombre}");
                }

                // Crear el Articulo basado en el DTO
                var articulo = new Articulo
                {
                    Nombre = articuloDTO.Nombre,
                    Fecha = articuloDTO.Fecha,
                    Costo = articuloDTO.Costo,
                    TipoSeguroId = articuloDTO.TipoSeguroId,
                    EspecialidadId = articuloDTO.EspecialidadId
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
                if (articuloDTO == null)
                {
                    return BadRequest(new StatusDTO(false, "El artículo no puede ser nulo."));
                }

                // Validar la existencia del artículo a actualizar
                var existingArticulo = bl.Get(id);
                if (existingArticulo == null)
                {
                    return NotFound(new StatusDTO(false, "Artículo no encontrado."));
                }

                // Validar la existencia del TipoSeguro si se va a actualizar
                if (articuloDTO.TipoSeguroId != existingArticulo.TipoSeguroId)
                {
                    var tipoSeguro = blTiposSeguros.Get(articuloDTO.TipoSeguroId);
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

                // Validar la existencia de la Especialidad si se va a actualizar
                if (articuloDTO.EspecialidadId != existingArticulo.EspecialidadId)
                {
                    var especialidad = blEspecialidades.Get(articuloDTO.EspecialidadId);
                    if (especialidad == null)
                    {
                        return NotFound(new StatusDTO(false, "Especialidad no encontrada."));
                    }
                    else
                    {
                        logger.LogInformation("Especialidad encontrada:");
                        logger.LogInformation($"Nombre: {especialidad.Nombre}");
                    }
                }

                // Actualizar el artículo con los nuevos valores del DTO
                existingArticulo.Nombre = articuloDTO.Nombre;
                existingArticulo.Fecha = articuloDTO.Fecha;
                existingArticulo.Costo = articuloDTO.Costo;
                existingArticulo.TipoSeguroId = articuloDTO.TipoSeguroId;
                existingArticulo.EspecialidadId = articuloDTO.EspecialidadId;

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
    }
}
