using System;

namespace WISETRACK.Datos.Serializable
{
	public class VehiculoEmpresas
	{
		public string NroPlaca { get; set; }
		public string Patente { get; set; }
		public string NroChasis { get; set; }
		public string NroMotor { get; set; }
		public string Modelo { get; set; }
		public int CodTipoV { get; set; }
		public int CodMarca { get; set; }
		public byte[] Foto { get; set; }
		public bool Estado { get; set; }
		public string UsuaReg { get; set; }
		public DateTime FechaReg { get; set; }
		public string UsuaModif { get; set; }
		public DateTime FechaModif { get; set; }
		public int year { get; set; }
		public string Marca { get; set; }
	}
}