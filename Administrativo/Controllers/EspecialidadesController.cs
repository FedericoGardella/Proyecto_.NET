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
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadesController : ControllerBase
    {
        private readonly IBL_Especialidades bl;
        private readonly ILogger<EspecialidadesController> logger;

        public EspecialidadesController(IBL_Especialidades _bl, ILogger<EspecialidadesController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/<EspecialidadesController>
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<Especialidad>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                var especialidades = bl.GetAll();
                if (especialidades == null || !especialidades.Any())
                {
                    return NotFound(new StatusDTO(false, "No se encontraron especialidades."));
                }

                return Ok(especialidades);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener especialidades");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener especialidades"));
            }
        }



        // GET api/<EspecialidadesController>/5
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(Especialidad), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                var especialidad = bl.Get(Id);
                if (especialidad == null)
                {
                    return NotFound(new StatusDTO(false, "Especialidad no encontrada."));
                }

                return Ok(especialidad);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener especialidad");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener especialidad"));
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Especialidades), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] EspecialidadDTO especialidadDTO)
        {
            try
            {
                // Validar que el DTO no sea nulo
                if (especialidadDTO == null)
                {
                    return BadRequest(new StatusDTO(false, "La especialidad no puede ser nula."));
                }

                // Validar los datos obligatorios
                if (string.IsNullOrWhiteSpace(especialidadDTO.Nombre))
                {
                    return BadRequest(new StatusDTO(false, "El nombre de la especialidad es obligatorio."));
                }

                if (especialidadDTO.TiempoCita <= TimeSpan.Zero)
                {
                    return BadRequest(new StatusDTO(false, "El tiempo de cita debe ser mayor a cero."));
                }

                // Crear la nueva especialidad
                var nuevaEspecialidad = new Especialidad
                {
                    Nombre = especialidadDTO.Nombre,
                    tiempoCita = especialidadDTO.TiempoCita
                };

                // Guardar la especialidad
                var especialidadCreada = bl.Add(nuevaEspecialidad);

                // Retornar la especialidad creada con código 201
                return CreatedAtAction(nameof(Get), new { id = especialidadCreada.Id }, especialidadCreada);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al crear la especialidad.");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al crear la especialidad."));
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Especialidades), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Put(long id, [FromBody] EspecialidadUpdateDTO especialidadDTO)
        {
            try
            {
                // Buscar la especialidad existente
                var especialidadExistente = bl.Get(id);
                if (especialidadExistente == null)
                {
                    return NotFound(new StatusDTO(false, "Especialidad no encontrada."));
                }

                logger.LogInformation("Especialidad encontrada con ID: {EspecialidadId}", id);

                // Solo actualiza los campos que se hayan enviado
                if (!string.IsNullOrWhiteSpace(especialidadDTO.Nombre))
                {
                    especialidadExistente.Nombre = especialidadDTO.Nombre;
                }

                if (especialidadDTO.TiempoCita != default(TimeSpan))
                {
                    especialidadExistente.tiempoCita = especialidadDTO.TiempoCita.Value;
                }

                // Actualizar la especialidad
                var especialidadActualizada = bl.Update(especialidadExistente);
                logger.LogInformation("Especialidad actualizada con ID: {EspecialidadId}", id);

                return Ok(especialidadActualizada);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar la especialidad con ID {EspecialidadId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al actualizar la especialidad."));
            }
        }


        // DELETE api/<EspecialidadesController>/5
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
                logger.LogError(ex, "Error al eliminar especialidad");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar especialidad"));
            }
        }
    }
}