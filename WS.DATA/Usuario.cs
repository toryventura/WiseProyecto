
using System;
namespace WS.DATA
{
	[Serializable]
	public class Usuario : ICloneable
	{
		public long ID { get; set; }
		public string Nombre { get; set; }
		public string Apellido1 { get; set; }
		public string Apellido2 { get; set; }
		public string Email { get; set; }
		public string Telefono { get; set; }
		public string Login { get; set; }
		public string Contrasena { get; set; }
		public bool Habilitado { get; set; }
		public bool EsSuperAdmin { get; set; }
		public bool CambiarContrasena { get; set; }
		public int IdFase { get; set; }

		public object Clone()
		{
			return Clonadora.Clonar(this);
		}

		public Usuario Clonar()
		{
			return (Usuario)this.Clone();
		}
	}


}
