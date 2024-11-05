using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = Administrativo.Models.StatusResponse;

namespace Administrativo.Controllers
{
    public class TiposSegurosController : ControllerBase
    {
        private readonly IBL_TiposSeguros bl;
        private readonly ILogger<TiposSegurosController> logger;

        public TiposSegurosController(IBL_TiposSeguros _bl, ILogger<TiposSegurosController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/<TiposSegurosController>
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<TipoSeguro>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener tiposSeguro");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener tiposSeguro"));
            }
        }


        // GET api/<TiposSegurosController>/5
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(TipoSeguro), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener tipoSeguro");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener tipoSeguro"));
            }
        }

        // POST api/<TiposSegurosController>
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(TipoSeguro), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] TipoSeguro x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar tipoSeguro");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar tipoSeguro"));
            }
        }

        // PUT api/<TiposSegurosController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(TipoSeguro), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] TipoSeguro x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar tipoSeguro");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar tipoSeguro"));
            }
        }

        // DELETE api/<TiposSegurosController>/5
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
                logger.LogError(ex, "Error al eliminar tipoSeguro");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar tipoSeguro"));
            }
        }
    }
}
