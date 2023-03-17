using MercedesBenzModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MercedesBenzApiPaquetes.Services
{
    public class ComparacionFacialService : IComparacionFacialService
    {
        private readonly HttpClient _httpClient;
        private readonly IDataService _dataService;

        public ComparacionFacialService(HttpClient httpClient, IDataService dataService)
        {
            _httpClient = httpClient;
            _dataService = dataService;
        }

        public async Task<(string, ComparacionfacialResponse)> ValidarComparacionFacial(string objectSerialize)
        {
            string mensaje = "";
            ComparacionfacialResponse response = null;
     
            (int statusCode, object content) = await _dataService.SendAsync(_httpClient, "POST", "compare", objectSerialize, null, true, false);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<ComparacionfacialResponse>((string)content);
            else
            {
                try
                {
                    response = null;
                    mensaje = "Error en el servicio de comparación facial.";
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                    mensaje = (string)content;
                }
            }

            return (mensaje, response);
        }

     
    }
}

