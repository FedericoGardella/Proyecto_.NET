﻿using BL.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using StatusResponse = Administrativo.Models.StatusResponse;

namespace Administrativo.Controllers
{
    public class ArticulosController : ControllerBase
    {
        private readonly IBL_Articulos bl;
        private readonly ILogger<ArticulosController> logger;

        public ArticulosController(IBL_Articulos _bl, ILogger<ArticulosController> _logger)
        {
            bl = _bl;
            logger = _logger;
        }


        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(List<Articulo>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok("ok");
                //return Ok(bl.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener articulos");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener articulos"));
            }
        }


        [Authorize(Roles = "ADMIN, X")]
        [ProducesResponseType(typeof(Articulo), 200)]
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
                return Ok(bl.Get(Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener articulo");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al obtener articulo"));
            }
        }


        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Articulo), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Articulo x)
        {
            try
            {
                return Ok(bl.Add(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar articulo");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al guardar articulo"));
            }
        }


        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Articulo), 200)]
        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [FromBody] Articulo x)
        {
            try
            {
                return Ok(bl.Update(x));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar articulo");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al actualizar articulo"));
            }
        }

        [Route("api/[controller]")]
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
                logger.LogError(ex, "Error al eliminar articulo");
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "Error al eliminar articulo"));
            }
        }
    }
}
