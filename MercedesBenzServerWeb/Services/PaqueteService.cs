using MercedesBenzModel;
using MercedesBenzServerWeb.Helpers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    public class PaqueteService : IPaqueteService
    {
        private readonly HttpClient _httpClient;
        private readonly RepositoryService _repositoryService;

        public PaqueteService(HttpClient httpClient, RepositoryService repositoryService)
        {
            _httpClient = httpClient;
            _repositoryService = repositoryService;
        }

        public async Task<(string, SolicitudFicha)> GetSolicitudFichaAsync(SolicitudRequest request)
        {
            string mensaje = "";
            SolicitudFicha response = null;
            string objectSerialize = JsonConvert.SerializeObject(request);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "paquetes/solicitudFicha", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<SolicitudFicha>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, SolicitudAvisoPrivacidad, string)> GetSolicitudAvisoPrivacidadAsync(SolicitudValidacionRequest request)
        {
            string mensaje = "";
            SolicitudAvisoPrivacidad response = null;
            string objectSerialize = JsonConvert.SerializeObject(request);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "paquetes/solicitudAvisoPrivacidad", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<SolicitudAvisoPrivacidad>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response, objectSerialize);
        }

        public async Task<(string, SolicitudIdentificacion, string)> GetSolicitudIdentificacionAsync(SolicitudValidacionRequest request)
        {
            string mensaje = "";
            SolicitudIdentificacion response = null;
            string objectSerialize = JsonConvert.SerializeObject(request);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "paquetes/solicitudIdentificacion", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<SolicitudIdentificacion>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response, objectSerialize);
        }

        public async Task<(string, SolicitudComparacionFacial, string)> GetSolicitudComparacionFacialAsync(SolicitudValidacionRequest request)
        {
            string mensaje = "";
            SolicitudComparacionFacial response = null;
            string objectSerialize = JsonConvert.SerializeObject(request);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "paquetes/solicitudComparacionFacial", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<SolicitudComparacionFacial>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response, objectSerialize);
        }

        public async Task<(string, SolicitudComparacionHuellas, string)> GetSolicitudComparacionHuellasAsync(SolicitudValidacionRequest request)
        {
            string mensaje = "";
            SolicitudComparacionHuellas response = null;
            string objectSerialize = JsonConvert.SerializeObject(request);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "paquetes/solicitudComparacionHuellas", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<SolicitudComparacionHuellas>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response, objectSerialize);
        }

        public async Task<(string, SolicitudDocumento, string)> GetSolicitudDocumentosAsync(SolicitudDocumentoRequest request)
        {
            string mensaje = "";
            SolicitudDocumento response = null;
            string objectSerialize = JsonConvert.SerializeObject(request);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "paquetes/solicitudDocumentos", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<SolicitudDocumento>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response, objectSerialize);
        }

        public async Task<(string, SolicitudValidacion)> GetSolicitudValidacionesAsync(SolicitudValidacionRequest request)
        {
            string mensaje = "";
            SolicitudValidacion response = null;
            string objectSerialize = JsonConvert.SerializeObject(request);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "paquetes/solicitudValidaciones", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<SolicitudValidacion>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, int)> UpdateListaNegraAsync(ListaNegraRequest request)
        {
            string mensaje = "";
            ListaNegra response = new();
            string objectSerialize = JsonConvert.SerializeObject(request);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "paquetes/ListaNegra", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<ListaNegra>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response.ListaNegraId);
        }

        public async Task<(string, int)> UpdateResolucionAsync(ResolucionRequest request)
        {
            string mensaje = "";
            Resolucion response = new();
            string objectSerialize = JsonConvert.SerializeObject(request);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "PUT", "paquetes/Resolucion", objectSerialize, null, true, true);

            if (statusCode == 200)
            {
                response = JsonConvert.DeserializeObject<Resolucion>((string)content);
                response.ResolucionId = response.SolicitanteId;
            }
            else
                mensaje = (string)content;

            return (mensaje, response.ResolucionId);
        }

        public async Task<(string, RespuestaPaginada)> GetSolicitudesEstatusAsync(SolicitudEstatusRequest request, PaginacionRequest paginacion)
        {
            string mensaje = "";
            RespuestaPaginada response = null;
            string parametros = $"?Paginacion.Pagina={paginacion.Pagina}&Paginacion.RegistrosPagina={paginacion.RegistrosPagina}";
            string objectSerialize = JsonConvert.SerializeObject(request);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "paquetes/solicitudesEstatus", objectSerialize, parametros, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<RespuestaPaginada>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, RespuestaPaginada)> GetSolicitudesNotificacionesAsync()
        {
            string mensaje = "";
            RespuestaPaginada response = null;

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "GET", "paquetes/notificaciones", null, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<RespuestaPaginada>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, byte[])> GetSolicitudHuellaAsync(int solicitanteId)
        {
            string mensaje = "";
            byte[] response = null;

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "GET", $"paquetes/downloadHuellas/{solicitanteId}", null, null, false, true);

            if (statusCode == 200)
                response = (byte[])content;
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

    }
}
