using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = GestionUsuarios.Models.StatusResponse;

namespace GestionUsuarios.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicosController : ControllerBase
    {
        private readonly IBL_Medicos bl;
        private readonly ILogger<MedicosController> logger;

        public MedicosController(IBL_Medicos _bl, ILogger<MedicosController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/<MedicosController>
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<Medico>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener medicos");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener medicos"));
            }
        }



        // GET api/<MedicosController>/5
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(Medico), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener medico");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener medico"));
            }
        }

        // POST api/<MedicosController>
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Medico), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Medico x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar medico");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar medico"));
            }
        }

        // PUT api/<MedicosController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Medico), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] Medico x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar medico");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar medico"));
            }
        }

        // DELETE api/<MedicosController>/5
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
                logger.LogError(ex, "Error al eliminar medico");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar medico"));
            }
        }
    }
}
