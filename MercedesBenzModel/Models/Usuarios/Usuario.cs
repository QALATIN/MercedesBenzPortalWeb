using System;
using System.ComponentModel.DataAnnotations;

namespace MercedesBenzModel
{
    public class Usuario 
    {
		public int UsuarioId { get; set; }

		[Required(ErrorMessage = "El Nombre es obligatorio")]
		public string Nombre { get; set; }
		[Required(ErrorMessage = "El Apellido Paterno es obligatorio")]
		public string ApellidoPaterno { get; set; }
		[Required(ErrorMessage = "El Apellido Materno es obligatorio")]
		public string ApellidoMaterno { get; set; }
		[Required(ErrorMessage = "La Fecha de Nacimiento es obligatorio")]
		public DateTime FechaNacimiento { get; set; }
		[Required(ErrorMessage = "El Usuario es obligatorio")]
		public string NombreUsuario { get; set; }
		[Required(ErrorMessage = "El Correo electrónico es obligatorio")]
		[RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Debe ingresar un Correo electrónico válido")]
		public string CorreoElectronico { get; set; }

		[Range(1, 99, ErrorMessage = "Debe seleccionar un Perfil")]
		public int PerfilId { get; set; }
		public string NombrePerfil { get; set; } = "";
		[Range(1, 32000, ErrorMessage = "Debe seleccionar una Agencia")]
		public int AgenciaId { get; set; }
		public string NombreAgencia { get; set; } = "";
		public bool ActivarCuenta { get; set; } = true;
		public bool Activo { get; set; } = true;
		public DateTime? FechaRegistro { get; set; } = null;
		public DateTime? FechaBaja { get; set; } = null;
		public string NombreCompleto { get; set; }

		public string Motivo { get; set; }

	}
}
