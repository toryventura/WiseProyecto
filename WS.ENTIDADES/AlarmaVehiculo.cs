namespace WS.ENTIDADES
{
	public class AlarmaVehiculo
	{
		public int CodAlarma { get; set; }
		public string NroPlaca { get; set; }
		public string UsuaReg { get; set; }
		public System.DateTime FechaReg { get; set; }
		public string UsuaModif { get; set; }
		public System.DateTime FechaModif { get; set; }
		public bool EstadoReq { get; set; }
		public int Cantidad { get; set; }
		public long IDReq { get; set; }
	}
}