using MercedesBenzApiRecuperacion.Contracts;
using MercedesBenzApiRecuperacion.Helpers;
using MercedesBenzLibrary;
using MercedesBenzModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MercedesBenzApiRecuperacion.Controllers
{
    [Route("api/recuperacion")]
    [ApiController]
    public class RecuperacionController : ControllerBase
    {
        private readonly IRecuperacionRepository _context;
        private readonly ILogger<RecuperacionController> _logger;
        private readonly MailService _mailService;

        public RecuperacionController(IRecuperacionRepository context, ILogger<RecuperacionController> logger, MailService mailService)
        {
            _context = context;
            _logger = logger;
            _mailService = mailService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Mercedes Benz - Api de Recuperación OnLine");
        }

        [HttpPost("sendMail/")]
        public async Task<IActionResult> SendMail(EmailRequest Request)
        {
            string mensaje = $"Usuario Email|{Request.CorreoElectronico}";
            try
            {
                _logger.LogWarning(mensaje);
                (EmailDatos datos, Correo correo, string urlPortal) = await _context.GetDatosCorreoAsync(Request.CorreoElectronico);

                string token = _mailService.GenerarTokenRecuperacion(Guid.NewGuid().ToString());

                string urlRecuperacion = $"{urlPortal}Login/Recovery/{token}";
                string asunto = "Latin Id - Id Biometric Portal del Análista - Recuperación de contraseña";
                string htmlBody = _mailService.HtmlBodyRecuperacion(urlRecuperacion);

                if (correo.Libreria.ToUpper() == "SENDGRID")
                    await _mailService.SendGridMailAsync(correo, Request.CorreoElectronico, datos.UsuarioNombre, asunto, htmlBody);
                else
                    await _mailService.SendNetMailAsync(correo, Request.CorreoElectronico, datos.UsuarioNombre, asunto, htmlBody);

                mensaje += "|Correo enviado";
                await _context.CreateTokenRecuperacionAsync(datos.UsuarioId, token);
                return Ok(new { mensaje });
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        [HttpPost("resetPassword/")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest Request)
        {
            string mensaje = $"Usuario Reset Password|{Request.CorreoElectronico}";
            try
            {
                _logger.LogWarning(mensaje);
                //Request.Password = CodificacionBase64.DecodificarTexto(Request.Password);
                //if (Request.Password == null)
                //{
                //    _mensaje += $"|Password en blanco o incorrecto";
                //    _logger.LogWarning(_mensaje);
                //    return StatusCode(400, new { mensaje = _mensaje });
                //}

                bool resultado = await _context.UpdatePasswordAsync(Request);
                if (!resultado)
                {
                    mensaje += "|Correo Electrónico incorrecto";
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

        [HttpPost("validarTokenRecuperacion/")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidarTokenRecuperacion(TokenRequest Request)
        {
            string mensaje = $"Usuario Token Recuperacion|{Request.Token}";
            try
            {
                _logger.LogWarning(mensaje);
                (string resultado, var data) = await _context.GetValidacionTokenRecuperacionAsync(Request.Token);
                if (!string.IsNullOrEmpty(resultado))
                {
                    mensaje += "|Token inválido ";
                    return StatusCode(400, new { mensaje = resultado });
                }
                else
                {
                    mensaje += "|Token recuperado ";
                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        [HttpGet("getDownloadApk/")]
        public FileResult GetDownloadApk()
        {
            string mensaje = $"Download APK";
            try
            {
                _logger.LogWarning(mensaje);
                string file = ApplicationSettings.GetApkName();
                string path = ApplicationSettings.GetApkPath();
                string pathFile = Path.Combine(path, file);
                string mimeType = GetMIMEType(pathFile);
                return PhysicalFile(pathFile, mimeType, file);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return null;
            }
        }

        [HttpGet("apkVersion/")]
        public async Task<IActionResult> ApkVersion()
        {
            string mensaje = "ApkVersion";
            try
            {
                _logger.LogWarning(mensaje);
                var apk = await _context.GetApkVersionAsync();
                if (apk.Version == null)
                {
                    mensaje += "|Error desconocido";
                    _logger.LogWarning(mensaje);
                    return NotFound(new { mensaje });
                }
                return Ok(apk);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        private static string GetMIMEType(string fileName)
        {
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out string contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

    }
}
