using MercedesBenzModel;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    interface IUsuarioService
    {
        Task<(string, RespuestaPaginada)> GetAllAsync(PaginacionRequest paginacion);
        Task<(string, Usuario)> GetByIdAsync(int id);
        Task<(string, Usuario)> AddAsync(Usuario usuario);
        Task<(string, Usuario)> UpdateAsync(Usuario usuario);
        Task<(string, bool)> DeleteAsync(int id);
        Task<(string, bool)> UpdateEstatuAsync(UsuarioEstatusRequest estatus);
        Task<string> LoginAsync(AutenticacionRequest userLogin);
        Task<(string, UsuarioCuenta)> GetCredencialAsync();
        Task<bool> LogOutAsync(string nombreUsuario);
    }
}
