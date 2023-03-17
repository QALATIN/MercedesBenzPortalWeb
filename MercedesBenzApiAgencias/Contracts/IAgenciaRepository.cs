using MercedesBenzModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MercedesBenzApiAgencias.Contracts
{
    public interface IAgenciaRepository
    {
        public Task<(int, IEnumerable<Agencia>)> GetAllAsync(PaginacionRequest paginacion);
        public Task<Agencia> GetByIdAsync(int agenciaId);
        public Task<Agencia> AddAsync(Agencia agencia);
        public Task<Agencia> UpdateAsync(Agencia agencia, string nombreUsuarioActualizo);
        public Task<bool> DeleteAsync(int agenciaId);
        public Task<bool> UpdateEstatusAsync(AgenciaEstatusRequest estatus);

    }
}
