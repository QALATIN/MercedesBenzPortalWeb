using MercedesBenzApiServicios.Services.Contracts;
using MercedesBenzJwtAuthentication;
using MercedesBenzModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MercedesBenzApiServicios.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(AuthenticationControllerFilter))]
    [Route("api/bitacora")]
    [ApiController]
    public class BitacoraController : ControllerBase
    {

        private readonly IGeneralRepository _context;
        private readonly ILogger<BitacoraController> _logger;

        public BitacoraController(IGeneralRepository Context, ILogger<BitacoraController> logger)
        {
            _context = Context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Mercedes Benz - Api Bitacora OnLine");
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(BitacoraRequest bitacora)
        {
            string mensaje = $"Bitacora Agregar|OrigenId:{bitacora.OrigenId},UsuarioId:{bitacora.UsuarioId}";
            try
            {
                _logger.LogWarning(mensaje);
                var newModel = await _context.AddBitacoraPostAsync(bitacora);
                return Ok(newModel);
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
