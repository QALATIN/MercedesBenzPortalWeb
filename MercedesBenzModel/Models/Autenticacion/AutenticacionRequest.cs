using System.ComponentModel.DataAnnotations;

namespace MercedesBenzModel
{
    public class AutenticacionRequest
    {
        [Required(ErrorMessage = "El Nombre de Usuario es obligatorio")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatorio")]
        public string Password { get; set; }
    }
}
