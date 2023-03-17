namespace MercedesBenzModel
{
    public class SolicitudAvisoPrivacidad
    {
        public int AvisoPrivacidadId { get; set; }
        public int SolicitanteId { get; set; }
        public string Referencia { get; set; }
        public byte[] Imagen { get; set; }

    }
}
