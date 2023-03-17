using MercedesBenzApiPaquetes.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MercedesBenzModel;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using MercedesBenzJwtAuthentication;
using System.Collections.Generic;
using System.Linq;
using MercedesBenzLibrary;
using Newtonsoft.Json;
using MercedesBenzSecurity;

namespace MercedesBenzApiPaquetes.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(AuthenticationControllerFilter))]
    [Route("api/paquetes")]
    [ApiController]
    public class PaqueteController : ControllerBase
    {
        private readonly IPaqueteRepository _context;
        private readonly ILogger<PaqueteController> _logger;

        public PaqueteController(IPaqueteRepository context, ILogger<PaqueteController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("validacionesDatosExternos/")]
        public async Task<IActionResult> ValidacionesDatosExternosPostAsync(SolicitudValidacionRequest request)
        {
            string userName = GetClaimUserName();
            int usuarioId = await _context.GetUserIdAsync(userName, false);
            await _context.IniciarValidacionDatosExternosAsync(request.ValidacionId, request.SolicitanteId, request.IdentificacionIne, usuarioId);
            await _context.IniciarValidacionDocumentosExternosAsync(request.ValidacionId, request.SolicitanteId, usuarioId);

            return Ok(true);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Mercedes Benz - Api de Paquetes OnLine");
        }

        [HttpGet("notificaciones/")]
        public async Task<IActionResult> GetNotificacionesAsync()
        {
            string nombreServicio = "Paquete Notificaciones";

            PaginacionRequest paginacion = new() { Pagina = 1, RegistrosPagina = 5 };
            return await ConsultarServicioPaginado(TipoServicio.PaqueteNotificacion, nombreServicio, null, paginacion);
        }

        [HttpGet("downloadHuellas/{id}")]
        public async Task<FileResult> GetDownloadHuellasAsync(int id)
        {
            string mensaje = $"Paquete Huellas Descargar|SolicitanteId:{id}";
            try
            {
                _logger.LogWarning(mensaje);
                string mimeType = "application/x-zip-compressed";
                (string pathFile, string file) = await _context.GetSolicitudHuellasByIdAsync(id);
                return PhysicalFile(pathFile, mimeType, file);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return null;
            }
        }

        [HttpPost("solicitudesEstatus/")]
        public async Task<IActionResult> GetSolicitudesEstatusAsync(SolicitudEstatusRequest request, [FromQuery] PaginacionRequest paginacion)
        {
            string mensaje = "Paquete Solicitudes Estatus";
            (int statusCode, string mensajeValidado) = ValidarParametrosSolicitudesEstatus(mensaje, request, paginacion);
            if (statusCode == 400)
                return StatusCode(400, mensajeValidado);

            return await ConsultarServicioPaginado(TipoServicio.BusquedaSolicitudesEstatus, mensaje, request, paginacion);
        }

        [HttpPost("solicitudFicha/")]
        public async Task<IActionResult> GetSolicitudFichaAsync(SolicitudRequest request)
        {
            string mensaje = $"Paquete Solicitud Consulta|SolicitanteId:{request.SolicitanteId}";
            try
            {
                _logger.LogWarning(mensaje);
                var solicitud = await _context.GetSolicitudFichaAsync(request);
                if (solicitud == null)
                {
                    mensaje += "|SolicitanteId incorrecto";
                    _logger.LogWarning(mensaje);
                    return NotFound(new { mensaje });
                }
                return Ok(solicitud);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }

        }

        [HttpPost("solicitudIdentificacion/")]
        public async Task<IActionResult> GetSolicitudIdentificacionAsync(SolicitudValidacionRequest request)
        {
            string mensaje = $"Paquete Solicitud Identificacion Validar|SolicitanteId:{request.SolicitanteId}";
            try
            {
                _logger.LogWarning(mensaje);
                var solicitud = await _context.GetSolicitudIdentificacionAsync(request);
                if (solicitud == null)
                {
                    mensaje += "|SolicitanteId incorrecto";
                    _logger.LogWarning(mensaje);
                    return NotFound(new { mensaje });
                }
                return Ok(solicitud);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }

        }

        [HttpPost("solicitudComparacionFacial/")]
        public async Task<IActionResult> GetSolicitudComparacionFacialAsync(SolicitudValidacionRequest request)
        {
            string mensaje = $"Paquete Solicitud Comparación Facial Validar|SolicitanteId:{request.SolicitanteId}";
            try
            {
                _logger.LogWarning(mensaje);
                var solicitud = await _context.GetSolicitudComparacionFacialAsync(request);
                if (solicitud == null)
                {
                    mensaje += "|SolicitanteId incorrecto";
                    _logger.LogWarning(mensaje);
                    return NotFound(new { mensaje });
                }
                return Ok(solicitud);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }

        }

        [HttpPost("ComparacionFacial/")]
        public async Task<IActionResult> ComparacionFacial(ComparacionfacialRequest request)
        {
            string mensaje = $"Comparación Facial|Servicio comparación facial";
            try
            {
                _logger.LogWarning(mensaje);
                ComparacionfacialResponse solicitud = await _context.ComparacionFacial(request);
                mensaje += "|Comparación facial generado";
                _logger.LogWarning(mensaje);
                return Ok(solicitud);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }

        }



        [HttpPost("solicitudComparacionHuellas/")]
        public async Task<IActionResult> GetSolicitudComparacionHuellasAsync(SolicitudValidacionRequest request)
        {
            string mensaje = $"Paquete Solicitud Comparación Huellas Validar|SolicitanteId:{request.SolicitanteId}";
            try
            {
                _logger.LogWarning(mensaje);
                var solicitud = await _context.GetSolicitudComparacionHuellasAsync(request);
                if (solicitud == null)
                {
                    mensaje += "|Datos incorrectos";
                    _logger.LogWarning(mensaje);
                    return NotFound(new { mensaje });
                }
                return Ok(solicitud);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }

        }

        [HttpPost("solicitudDocumentos/")]
        public async Task<IActionResult> GetSolicitudDocumentosAsync(SolicitudDocumentoRequest request)
        {
            string mensaje = $"Paquete Solicitud Documento|SolicitanteId:{request.SolicitanteId},DocumentoId:{request.DocumentoId}";
            try
            {
                _logger.LogWarning(mensaje);
                var solicitud = await _context.GetSolicitudDocumentoAsync(request);
                if (solicitud == null)
                {
                    mensaje += "|Datos incorrectos";
                    _logger.LogWarning(mensaje);
                    return NotFound(new { mensaje });
                }
                return Ok(solicitud);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }

        }

        [HttpPost("solicitudAvisoPrivacidad/")]
        public async Task<IActionResult> GetSolicitudAvisoPrivacidadAsync(SolicitudValidacionRequest request)
        {
            string mensaje = $"Paquete Solicitud Aviso de Privacidad|SolicitanteId:{request.SolicitanteId}";
            try
            {
                _logger.LogWarning(mensaje);
                var solicitud = await _context.GetSolicitudAvisoPrivacidadAsync(request);
                if (solicitud == null)
                {
                    mensaje += "|SolicitanteId incorrecto";
                    _logger.LogWarning(mensaje);
                    return NotFound(new { mensaje });
                }
                return Ok(solicitud);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }

        }

        [HttpPost("solicitudValidaciones/")]
        public async Task<IActionResult> GetSolicitudValidacionesAsync(SolicitudValidacionRequest request)
        {
            string mensaje = $"Paquete Solicitud Validaciones|SolicitanteId:{request.SolicitanteId}";
            try
            {
                _logger.LogWarning(mensaje);
                var solicitud = await _context.GetSolicitudValidacionesAsync(request);
                if (solicitud == null)
                {
                    mensaje += "|Datos incorrectos";
                    _logger.LogWarning(mensaje);
                    return NotFound(new { mensaje });
                }
                return Ok(solicitud);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        [HttpPost("solicitantes/")]
        public async Task<IActionResult> AddSolicitanteAsync(SolicitanteRequest request)
        {
            //var solicitante = JsonConvert.DeserializeObject<SolicitanteRequest>(System.IO.File.ReadAllText(@"C:\TmpPaquete\Solicitante4.json"));
            //var solicitanteEncrypt = EncryptionAes.EncryptObject(request);

            string mensaje = "Paquete Solicitante";
            int solicitanteId = request.SolicitanteId == 0 ? 1 : request.SolicitanteId;
            (int statusCode, string mensajeValidado) = ValidarParametros(mensaje, request.FechaDeRegistro, request.UsuarioId, solicitanteId, request.FechaDeNacimiento);
            if (statusCode == 400)
                return StatusCode(400, mensajeValidado);

            mensaje += "|Nuevo";
            return await ConsultarServicio(TipoServicio.PaqueteSolicitantes, mensaje, request, null, null, null, null, null);
        }

        [HttpPost("sendSolicitantes/")]
        public async Task<IActionResult> AddSolicitanteObjectAsync(DataRequest data)
        {
            string mensaje = "Paquete Solicitante";
            try
            {
                SolicitanteRequest request = EncryptionAes.DecryptObject(data.Object, new SolicitanteRequest());
                int solicitanteId = request.SolicitanteId == 0 ? 1 : request.SolicitanteId;
                (int statusCode, string mensajeValidado) = ValidarParametros(mensaje, request.FechaDeRegistro, request.UsuarioId, solicitanteId, request.FechaDeNacimiento);
                if (statusCode == 400)
                    return StatusCode(400, mensajeValidado);

                mensaje += "|Nuevo";
                return await ConsultarServicio(TipoServicio.PaqueteSolicitantes, mensaje, request, null, null, null, null, null);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        [HttpPost("huellas/")]
        public async Task<IActionResult> AddHuellaAsync(HuellaRequest request)
        {
            //var huellasEncrypt = EncryptionAes.EncryptObject(request);

            string mensaje = "Paquete Huella";
            (int statusCode, string mensajeValidado) = ValidarParametros(mensaje, request.FechaDeRegistro, request.UsuarioId, request.SolicitanteId, null);
            if (statusCode == 400)
                return StatusCode(400, mensajeValidado);

            mensaje += "|Nuevo";
            return await ConsultarServicio(TipoServicio.PaqueteHuellas, mensaje, null, request, null, null, null, null);
        }

        [HttpPost("sendHuellas/")]
        public async Task<IActionResult> AddHuellaObjectAsync(DataRequest data)
        {
            string mensaje = "Paquete Huella";
            try
            {
                HuellaRequest request = EncryptionAes.DecryptObject(data.Object, new HuellaRequest());
                (int statusCode, string mensajeValidado) = ValidarParametros(mensaje, request.FechaDeRegistro, request.UsuarioId, request.SolicitanteId, null);
                if (statusCode == 400)
                    return StatusCode(400, mensajeValidado);

                mensaje += "|Nuevo";
                return await ConsultarServicio(TipoServicio.PaqueteHuellas, mensaje, null, request, null, null, null, null);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        [HttpPost("documentos/")]
        public async Task<IActionResult> AddDocumentoAsync(DocumentoRequest request)
        {
            //var documentoEncrypt = EncryptionAes.EncryptObject(request);

            string mensaje = "Paquete Documento";
            (int statusCode, string mensajeValidado) = ValidarParametros(mensaje, request.FechaDeRegistro, request.UsuarioId, request.SolicitanteId, null);
            if (statusCode == 400)
                return StatusCode(400, mensajeValidado);

            mensaje += "|Nuevo";
            return await ConsultarServicio(TipoServicio.PaqueteDocumentos, mensaje, null, null, request, null, null, null);
        }

        [HttpPost("sendDocumentos/")]
        public async Task<IActionResult> AddDocumentoObjectAsync(DataRequest data)
        {
            string mensaje = "Paquete Documento";
            try
            {
                DocumentoRequest request = EncryptionAes.DecryptObject(data.Object, new DocumentoRequest());
                (int statusCode, string mensajeValidado) = ValidarParametros(mensaje, request.FechaDeRegistro, request.UsuarioId, request.SolicitanteId, null);
                if (statusCode == 400)
                    return StatusCode(400, mensajeValidado);

                mensaje += "|Nuevo";
                return await ConsultarServicio(TipoServicio.PaqueteDocumentos, mensaje, null, null, request, null, null, null);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        [HttpPost("avisoPrivacidad/")]
        public async Task<IActionResult> AddAvisoPrivacidadAsync(AvisoPrivacidadRequest request)
        {
            //var avisoPrivacidadEncrypt = EncryptionAes.EncryptObject(request);

            string mensaje = "Paquete Aviso de Privacidad";
            (int statusCode, string mensajeValidado) = ValidarParametros(mensaje, request.FechaDeRegistro, request.UsuarioId, request.SolicitanteId, null);
            if (statusCode == 400)
                return StatusCode(400, mensajeValidado);

            mensaje += "|Nuevo";
            return await ConsultarServicio(TipoServicio.PaqueteAvisosPrivacidad, mensaje, null, null, null, request, null, null);
        }

        [HttpPost("sendAvisoPrivacidad/")]
        public async Task<IActionResult> AddAvisoPrivacidadObjectAsync(DataRequest data)
        {
            string mensaje = "Paquete Aviso de Privacidad";
            try
            {
                AvisoPrivacidadRequest request = EncryptionAes.DecryptObject(data.Object, new AvisoPrivacidadRequest());
                (int statusCode, string mensajeValidado) = ValidarParametros(mensaje, request.FechaDeRegistro, request.UsuarioId, request.SolicitanteId, null);
                if (statusCode == 400)
                    return StatusCode(400, mensajeValidado);

                mensaje += "|Nuevo";
                return await ConsultarServicio(TipoServicio.PaqueteAvisosPrivacidad, mensaje, null, null, null, request, null, null);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                _logger.LogError(mensaje);
                return StatusCode(400, new { mensaje });
            }
        }

        [HttpPost("listaNegra/")]
        public async Task<IActionResult> AddListaNegraAsync(ListaNegraRequest request)
        {
            string mensaje = "Paquete Lista Negra";
            (int statusCode, string mensajeValidado) = ValidarParametros(mensaje, DateTime.Now, request.UsuarioId, request.SolicitanteId, null);
            if (statusCode == 400)
                return StatusCode(400, mensajeValidado);

            mensaje += "|Nuevo";
            return await ConsultarServicio(TipoServicio.PaqueteListaNegra, mensaje, null, null, null, null, request, null);
        }

        [HttpPut("resolucion/")]
        public async Task<IActionResult> UpdateResolucionAsync(ResolucionRequest request)
        {
            string mensaje = $"Paquete Resolución";
            (int statusCode, string mensajeValidado) = ValidarParametros(mensaje, DateTime.Now, request.UsuarioId, request.SolicitanteId, null);
            if (statusCode == 400)
                return StatusCode(400, mensajeValidado);

            mensaje += $"|SolicitanteId: {request.SolicitanteId}, TipoResolucionId: {request.TipoResolucionId}";
            return await ConsultarServicio(TipoServicio.PaqueteResolucion, mensaje, null, null, null, null, null, request);
        }

        private (int, string) ValidarParametros(string mensaje, DateTime fechaDeRegistro, int usuarioId, int solicitanteId, string fechaNacimiento)
        {
            int statusCode = 400;
            string mensajeValidado = "";

            if (fechaDeRegistro == DateTime.MinValue)
                mensajeValidado += "|Error La Fecha de Registro es obligatoria";

            if (usuarioId <= 0)
                mensajeValidado += "|Error El campo UsuarioId es obligatorio";

            if (solicitanteId <= 0)
                mensajeValidado += "|Error El campo SolicitanteId es obligatorio";

            if (!string.IsNullOrEmpty(fechaNacimiento))
            {
                if (!DateTime.TryParse(fechaNacimiento, out DateTime fechaNacimientoValidada))
                    mensajeValidado += "|Error El campo FechaDeNacimiento es incorrecto";
                else
                {
                    if ((fechaNacimientoValidada.Year < 1940 || fechaNacimientoValidada.Year > (DateTime.Now.Year - 16)))
                        mensajeValidado += "|Error El campo FechaDeNacimiento es incorrecto";
                }
            }

            if (string.IsNullOrEmpty(mensajeValidado))
                statusCode = 200;
            else
            {
                mensaje += mensajeValidado;
                _logger.LogError(mensaje);
            }

            return (statusCode, mensaje);
        }

        private (int, string) ValidarParametrosSolicitudesEstatus(string mensaje, SolicitudEstatusRequest request, PaginacionRequest paginacion)
        {
            int statusCode = 400;
            string mensajeValidado = "";
            string filtro = "";

            if (request.FechaInicial != DateTime.MinValue && request.FechaFinal != DateTime.MinValue)
            {
                filtro += $"|Fecha: Del {request.FechaInicial:dd MMMM yyyy} al {request.FechaFinal:dd MMMM yyyy}";
            } 
            if (string.IsNullOrEmpty(filtro))
            {
                mensajeValidado = "|Debe ingresar la fecha inicial y la fecha final";
            }
            else
            {
                if (paginacion.Pagina == null && paginacion.RegistrosPagina == null)
                {
                    filtro += "|Todo";
                }
                else
                {
                    if (paginacion.Pagina == null || paginacion.Pagina <= 0)
                    {
                        mensajeValidado += $"|El valor '{paginacion.Pagina}' no es válido en el campo Paginacion.Pagina";

                    }
                    else if (paginacion.RegistrosPagina == null || paginacion.RegistrosPagina <= 0)
                    {
                        mensajeValidado += $"|El valor '{paginacion.RegistrosPagina}' no es válido en el campo Paginacion.RegistrosPagina";
                    }
                    else
                    {
                        filtro += $"|Pagina: {paginacion.Pagina}, RegistrosPagina: {paginacion.RegistrosPagina}";
                    }
                }
            }

            if (string.IsNullOrEmpty(mensajeValidado))
            {
                statusCode = 200;
                mensaje += filtro;
            }
            else
            {
                mensaje += mensajeValidado;
                _logger.LogError(mensaje);
            }

            return (statusCode, mensaje);

        }

        private async Task<ObjectResult> ConsultarServicio(TipoServicio tipoServicio, string mensaje
            , SolicitanteRequest solicitante = null, HuellaRequest huella = null
            , DocumentoRequest documento = null, AvisoPrivacidadRequest privacidad = null
            , ListaNegraRequest listaNegra = null, ResolucionRequest resolucion = null
            )
        {
            ObjectResult objectResult = new(null);
            objectResult.StatusCode = 400;  

            _logger.LogWarning(mensaje);
            try
            {
                objectResult.StatusCode = 200;
                int id = -1;
                object dataResponse = null;
                switch (tipoServicio)
                {
                    case TipoServicio.PaqueteSolicitantes:
                        (int solicitanteId, int identificacionId, int capturaIdentificacionId1, int capturaIdentificacionId4, int fotoId1, int fotoId2) = await _context.AddSolicitanteAsync(solicitante);
                        string clavesNuevas = $"|SolicitanteId = {solicitanteId}";

                        if (identificacionId > 0)
                            clavesNuevas += $", IdentificacionId = {identificacionId}";
                        if (capturaIdentificacionId1 > 0)
                            clavesNuevas += $", CapturaIdentificacionId = {capturaIdentificacionId1}";
                        if (capturaIdentificacionId4 > 0)
                            clavesNuevas += $", CapturaIdentificacionId = {capturaIdentificacionId4}";
                        if (fotoId1 > 0)
                            clavesNuevas += $", FotoId = {fotoId1}";
                        if (fotoId2 > 0)
                            clavesNuevas += $", FotoId = {fotoId2}";

                        mensaje += clavesNuevas;
                        dataResponse = new { SolicitanteId = solicitanteId };
                        break;
                    case TipoServicio.PaqueteHuellas:
                        id = await _context.AddHuellaAsync(huella);
                        clavesNuevas = $"|HuellaId = {id}";
                        mensaje += clavesNuevas;
                        dataResponse = new { HuellaId = id };
                        break;
                    case TipoServicio.PaqueteDocumentos:
                        id = await _context.AddDocumentoAsync(documento);
                        clavesNuevas = $"|DocumentoId = {id}";
                        mensaje += clavesNuevas;
                        dataResponse = new { DocumentoId = id };
                        break;
                    case TipoServicio.PaqueteAvisosPrivacidad:
                        id = await _context.AddAvisoPrivacidadAsync(privacidad);
                        clavesNuevas = $"|AvisoPrivacidadId = {id}";
                        mensaje += clavesNuevas;
                        dataResponse = new { AvisoPrivacidadId = id };
                        break;
                    case TipoServicio.PaqueteListaNegra:
                        id = await _context.AddListaNegraAsync(listaNegra);
                        clavesNuevas = $"|ListaNegraId = {id}";
                        mensaje += clavesNuevas;
                        dataResponse = new { ListaNegraId = id };
                        break;
                    case TipoServicio.PaqueteResolucion:
                        var resolucionResponse = await _context.UpdateResolucionAsync(resolucion);
                        if(resolucionResponse == null)
                        {
                            mensaje += "|Id incorrecto";
                            objectResult.StatusCode = 400;
                            dataResponse = new { mensaje };
                        } 
                        else 
                            dataResponse = resolucionResponse;
                        break;
                }
                _logger.LogWarning(mensaje);
                objectResult.Value = dataResponse;
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

        private async Task<ObjectResult> ConsultarServicioPaginado(TipoServicio tipoServicio, string nombreServicio, SolicitudEstatusRequest request, PaginacionRequest paginacion)
        {
            ObjectResult objectResult = new(null);
            objectResult.StatusCode = 400;

            string mensaje = nombreServicio;
            _logger.LogWarning(mensaje);
            try
            {
                (int totalRegistros, IEnumerable<object> data) resultadoDB;
                resultadoDB.data = Array.Empty<object>();
                resultadoDB.totalRegistros = 0;
                switch (tipoServicio)
                {
                    case TipoServicio.BusquedaSolicitudesEstatus:
                        resultadoDB = await _context.GetSolicitantesEstatusAsync(request, paginacion);
                        break;
                    case TipoServicio.PaqueteNotificacion:
                        resultadoDB = await _context.GetNotificacionesAsync(paginacion);
                        break;
                }

                if (!resultadoDB.data.Any())
                {
                    mensaje += "|No se encontraron datos";
                    _logger.LogWarning(mensaje);
                    objectResult.StatusCode = 204;  // No Content
                    objectResult.Value = mensaje;
                }
                else
                {
                    var dataRespuesta = DataResponse.PaginateData(resultadoDB.data, paginacion, resultadoDB.totalRegistros, false);
                    objectResult.StatusCode = 200;  // OK
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

        private string GetClaimUserName()
        {
            string userName = "";
            var claims = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
            var usuario = claims.Claims.FirstOrDefault(x => x.Type.Contains("name"));
            if(usuario != null)
                userName = usuario.Value;
            return userName;
        }

    }
}
