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
    [Route("api/[controller]")]
    [ApiController]
    public class PreciosEspecialidadesController : ControllerBase
    {
        private readonly IBL_PreciosEspecialidades bl;
        private readonly IBL_Articulos blArticulos;
        private readonly IBL_Especialidades blEspecialidades;
        private readonly IBL_TiposSeguros blTiposSeguros;
        private readonly ILogger<PreciosEspecialidadesController> logger;

        public PreciosEspecialidadesController(IBL_PreciosEspecialidades _bl, IBL_Articulos _blArticulos, IBL_Especialidades _blEspecialidades, IBL_TiposSeguros _blTiposSeguros, ILogger<PreciosEspecialidadesController> _logger)
        {
            bl = _bl;
            blArticulos = _blArticulos;
            blEspecialidades = _blEspecialidades;
            blTiposSeguros = _blTiposSeguros;
            logger = _logger;
        }

        // GET: api/<PreciosEspecialidadesController>
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<PrecioEspecialidad>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener preciosEspecialidades");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener preciosEspecialidades"));
            }
        }


        // GET api/<PreciosEspecialidadesController>/5
        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(PrecioEspecialidad), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener precioEspecialidad");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener precioEspecialidad"));
            }
        }



        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(PreciosEspecialidades), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult Post([FromBody] PrecioEspecialidadDTO preciosEspecialidadesDTO)
        {
            try
            {
                // Validar que el DTO no sea nulo
                if (preciosEspecialidadesDTO == null)
                {
                    logger.LogWarning("El DTO de PrecioEspecialidad es nulo.");
                    return BadRequest(new StatusDTO(false, "El precio de especialidad no puede ser nulo."));
                }
                logger.LogInformation("DTO recibido: {@PrecioEspecialidadDTO}", preciosEspecialidadesDTO);


                // Validar la existencia de la Especialidad
                var especialidad = blEspecialidades.Get(preciosEspecialidadesDTO.EspecialidadId);
                if (especialidad == null)
                {
                    logger.LogWarning("No se encontró la especialidad con ID: {EspecialidadId}", preciosEspecialidadesDTO.EspecialidadId);
                    return NotFound(new StatusDTO(false, "Especialidad no encontrada."));
                }
                logger.LogInformation("Especialidad encontrada con ID: {EspecialidadId}", preciosEspecialidadesDTO.EspecialidadId);


                // Validar la existencia del TipoSeguro
                var tipoSeguro = blTiposSeguros.Get(preciosEspecialidadesDTO.TipoSeguroId);
                if (tipoSeguro == null)
                {
                    logger.LogWarning("No se encontró el TipoSeguro con ID: {TipoSeguroId}", preciosEspecialidadesDTO.TipoSeguroId);
                    return NotFound(new StatusDTO(false, "Tipo de seguro no encontrado."));
                }
                logger.LogInformation("TipoSeguro encontrado con ID: {TipoSeguroId}", preciosEspecialidadesDTO.TipoSeguroId);


                // Validar que no exista un registro con la misma combinación de EspecialidadId y TipoSeguroId
                var existePrecioEspecialidad = bl.Repetido(preciosEspecialidadesDTO.EspecialidadId, preciosEspecialidadesDTO.TipoSeguroId);
                if (existePrecioEspecialidad)
                {
                    logger.LogWarning("Ya existe un PrecioEspecialidad con EspecialidadId: {EspecialidadId} y TipoSeguroId: {TipoSeguroId}",
                        preciosEspecialidadesDTO.EspecialidadId, preciosEspecialidadesDTO.TipoSeguroId);
                    return BadRequest(new StatusDTO(false, "Ya existe un precio de especialidad con esa combinación de especialidad y tipo de seguro."));
                }

                logger.LogInformation("No existe un PrecioEspecialidad con la combinación de EspecialidadId y TipoSeguroId. Procediendo a crear...");


                // Crear un nuevo artículo asociado a PreciosEspecialidades
                var nuevoArticulo = new Articulo
                {
                    Fecha = DateTime.UtcNow,
                    Costo = preciosEspecialidadesDTO.Costo,
                };
                logger.LogInformation("Creando nuevo artículo con Fecha: {Fecha} y Costo: {Costo}", nuevoArticulo.Fecha, nuevoArticulo.Costo);

                var articuloCreado = blArticulos.Add(nuevoArticulo);
                logger.LogInformation("Nuevo artículo creado con ID: {ArticuloId}", articuloCreado.Id);


                // Crear el registro de PreciosEspecialidades
                var precioEspecialidad = new PrecioEspecialidad
                {
                    ArticuloId = articuloCreado.Id,
                    TipoSeguroId = preciosEspecialidadesDTO.TipoSeguroId,
                    EspecialidadId = preciosEspecialidadesDTO.EspecialidadId
                };
                logger.LogInformation("Creando registro de PreciosEspecialidades: {@PrecioEspecialidad}", precioEspecialidad);

                var createdPrecioEspecialidad = bl.Add(precioEspecialidad);
                logger.LogInformation("PrecioEspecialidad creado con ID: {PrecioEspecialidadId}", createdPrecioEspecialidad.Id);

                // Actualizar el artículo con el PreciosEspecialidadesId
                articuloCreado.PrecioEspecialidadId = createdPrecioEspecialidad.Id;
                logger.LogInformation("Actualizando artículo con PreciosEspecialidadesId: {PrecioEspecialidadesId}", createdPrecioEspecialidad.Id);

                blArticulos.Update(articuloCreado);
                logger.LogInformation("Artículo actualizado exitosamente con ID: {ArticuloId}", articuloCreado.Id);

                return Ok(createdPrecioEspecialidad);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar el precio de especialidad.");
                return BadRequest(new StatusDTO(false, "Error al guardar el precio de especialidad."));
            }
        }



        // PUT api/<PreciosEspecialidadesController>/5
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(PrecioEspecialidad), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] PrecioEspecialidad x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar precioEspecialidad");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar precioEspecialidad"));
            }
        }



        // DELETE api/<PreciosEspecialidadesController>/5
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
                logger.LogError(ex, "Error al eliminar precioEspecialidad");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar precioEspecialidad"));
            }
        }



        [HttpPut("updateCosto/{precioEspecialidadId}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Articulo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult UpdateCosto(long precioEspecialidadId, [FromBody] decimal nuevoCosto)
        {
            try
            {
                logger.LogInformation("Solicitud recibida para actualizar el costo de PrecioEspecialidad con ID: {PrecioEspecialidadId}", precioEspecialidadId);
                logger.LogInformation("Nuevo costo recibido: {NuevoCosto}", nuevoCosto);

                var nuevoArticulo = bl.UpdateCosto(precioEspecialidadId, nuevoCosto);

                logger.LogInformation("Costo actualizado exitosamente. Nuevo artículo creado con ID: {ArticuloId}, Fecha: {Fecha}, Costo: {Costo}",
                    nuevoArticulo.Id, nuevoArticulo.Fecha, nuevoArticulo.Costo);

                return Ok(nuevoArticulo);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar el costo para PrecioEspecialidad con ID: {PrecioEspecialidadId}", precioEspecialidadId);
                return BadRequest(new StatusDTO(false, ex.Message));
            }
        }


        [HttpGet("GetCosto")]
        [Authorize(Roles = "ADMIN, MEDICO")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status404NotFound)]
        public IActionResult GetCosto([FromQuery] long especialidadId, [FromQuery] long tipoSeguroId)
        {
            try
            {
                // Llama al BL para obtener el costo
                var costo = bl.GetCosto(especialidadId, tipoSeguroId);

                return Ok(costo);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener el costo para EspecialidadId: {EspecialidadId}, TipoSeguroId: {TipoSeguroId}",
                    especialidadId, tipoSeguroId);

                return BadRequest(new StatusDTO(false, ex.Message));
            }
        }


    }
}
