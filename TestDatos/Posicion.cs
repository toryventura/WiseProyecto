
namespace TestDatos
{
	public class Posicion
	{
		public Posicion(double latitud, double longitud)
		{
			Latitud = latitud;
			Longitud = longitud;
		}
		public double Latitud { get; set; }
		public double Longitud { get; set; }
	}
}
