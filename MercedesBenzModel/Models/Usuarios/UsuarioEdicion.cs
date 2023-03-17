using System;
using System.ComponentModel.DataAnnotations;

namespace MercedesBenzModel
{
    public class UsuarioEdicion
    {
		public int UsuarioId { get; set; }
		[Required(ErrorMessage = "El Nombre de Usuario es obligatorio")]
		public string NombreUsuario { get; set; }
		[Required(ErrorMessage = "El Nombre es obligatorio")]
		public string Nombre { get; set; }
		[Required(ErrorMessage = "El Apellido Paterno es obligatorio")]
		public string ApellidoPaterno { get; set; }
		[Required(ErrorMessage = "El Apellido Materno es obligatorio")]
		public string ApellidoMaterno { get; set; }
		[Required(ErrorMessage = "La Fecha de Nacimiento es obligatorio")]
		public DateTime FechaNacimiento { get; set; }
		[Required(ErrorMessage = "El Correo electrónico es obligatorio")]
		[RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Debe ingresar un Correo electrónico válido")]
		public string CorreoElectronico { get; set; }

		[Range(1, 99, ErrorMessage = "Debe seleccionar un Perfil")]
		public int PerfilId { get; set; }
		public string NombrePerfil { get; set; } = "";
		[Range(1, 32000, ErrorMessage = "Debe seleccionar una Agencia")]
		public int AgenciaId { get; set; }
		public string NombreAgencia { get; set; } = "";
		public DateTime? FechaRegistro { get; set; } = null;
		public bool Activo { get; set; } = true; 
		public string Motivo { get; set; } = "";

	}
}
