using System;
namespace WS.ENTIDADES
{
	public class Alerta2
	{
		public long ID { get; set; }
		public string NroPlaca { get; set; }
		public int CodAlarma { get; set; }
		public System.DateTime FechaInicio { get; set; }
		public long IDFin { get; set; }
		public System.DateTime FechaFin { get; set; }
		public int EstadoEnvio { get; set; }
		public int CantidadEmail { get; set; }
	}
}