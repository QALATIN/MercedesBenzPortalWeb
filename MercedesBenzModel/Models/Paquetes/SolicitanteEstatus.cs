using System;

namespace MercedesBenzModel
{
    public class SolicitanteEstatus
    {
        public int SolicitanteId { get; set; } = 0;
        public int SolicitanteIdOtro { get; set; } = 0;
        public string Folio { get; set; }
        public DateTime FechaCaptura { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string SolicitanteNombre { get; set; }
        public string SolicitanteApellidoPaterno { get; set; }
        public string SolicitanteApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; } = null;
        public string TipoCliente { get; set; }
        public string Estatus { get; set; }

        public byte[] Imagen { get; set; }

    }
}
