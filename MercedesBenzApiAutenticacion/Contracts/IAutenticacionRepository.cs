using MercedesBenzModel;
using System.Threading.Tasks;

namespace MercedesBenzApiAutenticacion.Contracts
{
    public interface IAutenticacionRepository
    {
        public Task<UsuarioResultado> AuthenticationAsync(AutenticacionRequest User);
        public Task<bool> CreateTokenUsuarioAsync(UsuarioAutenticado Usuario);

    }
}
