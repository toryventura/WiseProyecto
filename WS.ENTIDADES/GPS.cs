using System;
namespace WS.ENTIDADES
{
	public class GPS
	{
		public string IMEI { get; set; }
		public string ID { get; set; }
		public double NroTelefono { get; set; }
		public string Modelo { get; set; }
		public bool Estado { get; set; }
		public string UsuaReg { get; set; }
		public System.DateTime FechaReg { get; set; }
		public string UsuaModif { get; set; }
		public System.DateTime FechaModif { get; set; }
		public int TiempoEspera { get; set; }
		public bool EstadoPuerta { get; set; }
	}
}