using System;
namespace WS.ENTIDADES
{
	public class PuntosGeocerca
	{
		public int Nro { get; set; }
		public int CodigoGEO { get; set; }
		public float Latitud { get; set; }
		public float Longitud { get; set; }
		public string UsuaReg { get; set; }
		public System.DateTime FechaReg { get; set; }
		public string UsuaModif { get; set; }
		public System.DateTime FechaModif { get; set; }
	}
}