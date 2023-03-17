using MercedesBenzModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using MercedesBenzServerWeb.Helpers;

namespace MercedesBenzServerWeb.Services
{
    public class ReporteService : IReporteService
    {
        private readonly HttpClient _httpClient;
        private readonly RepositoryService _repositoryService;

        public ReporteService(HttpClient httpClient, RepositoryService repositoryService)
        {
            _httpClient = httpClient;
            _repositoryService = repositoryService;
        }

        public async Task<(string, RespuestaPaginada)> GetReporteAsync(string urlRequest, FechaRequest fechas, PaginacionRequest paginacion)
        {
            string mensaje  = "";
            RespuestaPaginada response = null;
            string parametros = $"?Paginacion.Pagina={paginacion.Pagina}&Paginacion.RegistrosPagina={paginacion.RegistrosPagina}";
            string objectSerialize = JsonConvert.SerializeObject(fechas);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", urlRequest, objectSerialize, parametros, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<RespuestaPaginada>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);

            //string fechasBase64 = CodificacionBase64.CodificarObjeto(Fechas);
            //string paginacionBase64 = CodificacionBase64.CodificarObjeto(Paginacion);
            //string parametros = $"?Fechas={fechasBase64}&Paginas={paginacionBase64}";
        }

        public async Task<(string, byte[])> DownloadReporteAsync(string urlRequest, FechaRequest fechas)
        {
            string mensaje = "";
            byte[] response = null;
            string objectSerialize = JsonConvert.SerializeObject(fechas);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", urlRequest, objectSerialize, null, false, true);

            if (statusCode == 200)
                response = (byte[])content;
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

    }
}
