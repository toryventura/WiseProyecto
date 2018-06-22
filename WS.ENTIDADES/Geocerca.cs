using System;
namespace WS.ENTIDADES
{
	public class Geocerca
	{
		public int CodigoGEO { get; set; }
		public string Descripcion { get; set; }
		public string ColorLimite { get; set; }
		public string ColorRelleno { get; set; }
		public int CodTipoGEO { get; set; }
		public string UsuaReg { get; set; }
		public System.DateTime FechaReg { get; set; }
		public string UsuaModif { get; set; }
		public System.DateTime FechaModif { get; set; }
		public string NIT { get; set; }
	}
}