namespace WS.ENTIDADES
{
	public class UsuarioEmpresa
	{
		public string NIT { get; set; }
		public string CI { get; set; }
		public string UsuaReg { get; set; }
		public System.DateTime FechaReg { get; set; }
		public string UsuaModif { get; set; }
		public System.DateTime FechaModif { get; set; }
		public bool Activo { get; set; }
		public bool Estado { get; set; }
	}
}