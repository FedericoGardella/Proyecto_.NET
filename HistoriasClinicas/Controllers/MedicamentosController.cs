using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Entities;
using StatusResponse = HistoriasClinicas.Models.StatusResponse;

namespace HistoriasClinicas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentosController : ControllerBase
    {
        private readonly IBL_Medicamentos _blMedicamentos;
        private readonly ILogger<MedicamentosController> _logger;

        public MedicamentosController(IBL_Medicamentos blMedicamentos, ILogger<MedicamentosController> logger)
        {
            _blMedicamentos = blMedicamentos;
            _logger = logger;
        }

        // GET: api/Medicamentos
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(List<Medicamento>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            try
            {
                var medicamentos = _blMedicamentos.GetAll();
                return Ok(medicamentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener medicamentos.");
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = "Error al obtener medicamentos." });
            }
        }

        // GET: api/Medicamentos/5
        [HttpGet("{id:long}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Medicamento), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status400BadRequest)]
        public IActionResult Get(long id)
        {
            try
            {
                var medicamento = _blMedicamentos.Get(id);
                if (medicamento == null)
                {
                    return NotFound(new StatusResponse { StatusOk = false, StatusMessage = "Medicamento no encontrado." });
                }
                return Ok(medicamento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener medicamento.");
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = "Error al obtener medicamento." });
            }
        }

        // POST: api/Medicamentos
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Medicamento), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Medicamento medicamento)
        {
            try
            {
                if (medicamento == null)
                {
                    return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = "El medicamento no puede ser nulo." });
                }

                var createdMedicamento = _blMedicamentos.Add(medicamento);
                return Ok(createdMedicamento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar medicamento.");
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = "Error al guardar medicamento." });
            }
        }

        // PUT: api/Medicamentos/5
        [HttpPut("{id:long}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Medicamento), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status404NotFound)]
        public IActionResult Put(long id, [FromBody] Medicamento medicamento)
        {
            try
            {
                if (medicamento == null || id != medicamento.Id)
                {
                    return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = "Datos del medicamento no son válidos." });
                }

                var updatedMedicamento = _blMedicamentos.Update(medicamento);
                if (updatedMedicamento == null)
                {
                    return NotFound(new StatusResponse { StatusOk = false, StatusMessage = "Medicamento no encontrado para actualizar." });
                }

                return Ok(updatedMedicamento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar medicamento.");
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = "Error al actualizar medicamento." });
            }
        }

        // DELETE: api/Medicamentos/5
        [HttpDelete("{id:long}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status404NotFound)]
        public IActionResult Delete(long id)
        {
            try
            {
                var medicamento = _blMedicamentos.Get(id);
                if (medicamento == null)
                {
                    return NotFound(new StatusResponse { StatusOk = false, StatusMessage = "Medicamento no encontrado para eliminar." });
                }

                _blMedicamentos.Delete(id);
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = "Medicamento eliminado correctamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar medicamento.");
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = "Error al eliminar medicamento." });
            }
        }
    }
}
