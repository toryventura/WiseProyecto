using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.DATA
{
	public class Auditoria
	{

		public string EstadoPuerta { get; set; }
		public string direcciones { get; set; }
		public long ID { get; set; }
		public string IMEI { get; set; }
		public int EstadoGPS { get; set; }
		public double Velocidad { get; set; }
		public double Asimut { get; set; }
		public string Longitud { get; set; }
		public string Latitud { get; set; }
		public double Altitud { get; set; }
		public string TipoMensaje { get; set; }
		public string TipoRespuesta { get; set; }
		public int EstadoMotor { get; set; }
		public DateTime FechaGPS { get; set; }
		public string NroPlaca { get; set; }
		public string IDButton { get; set; }
		public double Kilometraje { get; set; }
		public double Temperatura { get; set; }
		public double VoltajeBateria { get; set; }
		public string vinculo { get; set; }
		public string Patente { get; set; }
	}
}
