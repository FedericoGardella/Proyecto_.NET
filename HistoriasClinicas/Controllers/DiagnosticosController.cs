using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;

using BL.IBLs;
using Shared.DTOs;

using StatusResponse = HistoriasClinicas.Models.StatusResponse;

namespace HistoriasClinicas.Controllers
{
    public class DiagnosticosController : ControllerBase
    {
        private readonly IBL_Diagnosticos bl;
        private readonly ILogger<DiagnosticosController> logger;

        public DiagnosticosController(IBL_Diagnosticos _bl, ILogger<DiagnosticosController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/<DiagnosticosController>
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<Diagnostico>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener diagnosticos");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener diagnosticos"));
            }
        }



        // GET api/<DiagnosticosController>/5
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(Diagnostico), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener diagnostico");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener diagnostico"));
            }
        }

        // POST api/<DiagnosticosController>
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Diagnostico), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Diagnostico x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar diagnostico");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar diagnostico"));
            }
        }

        // PUT api/<DiagnosticosController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Diagnostico), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] Diagnostico x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar diagnostico");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar diagnostico"));
            }
        }

        // DELETE api/<DiagnosticosController>/5
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
