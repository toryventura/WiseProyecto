using System;
namespace WS.ENTIDADES
{
	public class EmpresaGPS
	{
		public int id { get; set; }
		public string imei { get; set; }
		public string nit { get; set; }
		public bool estado { get; set; }
		public string usuarioReg { get; set; }
		public System.DateTime fechaReg { get; set; }
		public string usuarioMod { get; set; }
		public System.DateTime fechaMod { get; set; }
	}
}