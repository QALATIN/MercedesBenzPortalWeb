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
    [Route("api/general")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly IGeneralRepository _context;
        private readonly ILogger<GeneralController> _logger;

        public GeneralController(IGeneralRepository Context, ILogger<GeneralController> logger)
        {
            _context = Context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Mercedes Benz - Api General OnLine");
        }

        [HttpPost("updateValidacion/")]
        public async Task<IActionResult> UpdateValidacionPostAsync(ValidationResult validation)
        {
            string mensaje = $"Validacion {validation.TipoValidacion}|ValidacionId:{validation.ValidacionId},Semaforo:{validation.Semaforo}";
            try
            {
                _logger.LogWarning(mensaje);
                var response = await _context.UpdateValidacionPostAsync(validation);
                return Ok(new { success = response });
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
