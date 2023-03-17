using MercedesBenzLibrary;
using MercedesBenzModel;
using MercedesBenzServerWeb.Helpers;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _httpClient;
        private readonly RepositoryService _repositoryService;

        public UsuarioService(HttpClient httpClient, RepositoryService repositoryService)
        {
            _httpClient = httpClient;
            _repositoryService = repositoryService;
        }

        public async Task<(string, RespuestaPaginada)> GetAllAsync(PaginacionRequest paginacion)
        {
            string mensaje = "";
            RespuestaPaginada response = null;
            string parametros = $"?Paginacion.Pagina={paginacion.Pagina}&Paginacion.RegistrosPagina={paginacion.RegistrosPagina}";
            //string paginacionBase64 = CodificacionBase64.CodificarObjeto(Paginacion);
            //string parametros = $"?Paginas={paginacionBase64}";

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "GET", "usuarios", null, parametros, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<RespuestaPaginada>((string)content);
                //respuesta = EncryptionAes.DecryptObject(responseBody, new RespuestaPaginada());
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, Usuario)> GetByIdAsync(int id)
        {
            string mensaje = "";
            string urlRequest = $"usuarios/{id}";
            Usuario response = null;

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "GET", urlRequest, null, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<Usuario>((string)content);
            else
                mensaje = (string)content;
            
            return (mensaje, response);
        }

        public async Task<(string, Usuario)> AddAsync(Usuario usuario)
        {
            string mensaje = "";
            Usuario response = null;
            string objectSerialize = JsonConvert.SerializeObject(usuario);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "usuarios", objectSerialize, null, true, true);

            if (statusCode == 200) 
                response = JsonConvert.DeserializeObject<Usuario>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, Usuario)> UpdateAsync(Usuario usuario)
        {
            string mensaje = "";
            Usuario response = null;
            string objectSerialize = JsonConvert.SerializeObject(usuario);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "PUT", "usuarios", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<Usuario>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, bool)> DeleteAsync(int id)
        {
            string mensaje = "";
            string urlRequest = $"usuarios/{id}";
            bool response = false;

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "DELETE", urlRequest, null, null, true, true);

            if (statusCode == 204)
                response = true;
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, bool)> UpdateEstatuAsync(UsuarioEstatusRequest estatus)
        {
            string mensaje = "";
            bool response = false;
            string objectSerialize = JsonConvert.SerializeObject(estatus);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", "usuarios/cambiarEstatus", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = true;
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<string> LoginAsync(AutenticacionRequest userLogin)
        {
            string respuesta = "";
            try
            {
                AutenticacionRequest newUserLogin = new() { NombreUsuario = userLogin.NombreUsuario, Password = userLogin.Password };
                //userEncrypt.Password = EncryptionAes.EncryptString(UserLogin.Password);
                newUserLogin.Password = CodificacionBase64.CodificarTexto(newUserLogin.Password);
                string userSerialize = JsonConvert.SerializeObject(newUserLogin);

                //string dataEncrypt = EncryptionAes.EncryptObject(user);

                HttpRequestMessage httpRequestMessage = new();
                httpRequestMessage.Method = new HttpMethod("POST");
                httpRequestMessage.RequestUri = new Uri($"{_httpClient.BaseAddress}usuarios/login");

                httpRequestMessage.Content = new StringContent(userSerialize, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(httpRequestMessage);
                if (response.StatusCode.ToString() == "OK")
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    var usuario = JsonConvert.DeserializeObject<UsuarioCuenta>(responseBody); // Respuesta sin cifrar
                    //var usuarioSession = JsonConvert.DeserializeObject<UserVM>(respuestaGenerica.Data.ToString());

                    //userVM = JsonConvert.DeserializeObject<UserVM>(responseBody);
                    //string decryptData = EncryptionAes.DecryptObject(responseBody, new UserVM());

                    //userVM = JsonConvert.DeserializeObject<UserVM>(decryptData);

                    //var customAuthentication = (CustomAuthentication)AuthenticationStateProvider;
                    //await customAuthentication.UpdateAuthenticationStateAsync(new UserSession
                    //{
                    //    UserName = userVM.NombreUsuario,
                    //    Rol = userVM.NombrePerfil
                    //});

                    //((NewAuthenticaticationStateProvider)AuthenticationStateProvider).UserLoggedOut();
                }
                else
                {
                    respuesta = "Usuario o Password incorrecto";
                }
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
                //respuesta = "No se puede establecer una conexión con el equipo de destino.";
            }
            return respuesta;
        }

        public async Task<(string, UsuarioCuenta)> GetCredencialAsync()
        {
            string mensaje = "";
            UsuarioCuenta response = null;

            //AutenticacionRequest autenticacionEncrypt = new() { NombreUsuario = _credencial.NombreUsuario, Password = null };
            //string objectSerialize = JsonConvert.SerializeObject(autenticacionEncrypt);
            //string autenticacionEncrypt = EncryptionAes.EncryptObject(autenticacionEncrypt);

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "GET", "usuarios/credencial", null, null, true, true);
            
            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<UsuarioCuenta>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<bool> LogOutAsync(string NombreUsuario)
        {

            //string mensaje = "";
            bool resultado = false;

            AutenticacionRequest autenticacionEncrypt = new() { NombreUsuario = NombreUsuario, Password = null };
            string autenticacionSerialize = JsonConvert.SerializeObject(autenticacionEncrypt);
            //string autenticacionEncrypt = EncryptionAes.EncryptObject(autenticacionEncrypt);

            (int statusCode, _) = await _repositoryService.SendAsync(_httpClient,"POST", "usuarios/LogOut", autenticacionSerialize, null, true, true);

            if (statusCode == 200)
                resultado = true;
            //else
            //    mensaje = "No se puede establecer una conexión con el servicio para finalizar la sessión";

            return resultado;

        }

    }
}
