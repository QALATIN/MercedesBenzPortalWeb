
namespace MercedesBenzModel
{
    public class SolicitudValidacionRequest
    {
        public int SolicitanteId { get; set; }
        public int ValidacionId { get; set; }
        public bool IdentificacionIne { get; set; }
        public string Validar { get; set; }
    }
}
