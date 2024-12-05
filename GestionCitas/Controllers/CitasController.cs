﻿using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = GestionCitas.Models.StatusResponse;

namespace GestionCitas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly IBL_Citas bl;
        private IBL_Pacientes blPacientes;
        private readonly ILogger<CitasController> logger;

        public CitasController(IBL_Citas _bl, IBL_Pacientes _blPacientes, ILogger<CitasController> _logger)
        {
            bl = _bl;
            blPacientes = _blPacientes;
            logger = _logger;
        }

        // GET: api/citas
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(List<Cita>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener citas");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener citas"));
            }
        }

        // GET api/citas/{id}
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(Cita), 200)]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(bl.Get(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener cita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener cita"));
            }
        }

        // POST api/citas
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(Cita), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Cita x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar cita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar cita"));
            }
        }

        // PUT api/citas/{id}
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(Cita), 200)]
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Cita x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar cita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar cita"));
            }
        }

        [Authorize(Roles = "ADMIN, PACIENTE")]
        [ProducesResponseType(typeof(StatusResponse), 200)]
        [HttpPatch("{id}/paciente/{pacienteId}/{especialidadId}")]
        public IActionResult UpdatePaciente(long id, long pacienteId, long especialidadId)
        {
            try
            {
                if (pacienteId <= 0)
                {
                    return BadRequest(new StatusDTO(false, "PacienteId debe ser un valor positivo."));
                }

                // Obtén el token del encabezado de autorización
                var token = HttpContext.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
                var token2 = HttpContext.Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new StatusDTO(false, "No se proporcionó un token de autenticación."));
                }

                // Decodificar el token para obtener los claims
                var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // Extraer el email desde los claims del token
                var emailFromToken = jwtToken.Claims
                    .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                    ?.Value;

                if (string.IsNullOrEmpty(emailFromToken))
                {
                    return Unauthorized(new StatusDTO(false, "No se pudo determinar el email del usuario."));
                }

                // Verificar que el email del token coincide con el email del paciente
                var emailPaciente = blPacientes.GetPacienteEmail(pacienteId);
                if (string.IsNullOrEmpty(emailPaciente))
                {
                    throw new Exception($"No se encontró el email para el paciente con ID {pacienteId}.");
                }

                // Validar si el rol es PACIENTE y los emails no coinciden
                var userRole = jwtToken.Claims
                    .FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                    ?.Value;

                if (userRole == "PACIENTE" && !emailFromToken.Equals(emailPaciente, StringComparison.OrdinalIgnoreCase))
                {
                    return Forbid(); // Responder con 403 Forbidden si no coincide
                }

                logger.LogInformation("Paciente validado con email: {Email}", emailPaciente);

                // Validar si el paciente tiene un contrato activo
                var paciente = blPacientes.Get(pacienteId, token2);
                if (paciente == null)
                {
                    throw new Exception($"No se encontró el paciente con ID {pacienteId}");
                }

                logger.LogInformation("Paciente encontrado con nombre: {Nombres}", paciente.Nombres);


                var contratoActivo = paciente.ContratosSeguros.FirstOrDefault(c => c.Activo);
                if (contratoActivo == null)
                {
                    throw new Exception($"El paciente con ID {pacienteId} no tiene un contrato activo.");
                }

                logger.LogInformation("El paciente tiene un contrato activo");

                // obtener el tipo de seguro del contrato activo
                var tipoSeguroId = contratoActivo.TipoSeguroId;

                // Log o uso del tipo de seguro según sea necesario
                Console.WriteLine($"El paciente tiene un contrato activo con el tipo de seguro: {tipoSeguroId}");


                // Verificar si la cita existe
                var cita = bl.Get(id); // Obtener la cita actual
                if (cita == null)
                {
                    return NotFound(new StatusDTO(false, "Cita no encontrada."));
                }

                var fechaHoy = DateTime.Now;

                // Buscar si el paciente ya tiene una cita futura en esta especialidad
                var citasFuturas = bl.GetCitasFuturasPorPacienteYEspecialidad(pacienteId, especialidadId, fechaHoy);

                if (citasFuturas.Any())
                {
                    return BadRequest(new StatusDTO(false, "El paciente ya tiene una cita futura en esta especialidad."));
                }

                // Realizar el update si pasa la validación
                bl.UpdatePaciente(id, pacienteId, tipoSeguroId);


                return Ok(new StatusResponse() { StatusOk = true, StatusMessage = "Cita actualizada exitosamente." });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error al actualizar el paciente para la cita con ID {id}");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "El paciente ya tiene una cita agendada para esta especialidad."));
            }
        }


        // DELETE api/citas/{id}
        [Authorize(Roles = "ADMIN, MEDICO")]
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
                logger.LogError(ex, "Error al eliminar cita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar cita"));
            }
        }
    }
}
