using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = PortalPaciente.Models.StatusResponse;

namespace PortalPaciente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortalPacienteController : ControllerBase
    {
        private readonly IBL_Pacientes blPacientes;
        private readonly IBL_HistoriasClinicas blHistoriasClinicas;
        private readonly ILogger<PortalPacienteController> _logger;
        public PortalPacienteController(IBL_Pacientes _blPacientes, IBL_HistoriasClinicas _blHistoriasClinicas, ILogger<PortalPacienteController> logger)
        {
            blPacientes = _blPacientes;
            blHistoriasClinicas = _blHistoriasClinicas;
            _logger = logger;
        }

        [Authorize(Roles = "PACIENTE")]
        [HttpGet("Pacientes/{id}/UltimaHistoriaClinica")]
        public IActionResult GetUltimaHistoriaClinica(long id)
        {
            try
            {

                // Obtén el token del encabezado de autorización
                var token = HttpContext.Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new StatusDTO(false, "No se proporcionó un token de autenticación."));
                }

                // Llama al BL y pasa el token
                var ultimaHistoria = blHistoriasClinicas.GetUltimaHistoriaClinicaPorPaciente(id, token);

                if (ultimaHistoria == null)
                {
                    return NotFound(new StatusDTO(false, "No se encontraron historias clínicas para este paciente."));
                }

                return Ok(ultimaHistoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new StatusDTO(false, "Ocurrió un error al procesar la solicitud."));
            }
        }

    }
}
