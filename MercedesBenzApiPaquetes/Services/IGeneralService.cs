using MercedesBenzModel;
using System.Threading.Tasks;

namespace MercedesBenzApiPaquetes.Services
{
    public interface IGeneralService
    {
        Task<(string, bool)> AddBitacoraPostAsync(BitacoraRequest Request);

        Task<(string, bool)> UpdateValidacionPostAsync(ValidationResult Request);
    }
}
