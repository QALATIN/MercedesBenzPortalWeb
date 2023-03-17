using MercedesBenzModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MercedesBenzApiPaquetes.Services
{
    public class GeneralService : IGeneralService
    {
        private readonly HttpClient _httpClient;
        private readonly IDataService _dataService;

        public GeneralService(HttpClient httpClient, IDataService dataService)
        {
            _httpClient = httpClient;
            _dataService = dataService;
        }

        public async Task<(string, bool)> AddBitacoraPostAsync(BitacoraRequest Request)
        {
            string mensaje = "";
            bool response = false;
            string objectSerialize = JsonConvert.SerializeObject(Request);

            (int statusCode, object content) = await _dataService.SendAsync(_httpClient, "POST", "Bitacora", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = true;
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, bool)> UpdateValidacionPostAsync(ValidationResult Request)
        {
            string mensaje = "";
            bool response = false;
            string objectSerialize = JsonConvert.SerializeObject(Request);

            (int statusCode, object content) = await _dataService.SendAsync(_httpClient, "POST", "general/updateValidacion", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = true;
            else
                mensaje = (string)content;

            return (mensaje, response);
        }
    }
}
