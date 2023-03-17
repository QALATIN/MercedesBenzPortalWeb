using MercedesBenzModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MercedesBenzServerWeb.Helpers;

namespace MercedesBenzServerWeb.Services
{
    public class AgenciaService : IAgenciaService
    {
        private readonly HttpClient _httpClient;
        private readonly RepositoryService _repositoryService;

        public AgenciaService(HttpClient httpClient, RepositoryService repositoryService)
        {
            _httpClient = httpClient;
            _repositoryService = repositoryService;
        }

        public async Task<(string, RespuestaPaginada)> GetAllAsync(PaginacionRequest paginacion)
        {

            string mensaje = "";
            RespuestaPaginada response = null;
            string parametros = $"?Paginacion.Pagina={paginacion.Pagina}&Paginacion.RegistrosPagina={paginacion.RegistrosPagina}";

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "GET", "agencias", null, parametros, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<RespuestaPaginada>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, Agencia)> GetByIdAsync(int id)
        {
            string mensaje = "";
            string urlRequest = $"agencias/{id}";
            Agencia response = null;

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "GET", urlRequest, null, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<Agencia>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, Agencia)> AddAsync(Agencia agencia)
        {
            string mensaje = "";
            Agencia response = null;
            string objectSerialize = JsonConvert.SerializeObject(agencia);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "agencias", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<Agencia>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, Agencia)> UpdateAsync(Agencia agencia)
        {
            string mensaje = "";
            Agencia response = null;
            string objectSerialize = JsonConvert.SerializeObject(agencia);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "PUT", "agencias", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<Agencia>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, bool)> DeleteAsync(int id)
        {
            string mensaje = "";
            string urlRequest = $"agencias/{id}";
            bool response = false;

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "DELETE", urlRequest, null, null, true, true);

            if (statusCode == 204)
                response = true;
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, bool)> UpdateEstatuAsync(AgenciaEstatusRequest estatus)
        {
            string mensaje = "";
            bool response = false;
            string objectSerialize = JsonConvert.SerializeObject(estatus);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "agencias/cambiarEstatus", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = true;
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

    }
}
