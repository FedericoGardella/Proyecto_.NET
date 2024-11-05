using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = HistoriasClinicas.Models.StatusResponse;

namespace HistoriasClinicas.Controllers
{
    public class MedicamentosController : ControllerBase
    {
        private readonly IBL_Medicamentos bl;
        private readonly ILogger<MedicamentosController> logger;

        public MedicamentosController(IBL_Medicamentos _bl, ILogger<MedicamentosController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }

        // GET: api/<MedicamentosController>
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<Medicamento>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener medicamentos");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener medicamentos"));
            }
        }



        // GET api/<MedicamentosController>/5
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(Medicamento), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener medicamento");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener medicamento"));
            }
        }

        // POST api/<MedicamentosController>
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Medicamento), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Medicamento x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar medicamento");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar medicamento"));
            }
        }

        // PUT api/<MedicamentosController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Medicamento), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] Medicamento x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar medicamento");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar medicamento"));
            }
        }

        // DELETE api/<MedicamentosController>/5
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
                logger.LogError(ex, "Error al eliminar medicamento");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar medicamento"));
            }
        }
    }
}
