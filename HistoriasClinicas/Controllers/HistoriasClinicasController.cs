using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;

using StatusResponse = HistoriasClinicas.Models.StatusResponse;

namespace HistoriasClinicas.Controllers
{
    public class HistoriasClinicasController : ControllerBase
    {
        private readonly IBL_HistoriasClinicas bl;
        private readonly ILogger<HistoriasClinicasController> logger;

        public HistoriasClinicasController(IBL_HistoriasClinicas _bl, ILogger<HistoriasClinicasController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/<HistoriasClinicasController>
        [Authorize(Roles = "ADMIN, X")]
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



        // GET api/<HistoriasClinicasController>/5
        [Authorize(Roles = "ADMIN, X")]
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

        // POST api/<HistoriasClinicasController>
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(HistoriaClinica), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] HistoriaClinica x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar historia clinica");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar historia clinica"));
            }
        }

        // PUT api/<HistoriasClinicasController>/5
        [Authorize(Roles = "ADMIN")]
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

        // DELETE api/<HistoriasClinicasController>/5
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
                logger.LogError(ex, "Error al eliminar historia clinica");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar historia clinica"));
            }
        }
    }
}
