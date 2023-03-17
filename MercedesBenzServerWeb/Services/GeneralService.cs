using MercedesBenzModel;
using MercedesBenzServerWeb.Helpers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    public class GeneralService : IGeneralService
    {
        private readonly HttpClient _httpClient;
        private readonly RepositoryService _repositoryService;

        public GeneralService(HttpClient httpClient, RepositoryService repositoryService)
        {
            _httpClient = httpClient;
            _repositoryService = repositoryService;
        }

        public async Task<(string, bool)> AddBitacoraAsync(BitacoraRequest request)
        {
            string mensaje = "";
            bool response = false;
            string objectSerialize = JsonConvert.SerializeObject(request);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "Bitacora", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = true;
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, bool)> UpdateValidacionAsync(ValidationResult request)
        {
            string mensaje = "";
            bool response = false;
            string objectSerialize = JsonConvert.SerializeObject(request);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "general/updateValidacion", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = true;
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

    }
}
