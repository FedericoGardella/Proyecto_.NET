using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;

using BL.IBLs;
using Shared.DTOs;

using StatusResponse = Administrativo.Models.StatusResponse;

namespace Administrativo.Controllers
{
    public class PreciosEspecialidadesController : ControllerBase
    {
        private readonly IBL_PreciosEspecialidades bl;
        private readonly ILogger<PreciosEspecialidadesController> logger;

        public PreciosEspecialidadesController(IBL_PreciosEspecialidades _bl, ILogger<PreciosEspecialidadesController> _logger)
        {
            bl = _bl;
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

        // POST api/<PreciosEspecialidadesController>
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(PrecioEspecialidad ), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] PrecioEspecialidad x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar precioEspecialidad");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar precioEspecialidad"));
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
