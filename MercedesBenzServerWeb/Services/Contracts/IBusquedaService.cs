using MercedesBenzModel;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    interface IBusquedaService
    {
        Task<(string, RespuestaPaginada)> GetBusquedaAsync(BusquedaRequest busqueda, PaginacionRequest paginacion);
    }
}
