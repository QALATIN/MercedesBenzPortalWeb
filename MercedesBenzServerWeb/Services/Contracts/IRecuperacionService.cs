using MercedesBenzModel;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    interface IRecuperacionService
    {
        Task<(string, ValidacionTokenRecuperacion)> ValidarTokenRecuperacionAsync(TokenRequest request);
        Task<(string, bool)> ResetPasswordAsync(ResetPassword request);
        Task<(string, bool)> SendCorreoAsync(EmailRequest request);
    }
}
