using MercedesBenzModel;
using System.Threading.Tasks;

namespace MercedesBenzApiRecuperacion.Contracts
{
    public interface IRecuperacionRepository
    {
        public Task<(EmailDatos, Correo, string)> GetDatosCorreoAsync(string correo);
        public Task<bool> CreateTokenRecuperacionAsync(int usuarioId, string token);
        public Task<bool> UpdatePasswordAsync(ResetPasswordRequest request);
        public Task<(string, ValidacionTokenRecuperacion)> GetValidacionTokenRecuperacionAsync(string token);
        public Task<ApkVersion> GetApkVersionAsync();

    }
}
