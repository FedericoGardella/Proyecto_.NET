using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = PortalPaciente.Models.StatusResponse;

namespace PortalPaciente.Controllers
{
    public class PacientesController : Controller
    {
        private readonly IBL_Pacientes bl;
        private readonly IBL_HistoriasClinicas hc;
        private readonly IBL_Citas bL_Citas;
        private readonly ILogger<PacientesController> logger;

        public PacientesController(IBL_Pacientes _bl,IBL_HistoriasClinicas _hc, IBL_Citas _bl_Citas ,ILogger<PacientesController> _logger)
        {
            bl = _bl;
            hc = _hc;
            bL_Citas = _bl_Citas;
            logger = _logger;
        }

        // GET: api/<PersonasController>
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<Paciente>), 200)]
        [HttpGet("api/portalpaciente")]
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



        // GET api/<PersonasController>/5
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(Paciente), 200)]
        [HttpGet("api/portalpaciente{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener paciente");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener paciente"));
            }
        }

        // POST api/<PersonasController>
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Paciente), 200)]
        [HttpPost("api/portalpaciente")]
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

        // PUT api/<PersonasController>/5
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

        // DELETE api/<PersonasController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(StatusResponse), 200)]
        [HttpDelete("api/portalpaciente{Id}")]
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
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar paciente"));
            }
        }
        // HistoriaClinica api/<PersonasController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(StatusResponse), 200)]
        [HttpGet("api/portalpaciente/historiaclinica/{Id}")]
        public IActionResult HistoriaClinica(long Id)
        {
            try
            {                
                return Ok( hc.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al buscar historia clinica");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al buscar  historia clinica"));
            }
        }

        // Citas api/<PersonasController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(StatusResponse), 200)]
        [HttpGet("api/portalpaciente/cita/{Id}")]
        public IActionResult Cita(long Id)
        {
            try
            {
                return Ok(bL_Citas.GetCitasPorPacienteID(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al buscar citas");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al buscar citas"));
            }
        }

    }
}
