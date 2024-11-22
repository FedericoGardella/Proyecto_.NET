using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;

using StatusResponse = HistoriasClinicas.Models.StatusResponse;

namespace HistoriasClinicas.Controllers
{
    [ApiController] // Asegúrate de incluir este atributo
    [Route("api/[controller]")] // Define la ruta base para el controlador
    public class HistoriasClinicasController : ControllerBase
    {
        private readonly IBL_HistoriasClinicas bl;
        private readonly ILogger<HistoriasClinicasController> logger;

        public HistoriasClinicasController(IBL_HistoriasClinicas _bl, ILogger<HistoriasClinicasController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/HistoriasClinicas
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(List<HistoriaClinica>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener historias clinicas");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener historias clinicas"));
            }
        }

        // GET api/HistoriasClinicas/5
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(HistoriaClinica), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener historia clinica");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener historia clinica"));
            }
        }
        // POST api/HistoriasClinicas
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(HistoriaClinica), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] HistoriaClinicaDTO historiaClinicaDTO)
        {
            try
            {
                // Mapea el DTO a la entidad HistoriasClinicas
                var historiaClinicaEntity = new HistoriaClinica
                {
                    FechaCreacion = historiaClinicaDTO.FechaCreacion,
                    PacienteId = historiaClinicaDTO.PacienteId
                };

                var historiaClinica = bl.Add(historiaClinicaEntity);
                return Ok(historiaClinica);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar historia clínica");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar historia clínica"));
            }
        }

        // PUT api/HistoriasClinicas/5
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(HistoriaClinica), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] HistoriaClinica x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar historia clinica");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar historia clinica"));
            }
        }

        // DELETE api/HistoriasClinicas/5
        [Authorize(Roles = "ADMIN, MEDICO")]
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
                logger.LogError(ex, "Error al eliminar historia clinica");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar historia clinica"));
            }
        }

        // GET api/HistoriasClinicas/MockPacientes
        [HttpGet("MockPacientes")]
        [ProducesResponseType(typeof(List<PacienteDTO>), 200)]
        public IActionResult GetMockPacientes()
        {
            // Datos de ejemplo
            var pacientesMock = new List<PacienteDTO>
            {
                new PacienteDTO { Id = 1, Nombres = "Juan", Apellidos = "Pérez", Documento = "12345678", Telefono = "555-1234", HistoriaClinicaId = 10007},
                new PacienteDTO { Id = 2, Nombres = "Ana", Apellidos = "Gómez", Documento = "87654321", Telefono = "555-5678", HistoriaClinicaId = 10007 },
                new PacienteDTO { Id = 3, Nombres = "Luis", Apellidos = "Martínez", Documento = "11223344", Telefono = "555-9012", HistoriaClinicaId = 10007 },
                new PacienteDTO { Id = 4, Nombres = "María", Apellidos = "Rodríguez", Documento = "44332211", Telefono = "555-3456", HistoriaClinicaId = 10007 }
            };

            return Ok(pacientesMock);
        }

        [HttpGet("{id}/Diagnosticos")]
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(List<DiagnosticoDTO>), 200)]
        [ProducesResponseType(typeof(StatusDTO), 400)]
        public IActionResult GetDiagnosticos(long id)
        {
            try
            {
                var diagnosticos = bl.GetDiagnosticos(id);

                if (diagnosticos == null || !diagnosticos.Any())
                {
                    return BadRequest(new StatusDTO(false, "No se encontró ningún diagnóstico para esta historia clínica."));
                }

                return Ok(diagnosticos);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener diagnósticos para la historia clínica.");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Ocurrió un error inesperado al obtener diagnósticos."));
            }
        }

        [HttpGet("{id}/ResultadoEstudios")]
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(List<ResultadoEstudio>), 200)]
        [ProducesResponseType(typeof(StatusDTO), 400)]
        public IActionResult GetResultadoEstudios(long id)
        {
            try
            {
                var resultadoEstudios = bl.GetResultadoEstudios(id);

                if (resultadoEstudios == null || !resultadoEstudios.Any())
                {
                    return BadRequest(new StatusDTO(false, "No se encontró ningún resultado de estudio para esta historia clínica."));
                }

                return Ok(resultadoEstudios);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener resultados de estudios para la historia clínica.");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Ocurrió un error inesperado al obtener resultados de estudios."));
            }
        }

        [HttpGet("{id}/Recetas")]
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(List<Receta>), 200)]
        [ProducesResponseType(typeof(StatusDTO), 400)]
        public IActionResult GetRecetas(long id)
        {
            try
            {
                var receta = bl.GetRecetas(id);

                if (receta == null || !receta.Any())
                {
                    return BadRequest(new StatusDTO(false, "No se encontró ningúna receta para esta historia clínica."));
                }

                return Ok(receta);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener recetas para la historia clínica.");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Ocurrió un error inesperado al obtener recetas."));
            }
        }
    }
}
