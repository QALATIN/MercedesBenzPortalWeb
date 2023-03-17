using MercedesBenzModel;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    interface IAgenciaService
    {
        Task<(string, RespuestaPaginada)> GetAllAsync(PaginacionRequest paginacion);
        Task<(string, Agencia)> GetByIdAsync(int id);
        Task<(string, Agencia)> AddAsync(Agencia agencia);
        Task<(string, Agencia)> UpdateAsync(Agencia agencia);
        Task<(string, bool)> DeleteAsync(int id);
        Task<(string, bool)> UpdateEstatuAsync(AgenciaEstatusRequest request);

    }
}
