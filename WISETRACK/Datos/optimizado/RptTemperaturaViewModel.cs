using System;

namespace WISETRACK.Datos.optimizado
{
	[Serializable]
	public class RptTemperaturaViewModel
	{
		//Agregado el 18/08/2016 15:19 pm
		public string EstadoPuerta { get; set; }
		public string direcciones { get; set; }
		public System.DateTime FechaGPS { get; set; }
		public double Temperatura { get; set; }
		public string NroPlaca { get; set; }
		public string IDButton { get; set; }
		public string Nombre { get; set; }
		public double Velocidad { get; set; }
		public string IMEI { get; set; }
		public string Latitud { get; set; }
		public string Longitud { get; set; }

	}
}