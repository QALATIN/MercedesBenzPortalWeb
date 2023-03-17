using MercedesBenzLibrary;
using MercedesBenzModel;
using MercedesBenzServerWeb.Helpers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    public class RecuperacionService : IRecuperacionService
    {
        private readonly HttpClient _httpClient;
        private readonly RepositoryService _repositoryService;

        public RecuperacionService(HttpClient httpClient, RepositoryService repositoryService)
        {
            _httpClient = httpClient;
            _repositoryService = repositoryService;
        }

        public async Task<(string, ValidacionTokenRecuperacion)> ValidarTokenRecuperacionAsync(TokenRequest request)
        {
            string mensaje = "";
            ValidacionTokenRecuperacion response = null;
            string objectSerialize = JsonConvert.SerializeObject(request);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "recuperacion/validarTokenRecuperacion", objectSerialize, null, true, false);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<ValidacionTokenRecuperacion>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, bool)> ResetPasswordAsync(ResetPassword request)
        {
            string mensaje  = "";
            bool response = false;

            ResetPasswordRequest newRequest = new() { CorreoElectronico = request.CorreoElectronico, Password = request.Password };
            newRequest.Password = CodificacionBase64.CodificarTexto(newRequest.Password);
            string objectSerialize = JsonConvert.SerializeObject(newRequest);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "recuperacion/resetPassword", objectSerialize, null, true, false);

            if (statusCode == 204)
                response = true;
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, bool)> SendCorreoAsync(EmailRequest request)
        {
            string mensaje = "";
            bool response = false;

            string objectSerialize = JsonConvert.SerializeObject(request);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "recuperacion/sendMail", objectSerialize, null, true, false);

            if (statusCode == 200)
                response = true;
            else
                mensaje = (string)content;

            return (mensaje, response);
        }
    }
}
