using MercedesBenzModel;
using MercedesBenzServerWeb.Helpers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    public class BusquedaService : IBusquedaService
    {
        private readonly HttpClient _httpClient;
        private readonly RepositoryService _repositoryService;

        public BusquedaService(HttpClient httpClient, RepositoryService repositoryService)
        {
            _httpClient = httpClient;
            _repositoryService = repositoryService;
        }

        public async Task<(string, RespuestaPaginada)> GetBusquedaAsync(BusquedaRequest busqueda, PaginacionRequest paginacion)
        {
            string mensaje = "";
            RespuestaPaginada response = null;
            string parametros = $"?Paginacion.Pagina={paginacion.Pagina}&Paginacion.RegistrosPagina={paginacion.RegistrosPagina}";
            string objectSerialize = JsonConvert.SerializeObject(busqueda);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "busquedas", objectSerialize, parametros, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<RespuestaPaginada>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);

            //response = EncryptionAes.DecryptObject(content, new RespuestaPaginada());
        }

    }
}
