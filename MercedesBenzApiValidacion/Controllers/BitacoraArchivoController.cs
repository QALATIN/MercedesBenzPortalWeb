using MercedesBenzApiServicios.Services.Contracts;
using MercedesBenzModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercedesBenzApiServicios.Controllers
{
    [Route("api/bitacoraarchvio")]
    [ApiController]
    public class BitacoraArchivoController : ControllerBase
    {
        private readonly IGeneralRepository _context;
        private readonly ILogger<BitacoraArchivoController> _logger;
        private readonly IOptions<AppSettings> _app;
        public BitacoraArchivoController(IGeneralRepository Context, ILogger<BitacoraArchivoController> logger, IOptions<AppSettings> cfg)
        {
            _context = Context;
            _app = cfg;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Mercedes Benz - Api Bitacora OnLine");
        }
        [HttpPost]
        public IActionResult AgregarBitacoraArchivo(BitacoraArchivoRequest bitacora)
        {
            string mensaje = $"Bitacora Agregar Archivo";
            try
            {
                _logger.LogWarning(mensaje);
                var response = _context.SubirArchivoBitacora(bitacora, _app.Value.RepositoryPath);
                return Ok(response);
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
