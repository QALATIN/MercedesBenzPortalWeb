using MercedesBenzModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MercedesBenzApiUsuarios.Contracts
{
    public interface IUsuarioRepository
    {
        public Task<(int, IEnumerable<Usuario>)> GetAllAsync(PaginacionRequest paginacion);
        public Task<Usuario> GetByIdAsync(int id);
        public Task<Usuario> AddAsync(Usuario usuario);
        public Task<Usuario> UpdateAsync(Usuario usuario, string nombreUsuarioActualizo);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> UpdateEstatusAsync(UsuarioEstatusRequest usuario);
        public Task<UsuarioCuenta> LoginAsync(AutenticacionRequest usuario);
        public Task<bool> LogOutAsync(string nombreUsuario);
        public Task<UsuarioCuenta> CredencialAsync(string nombreUsuario);

    }
}
