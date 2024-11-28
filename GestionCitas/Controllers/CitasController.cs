using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = GestionCitas.Models.StatusResponse;

namespace GestionCitas.Controllers
{
    public class CitasController : ControllerBase
    {
        private readonly IBL_Citas bl;
        private readonly ILogger<CitasController> logger;

        public CitasController(IBL_Citas _bl, ILogger<CitasController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/<CitasController>
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(List<Cita>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener citas");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener citas"));
            }
        }

        // GET api/<CitasController>/5
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(Cita), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener cita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener cita"));
            }
        }

        // POST api/<CitasController>
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(Cita), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Cita x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar cita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar cita"));
            }
        }

        // PUT api/<CitasController>/5
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(Cita), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] Cita x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar cita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar cita"));
            }
        }

        // DELETE api/<CitasController>/5
        [Authorize(Roles = "ADMIN, MEDICO")]
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
                logger.LogError(ex, "Error al eliminar cita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar cita"));
            }
        }
    }
}
