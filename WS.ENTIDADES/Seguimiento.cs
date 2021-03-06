using System;
namespace WS.ENTIDADES
{
	public class Seguimiento
	{
		public int CodSeguimiento { get; set; }
		public System.DateTime FechaInicio { get; set; }
		public System.DateTime FechaFin { get; set; }
		public string NIT { get; set; }
		public string IMEI { get; set; }
		public string NroPlaca { get; set; }
		public string UsuaReg { get; set; }
		public System.DateTime FechaReg { get; set; }
		public string UsuaModif { get; set; }
		public System.DateTime FechaModif { get; set; }
		public bool estado { get; set; }
	}
}