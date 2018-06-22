
namespace WS.DATA
{
	public class Posicion
	{
		public Posicion(double latitud, double longitud)
		{
			Latitud = latitud;
			Longitud = longitud;
		}
		public Posicion()
		{
			Latitud = 0;
			Longitud = 0;
		}
		public double Latitud { get; set; }
		public double Longitud { get; set; }
	}
}
