using System;
namespace WS.ENTIDADES
{
	public class Alerta
	{
		public long ID { get; set; }
		public int CodSeguimiento { get; set; }
		public string NroPlaca { get; set; }
		public int CodAlarma { get; set; }
		public int Estado { get; set; }
		public System.DateTime FechaHora { get; set; }
		public bool Visto { get; set; }
		public System.DateTime FechaReg { get; set; }
		public long IDTrama { get; set; }
		public int CodigoGEO { get; set; }
		public int Ultimo { get; set; }
		public bool Procesado { get; set; }
	}
}