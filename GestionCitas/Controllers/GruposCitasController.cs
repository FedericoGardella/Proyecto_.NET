using BL.BLs;
using BL.IBLs;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = GestionCitas.Models.StatusResponse;

namespace GestionCitas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GruposCitasController : ControllerBase
    {
        private readonly IBL_GruposCitas bl;
        private readonly IBL_Especialidades blEspecialidades;
        private readonly ILogger<GruposCitasController> logger;

        public GruposCitasController(IBL_GruposCitas _bl, IBL_Especialidades _blEspecialidades, ILogger<GruposCitasController> _logger)
        {
            bl = _bl;
            blEspecialidades = _blEspecialidades;
            logger = _logger;
        }

        // GET: api/grupos-citas
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<GrupoCita>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener gruposCitas");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener gruposCitas"));
            }
        }

        // GET api/grupos-citas/{id}
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(GrupoCita), 200)]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(bl.Get(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener grupoCita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener grupoCita"));
            }
        }

        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(GrupoCita), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] GrupoCitaPostDTO dto)
        {
            try
            {
                if (dto.CantidadCitas <= 0 || dto.IntervaloMinutos <= 0)
                {
                    return BadRequest(new StatusDTO(false, "La cantidad de citas y el intervalo deben ser mayores a 0."));
                }

                // Delegar la creación al BL
                var grupoCita = bl.AddGrupoCitaConCitas(dto);
                return Ok(grupoCita);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar grupoCita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar grupoCita"));
            }
        }



        // PUT api/grupos-citas/{id}
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(GrupoCita), 200)]
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] GrupoCita x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar grupoCita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar grupoCita"));
            }
        }

        // DELETE api/grupos-citas/{id}
        [Authorize(Roles = "ADMIN")]
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
                logger.LogError(ex, "Error al eliminar grupoCita");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar grupoCita"));
            }
        }

        // GET api/grupos-citas/especialidad/{especialidadId}/mes/{mes}
        [Authorize(Roles = "ADMIN, PACIENTE")]
        [ProducesResponseType(typeof(List<GrupoCitaDTO>), 200)]
        [HttpGet("especialidad/{especialidadId}/mes/{mes}")]
        public IActionResult GetByEspecialidadAndMes(long especialidadId, int mes)
        {
            try
            {
                var gruposCitas = bl.GetByEspecialidadAndMes(especialidadId, mes);
                return Ok(gruposCitas);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener grupos de citas por especialidad y mes");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener grupos de citas"));
            }
        }

        [HttpGet("detalle/{id}")]
        [Authorize(Roles = "ADMIN, PACIENTE")]
        [ProducesResponseType(typeof(GrupoCitaDetalleDTO), 200)]
        public IActionResult GetDetalle(long id)
        {
            try
            {
                var grupoCita = bl.GetDetalle(id);
                if (grupoCita == null)
                {
                    return NotFound(new StatusDTO(false, "Grupo de citas no encontrado"));
                }

                // Mapear al DTO
                var grupoCitaDetalle = new GrupoCitaDetalleDTO
                {
                    Id = grupoCita.Id,
                    Lugar = grupoCita.Lugar,
                    Fecha = grupoCita.Fecha,
                    MedicoNombre = $"{grupoCita.Medico.Apellidos}, {grupoCita.Medico.Nombres}",
                    EspecialidadNombre = grupoCita.Especialidad.Nombre,
                    Citas = grupoCita.Citas.Select(cita => new CitaDTO
                    {
                        Id = cita.Id,
                        Hora = cita.Hora,
                        PacienteId = cita.PacienteId
                    }).ToList()
                };

                return Ok(grupoCitaDetalle);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener el detalle del grupo de citas");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener el detalle del grupo de citas"));
            }
        }

        //[HttpGet("{id}/especialidad")]
        //[ProducesResponseType(typeof(Especialidad), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        //public IActionResult GetEspecialidad(long id)
        //{
        //    try
        //    {
        //        // Obtener el GrupoCita
        //        var grupoCita = bl.Get(id);
        //        if (grupoCita == null)
        //        {
        //            return NotFound(new StatusDTO(false, $"No se encontró el GrupoCita con ID {id}."));
        //        }

        //        // Obtén el token del encabezado de autorización
        //        var token = HttpContext.Request.Headers["Authorization"].ToString();

        //        if (string.IsNullOrEmpty(token))
        //        {
        //            return Unauthorized(new StatusDTO(false, "No se proporcionó un token de autenticación."));
        //        }

        //        // Obtener la Especialidad relacionada
        //        var especialidad = blEspecialidades.GetEspecialidad(grupoCita.EspecialidadId, token);
        //        if (especialidad == null)
        //        {
        //            return NotFound(new StatusDTO(false, $"No se encontró la Especialidad con ID {grupoCita.EspecialidadId}."));
        //        }

        //        return Ok(especialidad);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex, "Error al obtener la especialidad para el GrupoCita con ID {Id}", id);
        //        return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al procesar la solicitud."));
        //    }
        //}


        // GET: api/grupos-citas/medico/{medicoId}/hoy
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(GrupoCita), 200)]
        [HttpGet("medico/{medicoId}/hoy")]
        public IActionResult GetByMedicoAndToday(long medicoId)
        {
            try
            {
                var fechaHoy = DateTime.Today;

                // Obtener el grupo de citas del BL
                var grupoCita = bl.GetGrupoCitasMedico(medicoId, fechaHoy, null);

                if (grupoCita == null)
                {
                    return NotFound(new StatusDTO(false, "No se encontró un grupo de citas para el médico y la fecha especificada."));
                }

                return Ok(grupoCita);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error al obtener grupo de citas para el médico {medicoId} y la fecha de hoy");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener grupo de citas."));
            }
        }

    }
}
