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
    public class RecetasController : ControllerBase
    {
        private readonly IBL_Recetas bl;
        private readonly ILogger<RecetasController> logger;

        public RecetasController(IBL_Recetas _bl, ILogger<RecetasController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/<RecetasController>
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<Receta>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener recetas");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener recetas"));
            }
        }



        // GET api/<RecetasController>/5
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(Receta), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener receta");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener receta"));
            }
        }

        // POST api/<RecetasController>
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Recetas), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Receta x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar receta");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar receta"));
            }
        }

        // PUT api/<RecetasController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Receta), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] Receta x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar receta");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar receta"));
            }
        }

        // DELETE api/<RecetasController>/5
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
                logger.LogError(ex, "Error al eliminar receta");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar receta"));
            }
        }
    }
}
