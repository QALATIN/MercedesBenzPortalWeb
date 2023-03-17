using System.Net.Http;
using System.Threading.Tasks;

namespace MercedesBenzApiPaquetes.Services
{
    public interface IDataService
    {
        Task<(int, object)> SendAsync(HttpClient httpClient, string metodo, string urlRequest, string contentRequest, string parametros, bool responseString, bool sendToken);
    }
}
