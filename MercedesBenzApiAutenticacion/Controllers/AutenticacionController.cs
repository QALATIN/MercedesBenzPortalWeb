using MercedesBenzApiAutenticacion.Contracts;
using MercedesBenzJwtAuthentication;
using MercedesBenzModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MercedesBenzApiAutenticacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IAutenticacionRepository _context;
        private readonly ILogger<AutenticacionController> _logger;
        private readonly JwtAuthentication _jwtAuthentication;

        public AutenticacionController(IAutenticacionRepository Context, ILogger<AutenticacionController> Logger, JwtAuthentication JwtAuthentication)
        {
            _context = Context;
            _logger = Logger;
            _jwtAuthentication = JwtAuthentication;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Mercedes Benz - Api de Autenticacion OnLine");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AutenticacionRequest Request)
        {
            string mensaje = "Autenticacion";
            try
            {
                mensaje += $"|{Request.NombreUsuario}";
                var usuario = await _context.AuthenticationAsync(Request);
                if (usuario == null)
                {
                    mensaje += $"|Nombre de Usuario Incorrecto";
                    _logger.LogWarning(mensaje);
                    return StatusCode(400, new { mensaje });
                }
                else
                {
                    if (string.IsNullOrEmpty(usuario.NombreUsuario))
                    {
                        mensaje += $"|Nombre de Usuario o Password Incorrecto";
                        _logger.LogWarning(mensaje);
                        return StatusCode(400, new { mensaje });
                    }
                    else
                    {
                        var token = _jwtAuthentication.GenerarToken(usuario);
                        if (token == null)
                        {
                            mensaje += "|Error al generar el token";
                            _logger.LogError(mensaje);
                            return StatusCode(400, new { mensaje });
                        }
                        UsuarioAutenticado usuarioToken = new()
                        {
                            NombreUsuario = token.NombreUsuario,
                            Token = token.Token,
                            TokenVigencia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59)
                        };
                        bool resultado = await _context.CreateTokenUsuarioAsync(usuarioToken);
                        if (!resultado)
                        {
                            mensaje += "|Error al guardar el token";
                            _logger.LogError(mensaje);
                            return StatusCode(400, new { mensaje });
                        }
                        mensaje += "|Usuario Autenticado";
                        _logger.LogWarning(mensaje);
                        return Ok(token);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

    }
}
