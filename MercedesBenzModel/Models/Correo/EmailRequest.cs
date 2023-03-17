
using System.ComponentModel.DataAnnotations;

namespace MercedesBenzModel
{
    public class EmailRequest
    {
        [Required(ErrorMessage = "El Correo electrónico es obligatorio")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Debe ingresar un Correo electrónico válido")]
        public string CorreoElectronico { get; set; }
    }
}
