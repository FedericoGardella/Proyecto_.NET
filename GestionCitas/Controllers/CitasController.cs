using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = GestionCitas.Models.StatusResponse;

namespace GestionCitas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly IBL_Citas bl;
        private readonly ILogger<CitasController> logger;

        public CitasController(IBL_Citas _bl, ILogger<CitasController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/citas
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

        // GET api/citas/{id}
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(Cita), 200)]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(bl.Get(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener cita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener cita"));
            }
        }

        // POST api/citas
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

        // PUT api/citas/{id}
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(Cita), 200)]
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Cita x)
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

        [Authorize(Roles = "ADMIN, PACIENTE")]
        [ProducesResponseType(typeof(StatusResponse), 200)]
        [HttpPatch("{id}/paciente/{pacienteId}")]
        public IActionResult UpdatePaciente(long id, long pacienteId)
        {
            try
            {
                if (pacienteId <= 0)
                {
                    return BadRequest(new StatusDTO(false, "PacienteId debe ser un valor positivo."));
                }

                bl.UpdatePaciente(id, pacienteId);

                return Ok(new StatusResponse() { StatusOk = true, StatusMessage = "Cita actualizada exitosamente." });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error al actualizar el paciente para la cita con ID {id}");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar la cita."));
            }
        }

        // DELETE api/citas/{id}
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(StatusResponse), 200)]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                bl.Delete(id);
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
