using System;
namespace WS.ENTIDADES
{
	public class CategoriaAlarma
	{
		public int CodCategoria { get; set; }
		public string Descripcion { get; set; }
		public string UsuaReg { get; set; }
		public System.DateTime FechaReg { get; set; }
		public string UsuaModif { get; set; }
		public System.DateTime FechaModif { get; set; }
	}
}