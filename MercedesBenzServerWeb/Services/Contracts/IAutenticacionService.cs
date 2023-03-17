using MercedesBenzModel;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    interface IAutenticacionService
    {
        Task<(string, AutenticacionResponse)> GetValidacionAutenticacionAsync(UsuarioLogin autenticacion);

    }
}
