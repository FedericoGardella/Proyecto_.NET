using BL.BLs;
using BL.IBLs;
using DAL.Models;
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
        private readonly IBL_Pacientes blPacientes;
        private readonly ILogger<FacturasController> logger;

        public FacturasController(IBL_Facturas _bl, IBL_Pacientes _blPacientes, ILogger<FacturasController> _logger)
        {
            bl = _bl;
            blPacientes = _blPacientes;
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

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Facturas), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult PostFactura([FromBody] FacturaDTO facturaDTO)
        {
            try
            {
                if (facturaDTO == null)
                {
                    return BadRequest(new StatusDTO(false, "La factura no puede ser nula."));
                }

                // Validar que la factura tenga al menos un ContratosSegurosId o un CitasId
                //if (facturaDTO.ContratosSegurosId == null && facturaDTO.CitasId == null)
                //{
                //    return BadRequest(new StatusDTO(false, "La factura debe estar asociada a un contrato de seguro o una cita."));
                //}

                // Validar que no tenga ambos IDs al mismo tiempo
                //if (facturaDTO.ContratosSegurosId != null && facturaDTO.CitasId != null)
                //{
                //    return BadRequest(new StatusDTO(false, "La factura no puede estar asociada a un contrato de seguro y una cita al mismo tiempo."));
                //}

                // Obtén el token del encabezado de autorización
                var token = HttpContext.Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new StatusDTO(false, "No se proporcionó un token de autenticación."));
                }

                // Validar la existencia del paciente
                var paciente = blPacientes.Get(facturaDTO.PacienteId, token);
                if (paciente == null)
                {
                    return NotFound(new StatusDTO(false, "Paciente no encontrado."));
                }

                logger.LogInformation("Paciente encontrado con ID: {PacienteId}", facturaDTO.PacienteId);

                // Validar y calcular el costo de la factura según el contrato de seguro o la cita
                //decimal costo = 0;
                //if (facturaDTO.ContratosSegurosId != null)
                //{
                //    var contratoSeguro = blContratosSeguros.Get((long)facturaDTO.ContratosSegurosId);
                //    if (contratoSeguro == null)
                //    {
                //        return NotFound(new StatusDTO(false, "Contrato de seguro no encontrado."));
                //    }

                //    logger.LogInformation("Contrato de seguro encontrado con ID: {ContratosSegurosId}", facturaDTO.ContratosSegurosId);
                //    costo = contratoSeguro.TiposSeguros.Articulos.Costo; // Asumiendo que el costo proviene de un artículo relacionado
                //}
                //else if (facturaDTO.CitasId != null)
                //{
                //    var cita = blCitas.Get((long)facturaDTO.CitasId);
                //    if (cita == null)
                //    {
                //        return NotFound(new StatusDTO(false, "Cita no encontrada."));
                //    }

                //    logger.LogInformation("Cita encontrada con ID: {CitasId}", facturaDTO.CitasId);
                //    costo = cita.PreciosEspecialidades.Articulos.Costo; // Asumiendo que el costo proviene de un artículo relacionado
                //}

                //logger.LogInformation("Costo calculado para la factura: {Costo}", costo);

                // Crear la nueva factura
                var nuevaFactura = new Factura
                {
                    PacienteId = facturaDTO.PacienteId,
                    ContratoSeguroId = facturaDTO.ContratoSeguroId,
                    CitaId = facturaDTO.CitaId,
                    Fecha = DateTime.UtcNow,
                    Costo = facturaDTO.Costo,
                    Pago = false // Por defecto, no está pagada
                };

                // Guardar la factura
                var facturaCreada = bl.Add(nuevaFactura);
                logger.LogInformation("Nueva factura creada con ID: {FacturaId}", facturaCreada.Id);

                return Ok(facturaCreada);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al crear la factura.");
                return BadRequest(new StatusDTO(false, "Error al crear la factura."));
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
