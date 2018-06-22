
using System;
namespace TestDatos
{
	public static class Extensiones
	{
		public const float RadioTierraKm = 6378.0F;
		public static float DistanciaKm(this Posicion posOrigen, Posicion posDestino)
		{ // TODO 

			//var difLatitud = EnRadianes(posDestino.Latitud – posOrigen.Latitud);
			///var difLongitud = EnRadianes(posDestino.Longitud - posOrigen.Longitud);
			return 0;
		}
		static float EnRadianes(this float valor)
		{
			return Convert.ToSingle(Math.PI / 180) * valor;
		}
		static double AlCuadrado(this double valor)
		{
			return Math.Pow(valor, 2);
		}
	}

}
