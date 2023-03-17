using MercedesBenzApiBusquedas.Contracts;
using MercedesBenzJwtAuthentication;
using MercedesBenzLibrary;
using MercedesBenzModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercedesBenzApiBusquedas.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(AuthenticationControllerFilter))]
    [Route("api/busquedas")]
    [ApiController]
    public class BusquedaController : ControllerBase
    {
        private readonly IBusquedaRepository _context;
        private readonly ILogger<BusquedaController> _logger;

        public BusquedaController(IBusquedaRepository context, ILogger<BusquedaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Mercedes Benz - Api de Búsquedas OnLine");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] string pages, [FromQuery] PaginacionRequest paginacion, BusquedaRequest busqueda)
        {
            string mensaje = "Búsqueda";

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

            //BusquedaRequest busquedaRequest = RequestParameters.ConvertBusqueda();

            (int statusCodeBusqueda, string mensajeBusqueda) = RequestParameters.ValidarBusqueda(busqueda);
            mensaje += mensajeBusqueda;

            if (statusCodeBusqueda == 400)
            {
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }

            _logger.LogWarning(mensaje);
            try
            {
                (int totalRegistros, IEnumerable<object> data) = await _context.BusquedaAsync(busqueda, paginacionRequest);

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

    }
}
