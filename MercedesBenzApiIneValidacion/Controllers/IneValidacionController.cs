using MercedesBenzApiIneValidacion.Contracts;
using MercedesBenzModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MercedesBenzApiIneValidacion.Controllers
{
    [Route("api/ineValidacion")]
    [ApiController]
    public class IneValidacionController : ControllerBase
    {
        private readonly IIneRepository _context;
        private readonly ILogger<IneValidacionController> _logger;
        private string _mensaje = "";

        public IneValidacionController(IIneRepository Context, ILogger<IneValidacionController> logger)
        {
            _context = Context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _mensaje = "Api Ine - Prueba de disponibilidad";
            try
            {
                _logger.LogWarning(_mensaje);

                return Ok("Mercedes Benz - Api de Ine OnLine");
            }
            catch (Exception ex)
            {
                _mensaje += "|" + ex.Message;
                _logger.LogError(_mensaje);
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(IneValidacionRequest Request)
        {
            _mensaje = $"Ine Validación Respuesta Portal|Guid:{Request.Guid.ToString()}";
            try
            {
                _logger.LogWarning(_mensaje);
                var newModel = await _context.AddPostAsync(Request);
                return Ok(newModel);
                //return CreatedAtRoute("IneValidacionByGuid", new { Guid = newModel.IneValidacion }, newModel);
            }
            catch (Exception ex)
            {
                _mensaje += "|" + ex.Message;
                _logger.LogError(_mensaje);
                return StatusCode(400, new { mensaje = _mensaje });
            }
        }

    }
}
