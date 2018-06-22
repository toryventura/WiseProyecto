using System;

namespace WS.DATA
{
	[Serializable]
	public class ResultDetenciones
	{
		public string IMEI { get; set; }
		public int EstadoGPS { get; set; }
		public double Velocidad { get; set; }
		public double Latitud { get; set; }
		public double Longitud { get; set; }
		public int EstadoMotor { get; set; }
		public System.DateTime FechaGPS { get; set; }
		public double Temperatura { get; set; }
		public double VoltajeBateria { get; set; }
		public bool EstadoPuerta { get; set; }
		public string direcciones { get; set; }
	}
}
