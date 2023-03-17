using MercedesBenzApiUsuarios.Contracts;
using MercedesBenzJwtAuthentication;
using MercedesBenzLibrary;
using MercedesBenzModel;
using MercedesBenzSecurity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercedesBenzApiUsuarios.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(AuthenticationControllerFilter))]
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _context;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioRepository context, ILogger<UsuarioController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "UsuarioById")]
        public async Task<IActionResult> GetById(int id)
        {
            string mensaje = $"Usuario Consulta|Id:{id}";
            try
            {
                _logger.LogWarning(mensaje);
                var usuario = await _context.GetByIdAsync(id);
                if (usuario == null)
                {
                    mensaje += "|Id incorrecto";
                    _logger.LogWarning(mensaje);
                    return NotFound(new { mensaje });
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Usuario usuario)
        {
            string mensaje = $"Usuario Agregar|{usuario.NombreUsuario}-{usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}";
            try
            {
                _logger.LogWarning(mensaje);
                var newUsuario = await _context.AddAsync(usuario);
                return CreatedAtRoute("UsuarioById", new { id = newUsuario.UsuarioId }, newUsuario);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Usuario usuario)
        {
            string mensaje = $"Usuario Editar|{usuario.UsuarioId}-{usuario.NombreUsuario}-{usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}";
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
                var updateUsuario = await _context.UpdateAsync(usuario, nombreUsuario);
                if (updateUsuario == null)
                {
                    mensaje += "|Id incorrecto";
                    _logger.LogWarning(mensaje);
                    return NotFound(new { mensaje });
                }
                return Ok(updateUsuario);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string mensaje = $"Usuario Borrar|Id:{id}";
            try
            {
                _logger.LogWarning(mensaje);
                var response = await _context.DeleteAsync(id);
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
        public async Task<IActionResult> CambiarEstatus(UsuarioEstatusRequest request)
        {
            string mensaje = $"Usuario cambio de estatus";
            try
            {
                if (request.Activar)
                    mensaje += "|Activar";
                else
                    mensaje += "|Desactivar";
                mensaje += $"|UsuarioId:{request.UsuarioId}";

                _logger.LogWarning(mensaje);
                var response = await _context.UpdateEstatusAsync(request);
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

        [HttpPost("loginO/")]
        public async Task<IActionResult> Login(AutenticacionRequest request)
        {
            try
            {
                //user.Password = EncryptionAes.DecryptString(user.Password);
                request.Password = CodificacionBase64.DecodificarTexto(request.Password);
                if (request.Password == null)
                {
                    _logger.LogWarning($"Login User: {request.NombreUsuario} Nombre de Usuario o Password Incorrecto");
                    return NotFound();
                }
                var usuario = await _context.LoginAsync(request);
                if (usuario == null)
                {
                    _logger.LogWarning($"Login User: {request.NombreUsuario} Nombre de Usuario No Encontrado");
                    return NotFound();
                }
                else
                {
                    if (string.IsNullOrEmpty(usuario.NombreCompleto))
                    {
                        _logger.LogWarning($"Login User: {usuario.NombreUsuario} Nombre de Usuario o Password Incorrecto");
                        return NotFound();
                    }
                    else
                    {
                        return Ok(usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return BadRequest();
            }
        }

        [HttpPost("logOut/")]
        public async Task<IActionResult> LogOut()
        {
            string mensaje = "LogOut";

            string nombreUsuario = GetClaimNombreUsuario();
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                mensaje += "|Usuario desconocido";
                _logger.LogWarning(mensaje);
                return Unauthorized("");
            }

            try
            {
                mensaje += $"|{nombreUsuario}";
                bool resultado = await _context.LogOutAsync(nombreUsuario);
                if (!resultado)
                {
                    mensaje += "|Nombre de Usuario No Encontrado";
                    _logger.LogWarning(mensaje);
                    return BadRequest(mensaje);
                }
                else
                {
                    mensaje += "|Sessión Finalizada";
                    _logger.LogWarning(mensaje);
                    return Ok("Sessión Cerrada");
                }
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return BadRequest(mensaje);
            }
        }

        [HttpGet("credencial/")]
        public async Task<IActionResult> Credencial()
        {
            string mensaje = "Credencial";

            string nombreUsuario = GetClaimNombreUsuario();
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                mensaje += "|Usuario desconocido";
                _logger.LogWarning(mensaje);
                return Unauthorized("");
            }

            try
            {
                mensaje += $"|{nombreUsuario}";
                var credencial = await _context.CredencialAsync(nombreUsuario);
                if (credencial == null)
                {
                    mensaje += "|Nombre de Usuario No Encontrado";
                    _logger.LogWarning(mensaje);
                    return Unauthorized("");
                }
                else
                {
                    mensaje += "|Consulta";
                    _logger.LogWarning(mensaje);
                    return Ok(credencial);
                }
            }
            catch (Exception ex)
            {
                mensaje += "|"+ ex.Message;
                _logger.LogError(mensaje);
                return BadRequest(mensaje);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string pages, [FromQuery] PaginacionRequest paginacion)
        {
            string mensaje = "Usuario Consulta";
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
