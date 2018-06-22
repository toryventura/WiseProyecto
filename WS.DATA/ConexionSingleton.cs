using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WS.DATA
{
	public sealed class ConexionSingleton
	{
		private static string cadena = ConfigurationManager.AppSettings["SqlConexion"];
		private static SqlConnection db = null;
		//private bool disposed = false; // to detect redundant calls

		public ConexionSingleton()
		{
			try
			{
				//CadenaConexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\Totai Pesaje\oldSoftInBal.mdb;Persist Security Info=False;";
				db = new SqlConnection(cadena);


			}
			catch
			{
			}
		}
		//protected virtual void Dispose(bool disposing)
		//{
		//	if (!disposed)
		//	{
		//		if (disposing)
		//		{
		//			if (db != null)
		//			{
		//				db.Dispose();
		//			}
		//		}

		//		// shared cleanup logic
		//		disposed = true;
		//	}
		//}


		//public void Dispose()
		//{
		//	Dispose(true);
		//	GC.SuppressFinalize(this);
		//}

		public static SqlConnection getConectionDB()
		{
			if (db == null)
			{
				new ConexionSingleton();
			}
			else
			{
				if (db != null && !isConnected)
				{
					db.ConnectionString = cadena;
				}
			}
			return db;
		}
		public static bool isConnected
		{
			get { return db.State == ConnectionState.Open; }
		}

	}
}
