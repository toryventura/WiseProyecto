using System;
namespace WS.ENTIDADES
{
	public class IDButtonGps
	{
		public int ID { get; set; }
		public string IMEI { get; set; }
		public int CODIDButton { get; set; }
		public bool Estado { get; set; }
		public System.DateTime FechaReg { get; set; }
		public string UsuReg { get; set; }
	}
}