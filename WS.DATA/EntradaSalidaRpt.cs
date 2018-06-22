using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.DATA
{
	[Serializable]
	public class EntradaSalidaRpt
	{
		public int IDReporte { get; set; }
		public string Vehiculo { get; set; }
		public string Conductor { get; set; }
		public string Geocerca { get; set; }
		public DateTime FechaEntrada { get; set; }
		public string Ubicacion { get; set; }
		public double Longitud { get; set; }
		public double Latitud { get; set; }
		public DateTime FechaSalida { get; set; }
		public string Tiempo { get; set; }
	}
}
