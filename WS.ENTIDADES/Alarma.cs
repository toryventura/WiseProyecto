
using System;
namespace WS.ENTIDADES
{
	public class Alarma
	{
		public int CodAlarma { get; set; }
		public bool email { get; set; }
		public bool sms { get; set; }
		public string NombreAlarma { get; set; }
		public int CodTipoAlarma { get; set; }
		public string UsuaReg { get; set; }
		public DateTime FechaReg { get; set; }
		public string UsuaModif { get; set; }
		public DateTime FechaModif { get; set; }
		public int CodCategoria { get; set; }
		public bool Activa { get; set; }
		public int CantidadEnvio { get; set; }
		public int TiempoEnvio { get; set; }
		public int IntervaloEnvio { get; set; }
		public string NIT { get; set; }
		public float Velocidad { get; set; }
		public int Tiempo { get; set; }
		public float Distancia { get; set; }
		public int Temperatura { get; set; }
		public float Velocidad2 { get; set; }
		public int Tiempo2 { get; set; }
		public float Distancia2 { get; set; }
		public int Temperatura2 { get; set; }
		public DateTime FechaHora { get; set; }
		public DateTime FechaHora2 { get; set; }
	}
}