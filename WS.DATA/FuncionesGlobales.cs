using System;
using System.Collections.Generic;

namespace WS.DATA
{
	public class FuncionesGlobales
	{
		public const double EarthRadius = 6371;
		public static double GetDistanceKm(Posicion point1, Posicion point2)
		{
			double distance = 0;
			double Lat = (point2.Latitud - point1.Latitud) * (Math.PI / 180);
			double Lon = (point2.Longitud - point1.Longitud) * (Math.PI / 180);

			double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(point1.Latitud * (Math.PI / 180)) * Math.Cos(point2.Latitud * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
			double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
			distance = EarthRadius * c;
			return distance;


		}
		public static double GetDistanceM(Posicion point1, Posicion point2)
		{
			double distance = 0;
			double Lat = (point2.Latitud - point1.Latitud) * (Math.PI / 180);
			double Lon = (point2.Longitud - point1.Longitud) * (Math.PI / 180);

			double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(point1.Latitud * (Math.PI / 180)) * Math.Cos(point2.Latitud * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
			double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
			distance = EarthRadius * c;
			return distance * 100;

		}
		public static List<KeyValuePair<int, string>> getdatosFechas()
		{
			List<KeyValuePair<int, string>> datos = new List<KeyValuePair<int, string>>();
			for (int i = 0 ; i < 24 ; i++)
			{
				string str = i < 10 ? "0" + i + ":00" : i + ":00";
				datos.Add(new KeyValuePair<int, string>(i, str));
			}
			return datos;
		}
		public static List<KeyValuePair<int, string>> getCondicional()
		{
			List<KeyValuePair<int, string>> datos = new List<KeyValuePair<int, string>>()

				{
					new KeyValuePair<int, string> (0, "Igual A"),
					new KeyValuePair<int, string> (1, "Menor A"),
					new KeyValuePair<int, string> (2, "Mayor A"),
					new KeyValuePair<int, string> (3, "Mayor o Igual A"),
					new KeyValuePair<int, string> (4, "Menor o Igual A"),
					
				};
			return datos;
		}
	}
}
