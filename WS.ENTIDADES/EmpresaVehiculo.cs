using System;
namespace WS.ENTIDADES
{
	public class EmpresaVehiculo
	{
		public int id { get; set; }
		public string nroplaca { get; set; }
		public string nit { get; set; }
		public bool estado { get; set; }
		public string usuarioReg { get; set; }
		public System.DateTime fechaReg { get; set; }
		public string usuarioMod { get; set; }
		public System.DateTime fechaMod { get; set; }
	}
}