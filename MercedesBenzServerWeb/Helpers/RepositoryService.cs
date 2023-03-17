using MercedesBenzModel;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Helpers
{
    public class RepositoryService
    {

        private UserCredential _credential;
        private readonly LocalStorage _localStorage;
        private readonly AuthenticationStateProvider _authentication;

        public RepositoryService(UserCredential credential, LocalStorage localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _credential = credential;
            _localStorage = localStorage;
            _authentication = authenticationStateProvider;
        }

        public async Task<(int, object)> SendAsync(HttpClient httpClient, string metodo, string urlRequest, string contentRequest, string parametros, bool responseString, bool sendToken)
        {

            int statusCode = 400;
            object content;
            try
            {
                if (string.IsNullOrEmpty(_credential.NombreUsuario))
                {
                    var authentication = await _authentication.GetAuthenticationStateAsync();
                    if(authentication.User.Identity.IsAuthenticated)
                    {
                        var credential = await _localStorage.GetCredentialAsync("credential");
                        if (credential != null)
                            _credential = credential;
                    }
                }

                HttpRequestMessage httpRequestMessage = new();

                httpRequestMessage.Headers.Add("X-Forwarded-For", _credential.RemoteIpAddress);

                httpRequestMessage.Method = new HttpMethod(metodo);
                httpRequestMessage.RequestUri = new Uri($"{httpClient.BaseAddress}{urlRequest}{parametros}");

                if(contentRequest != null)
                    httpRequestMessage.Content = new StringContent(contentRequest, Encoding.UTF8, "application/json");

                if(sendToken)
                    httpRequestMessage.Headers.Add("Authorization", $"Bearer {_credential.Token}");

                var response = await httpClient.SendAsync(httpRequestMessage);
                if (response.StatusCode.ToString() == "OK" || response.StatusCode.ToString() == "Created")
                {
                    statusCode = 200;
                    if(responseString)
                        content = await response.Content.ReadAsStringAsync();
                    else
                        content = await response.Content.ReadAsByteArrayAsync();
                }
                else if (response.IsSuccessStatusCode)
                {
                    statusCode = 204;
                    content = response.IsSuccessStatusCode.ToString();
                } else 
                {
                    string reason = await response.Content.ReadAsStringAsync();
                    if(string.IsNullOrEmpty(reason))
                        reason = response.ReasonPhrase;
                    else
                    {
                        var mensaje = JsonConvert.DeserializeObject<MensajeBadRequest>(reason);
                        if(string.IsNullOrEmpty(mensaje.Mensaje))
                            reason = response.ReasonPhrase;
                        else
                            reason = mensaje.Mensaje;
                    }
                    string[] phrase = reason.Split('|');
                    if (phrase.Length > 1)
                        content = phrase[^1];
                    else
                        content = reason;
                }
                return (statusCode, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (statusCode, "No se puede establecer una conexión con el servicio de datos");
            }

        }

    }
}
