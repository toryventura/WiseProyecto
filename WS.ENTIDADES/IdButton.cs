using System;
namespace WS.ENTIDADES
{
	public class IdButton
	{
		public int ID { get; set; }
		public string Keys { get; set; }
		public string Alias { get; set; }
		public System.DateTime FechaReg { get; set; }
		public string UsuaReg { get; set; }
		public System.DateTime FechaMod { get; set; }
		public string UsuaMod { get; set; }
		public bool Estado { get; set; }
	}
}