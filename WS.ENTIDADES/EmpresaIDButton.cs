using System;
namespace WS.ENTIDADES
{
	public class EmpresaIDButton
	{
		public int ID { get; set; }
		public int CodButton { get; set; }
		public string Nit { get; set; }
		public System.DateTime FechaReg { get; set; }
		public string UsuarioReg { get; set; }
		public System.DateTime FechaMod { get; set; }
		public string UsuarioMod { get; set; }
		public bool Estado { get; set; }
	}
}