namespace WS.ENTIDADES
{
	public class Vehiculo
	{
		public string NroPlaca { get; set; }
		public string Patente { get; set; }
		public string NroChasis { get; set; }
		public string NroMotor { get; set; }
		public string Modelo { get; set; }
		public int CodTipoV { get; set; }
		public int CodMarca { get; set; }
		public byte Foto { get; set; }
		public bool Estado { get; set; }
		public string UsuaReg { get; set; }
		public System.DateTime FechaReg { get; set; }
		public string UsuaModif { get; set; }
		public System.DateTime FechaModif { get; set; }
		public int Año { get; set; }
	}
}