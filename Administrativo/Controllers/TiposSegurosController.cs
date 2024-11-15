using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using System.Collections.Generic;

namespace Administrativo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposSegurosController : ControllerBase
    {
        private readonly IBL_TiposSeguros bl;
        private readonly ILogger<TiposSegurosController> logger;

        public TiposSegurosController(IBL_TiposSeguros _bl, ILogger<TiposSegurosController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/TiposSeguros
        [HttpGet]
        [Authorize(Roles = "ADMIN, USER")]
        [ProducesResponseType(typeof(List<TipoSeguro>), 200)]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener tipos de seguro");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al obtener tipos de seguro"));
            }
        }

        // GET: api/TiposSeguros/5
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN, USER")]
        [ProducesResponseType(typeof(TipoSeguro), 200)]
        public IActionResult Get(long id)
        {
            try
            {
                var tipoSeguro = bl.Get(id);
                if (tipoSeguro == null)
                    return NotFound(new StatusDTO(false, "Tipo de seguro no encontrado"));

                return Ok(tipoSeguro);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener el tipo de seguro");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al obtener el tipo de seguro"));
            }
        }

        // POST: api/TiposSeguros
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(TipoSeguro), 201)]
        public IActionResult Post([FromBody] TipoSeguro tipoSeguro)
        {
            try
            {
                var createdTipoSeguro = bl.Add(tipoSeguro);
                return CreatedAtAction(nameof(Get), new { id = createdTipoSeguro.Id }, createdTipoSeguro);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al crear el tipo de seguro");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al crear el tipo de seguro"));
            }
        }

        // PUT: api/TiposSeguros/5
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(TipoSeguro), 200)]
        public IActionResult Put(long id, [FromBody] TipoSeguro tipoSeguro)
        {
            try
            {
                var existingTipoSeguro = bl.Get(id);
                if (existingTipoSeguro == null)
                    return NotFound(new StatusDTO(false, "Tipo de seguro no encontrado"));

                tipoSeguro.Id = id; // Aseguramos que el ID sea el mismo que el que queremos actualizar
                var updatedTipoSeguro = bl.Update(tipoSeguro);
                return Ok(updatedTipoSeguro);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar el tipo de seguro");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al actualizar el tipo de seguro"));
            }
        }

        // DELETE: api/TiposSeguros/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(StatusDTO), 200)]
        public IActionResult Delete(long id)
        {
            try
            {
                var existingTipoSeguro = bl.Get(id);
                if (existingTipoSeguro == null)
                    return NotFound(new StatusDTO(false, "Tipo de seguro no encontrado"));

                bl.Delete(id);
                return Ok(new StatusDTO(true, "Tipo de seguro eliminado correctamente"));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al eliminar el tipo de seguro");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al eliminar el tipo de seguro"));
            }
        }
    }
}
