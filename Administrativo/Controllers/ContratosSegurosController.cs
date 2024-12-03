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
    public class ContratosSegurosController : ControllerBase
    {
        private readonly IBL_ContratosSeguros _bl;
        private readonly IBL_Pacientes blPacientes;
        private readonly IBL_TiposSeguros blTiposSeguros;
        private readonly IBL_Facturas blFacturas;
        private readonly ILogger<ContratosSegurosController> logger;

        public ContratosSegurosController(IBL_ContratosSeguros bl, IBL_Pacientes _blPacientes, IBL_TiposSeguros _blTiposSeguros, IBL_Facturas _blFacturas, ILogger<ContratosSegurosController> _logger)
        {
            _bl = bl;
            blPacientes = _blPacientes;
            blTiposSeguros = _blTiposSeguros;
            blFacturas = _blFacturas;
            logger = _logger;
        }

        // GET: api/ContratosSeguros
        [HttpGet]
        [Authorize(Roles = "ADMIN, PACIENTE")]
        [ProducesResponseType(typeof(List<ContratoSeguro>), 200)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener los contratos de seguro");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al obtener los contratos de seguro"));
            }
        }

        // GET: api/ContratosSeguros/5
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN, PACIENTE")]
        [ProducesResponseType(typeof(ContratoSeguro), 200)]
        public IActionResult Get(long id)
        {
            try
            {
                var contratoSeguro = _bl.Get(id);
                if (contratoSeguro == null)
                    return NotFound(new StatusDTO(false, "Contrato de seguro no encontrado"));

                return Ok(contratoSeguro);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener el contrato de seguro");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al obtener el contrato de seguro"));
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ContratosSeguros), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult PostContratoSeguro([FromBody] ContratoSeguroDTO contratoDTO)
        {
            try
            {
                if (contratoDTO == null)
                {
                    return BadRequest(new StatusDTO(false, "El contrato no puede ser nulo."));
                }

                // Obtén el token del encabezado de autorización
                var token = HttpContext.Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new StatusDTO(false, "No se proporcionó un token de autenticación."));
                }

                //Console.WriteLine($"Recibido PacientesId: {contratoSeguroDTO.PacienteId}, TipoSeguroId: {contratoSeguroDTO.TipoSeguroId}");

                // Buscar el paciente asociado
                var paciente = blPacientes.Get(contratoDTO.PacienteId, token);
                if (paciente == null)
                {
                    throw new Exception($"Paciente con ID {contratoDTO.PacienteId} no encontrado en la base de datos.");
                }
                else
                {
                    logger.LogInformation("Paciente encontrado:");
                    logger.LogInformation($"ID: {paciente.Id}");
                    logger.LogInformation($"Nombre: {paciente.Nombres}");
                }

                // Validar la existencia del TipoSeguro
                var tipoSeguro = blTiposSeguros.Get(contratoDTO.TipoSeguroId);
                if (tipoSeguro == null)
                {
                    return NotFound(new StatusDTO(false, "Tipo de seguro no encontrado."));
                }
                logger.LogInformation("TipoSeguro encontrado con ID: {TipoSeguroId}", contratoDTO.TipoSeguroId);

                // Verificar si el paciente ya tiene un contrato activo
                var contratoActivo = _bl.GetContratoActivoPorPaciente(contratoDTO.PacienteId);
                if (contratoActivo != null)
                {
                    return BadRequest(new StatusDTO(false, "El paciente ya tiene un contrato activo."));
                }

                // Obtener el costo del artículo asociado al TipoSeguro
                var costoArticulo = blTiposSeguros.GetCostoArticulo(contratoDTO.TipoSeguroId);
                logger.LogInformation("Costo del artículo asociado: {Costo}", costoArticulo);

                // Crear el contrato de seguro
                var nuevoContrato = new ContratoSeguro
                {
                    PacienteId = contratoDTO.PacienteId,
                    TipoSeguroId = contratoDTO.TipoSeguroId,
                    FechaInicio = DateTime.UtcNow,
                    Activo = true
                };

                var contratoCreado = _bl.Add(nuevoContrato);
                logger.LogInformation("Contrato de seguro creado con ID: {ContratoId}", contratoCreado.Id);

                // Crear la factura relacionada al contrato
                var nuevaFactura = new Factura
                {
                    Fecha = DateTime.UtcNow,
                    Pago = false, // Por defecto, la factura no está pagada
                    Costo = costoArticulo,
                    PacienteId = contratoDTO.PacienteId,
                    ContratoSeguroId = contratoCreado.Id // Relacionar la factura con el contrato
                };

                var facturaCreada = blFacturas.Add(nuevaFactura);
                logger.LogInformation("Factura creada con ID: {FacturaId}", facturaCreada.Id);

                return Ok(new { ContratoSeguro = contratoCreado, Factura = facturaCreada });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al crear el contrato de seguro.");
                return BadRequest(new StatusDTO(false, "Error al crear el contrato de seguro."));
            }
        }



        // PUT: api/ContratosSeguros/5
        [HttpPut("{id:long}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ContratoSeguro), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Put(long id, [FromBody] ContratoSeguroDTO contratoSeguroDTO)
        {
            try
            {
                // Validar que el objeto enviado no sea nulo y el ID coincida
                if (contratoSeguroDTO == null)
                {
                    return BadRequest(new StatusDTO(false, "Datos del contrato no son válidos."));
                }

                if (id != contratoSeguroDTO.Id)
                {
                    return BadRequest(new StatusDTO(false, "Id != dto.id"));
                }

                // Buscar entidades relacionadas
                var paciente = blPacientes.Get(contratoSeguroDTO.PacienteId, null);
                if (paciente == null)
                {
                    return BadRequest(new StatusDTO(false, "Paciente no encontrado."));
                }

                var tipoSeguro = blTiposSeguros.Get(contratoSeguroDTO.TipoSeguroId);
                if (tipoSeguro == null)
                {
                    return BadRequest(new StatusDTO(false, "Tipo de seguro no encontrado."));
                }

                // Crear el contrato actualizado
                var contratoSeguro = new ContratoSeguro
                {
                    Id = id,
                    Activo = contratoSeguroDTO.Activo,
                    PacienteId = contratoSeguroDTO.PacienteId,
                    TipoSeguroId = contratoSeguroDTO.TipoSeguroId
                };

                // Llamar a la capa BL para actualizar el contrato
                var updatedContratoSeguro = _bl.Update(contratoSeguro);
                if (updatedContratoSeguro == null)
                {
                    return NotFound(new StatusDTO(false, "Contrato de seguro no encontrado para actualizar."));
                }

                return Ok(updatedContratoSeguro);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar contrato de seguro.");
                return BadRequest(new StatusDTO(false, "Error al actualizar contrato de seguro."));
            }
        }


        // DELETE: api/ContratosSeguros/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(StatusDTO), 200)]
        public IActionResult Delete(long id)
        {
            try
            {
                var existingContratoSeguro = _bl.Get(id);
                if (existingContratoSeguro == null)
                    return NotFound(new StatusDTO(false, "Contrato de seguro no encontrado"));

                _bl.Delete(id);
                return Ok(new StatusDTO(true, "Contrato de seguro eliminado correctamente"));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al eliminar el contrato de seguro");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al eliminar el contrato de seguro"));
            }
        }



        [HttpPut("desactivar/{pacienteId:long}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ContratoSeguro), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Desactivar(long pacienteId)
        {
            try
            {
                var contratoSeguro = _bl.GetContratoActivoPaciente(pacienteId);
                if (contratoSeguro == null)
                {
                    return BadRequest(new StatusDTO(false, "El paciente no tiene un contrato activo."));
                }

                // Desactivar el contrato
                contratoSeguro.Activo = false;
                var updatedContratoSeguro = _bl.Update(contratoSeguro);

                return Ok(updatedContratoSeguro);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al desactivar contrato de seguro del paciente con ID: {Id}", pacienteId);
                return BadRequest(new StatusDTO(false, "Error al desactivar contrato de seguro."));
            }
        }

    }
}
