using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = PortalPaciente.Models.StatusResponse;

namespace PortalPaciente.Controllers
{
    public class PacientesController : Controller
    {
        private readonly IBL_Pacientes bl;
        private readonly ILogger<PacientesController> logger;

        public PacientesController(IBL_Pacientes _bl, ILogger<PacientesController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/<PersonasController>
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<Paciente>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener pacientes");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener pacientes"));
            }
        }



        // GET api/<PersonasController>/5
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(Paciente), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener paciente");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener paciente"));
            }
        }

        // POST api/<PersonasController>
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Paciente), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Paciente x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar paciente");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar paciente"));
            }
        }

        // PUT api/<PersonasController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Paciente), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] Paciente x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar paciente");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar paciente"));
            }
        }

        // DELETE api/<PersonasController>/5
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
                logger.LogError(ex, "Error al eliminar diagnostico");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar diagnostico"));
            }
        }
    }
}
