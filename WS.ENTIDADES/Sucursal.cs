using System;
namespace WS.ENTIDADES
{
	public class Sucursal
	{
		public string NIT { get; set; }
		public int Nro { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
		public double Latitud { get; set; }
		public double Longitud { get; set; }
		public string UsuaReg { get; set; }
		public System.DateTime FechaReg { get; set; }
		public string UsuaModif { get; set; }
		public System.DateTime FechaModif { get; set; }
	}
}