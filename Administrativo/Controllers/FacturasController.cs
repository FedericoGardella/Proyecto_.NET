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
    public class FacturasController : ControllerBase
    {
        private readonly IBL_Facturas bl;
        private readonly ILogger<FacturasController> logger;

        public FacturasController(IBL_Facturas _bl, ILogger<FacturasController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/Facturas
        [HttpGet]
        [Authorize(Roles = "ADMIN, PACIENTE")]
        [ProducesResponseType(typeof(List<Factura>), 200)]
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

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN, PACIENTE")]
        [ProducesResponseType(typeof(Factura), 200)]
        public IActionResult Get(long id)
        {
            try
            {
                var factura = bl.Get(id);
                if (factura == null)
                    return NotFound(new StatusDTO(false, "Factura no encontrada"));

                return Ok(factura);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener la factura");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al obtener la factura"));
            }
        }

        // POST: api/Facturas
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Factura), 201)]
        public IActionResult Post([FromBody] Factura factura)
        {
            try
            {
                var createdFactura = bl.Add(factura);
                return CreatedAtAction(nameof(Get), new { id = createdFactura.Id }, createdFactura);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al crear la factura");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al crear la factura"));
            }
        }

        // PUT: api/Facturas/5
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Factura), 200)]
        public IActionResult Put(long id, [FromBody] Factura factura)
        {
            try
            {
                var existingFactura = bl.Get(id);
                if (existingFactura == null)
                    return NotFound(new StatusDTO(false, "Factura no encontrada"));

                factura.Id = id; // Aseguramos que el ID sea el mismo que el que queremos actualizar
                var updatedFactura = bl.Update(factura);
                return Ok(updatedFactura);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar la factura");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al actualizar la factura"));
            }
        }

        // DELETE: api/Facturas/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(StatusDTO), 200)]
        public IActionResult Delete(long id)
        {
            try
            {
                var existingFactura = bl.Get(id);
                if (existingFactura == null)
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
