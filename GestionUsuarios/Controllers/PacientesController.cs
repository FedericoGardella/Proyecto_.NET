using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using System.Security.Claims;
using StatusResponse = GestionUsuarios.Models.StatusResponse;

namespace GestionUsuarios.Controllers
{
    public class PacientesController : ControllerBase
    {
        private readonly IBL_Pacientes bl;
        private readonly ILogger<PacientesController> logger;

        public PacientesController(IBL_Pacientes _bl, ILogger<PacientesController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/<PacientesController>
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<Paciente>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener pacientes");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener pacientes"));
            }
        }



        // GET api/<PacientesController>/5
        [Authorize(Roles = "ADMIN, X, PACIENTE")]
        [ProducesResponseType(typeof(Paciente), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                var userRoles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!long.TryParse(userIdClaim, out var userId))
                {
                    logger.LogWarning("No se pudo convertir el userId del JWT a un valor válido de tipo long.");
                    return Unauthorized(new StatusDTO(false, "Token inválido."));
                }
                if (userRoles.Contains("PACIENTE"))
                {
                    // Obtener el ID del paciente logueado
                    var paciente = bl.Get(userId);
                    if (paciente == null)
                    {
                        logger.LogWarning($"Paciente no encontrado para el usuario {userId}");
                        return NotFound(new StatusDTO(false, "Paciente no encontrado."));
                    }
                    return Ok(paciente);
                }
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener paciente");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener paciente"));
            }
        }

        // POST api/<PacientesController>
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Paciente), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Paciente x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar paciente");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar paciente"));
            }
        }

        // PUT api/<PacientesController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Paciente), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] Paciente x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar paciente");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar paciente"));
            }
        }

        // DELETE api/<PacientesController>/5
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
                logger.LogError(ex, "Error al eliminar paciente");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar paciente"));
            }
        }
    }
}
