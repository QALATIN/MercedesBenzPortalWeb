using MercedesBenzModel;
using System.Threading.Tasks;

namespace MercedesBenzApiPaquetes.Services
{
    public interface IValidacionService
    {
        Task<(string, CurpResponse, ValidationResult)> ValidarCurpAsync(string objectSerialize);
        Task<(string, CorreoResponse, ValidationResult)> ValidarCorreoAsync(string objectSerialize);
        Task<(string, TelefonoResponse, ValidationResult)> ValidarTelefonoAsync(string objectSerialize);
        Task<(string, ListaResponse, ValidationResult)> ValidarListasAsync(string objectSerialize);
        Task<(string, IneResponse, ValidationResult)> ValidarIneAsync(string objectSerialize);
        Task<(string, ComprobanteResponse, ValidationResult)> ValidarComprobanteAsync(string objectSerialize);

    }
}
