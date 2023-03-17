
namespace MercedesBenzModel
{
    public class SolicitanteSolidario
    {
        public int SolicitanteId { get; set; } = 0;
        public int SolicitanteIdOrigen { get; set; } = 0;
        public string Folio { get; set; }
        public string TipoCliente { get; set; }
        public string SolicitanteNombre { get; set; }
        public string SolicitanteApellidoPaterno { get; set; }
        public string SolicitanteApellidoMaterno { get; set; }
    }
}
