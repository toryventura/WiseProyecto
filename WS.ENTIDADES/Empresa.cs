using System;
namespace WS.ENTIDADES
{
	public class Empresa
	{
		public string NIT { get; set; }
		public string RazonSocial { get; set; }
		public string email { get; set; }
		public string Contacto { get; set; }
		public string emailContacto { get; set; }
		public string UsuaReg { get; set; }
		public System.DateTime FechaReg { get; set; }
		public string UsuaModif { get; set; }
		public System.DateTime FechaModif { get; set; }
	}
}