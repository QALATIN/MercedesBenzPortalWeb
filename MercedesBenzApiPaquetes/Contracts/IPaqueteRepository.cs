using MercedesBenzModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MercedesBenzApiPaquetes.Contracts
{
    public interface IPaqueteRepository
    {
        public Task<(int solicitanteId, int identificacionId, int capturaIdentificacionId1, int capturaIdentificacionId4, int fotoId1, int fotoId2)> AddSolicitanteAsync(SolicitanteRequest request);
        public Task<int> AddHuellaAsync(HuellaRequest request);
        public Task<int> AddDocumentoAsync(DocumentoRequest request);
        public Task<int> AddAvisoPrivacidadAsync(AvisoPrivacidadRequest request);
        public Task<int> AddListaNegraAsync(ListaNegraRequest request);

        public Task<Resolucion> UpdateResolucionAsync(ResolucionRequest request);

        public Task<SolicitudFicha> GetSolicitudFichaAsync(SolicitudRequest request);
        public Task<SolicitudIdentificacion> GetSolicitudIdentificacionAsync(SolicitudValidacionRequest request);
        public Task<SolicitudComparacionFacial> GetSolicitudComparacionFacialAsync(SolicitudValidacionRequest request);
        public Task<SolicitudComparacionHuellas> GetSolicitudComparacionHuellasAsync(SolicitudValidacionRequest request);
        public Task<SolicitudDocumento> GetSolicitudDocumentoAsync(SolicitudDocumentoRequest request);
        public Task<SolicitudAvisoPrivacidad> GetSolicitudAvisoPrivacidadAsync(SolicitudValidacionRequest request);
        public Task<SolicitudValidacion> GetSolicitudValidacionesAsync(SolicitudValidacionRequest request);
        public Task<(string, string)> GetSolicitudHuellasByIdAsync(int solicitanteId);

        public Task<(int, IEnumerable<SolicitanteEstatus>)> GetSolicitantesEstatusAsync(SolicitudEstatusRequest request, PaginacionRequest paginacion);
        public Task<(int, IEnumerable<SolicitudNotificacion>)> GetNotificacionesAsync(PaginacionRequest paginacion);

        public Task<int> GetUserIdAsync(string userName, bool regresarError);
        public Task<BitacoraRequest> AddBitacoraAsync(BitacoraRequest bitacora, bool regresarError);
        public Task<bool> UpdateValidacionAsync(ValidationResult validation, bool regresarError);
        public void IniciarValidacionDatosExternos(int validacionId, int solicitanteId, bool identificacionIne, int usuarioId);
        public Task IniciarValidacionDatosExternosAsync(int validacionId, int solicitanteId, bool identificacionIne, int usuarioId);
        public Task<ComparacionfacialResponse> ComparacionFacial(ComparacionfacialRequest request);
        public void IniciarValidacionDocumentosExternos(int validacionId, int solicitanteId, int usuarioId, string tipoComprobante);
        public Task IniciarValidacionDocumentosExternosAsync(int validacionId, int solicitanteId, int usuarioId);
    }
}
