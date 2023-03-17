using MercedesBenzApiAfis.Contracts;
using MercedesBenzModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MercedesBenzApiAfis.Controllers
{
    [Route("api/afis")]
    [ApiController]
    public class AfiController : ControllerBase
    {
        private readonly IAfiRepository _context;
        private readonly ILogger<AfiController> _logger;

        public AfiController(IAfiRepository context, ILogger<AfiController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string mensaje = "Afis Validación";
            try
            {
                _logger.LogWarning(mensaje);
                var data = await _context.GetAsync();
                if (data == null)
                {
                    mensaje += "|No se encontraron Paquetes para validar";
                    _logger.LogWarning(mensaje);
                    return StatusCode(204, new { mensaje });
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(AfisRequest Request)
        {
            string mensaje = $"Afis Respuesta|PaqueteId:{Request.PaqueteId},AfisId:{Request.AfisId}";
            try
            {
                _logger.LogWarning(mensaje);
                var update = await _context.AddAsync(Request);
                if(update)
                {
                    mensaje += "|Validado";
                    _logger.LogWarning(mensaje);
                    return Ok(mensaje);
                } else
                {
                    mensaje += "|Error inesperado";
                    _logger.LogError(mensaje);
                    return StatusCode(400, new { mensaje });
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
