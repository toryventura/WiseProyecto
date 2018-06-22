using System;

namespace WISETRACK.Datos.optimizado
{
	[Serializable]
	public class RptAuditoriaViewModel
	{
		public string EstadoPuerta { get; set; }
		public string direcciones { get; set; }
		public Int64 ID { get; set; }
		public string IMEI { get; set; }
		public int EstadoGPS { get; set; }
		public float Velocidad { get; set; }
		public float Asimut { get; set; }
		public float Altitud { get; set; }
		public string Longitud { get; set; }
		public string Latitud { get; set; }
		public string TipoMensaje { get; set; }
		public string TipoRespuesta { get; set; }
		public int EstadoMotor { get; set; }
		public System.DateTime FechaGPS { get; set; }
		public string NroPlaca { get; set; }
		public string IDButton { get; set; }
		public float Kilometraje { get; set; }
		public string Temperatura { get; set; }
		public float VoltajeBateria { get; set; }
		public string Nombre { get; set; }

	}
}