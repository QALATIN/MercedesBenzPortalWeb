
namespace MercedesBenzModel
{
    public class SolicitudHuella
    {
        public int HuellaId { get; set; }
        public int SolicitanteId { get; set; }
        public int DedoIndiceId { get; set; }
        public string DedoIndiceNombre { get; set; }
        public int DedoEstatusId { get; set; }
        public string DedoEstatusNombre { get; set; }
        public string DedoEstatusClave { get; set; }
        public byte[] Imagen { get; set; }
    }
}
