using System;
namespace WS.ENTIDADES
{
	public class Privilegios
	{
		public int CodPrivilegio { get; set; }
		public string Descripcion { get; set; }
		public string DirPagina { get; set; }
		public string UsuaReg { get; set; }
		public System.DateTime FechaReg { get; set; }
		public string UsuaModif { get; set; }
		public System.DateTime FechaModif { get; set; }
	}
}