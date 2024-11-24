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
    public class PreciosEspecialidadesController : ControllerBase
    {
        private readonly IBL_PreciosEspecialidades bl;
        private readonly IBL_Articulos blArticulos;
        private readonly ILogger<PreciosEspecialidadesController> logger;

        public PreciosEspecialidadesController(IBL_PreciosEspecialidades _bl, IBL_Articulos _blArticulos, ILogger<PreciosEspecialidadesController> _logger)
        {
            bl = _bl;
            blArticulos = _blArticulos;
            logger = _logger;
        }

        // GET: api/<PreciosEspecialidadesController>
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<PrecioEspecialidad>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener preciosEspecialidades");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener preciosEspecialidades"));
            }
        }


        // GET api/<PreciosEspecialidadesController>/5
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(PrecioEspecialidad), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener precioEspecialidad");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener precioEspecialidad"));
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(PreciosEspecialidades), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Post([FromBody] PrecioEspecialidadDTO precioEspecialidadDTO)
        {
            try
            {
                if (precioEspecialidadDTO == null)
                {
                    return BadRequest(new StatusDTO(false, "El precio de especialidad no puede ser nulo."));
                }

                // Validar la existencia del Articulo asociado
                var articulo = blArticulos.Get(precioEspecialidadDTO.ArticuloId);
                if (articulo == null)
                {
                    return NotFound(new StatusDTO(false, "Artículo no encontrado."));
                }
                else
                {
                    logger.LogInformation("Artículo encontrado:");
                    logger.LogInformation($"Nombre: {articulo.Nombre}");
                }

                // Crear el objeto PreciosEspecialidades basado en el DTO
                var precioEspecialidad = new PrecioEspecialidad
                {
                    ArticuloId = precioEspecialidadDTO.ArticuloId
                };

                // Guardar el nuevo registro
                var createdPrecioEspecialidad = bl.Add(precioEspecialidad);

                return Ok(createdPrecioEspecialidad);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar el precio de especialidad.");
                return BadRequest(new StatusDTO(false, "Error al guardar el precio de especialidad."));
            }
        }


        // PUT api/<PreciosEspecialidadesController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(PrecioEspecialidad), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] PrecioEspecialidad x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar precioEspecialidad");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar precioEspecialidad"));
            }
        }

        // DELETE api/<PreciosEspecialidadesController>/5
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
                logger.LogError(ex, "Error al eliminar precioEspecialidad");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar precioEspecialidad"));
            }
        }
    }
}
