using MercedesBenzModel;
using MercedesBenzServerWeb.Helpers;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    public class ValidacionesExternasService : IValidacionesExternasService
    {
        private readonly HttpClient _httpClient;
        private readonly RepositoryService _repositoryService;

        public ValidacionesExternasService(HttpClient httpClient, RepositoryService repositoryService)
        {
            _httpClient = httpClient;
            _repositoryService = repositoryService;
        }

        public async Task<(string, CurpResponse, ValidationResult)> GetValidacionCurpAsync(string objectSerialize)
        {
            string mensaje = "";
            CurpResponse response = null;
            ValidationResult validationResult = null;
            string urlRequest = _httpClient.BaseAddress.Host == "localhost" ? "" : "validaciones/";

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", $"{urlRequest}ValidarCurp", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<CurpResponse>((string)content);
            else
            {
                try
                {
                    validationResult = JsonConvert.DeserializeObject<ValidationResult>((string)content);
                    validationResult.TipoValidacion = TipoSemaforo.Curp;
                    mensaje = "Error al actualizar la base de datos con el resultado de la validación.";
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                    mensaje = (string)content;
                }
            }

            return (mensaje, response, validationResult);
        }

        public async Task<(string, CorreoResponse, ValidationResult)> GetValidacionCorreoAsync(string objectSerialize)
        {
            string mensaje = "";
            CorreoResponse response = null;
            ValidationResult validationResult = null;
            string urlRequest = _httpClient.BaseAddress.Host == "localhost" ? "" : "validaciones/";

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", $"{urlRequest}ValidarCorreo", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<CorreoResponse>((string)content);
            else
            {
                try
                {
                    validationResult = JsonConvert.DeserializeObject<ValidationResult>((string)content);
                    validationResult.TipoValidacion = TipoSemaforo.Correo;
                    mensaje = "Error al actualizar la base de datos con el resultado de la validación.";
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                    mensaje = (string)content;
                }
            }

            return (mensaje, response, validationResult);
        }

        public async Task<(string, TelefonoResponse, ValidationResult)> GetValidacionTelefonoAsync(string objectSerialize)
        {
            string mensaje = "";
            TelefonoResponse response = null;
            ValidationResult validationResult = null;
            string urlRequest = _httpClient.BaseAddress.Host == "localhost" ? "" : "validaciones/";

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", $"{urlRequest}ValidarTelefono", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<TelefonoResponse>((string)content);
            else
            {
                try
                {
                    validationResult = JsonConvert.DeserializeObject<ValidationResult>((string)content);
                    validationResult.TipoValidacion = TipoSemaforo.Telefono;
                    mensaje = "Error al actualizar la base de datos con el resultado de la validación.";
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                    mensaje = (string)content;
                }
            }

            return (mensaje, response, validationResult);
        }

        public async Task<(string, ListaResponse, ValidationResult)> GetValidacionListasAsync(string objectSerialize)
        {
            string mensaje = "";
            ListaResponse response = null;
            ValidationResult validationResult = null;
            string urlRequest = _httpClient.BaseAddress.Host == "localhost" ? "" : "validaciones/";

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", $"{urlRequest}ValidarListaInteres", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<ListaResponse>((string)content);
            else
            {
                try
                {
                    validationResult = JsonConvert.DeserializeObject<ValidationResult>((string)content);
                    validationResult.TipoValidacion = TipoSemaforo.ListasInteres;
                    mensaje = "Error al actualizar la base de datos con el resultado de la validación.";
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                    mensaje = (string)content;
                }
            }

            return (mensaje, response, validationResult);
        }

        public async Task<(string, IneResponse, ValidationResult)> GetValidacionIneAsync(string objectSerialize)
        {
            string mensaje = "";
            IneResponse response = null;
            ValidationResult validationResult = null;
            string urlRequest = _httpClient.BaseAddress.Host == "localhost" ? "" : "validaciones/";

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", $"{urlRequest}ValidarIdentificacion", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<IneResponse>((string)content);
            else
            {
                try
                {
                    validationResult = JsonConvert.DeserializeObject<ValidationResult>((string)content);
                    validationResult.TipoValidacion = TipoSemaforo.Ine;
                    mensaje = "Error al actualizar la base de datos con el resultado de la validación.";
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                    mensaje = (string)content;
                }
            }

            return (mensaje, response, validationResult);
        }

        public async Task<(string, ComprobanteResponse, ValidationResult)> GetValidacionComprobanteAsync(string objectSerialize)
        {
            string mensaje = "";
            ComprobanteResponse response = null;
            ValidationResult validationResult = null;
            string urlRequest = _httpClient.BaseAddress.Host == "localhost" ? "" : "validaciones/";

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", $"{urlRequest}ValidarDocumentos", objectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<ComprobanteResponse>((string)content);
            else
            {
                try
                {
                    validationResult = JsonConvert.DeserializeObject<ValidationResult>((string)content);
                    mensaje = "Error al actualizar la base de datos con el resultado de la validación.";
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                    mensaje = (string)content;
                }
            }

            return (mensaje, response, validationResult);
        }

        public async Task<(string, GeoreferenciaResponse, ValidationResult)> GetValidacionGeoreferenciaAsync(string ObjectSerialize)
        {
            string mensaje = "";
            GeoreferenciaResponse response = null;
            ValidationResult validationResult = null;
            string urlRequest = _httpClient.BaseAddress.Host == "localhost" ? "" : "validaciones/";

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "POST", $"{urlRequest}ValidarGeoreferencia", ObjectSerialize, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<GeoreferenciaResponse>((string)content);
            else
            {
                mensaje = (string)content;
            }

            return (mensaje, response, validationResult);
        }
    }
}
