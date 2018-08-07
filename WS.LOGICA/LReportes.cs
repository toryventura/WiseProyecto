
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WS.DATA;





namespace WS.LOGICA
{
	public class LReportes
	{
		public List<Auditoria> getlistAuditoria(DateTime fini, DateTime ffin, String nroplaca)
		{
			List<Auditoria> lis = new List<Auditoria>();
			try
			{
				ManagerDB db = new ManagerDB();

				SqlParameter[] par = { new SqlParameter("@fini", fini), new SqlParameter("@ffin", ffin), new SqlParameter("@placa", nroplaca) };
				lis = db.DataReaderMapToList<Auditoria>("sp_obtenerAuditoriaOptimizado1", par);

			}
			catch (Exception ex)
			{
				lis = null;
				throw new Exception("Error", ex);

			}

			return lis;
		}
		public List<EntradaSalidaRpt> getlistEntradaSalida(DateTime fini, DateTime ffin, String nroplaca, string codGeo)
		{
			try
			{
				ManagerDB db = new ManagerDB();
				List<EntradaSalidaRpt> lis = new List<EntradaSalidaRpt>();
				SqlParameter[] par = { new SqlParameter("@DateFrom", fini), new SqlParameter("@DateTo", ffin), new SqlParameter("@listaPlacas", nroplaca), new SqlParameter("@codGEO", codGeo) };
				lis = db.DataReaderMapToList<EntradaSalidaRpt>("sp_ReporteEntradaSalidaGeocercas", par);
				return lis;
			}
			catch (Exception)
			{
				return null;

			}

		}
		public List<ReporteTempuratura> getlistTemperatura(DateTime fini, DateTime ffin, String nroplaca)
		{
			try
			{
				ManagerDB db = new ManagerDB();
				List<ReporteTempuratura> lis = new List<ReporteTempuratura>();
				SqlParameter[] par = { new SqlParameter("@DateFrom", fini), new SqlParameter("@DateTo", ffin), new SqlParameter("@placa", nroplaca) };

				lis = db.DataReaderMapToList<ReporteTempuratura>("sp_reporteTemperaturaOptimizado1", par);
				return lis;
			}
			catch (Exception)
			{
				return null;

			}

		}
		public string SqlReporteDetenciones(string imeis, string Todate, string toend)
		{
			//string fini = Convert.ToString(Todate);
			//string ffin = Convert.ToString(toend);
			string sql = "select tr.IMEI, tr.EstadoGPS, tr.Velocidad, tr.Latitud, tr.Longitud, tr.EstadoMotor, tr.FechaGPS, tr.Temperatura, tr.VoltajeBateria, tr.EstadoPuerta, " +

	   " CAST(CASE WHEN d.Descripcion is null THEN 'Ver en Mapa' else d.Descripcion end as varchar(256))as direcciones" +
	   " from Trama tr" +
	   " left join OSM.dbo.DireccionRound d on (d.Latitud = ROUND(tr.Latitud,4) and d.Longitud= ROUND(tr.Longitud,4))" +
	   " where tr.IMEI in(" + imeis + ")" +
	   " and tr.FechaGPS >=" + "CONVERT(datetime, '" + Todate + "', 103) " +
	   " and tr.FechaGPS <=" + "CONVERT(datetime, '" + toend + "', 103) ";
			return sql;
		}
		public List<DetencionesRpt> ListarReporteDetenciones(string imei, string rfini, string rffin, int tiporel, int tiempodet, int x1)
		{
			//var  imei = bd.Seguimiento.Where(s => placa.Contains(s.NroPlaca) && s.estado == true ).ToList().IMEI;
			ManagerDB db = new ManagerDB();

			DateTime f1 = Convert.ToDateTime(rfini);
			DateTime f2 = Convert.ToDateTime(rffin);
			Nullable<System.DateTime> fechagpsini = null;
			Nullable<System.DateTime> fechagpsfin = null;
			string lat = String.Empty;
			string lng = String.Empty;
			string direcciones = String.Empty;
			double cantminutos = 0;
			List<DetencionesRpt> lista = new List<DetencionesRpt>();
			List<ResultDetenciones> collection = new List<ResultDetenciones>();
			//SqlParameter[] par = { new SqlParameter("@DateFrom", rfini), new SqlParameter("@DateTo", rffin), new SqlParameter("@imei", imei) };
			string sql = SqlReporteDetenciones(imei, rfini, rffin);
			collection = db.DataReaderMapToList<ResultDetenciones>(sql);


			imei = imei.Replace("'", "");
			List<string> imeis = imei.Split(',').ToList();
			bool sw = true;
			ResultDetenciones ant = new ResultDetenciones();
			if (collection != null)
			{
				foreach (var m in imeis)
				{
					var sublista = collection.Where(x => x.IMEI == m).OrderBy(s => s.FechaGPS).ToList();
					int c = sublista.Count - 1;
					Posicion p1 = new Posicion();
					double difdia;
					foreach (var item in sublista)
					{
						if (sw)
						{
							p1.Latitud = item.Latitud;
							p1.Longitud = item.Longitud;
							fechagpsini = item.FechaGPS;
							lat = item.Latitud.ToString();
							lng = item.Longitud.ToString();
							sw = false;
						}
						else
						{
							Posicion p2 = new Posicion(item.Latitud, item.Longitud);
							difdia = DATA.FuncionesGlobales.GetDistanceM(p1, p2);
							if (difdia > x1)
							{
								p1 = p2;

								//fechagpsini = item.FechaGPS;
								//lat = item.Latitud.ToString();
								//lng = item.Longitud.ToString();
								if (fechagpsini != null && fechagpsfin != null)
								{
									cantminutos = ant.FechaGPS.Subtract(fechagpsini.Value).TotalMinutes;
									DetencionesRpt abc = new DetencionesRpt
									{
										Conductor = "No definido",
										FechaInicio = fechagpsini.Value.ToString(),
										FechaFin = fechagpsfin.Value.ToString(),
										Geocerca = "No definido",
										IDReporte = 1,
										Latitud = lat,
										Longitud = lng,
										Tiempo = Convert.ToInt32(Math.Round(cantminutos, 0)),
										Ubicacion = direcciones,
										Vehiculo = m
									};
									if (abc.Tiempo >= 1)
									{
										lista.Add(abc);
									}
								}
								fechagpsini = item.FechaGPS;
								lat = item.Latitud.ToString();
								lng = item.Longitud.ToString();
								cantminutos = 0;
								fechagpsfin = null;


							}
							else
							{
								fechagpsfin = item.FechaGPS;
								direcciones = item.direcciones;
								ant = item;
							}


						}

					}
					if (fechagpsini != null && fechagpsfin != null)
					{
						Posicion p2 = new Posicion(ant.Latitud, ant.Longitud);
						difdia = DATA.FuncionesGlobales.GetDistanceM(p1, p2);
						if (difdia <= x1)
						{
							cantminutos = ant.FechaGPS.Subtract(fechagpsini.Value).TotalMinutes;
							DetencionesRpt abc = new DetencionesRpt
							{
								Conductor = "No definido",
								FechaInicio = fechagpsini.Value.ToString(),
								FechaFin = fechagpsfin.Value.ToString(),
								Geocerca = "No definido",
								IDReporte = 1,
								Latitud = lat,
								Longitud = lng,
								Tiempo = Convert.ToInt32(Math.Round(cantminutos, 0)),
								Ubicacion = direcciones,
								Vehiculo = m
							};
							if (abc.Tiempo >= 1)
							{
								lista.Add(abc);
							}
						}
						cantminutos = 0;
						fechagpsfin = null;
					}
					sw = true;
				}
			}


			switch (tiporel)
			{
				//Igual a
				case 0:
					lista = lista.Where(s => s.Tiempo == tiempodet).ToList();
					break;
				case 1:
					lista = lista.Where(s => s.Tiempo > tiempodet).ToList();
					break;
				case 2:
					lista = lista.Where(s => s.Tiempo < tiempodet).ToList();
					break;
				case 3:
					lista = lista.Where(s => s.Tiempo >= tiempodet).ToList();
					break;
				case 4:
					lista = lista.Where(s => s.Tiempo <= tiempodet).ToList();
					break;
			}

			return lista;
		}
	}
}
