using MercedesBenzModel;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    interface IValidacionesExternasService
    {
        Task<(string, CurpResponse, ValidationResult)> GetValidacionCurpAsync(string objectSerialize);
        Task<(string, CorreoResponse, ValidationResult)> GetValidacionCorreoAsync(string objectSerialize);
        Task<(string, TelefonoResponse, ValidationResult)> GetValidacionTelefonoAsync(string objectSerialize);
        Task<(string, ListaResponse, ValidationResult)> GetValidacionListasAsync(string objectSerialize);
        Task<(string, IneResponse, ValidationResult)> GetValidacionIneAsync(string objectSerialize);
        Task<(string, ComprobanteResponse, ValidationResult)> GetValidacionComprobanteAsync(string objectSerialize);
        Task<(string, GeoreferenciaResponse, ValidationResult)> GetValidacionGeoreferenciaAsync(string objectSerialize);
    }
}
