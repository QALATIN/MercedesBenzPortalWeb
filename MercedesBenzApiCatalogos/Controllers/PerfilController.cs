using MercedesBenzApiCatalogos.Contracts;
using MercedesBenzJwtAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MercedesBenzApiCatalogos.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(AuthenticationControllerFilter))]
    [Route("api/perfiles")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilRepository _context;
        private readonly ILogger<PerfilController> _logger;

        public PerfilController(IPerfilRepository context, ILogger<PerfilController> Logger)
        {
            _context = context;
            _logger = Logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string mensaje = "Catálogo Perfil|Consulta";
            try
            {
                _logger.LogWarning(mensaje);
                var data = await _context.GetAllAsync();
                if (data == null)
                {
                    mensaje += $"|Catálogo vacío";
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
    }
}
