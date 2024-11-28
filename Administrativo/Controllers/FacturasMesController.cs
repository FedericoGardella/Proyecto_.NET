using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = Administrativo.Models.StatusResponse;


namespace Administrativo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasMesController: ControllerBase
    {
        private readonly IBL_FacturasMes bl;
        private readonly ILogger<FacturasMesController> logger;

        public FacturasMesController(IBL_FacturasMes _bl, ILogger<FacturasMesController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/FacturasMes
        [HttpGet]
        [Authorize(Roles = "ADMIN, PACIENTE")]
        [ProducesResponseType(typeof(List<FacturaMes>), 200)]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener las facturas");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al obtener las facturas"));
            }
        }

        // GET: api/FacturasMes/5
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN, PACIENTE")]
        [ProducesResponseType(typeof(FacturaMes), 200)]
        public IActionResult Get(long id)
        {
            try
            {
                var facturaMes = bl.Get(id);
                if (facturaMes == null)
                    return NotFound(new StatusDTO(false, "Factura no encontrada"));

                return Ok(facturaMes);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener la factura");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al obtener la factura"));
            }
        }

        // POST: api/FacturasMes
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(FacturaMes), 201)]
        public IActionResult Post([FromBody] FacturaMes facturaMes)
        {
            try
            {
                var createdFacturaMes = bl.Add(facturaMes);
                return CreatedAtAction(nameof(Get), new { id = createdFacturaMes.Id }, createdFacturaMes);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al crear la factura");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al crear la factura"));
            }
        }

        // PUT: api/FacturasMes/5
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(FacturaMes), 200)]
        public IActionResult Put(long id, [FromBody] FacturaMes facturaMes)
        {
            try
            {
                var existingFacturaMes = bl.Get(id);
                if (existingFacturaMes == null)
                    return NotFound(new StatusDTO(false, "Factura no encontrada"));

                facturaMes.Id = id; // Aseguramos que el ID sea el mismo que el que queremos actualizar
                var updatedFacturaMes = bl.Update(facturaMes);
                return Ok(updatedFacturaMes);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar la factura");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al actualizar la factura"));
            }
        }

        // DELETE: api/FacturasMes/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(StatusDTO), 200)]
        public IActionResult Delete(long id)
        {
            try
            {
                var existingFacturaMes = bl.Get(id);
                if (existingFacturaMes == null)
                    return NotFound(new StatusDTO(false, "Factura no encontrada"));

                bl.Delete(id);
                return Ok(new StatusDTO(true, "Factura eliminada correctamente"));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al eliminar la factura");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al eliminar la factura"));
            }
        }
    }
}
