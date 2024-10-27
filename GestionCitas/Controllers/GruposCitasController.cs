using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;

using BL.IBLs;
using Shared.DTOs;

using StatusResponse = GestionCitas.Models.StatusResponse;
using DAL.Models;

namespace GestionCitas.Controllers
{
    public class GruposCitasController : ControllerBase
    {
        private readonly IBL_GruposCitas bl;
        private readonly ILogger<GruposCitasController> logger;

        public GruposCitasController(IBL_GruposCitas _bl, ILogger<GruposCitasController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/<GruposCitasController>
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<GrupoCita>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener gruposCitas");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener gruposCitas"));
            }
        }

        // GET api/<GruposCitasController>/5
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(GrupoCita), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener grupoCita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener grupoCita"));
            }
        }

        // POST api/<GruposCitasController>
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(GrupoCita), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] GrupoCita x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar grupoCita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar grupoCita"));
            }
        }

        // PUT api/<GruposCitasController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(GrupoCita), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] GrupoCita x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar grupoCita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar grupoCita"));
            }
        }

        // DELETE api/<GruposCitasController>/5
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
                logger.LogError(ex, "Error al eliminar grupoCita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar grupoCita"));
            }
        }
    }
}
