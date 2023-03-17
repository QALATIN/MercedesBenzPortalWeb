using MercedesBenzApiReportes.Contracts;
using MercedesBenzModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using MercedesBenzJwtAuthentication;
using System.Data;
using MercedesBenzApiReportes.Helpers;
using MercedesBenzLibrary;

namespace MercedesBenzApiReportes.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(AuthenticationControllerFilter))]
    [Route("api/reportes")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteRepository _context;
        private readonly ILogger<ReporteController> _logger;
        private readonly ExcelHelper _excel;

        public ReporteController(IReporteRepository context, ILogger<ReporteController> logger, ExcelHelper excel) 
        {
            _context = context;
            _logger = logger;
            _excel = excel;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Mercedes Benz - Api de Reportes OnLine");
        }

        [HttpPost("semaforos/")]
        public async Task<IActionResult> Semaforos(string pages, [FromQuery] PaginacionRequest paginacion, FechaRequest periodo)
        {
            return await ConsultarReporte(TipoServicio.ReporteSemaforos, pages, paginacion, periodo);
        }

        [HttpPost("semaforosDescargar/")]
        public async Task<IActionResult> SemaforosDescargar(FechaRequest periodo)
        {
            (int statusCode, string mensaje, FileContentResult contentResult) = await ConsultarReporteDescarga(TipoServicio.ReporteSemaforosDescargar, periodo);
            if (statusCode == 200)
                return contentResult; 
            else if (statusCode == 204)
                return StatusCode(204, new { mensaje });
            else
                return StatusCode(400, mensaje);
        }

        [HttpPost("bitacora/")]
        public async Task<IActionResult> Bitacora(string pages, [FromQuery] PaginacionRequest paginacion, FechaRequest periodo)
        {
            return await ConsultarReporte(TipoServicio.ReporteBitacora, pages, paginacion, periodo);
        }

        [HttpPost("bitacoraDescargar/")]
        public async Task<IActionResult> BitacoraDescargar(FechaRequest periodo)
        {
            (int statusCode, string mensaje, FileContentResult contentResult) = await ConsultarReporteDescarga(TipoServicio.ReporteBitacoraDescargar, periodo);
            if (statusCode == 200)
                return contentResult;
            else if (statusCode == 204)
                return StatusCode(204, new { mensaje });
            else
                return StatusCode(400, mensaje);
        }

        [HttpPost("listaNegra/")]
        public async Task<IActionResult> ListaNegra(string pages, [FromQuery] PaginacionRequest paginacion, FechaRequest periodo)
        {
            return await ConsultarReporte(TipoServicio.ReporteListaNegra, pages, paginacion, periodo);
        }

        [HttpPost("listaNegraDescargar/")]
        public async Task<IActionResult> ListaNegraDescargar(FechaRequest periodo)
        {
            (int statusCode, string mensaje, FileContentResult contentResult) = await ConsultarReporteDescarga(TipoServicio.ReporteListaNegraDescargar, periodo);
            if (statusCode == 200)
                return contentResult;
            else if (statusCode == 204)
                return StatusCode(204, new { mensaje });
            else
                return StatusCode(400, mensaje);
        }

        [HttpPost("semaforoFacialDetalle/")]
        public async Task<IActionResult> SemaforoFacialDetalle(string pages, [FromQuery] PaginacionRequest paginacion, FechaRequest periodo)
        {
            return await ConsultarReporte(TipoServicio.ReporteSemaforoFacialDetalle, pages, paginacion, periodo);
        }

        [HttpPost("semaforoFacialDetalleDescargar/")]
        public async Task<IActionResult> SemaforoFacialDetalleDescargar(FechaRequest periodo)
        {
            (int statusCode, string mensaje, FileContentResult contentResult) = await ConsultarReporteDescarga(TipoServicio.ReporteSemaforoFacialDetalleDescargar, periodo);
            if (statusCode == 200)
                return contentResult;
            else if (statusCode == 204)
                return StatusCode(204, new { mensaje });
            else
                return StatusCode(400, mensaje);
        }

        [HttpPost("detalleEnvio/")]

        public async Task<IActionResult> DetalleEnvio(string pages, [FromQuery] PaginacionRequest paginacion, FechaRequest periodo)
        {
            return await ConsultarReporte(TipoServicio.ReporteDetalleEnvio, pages, paginacion, periodo);
        }

        [HttpPost("detalleEnvioDescargar/")]
        public async Task<IActionResult> DetalleEnvioDescargar(FechaRequest periodo)
        {
            (int statusCode, string mensaje, FileContentResult contentResult) = await ConsultarReporteDescarga(TipoServicio.ReporteDetalleEnvioDescargar, periodo);
            if (statusCode == 200)
                return contentResult;
            else if (statusCode == 204)
                return StatusCode(204, new { mensaje });
            else
                return StatusCode(400, mensaje);
        }

        private async Task<ObjectResult> ConsultarReporte(TipoServicio tipoServicio, string pages, PaginacionRequest paginacion, FechaRequest periodo)
        {
            ObjectResult objectResult = new(null);
            objectResult.StatusCode = 400;

            string mensaje = tipoServicio switch
            {
                TipoServicio.ReporteSemaforos => "Reporte semáforos",
                TipoServicio.ReporteDetalleEnvio => "Reporte detalle de envío",
                TipoServicio.ReporteListaNegra => "Reporte lista negra",
                TipoServicio.ReporteSemaforoFacialDetalle => "Reporte semáforo facial detalle",
                TipoServicio.ReporteBitacora => "Reporte bitácora",
                _ => "Reporte desconocido"
            };

            PaginacionRequest paginacionRequest = RequestParameters.ConvertPagination(pages);
            if (paginacionRequest is null)
                paginacionRequest = paginacion;

            (int statusCode, string mensajeValidado, PaginacionRequest paginacionValidada) = RequestParameters.ValidarPaginacion(paginacionRequest);
            mensaje += mensajeValidado;

            if (statusCode == 400)
            {
                _logger.LogError(mensaje);
                objectResult.Value = mensaje;
                return objectResult;
            }
            paginacionRequest = paginacionValidada;

            (int statusCodeFechas, string mensajeFechas) = RequestParameters.ValidarFechas(periodo);
            mensaje += mensajeFechas;

            if (statusCodeFechas == 400)
            {
                _logger.LogError(mensaje);
                objectResult.Value = mensaje;
                return objectResult;
            }

            _logger.LogWarning(mensaje);
            try
            {
                (int totalRegistros, IEnumerable<object> data) resultadoDB;
                resultadoDB.data = Array.Empty<object>();
                resultadoDB.totalRegistros = 0;
                switch (tipoServicio)
                {
                    case TipoServicio.ReporteSemaforos:
                        resultadoDB = await _context.SemaforosAsync(periodo, paginacionRequest);
                        break;
                    case TipoServicio.ReporteBitacora:
                        resultadoDB = await _context.BitacoraAsync(periodo, paginacionRequest);
                        break;
                    case TipoServicio.ReporteListaNegra:
                        resultadoDB = await _context.ListaNegraAsync(periodo, paginacionRequest);
                        break;
                    case TipoServicio.ReporteSemaforoFacialDetalle:
                        resultadoDB = await _context.SemaforoFacialDetalleAsync(periodo, paginacionRequest);
                        break;
                    case TipoServicio.ReporteDetalleEnvio:
                        resultadoDB = await _context.DetalleEnvioAsync(periodo, paginacionRequest);
                        break;
                }

                if (!resultadoDB.data.Any())
                {
                    mensaje += "|No se encontraron registros";
                    _logger.LogWarning(mensaje);
                    objectResult.StatusCode = 204;
                    objectResult.Value = mensaje;
                }
                else
                {
                    var dataRespuesta = DataResponse.PaginateData(resultadoDB.data, paginacionRequest, resultadoDB.totalRegistros, false);
                    objectResult.StatusCode = 200;
                    objectResult.Value = dataRespuesta;
                }
                return objectResult;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                objectResult.Value = mensaje;
                return objectResult;
            }
        }

        private async Task<(int, string, FileContentResult)> ConsultarReporteDescarga(TipoServicio tipoServicio, FechaRequest periodo)
        {
            string mensaje = tipoServicio switch
            {
                TipoServicio.ReporteSemaforosDescargar => "Reporte semáforos descargar",
                TipoServicio.ReporteDetalleEnvioDescargar => "Reporte detalle de envío descargar",
                TipoServicio.ReporteListaNegraDescargar => "Reporte lista negra descargar",
                TipoServicio.ReporteSemaforoFacialDetalleDescargar => "Reporte semáforo facial detalle descargar",
                TipoServicio.ReporteBitacoraDescargar => "Reporte bitácora descargar",
                _ => "Reporte desconocido"
            };

            (int statusCode, string mensajeValidacion) = RequestParameters.ValidarFechas(periodo);
            mensaje += mensajeValidacion;

            if (statusCode == 400)
            {
                _logger.LogError(mensaje);
                return (400, mensaje, null);
            }

            _logger.LogWarning($"{mensaje}");
            try
            {
                DataTable data = null;
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = "reporte.xlsx";
                string nombreReporte = "";

                switch (tipoServicio)
                {
                    case TipoServicio.ReporteSemaforosDescargar:
                        nombreReporte = "Semaforos";
                        data = await _context.SemaforosDescargarAsync(periodo);
                        break;
                    case TipoServicio.ReporteDetalleEnvioDescargar:
                        nombreReporte = "Detalle de envío";
                        data = await _context.DetalleEnvioDescargarAsync(periodo);
                        break;
                    case TipoServicio.ReporteListaNegraDescargar:
                        nombreReporte = "Lista negra";
                        data = await _context.ListaNegraDescargarAsync(periodo);
                        break;
                    case TipoServicio.ReporteSemaforoFacialDetalleDescargar:
                        nombreReporte = "Semaforo facial";
                        data = await _context.SemaforoFacialDetalleDescargarAsync(periodo);
                        break;
                    case TipoServicio.ReporteBitacoraDescargar:
                        nombreReporte = "Bitácora";
                        data = await _context.BitacoraDescargarAsync(periodo);
                        break;
                }

                if (data == null)
                {
                    mensaje += "|No se encontraron registros";
                    _logger.LogWarning(mensaje);
                    return (204, mensaje, null);
                }
                else
                {
                    var file = File(_excel.CrearExcel(nombreReporte, data), contentType, fileName);
                    return (200, mensaje, file);
                }
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return (400, mensaje, null);
            }
        }

    }
}
