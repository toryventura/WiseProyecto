using System;
namespace WS.ENTIDADES
{
	public class AlarmaGeocerca
	{
		public int CodAlarma { get; set; }
		public int CodigoGEO { get; set; }
		public bool Inicio { get; set; }
		public string UsuaReg { get; set; }
		public System.DateTime FechaReg { get; set; }
		public string UsuaModif { get; set; }
		public System.DateTime FechaModif { get; set; }
	}
}