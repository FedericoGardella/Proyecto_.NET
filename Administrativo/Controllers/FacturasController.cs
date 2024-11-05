using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = Administrativo.Models.StatusResponse;

namespace Administrativo.Controllers
{
    public class FacturasController : ControllerBase
    {
        private readonly IBL_Facturas bl;
        private readonly ILogger<FacturasController> logger;

        public FacturasController(IBL_Facturas _bl, ILogger<FacturasController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/<FacturasController>
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<Factura>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener facturas");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener facturas"));
            }
        }

        // GET api/<FacturasController>/5
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(Factura), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener factura");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener factura"));
            }
        }

        // POST api/<FacturasController>
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Factura), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Factura x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar factura");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar factura"));
            }
        }

        // PUT api/<FacturasController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Factura), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] Factura x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar factura");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar factura"));
            }
        }

        // DELETE api/<FacturasController>/5
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
                logger.LogError(ex, "Error al eliminar factura");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar factura"));
            }
        }
    }
}
