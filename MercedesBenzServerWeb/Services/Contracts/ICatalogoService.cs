using MercedesBenzModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    interface ICatalogoService
    {
        Task<(string, IEnumerable<AgenciaTipoSeleccion>)> GetAllTiposAgenciaAsync();
        Task<(string, IEnumerable<Perfil>)> GetAllPerfilAsync();
        Task<(string, IEnumerable<AgenciaSeleccion>)> GetAllAgenciaAsync();

    }
}
