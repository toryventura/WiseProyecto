using System;

namespace WISETRACK.Datos.Serializable
{
	[Serializable]
	public class geocercaSerial
	{
		public string Descripcion { get; set; }
		public string ColorLimite { get; set; }

		public string ColorRelleno { get; set; }
		public int CodTipoGEO { get; set; }

	}
}