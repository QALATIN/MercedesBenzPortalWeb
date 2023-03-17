using MercedesBenzModel;
using MercedesBenzServerWeb.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    public class CatalogoService : ICatalogoService
    {
        private readonly HttpClient _httpClient;
        private readonly RepositoryService _repositoryService;

        public CatalogoService(HttpClient httpClient, RepositoryService repositoryService)
        {
            _httpClient = httpClient;
            _repositoryService = repositoryService;
        }

        public async Task<(string, IEnumerable<AgenciaSeleccion>)> GetAllAgenciaAsync()
        {
            string mensaje = "";
            IEnumerable<AgenciaSeleccion> response = null;

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "GET", "catalogos/agencias", null, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<IEnumerable<AgenciaSeleccion>>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, IEnumerable<AgenciaTipoSeleccion>)> GetAllTiposAgenciaAsync()
        {
            string mensaje = "";
            IEnumerable<AgenciaTipoSeleccion> response = null;

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "GET", "catalogos/agencias/tipos", null, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<IEnumerable<AgenciaTipoSeleccion>>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

        public async Task<(string, IEnumerable<Perfil>)> GetAllPerfilAsync()
        {
            string mensaje = "";
            IEnumerable<Perfil> response = null;

            (int statusCode, object content) = await _repositoryService.SendAsync(_httpClient, "GET", "catalogos/perfiles", null, null, true, true);

            if (statusCode == 200)
                response = JsonConvert.DeserializeObject<IEnumerable<Perfil>>((string)content);
            else
                mensaje = (string)content;

            return (mensaje, response);
        }

    }
}
