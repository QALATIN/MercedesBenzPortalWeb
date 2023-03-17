using System;
using System.ComponentModel.DataAnnotations;

namespace MercedesBenzModel
{
    public class BitacoraRequest
    {
        public int BitacoraId { get; set; }
        public int OrigenId { get; set; }
        public int TipoLogId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaRegistro { get; set; }
        [Required]
        public string Mensaje { get; set; }
        public string Referencia { get; set; }

    }
}
