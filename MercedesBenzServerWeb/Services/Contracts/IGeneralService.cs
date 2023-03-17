using MercedesBenzModel;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    interface IGeneralService
    {
        Task<(string, bool)> AddBitacoraAsync(BitacoraRequest request);

        Task<(string, bool)> UpdateValidacionAsync(ValidationResult request);
    }
}
