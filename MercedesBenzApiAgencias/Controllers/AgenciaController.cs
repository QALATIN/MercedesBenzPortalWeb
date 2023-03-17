using MercedesBenzApiAgencias.Contracts;
using MercedesBenzModel;
using MercedesBenzSecurity;
using MercedesBenzJwtAuthentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MercedesBenzLibrary;

namespace MercedesBenzApiAgencias.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(AuthenticationControllerFilter))]
    [Route("api/agencias")]
    [ApiController]
    public class AgenciaController : ControllerBase
    {
        private readonly IAgenciaRepository _context;
        private readonly ILogger<AgenciaController> _logger;

        public AgenciaController(IAgenciaRepository Context, ILogger<AgenciaController> Logger)
        {
            _context = Context;
            _logger = Logger;
        }

        [HttpGet("{id}", Name = "AgenciaById")]
        public async Task<IActionResult> GetById(int Id)
        {
            string mensaje = $"Agencia Consulta|Id:{Id}";
            try
            {
                _logger.LogWarning(mensaje);
                var agencia = await _context.GetByIdAsync(Id);
                if (agencia == null)
                {
                    mensaje += "|Id incorrecto";
                    _logger.LogWarning(mensaje);
                    return NotFound(new { mensaje });
                }
                return Ok(agencia);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Agencia Agencia)
        {
            string mensaje = $"Agencia Agregar|{Agencia.ClaveAgencia}-{Agencia.NombreAgencia}";
            try
            {
                _logger.LogWarning(mensaje);
                var newAgencia = await _context.AddAsync(Agencia);
                return CreatedAtRoute("AgenciaById", new { id = newAgencia.AgenciaId }, newAgencia);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Agencia Agencia)
        {
            string mensaje  = $"Agencia Editar|{Agencia.AgenciaId}-{Agencia.ClaveAgencia}-{Agencia.NombreAgencia}";
            try
            {
                _logger.LogWarning(mensaje);
                string nombreUsuario = GetClaimNombreUsuario();
                if (string.IsNullOrEmpty(nombreUsuario))
                {
                    mensaje += "|Usuario desconocido";
                    _logger.LogWarning(mensaje);
                    return StatusCode(400, new { mensaje });
                }
                var updateAgencia = await _context.UpdateAsync(Agencia, nombreUsuario);
                if (updateAgencia == null)
                {
                    mensaje += "|Id incorrecto";
                    _logger.LogWarning(mensaje);
                    return NotFound(new { mensaje });
                }
                return Ok(updateAgencia);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            string mensaje = $"Agencia Borrar|Id:{Id}";
            try
            {
                _logger.LogWarning(mensaje);
                var response = await _context.DeleteAsync(Id);
                if (!response)
                {
                    mensaje += "|Id incorrecto";
                    _logger.LogWarning(mensaje);
                    return NotFound(new { mensaje });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        [HttpPost("cambiarEstatus/")]
        public async Task<IActionResult> UpdateEstatus(AgenciaEstatusRequest Request)
        {
            string mensaje = $"Agencia cambio de estatus";
            try
            {
                if (Request.Activar)
                    mensaje += "|Activar";
                else
                    mensaje += "|Desactivar";
                mensaje += $"|AgenciaId:{Request.AgenciaId}";

                _logger.LogWarning(mensaje);
                var response = await _context.UpdateEstatusAsync(Request);
                if (!response)
                {
                    mensaje += "|Datos incorrectos";
                    _logger.LogWarning(mensaje);
                    return NotFound(new { mensaje });
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string pages, [FromQuery] PaginacionRequest paginacion)
        {
            // Pages=eyJQYWdpbmEiOjEsIlJlZ2lzdHJvc1BhZ2luYSI6MTB9
            // Paginacion.Pagina=1&Paginacion.RegistrosPagina=10

            string mensaje = "Agencia Consulta";

            PaginacionRequest paginacionRequest = RequestParameters.ConvertPagination(pages);
            if (paginacionRequest is null)
                paginacionRequest = paginacion;

            (int statusCode, string mensajeValidado, PaginacionRequest paginacionValidada) = RequestParameters.ValidarPaginacion(paginacionRequest);

            mensaje += mensajeValidado;

            if (statusCode == 400)
            {
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }

            paginacionRequest = paginacionValidada;
            _logger.LogWarning(mensaje);
            try
            {
                (int totalRegistros, IEnumerable<object> data) = await _context.GetAllAsync(paginacionRequest);

                if (!data.Any())
                {
                    mensaje += "|No se encontraron registros";
                    _logger.LogWarning(mensaje);
                    return StatusCode(204, new { mensaje });
                }
                else
                {
                    var dataRespuesta = DataResponse.PaginateData(data, paginacionRequest, totalRegistros, false);
                    return Ok(dataRespuesta);
                }
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }

        }

        private string GetClaimNombreUsuario()
        {
            string nombreUsuario = "";
            var claims = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
            var usuario = claims.Claims.FirstOrDefault(x => x.Type.Contains("name"));
            if (usuario != null)
                nombreUsuario = usuario.Value;

            return nombreUsuario;
        }

    }
}
