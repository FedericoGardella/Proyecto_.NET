using BL.IBLs;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = GestionCitas.Models.StatusResponse;

namespace GestionCitas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GruposCitasController : ControllerBase
    {
        private readonly IBL_GruposCitas bl;
        private readonly ILogger<GruposCitasController> logger;

        public GruposCitasController(IBL_GruposCitas _bl, ILogger<GruposCitasController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/grupos-citas
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

        // GET api/grupos-citas/{id}
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(GrupoCita), 200)]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(bl.Get(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener grupoCita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener grupoCita"));
            }
        }

        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(GrupoCita), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] GrupoCitaPostDTO dto)
        {
            try
            {
                if (dto.CantidadCitas <= 0 || dto.IntervaloMinutos <= 0)
                {
                    return BadRequest(new StatusDTO(false, "La cantidad de citas y el intervalo deben ser mayores a 0."));
                }

                // Delegar la creación al BL
                var grupoCita = bl.AddGrupoCitaConCitas(dto);
                return Ok(grupoCita);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar grupoCita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar grupoCita"));
            }
        }



        // PUT api/grupos-citas/{id}
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(GrupoCita), 200)]
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] GrupoCita x)
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

        // DELETE api/grupos-citas/{id}
        [Authorize(Roles = "ADMIN")]
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
                logger.LogError(ex, "Error al eliminar grupoCita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar grupoCita"));
            }
        }
    }
}
