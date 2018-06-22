using System;

namespace TestDatos
{
	class Program
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
		static void Main(string[] args)
		{

			//LReportes l = new LReportes();
			//DateTime fini=Convert.ToDateTime( "21/06/2017 00:00");
			//DateTime ffin=Convert.ToDateTime( "26/06/2017 15:00");;
			//List<Auditoria> list = l.getlistAuditoria(fini, ffin, "3829-XIV");
			Posicion p1 = new Posicion(-17.750360, -63.176360);
			Posicion p2 = new Posicion(-17.750470, -63.175680);
			var rs = GetDistanceKm(p1, p2);
			var m = GetDistanceM(p1, p2);
			Console.WriteLine(rs);
			Console.WriteLine(m);


			Console.ReadLine();
		}
	}
}
