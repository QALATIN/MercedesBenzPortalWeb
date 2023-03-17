using MercedesBenzModel;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    interface IReporteService
    {
        Task<(string, RespuestaPaginada)> GetReporteAsync(string urlRequest, FechaRequest fechas, PaginacionRequest paginacion);
        Task<(string, byte[])> DownloadReporteAsync(string urlRequest, FechaRequest fechas);
    }
}
