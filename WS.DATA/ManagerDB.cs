using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace WS.DATA
{
	public class ManagerDB
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
				SqlCommand command = new SqlCommand
				{
					Connection = db,
					CommandType = CommandType.StoredProcedure,
					CommandText = name
				};
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


				}
				catch (Exception)
				{

					i = 0;
				}
				finally
				{
					db.Close();
					db.Dispose();
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
				SqlCommand command = new SqlCommand(sqlquery)
				{
					Connection = db
				};
				List<T> list = new List<T>();
				try
				{
					db.Open();

					SqlDataReader dr = null;
					dr = command.ExecuteReader();

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

				}
				catch (Exception)
				{

					list = null;
				}
				finally
				{
					db.Close();
					db.Dispose();
				}
				return list;
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
				SqlCommand command = new SqlCommand
				{
					Connection = db,
					CommandType = CommandType.StoredProcedure,
					CommandText = name
				};
				List<T> list = new List<T>();
				try
				{
					db.Open();
					command.CommandTimeout = 180;

					if (parametros != null)
					{
						foreach (SqlParameter p in parametros)
							command.Parameters.Add(p);
					}
					SqlDataReader dr = null;
					dr = command.ExecuteReader();

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

				}
				catch (Exception ex)
				{
					list = null;
					throw new Exception("Error", ex);
				}
				finally
				{
					db.Close();
					db.Dispose();
				}
				return list;
			}
		}
	}
}
