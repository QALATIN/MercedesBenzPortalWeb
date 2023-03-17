using MercedesBenzModel;
using System.Threading.Tasks;

namespace MercedesBenzApiServicios.Services.Contracts
{
    public interface IGeneralRepository
    {
        public Task<BitacoraRequest> AddBitacoraPostAsync(BitacoraRequest Bitacora);
        public Task<bool> UpdateValidacionPostAsync(ValidationResult validation);
        public bool SubirArchivoBitacora(BitacoraArchivoRequest bitacora, string Repositorio);
    }
}
