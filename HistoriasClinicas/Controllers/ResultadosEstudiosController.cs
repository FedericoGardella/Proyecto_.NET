using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;

using BL.IBLs;
using Shared.DTOs;

using StatusResponse = HistoriasClinicas.Models.StatusResponse;
using DAL.Models;

namespace HistoriasClinicas.Controllers
{
    public class ResultadosEstudiosController : ControllerBase
    {
        private readonly IBL_ResultadosEstudios bl;
        private readonly ILogger<ResultadosEstudiosController> logger;

        public ResultadosEstudiosController(IBL_ResultadosEstudios _bl, ILogger<ResultadosEstudiosController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/<ResultadosEstudiosController>
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<ResultadoEstudio>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener resultados");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener resultados"));
            }
        }


        // GET api/<ResultadosEstudiosController>/5
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(ResultadoEstudio), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener resultados");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener resultados"));
            }
        }

        // POST api/<ResultadosEstudiosController>
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ResultadoEstudio), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] ResultadoEstudio x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar resultados");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar resultados"));
            }
        }

        // PUT api/<ResultadosEstudiosController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ResultadoEstudio), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] ResultadoEstudio x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar resultados");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar resultados"));
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
                logger.LogError(ex, "Error al eliminar resultados");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar resultados"));
            }
        }
    }
}
