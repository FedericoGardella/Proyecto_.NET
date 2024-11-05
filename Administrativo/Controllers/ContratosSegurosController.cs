using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = Administrativo.Models.StatusResponse;

namespace Administrativo.Controllers
{
    public class ContratosSegurosController : ControllerBase
    {
        private readonly IBL_ContratosSeguros bl;
        private readonly ILogger<ContratosSegurosController> logger;

        public ContratosSegurosController(IBL_ContratosSeguros _bl, ILogger<ContratosSegurosController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/<ContratosSegurosController>
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<ContratoSeguro>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener contratosseguros");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener contratosseguros"));
            }
        }


        // GET api/<ContratosSegurosController>/5
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(ContratoSeguro), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener contratoseguro");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener contratoseguro"));
            }
        }

        // POST api/<ContratosSegurosController>
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ContratoSeguro), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] ContratoSeguro x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar contratoseguro");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar contratoseguro"));
            }
        }

        // PUT api/<ContratosSegurosController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ContratoSeguro), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] ContratoSeguro x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar contratoseguro");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar contratoseguro"));
            }
        }

        // DELETE api/<ContratosSegurosController>/5
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
                logger.LogError(ex, "Error al eliminar contratoseguro");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar contratoseguro"));
            }
        }
    }
}
