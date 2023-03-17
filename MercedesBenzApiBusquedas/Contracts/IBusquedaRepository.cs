using MercedesBenzModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MercedesBenzApiBusquedas.Contracts
{
    public interface IBusquedaRepository
    {
        public Task<(int, IEnumerable<BusquedaResultado>)> BusquedaAsync(BusquedaRequest busqueda, PaginacionRequest paginacion);

    }
}
