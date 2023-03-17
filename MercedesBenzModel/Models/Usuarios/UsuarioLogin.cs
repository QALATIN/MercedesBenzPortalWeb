using System.ComponentModel.DataAnnotations;

namespace MercedesBenzModel
{
    public class UsuarioLogin
    {
        [Required(ErrorMessage = "El Nombre de Usuario es obligatorio")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatorio")]
        [StringLength(15, ErrorMessage = "La contraseña debe tener una longitud entre {2} y {1} caracteres.", MinimumLength = 8)] // 0 Campo, 1 Maximo 2 Minimo
        //[RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[_!@#$&*.]).{8,15}$", ErrorMessage = "La contraseña debe tener una longitud entre 8 y 15 caracteres, al menos 1 letra mayúscula, 1 letra minúscula, 1 número y un caracter especial")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
