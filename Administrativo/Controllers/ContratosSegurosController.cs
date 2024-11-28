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
        private readonly ILogger<ContratosSegurosController> logger;

        public ContratosSegurosController(IBL_ContratosSeguros bl, IBL_Pacientes _blPacientes, IBL_TiposSeguros _blTiposSeguros, ILogger<ContratosSegurosController> _logger)
        {
            _bl = bl;
            blPacientes = _blPacientes;
            blTiposSeguros = _blTiposSeguros;
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

        // POST: api/ContratosSeguros
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ContratosSeguros), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Post([FromBody] ContratoSeguroDTO contratoSeguroDTO)
        {
            try
            {
                if (contratoSeguroDTO == null)
                {
                    return BadRequest(new StatusDTO(false, "El contrato de seguro no puede ser nulo."));
                }

                //Console.WriteLine($"Recibido PacientesId: {contratoSeguroDTO.PacienteId}, TipoSeguroId: {contratoSeguroDTO.TipoSeguroId}");

                // Buscar el paciente asociado
                var paciente = blPacientes.Get(contratoSeguroDTO.PacienteId, null);
                if (paciente == null)
                {
                    throw new Exception($"Paciente con ID {contratoSeguroDTO.PacienteId} no encontrado en la base de datos.");
                }
                else
                {
                    logger.LogInformation("Paciente encontrado:");
                    logger.LogInformation($"ID: {paciente.Id}");
                    logger.LogInformation($"Nombre: {paciente.Nombres}");
                }

                // Buscar el tipo de seguro asociado
                var tipoSeguro = blTiposSeguros.Get(contratoSeguroDTO.TipoSeguroId);
                if (tipoSeguro == null)
                {
                    return NotFound(new StatusDTO(false, "Tipo de seguro no encontrado."));
                }
                else
                {
                    logger.LogInformation("TipoSeguro encontrado:");
                    logger.LogInformation($"ID: {tipoSeguro.Id}");
                    logger.LogInformation($"Nombre: {tipoSeguro.Nombre}");
                }

                // Crear el ContratoSeguro basado en el DTO
                var contratoSeguro = new ContratoSeguro
                {
                    FechaInicio = contratoSeguroDTO.FechaInicio,
                    Estado = contratoSeguroDTO.Estado,
                    PacienteId = contratoSeguroDTO.PacienteId,
                    TipoSeguroId = contratoSeguroDTO.TipoSeguroId
                };

                // Agregar el nuevo ContratoSeguro a la lista de ConratosSeguros del Paciente
                if (paciente.ContratosSeguros == null)
                {
                    paciente.ContratosSeguros = new List<ContratoSeguro>();
                }
                paciente.ContratosSeguros.Add(contratoSeguro);

                var createdContratoSeguro = _bl.Add(contratoSeguro);

                return Ok(createdContratoSeguro);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al crear el contrato de seguro.");
                return BadRequest(new StatusDTO(false, "Error al guardar contrato de seguro."));
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
                    Estado = contratoSeguroDTO.Estado,
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

    }
}
