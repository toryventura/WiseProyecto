using System;

namespace WS.DATA
{
	public class ReporteTempuratura
	{



		////////////////
		public bool EstadoPuerta { get; set; }
		public string direcciones { get; set; }
		public DateTime FechaGPS { get; set; }
		public double Temperatura { get; set; }
		public string NroPlaca { get; set; }
		public string IDButton { get; set; }
		public string Nombre { get; set; }
		public double Velocidad { get; set; }
		public string IMEI { get; set; }
		public string Latitud { get; set; }
		public string Longitud { get; set; }
		public string TipoMensaje { get; set; }
	}
}
