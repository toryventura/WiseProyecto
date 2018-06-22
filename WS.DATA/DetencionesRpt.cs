using System;

namespace WS.DATA
{
	[Serializable]
	public class DetencionesRpt
	{
		public Int64 IDReporte { get; set; }
		public string Vehiculo { get; set; }
		public string Conductor { get; set; }
		public string Geocerca { get; set; }
		public string FechaInicio { get; set; }
		public string Ubicacion { get; set; }
		public string Longitud { get; set; }
		public string Latitud { get; set; }
		public string FechaFin { get; set; }
		public int Tiempo { get; set; }
	}
}
