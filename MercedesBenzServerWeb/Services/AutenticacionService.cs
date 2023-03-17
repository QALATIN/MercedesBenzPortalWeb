using MercedesBenzLibrary;
using MercedesBenzModel;
using MercedesBenzSecurity;
using MercedesBenzServerWeb.Helpers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    public class AutenticacionService : IAutenticacionService
    {
        private readonly HttpClient _httpClient;
        private readonly RepositoryService _repositoryService;

        public AutenticacionService(HttpClient httpClient, RepositoryService repositoryService)
        {
            _httpClient = httpClient;
            _repositoryService = repositoryService;
        }

        public async Task<(string, AutenticacionResponse)> GetValidacionAutenticacionAsync(UsuarioLogin login)
        {
            string mensaje = "";
            AutenticacionResponse response = null;

            //string passDec = CodificacionBase64.DecodificarTexto("RGFuaWVsQDI1");
            //string textDec = EncryptionAes.DecryptString("fukohBJqg0JaAPO/2a9X4qzHZEp5q0eM10BUULSL1SzOy/Ynw4JQab00GNqqcP+inwBHP83x9EqTzs5amiHIhw==");
            AutenticacionRequest autenticacionEncrypt = new() { NombreUsuario = login.NombreUsuario, Password = login.Password };
            autenticacionEncrypt.Password = CodificacionBase64.CodificarTexto(autenticacionEncrypt.Password);
            //string autenticacionEncrypt = EncryptionAes.EncryptObject(autenticacionEncrypt);
            string objectSerialize = JsonConvert.SerializeObject(autenticacionEncrypt);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "Autenticacion", objectSerialize, null, true, false);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<AutenticacionResponse>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

    }
}
