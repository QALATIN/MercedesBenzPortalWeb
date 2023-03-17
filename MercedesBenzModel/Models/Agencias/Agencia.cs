using System;
using System.ComponentModel.DataAnnotations;

namespace MercedesBenzModel
{
    public class Agencia
    {
        public int AgenciaId { get; set; } = 0;
        [Required(ErrorMessage = "La clave de la agencia es obligatoria")]
        public string ClaveAgencia { get; set; } = "";
        [Required(ErrorMessage = "El nombre de la agencia es obligatoria")]
        public string NombreAgencia { get; set; } = "";
        [Required(ErrorMessage = "La dirección de la agencia es obligatoria")]
        public string Direccion { get; set; } = "";
        //[Required(ErrorMessage = "El teléfono de la agencia es obligatorio")]
        public string Telefono { get; set; } = "-";
        [Range(1, 10, ErrorMessage = "Debe seleccionar un Tipo de Agencia")]
        public int TipoAgenciaId { get; set; } = 0;
        public string TipoAgenciaNombre { get; set; } = "";
        //[Range(1, 99, ErrorMessage = "Debe seleccionar un Estado")]
        public int EstadoId { get; set; } = 0;
        public string NombreEstado { get; set; } = "";
        public bool Activo { get; set; } = false;
        public DateTime? FechaRegistro { get; set; } = null;
        public DateTime? FechaBaja { get; set; } = null;
        public string Motivo { get; set; } = "";
    }
}
