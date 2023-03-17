using MercedesBenzModel;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Services
{
    interface IPaqueteService
    {
        Task<(string, SolicitudFicha)> GetSolicitudFichaAsync(SolicitudRequest request);
        Task<(string, SolicitudAvisoPrivacidad, string)> GetSolicitudAvisoPrivacidadAsync(SolicitudValidacionRequest request);
        Task<(string, SolicitudIdentificacion, string)> GetSolicitudIdentificacionAsync(SolicitudValidacionRequest request);
        Task<(string, SolicitudComparacionFacial, string)> GetSolicitudComparacionFacialAsync(SolicitudValidacionRequest request);
        Task<(string, SolicitudComparacionHuellas, string)> GetSolicitudComparacionHuellasAsync(SolicitudValidacionRequest request);
        Task<(string, SolicitudDocumento, string)> GetSolicitudDocumentosAsync(SolicitudDocumentoRequest request);
        Task<(string, SolicitudValidacion)> GetSolicitudValidacionesAsync(SolicitudValidacionRequest request);
        Task<(string, int)> UpdateListaNegraAsync(ListaNegraRequest request);
        Task<(string, int)> UpdateResolucionAsync(ResolucionRequest request);
        Task<(string, RespuestaPaginada)> GetSolicitudesEstatusAsync(SolicitudEstatusRequest request, PaginacionRequest paginacion);
        Task<(string, RespuestaPaginada)> GetSolicitudesNotificacionesAsync();
        Task<(string, byte[])> GetSolicitudHuellaAsync(int solicitanteId);

    }
}
