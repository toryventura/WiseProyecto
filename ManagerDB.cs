using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WS.DATA
{
 public 	class ManagerDB
	{
		/// <summary>
		/// -1 significa que nunca se ejecuto
		/// 0 significa que entro que hubo algun error al ejecutarse el procedimientos almacenado
		/// 1 significa que se realizo correctamenta, con exitoso
		/// </summary>
		/// <param name="name">nombre del procedimiento almacenado</param>
		/// <param name="parametros"> parametros que se va insertar</param>
		/// <returns>retorna un entro como respuesta</returns>
		public int ExecuteStoreProcedure(string name, params SqlParameter[] parametros)
		{
			int i = -1;
			using (SqlConnection db = ConexionSingleton.getConectionDB())
			{
				SqlCommand command = new SqlCommand();
				command.Connection = db;
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = name;
				try
				{
					db.Open();
					if (parametros != null)
					{
						foreach (SqlParameter p in parametros)
							command.Parameters.Add(p);
					}
					command.ExecuteReader();
					i = 1;
					db.Close();
				}
				catch (Exception)
				{
					db.Close();
					i = 0;
				}
			}

			return i;
		}
		
		/// <summary>
		/// retorna de la lista de objetos.
		/// </summary>
		/// <typeparam name="T">es el objeto a realizar con que se va ejecutar</typeparam>
		/// <param name="sqlquery">consulta sql que se va ejecuatar </param>
		/// <returns>una lista de obejtos  de tipo objetos T</returns>
	
		public List<T> DataReaderMapToList<T>(string sqlquery)
		{
			using (SqlConnection db = ConexionSingleton.getConectionDB())
			{
				SqlCommand command = new SqlCommand(sqlquery);
				command.Connection = db;
				try
				{
					db.Open();

					SqlDataReader dr = null;
					dr = command.ExecuteReader();
					List<T> list = new List<T>();
					T obj = default(T);

					while (dr.Read())
					{
						obj = Activator.CreateInstance<T>();

						foreach (PropertyInfo prop in obj.GetType().GetProperties())
						{
							if (!object.Equals(dr[prop.Name], DBNull.Value))
							{
								prop.SetValue(obj, dr[prop.Name], null);
							}

						}

						list.Add(obj);
					}
					db.Close();
					return list;
				}
				catch (Exception)
				{
					db.Close();
					return null;
				}

			}
		}
	 /// <summary>
	 /// 
	 /// </summary>
	 /// <typeparam name="T">tipo de objeto</typeparam>
	 /// <param name="name">nombre del procedimiendo almacenado</param>
	 /// <param name="parametros">los parametros  del procedemiento</param>
	 /// <returns>lista que retorn</returns>
		public List<T> DataReaderMapToList<T>(string name, params SqlParameter[] parametros)
		{
			using (SqlConnection db = ConexionSingleton.getConectionDB())
			{
				SqlCommand command = new SqlCommand();
				command.Connection = db;
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = name;
				try
				{
					db.Open();
					if (parametros != null)
					{
						foreach (SqlParameter p in parametros)
							command.Parameters.Add(p);
					}
					SqlDataReader dr = null;
					dr = command.ExecuteReader();
					List<T> list = new List<T>();
					T obj = default(T);

					while (dr.Read())
					{
						obj = Activator.CreateInstance<T>();

						foreach (PropertyInfo prop in obj.GetType().GetProperties())
						{
							if (!object.Equals(dr[prop.Name], DBNull.Value))
							{
								prop.SetValue(obj, dr[prop.Name], null);
							}

						}

						list.Add(obj);
					}
					db.Close();
					return list;
				}
				catch (Exception)
				{
					db.Close();
					return null;
				}

			}
		}
	}
}
