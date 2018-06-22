using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.DATA
{
	public sealed class ConexionSingleton
	{
		private static string cadena = ConfigurationManager.AppSettings["SqlConexion"];
		private static SqlConnection db = null;
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
		public static SqlConnection getConectionDB()
		{
			if (db == null)
			{
				new ConexionSingleton();
			}
			return db;
		}
	}
}
