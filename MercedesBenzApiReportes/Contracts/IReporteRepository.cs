using MercedesBenzModel;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MercedesBenzApiReportes.Contracts
{
    public interface IReporteRepository
    {
        public Task<(int, IEnumerable<ReporteSemaforo>)> SemaforosAsync(FechaRequest fechas, PaginacionRequest paginacion);
        public Task<DataTable> SemaforosDescargarAsync(FechaRequest fechas);
        public Task<(int, IEnumerable<ReporteBitacora>)> BitacoraAsync(FechaRequest fechas, PaginacionRequest paginacion);
        public Task<DataTable> BitacoraDescargarAsync(FechaRequest fechas);
        public Task<(int, IEnumerable<ReporteListaNegra>)> ListaNegraAsync(FechaRequest fechas, PaginacionRequest paginacion);
        public Task<DataTable> ListaNegraDescargarAsync(FechaRequest fechas);
        public Task<(int, IEnumerable<ReporteSemaforoFacialDetalleResponse>)> SemaforoFacialDetalleAsync(FechaRequest fechas, PaginacionRequest paginacion);
        public Task<DataTable> SemaforoFacialDetalleDescargarAsync(FechaRequest fechas);
        public Task<(int, IEnumerable<ReporteDetalleEnvioResponse>)> DetalleEnvioAsync(FechaRequest fechas, PaginacionRequest paginacion);
        public Task<DataTable> DetalleEnvioDescargarAsync(FechaRequest fechas);
    }
}
