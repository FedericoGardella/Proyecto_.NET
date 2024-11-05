using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = GestionUsuarios.Models.StatusResponse;

namespace GestionUsuarios.Controllers
{
    public class EspecialidadesController : ControllerBase
    {
        private readonly IBL_Especialidades bl;
        private readonly ILogger<EspecialidadesController> logger;

        public EspecialidadesController(IBL_Especialidades _bl, ILogger<EspecialidadesController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/<EspecialidadesController>
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<Especialidad>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener especialidades");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener especialidades"));
            }
        }



        // GET api/<EspecialidadesController>/5
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(Especialidad), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener especialidad");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener especialidad"));
            }
        }

        // POST api/<EspecialidadesController>
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Especialidad), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Especialidad x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar especialidad");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar especialidad"));
            }
        }

        // PUT api/<EspecialidadesController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Especialidad), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] Especialidad x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar especialidad");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar especialidad"));
            }
        }

        // DELETE api/<EspecialidadesController>/5
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
                logger.LogError(ex, "Error al eliminar especialidad");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar especialidad"));
            }
        }
    }
}
