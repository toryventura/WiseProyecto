using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WISETRACK.Datos;
using WISETRACK.Datos.optimizado;
using WISETRACK.Datos.Serializable;
using WISETRACK.Models;

namespace WISETRACK.Controller
{
	public class ReporteController
	{
		WISETRACKEntities db = new WISETRACKEntities();
		//GABRIEL
		public List<TipoAlarmaCboDet> TiposAlarmaDistinct { get; set; }
		public List<CategAlarmaCboDet> CategsAlarmaDistinct { get; set; }
		public List<VehiculoCboDet> VehiculosDistinct { get; set; }

		#region Strings RptAlarma Consulta

		private const string rptAlarmaSelect = "SELECT am.CodAlarma AS Codigo, case when ta.CodTipoAlarma in(6,12,13) then 'Geocerca: '+(select Descripcion from Geocerca where CodigoGEO=at.CodigoGEO) else am.NombreAlarma end AS Nombre, "
			+ "ta.Descripcion AS Tipo, ca.Descripcion AS Categoria, "
			+ "v.NroPlaca AS Vehiculo, at2.FechaInicio AS FechaInicio,at2.FechaFin AS FechaFin,"
			+ "CAST(CASE WHEN ta.CodTipoAlarma = 1 THEN 'Control' "
			+ "WHEN ta.CodTipoAlarma = 2 THEN "
				+ "CAST(CASE WHEN am.CodCategoria <= 3 THEN 'Menor o Igual a ' + CAST(am.Tiempo AS VARCHAR) "
					+ "ELSE 'Mayor o Igual a ' + CAST(am.Tiempo2 AS VARCHAR) + ' Menor o Igual a ' + CAST(am.Tiempo AS VARCHAR) END AS VARCHAR) + ' Min' "
			+ "WHEN ta.CodTipoAlarma = 3 THEN "
				+ "CAST(CASE WHEN am.CodCategoria <= 3 THEN 'Mayor o Igual a ' + CAST(am.Tiempo2 AS VARCHAR) "
					+ "ELSE 'Mayor o Igual a ' + CAST(am.Tiempo2 AS VARCHAR) + ' Menor o Igual a ' + CAST(am.Tiempo AS VARCHAR) END AS VARCHAR) + ' Min' "
			+ "WHEN ta.CodTipoAlarma = 4 THEN 'Entrada/Salida' "
			+ "WHEN ta.CodTipoAlarma = 5 THEN "
				+ "CAST(CASE WHEN am.CodCategoria <= 3 THEN 'Menor o Igual a ' + CAST(CAST(am.Velocidad AS DECIMAL(10,1)) AS VARCHAR) "
					+ "ELSE 'Mayor o Igual a ' + CAST(CAST(am.Velocidad2 AS DECIMAL(10,1)) AS VARCHAR) + ' Menor o Igual a ' + CAST(CAST(am.Velocidad AS DECIMAL(10,1)) AS VARCHAR) "
					+ "END AS VARCHAR) + ' Km/h' "
			+ "WHEN ta.CodTipoAlarma = 6 THEN 'Entrada/Salida' "
			+ "WHEN ta.CodTipoAlarma = 7 THEN CAST(CASE WHEN am.CodCategoria = 1 THEN 'Menor o Igual a ' + CAST(am.Distancia AS VARCHAR) "
				+ "ELSE 'Mayor o Igual a ' + CAST(am.Distancia2 AS VARCHAR) + ' Menor o Igual a ' + CAST(am.Distancia AS VARCHAR) END AS VARCHAR) + ' Km' "
			+ "WHEN ta.CodTipoAlarma = 8 THEN "
				+ "CAST(CASE WHEN am.CodCategoria <= 3 THEN 'Menor o Igual a ' + CAST(am.Temperatura AS VARCHAR) "
					+ "ELSE 'Mayor o Igual a ' + CAST(am.Temperatura2 AS VARCHAR) + ' Menor o Igual a ' + CAST(am.Temperatura AS VARCHAR) END AS VARCHAR) + ' ºC' "
			+ "WHEN ta.CodTipoAlarma = 9 THEN "
				+ "CAST(CASE WHEN am.CodCategoria <= 3 THEN 'Mayor o Igual a ' + CAST(am.Temperatura2 AS VARCHAR) "
					+ "ELSE 'Mayor o Igual a ' + CAST(am.Temperatura2 AS VARCHAR) + ' Menor o Igual a ' + CAST(am.Temperatura AS VARCHAR) END AS VARCHAR) + ' ºC' "
			+ "WHEN ta.CodTipoAlarma = 10 THEN 'Apertura/Cierre' "
			+ "WHEN ta.CodTipoAlarma = 11 THEN 'Voltaje' "
			+ "WHEN ta.CodTipoAlarma = 12 THEN 'Entrada' "
			+ "WHEN ta.CodTipoAlarma = 13 THEN 'Salida' "

			+ "ELSE '-' END AS VARCHAR(128)) AS ValorPerm, "
			+ "CAST(CASE WHEN ta.CodTipoAlarma = 1 THEN CAST(CASE WHEN at.Estado = 1 THEN 'Inicio' ELSE 'Fin' END AS VARCHAR) "
				+ "WHEN ta.CodTipoAlarma = 2 OR ta.CodTipoAlarma = 3 THEN  CAST(DATEDIFF(MINUTE, at2.FechaInicio, at2.FechaFin) AS VARCHAR) + ' Min' "
				+ "WHEN ta.CodTipoAlarma = 4 THEN CAST(CASE WHEN at.Estado = 1 THEN 'Entrada' ELSE 'Salida' END AS VARCHAR) "
				+ "WHEN ta.CodTipoAlarma = 5 THEN CAST(CAST(ti.Velocidad AS DECIMAL(10,1)) AS VARCHAR) + ' Km/h' "
				+ "WHEN ta.CodTipoAlarma = 6 THEN CAST(DATEDIFF(MINUTE, at2.FechaInicio, at2.FechaFin) AS VARCHAR) + ' Min' "
				+ "WHEN ta.CodTipoAlarma = 7 THEN CAST(ti.Kilometraje AS VARCHAR) + ' Km' "
				+ "WHEN ta.CodTipoAlarma = 8 OR ta.CodTipoAlarma = 9 THEN CAST(ti.Temperatura AS VARCHAR) + ' ºC' "
				+ "WHEN ta.CodTipoAlarma = 10 THEN CAST(CASE WHEN at.Estado = 1 THEN 'Cierre' ELSE 'Apertura' END AS VARCHAR) "
				+ "WHEN ta.CodTipoAlarma = 11THEN CAST(ti.VoltajeBateria AS VARCHAR) + ' V' "
				+ "WHEN ta.CodTipoAlarma = 12 THEN CAST(CASE WHEN at.Estado = 1 THEN 'Entrada' ELSE 'Entrada' END AS VARCHAR)  "
				+ "WHEN ta.CodTipoAlarma = 13 THEN CAST(CASE WHEN at.Estado = 1 THEN 'Entrada' ELSE 'Salida' END AS VARCHAR)  "
				+ "ELSE '-' END AS VARCHAR(128)) AS ValorReg, "
			+ "'Buscando..' AS Ubicacion, ti.Longitud AS Longitud, ti.Latitud AS Latitud ";

		private string rptAlarmaFrom = "FROM Alarma am, Alerta at, Alerta2 at2, TipoAlarma ta, CategoriaAlarma ca, "
			+ "Vehiculo v, Seguimiento s, Trama ti "
			+ "WHERE am.CodAlarma = at2.CodAlarma and at.ID = at2.ID "
			+ "and am.CodAlarma = at.CodAlarma and am.CodTipoAlarma = ta.CodTipoAlarma "
			+ "and am.CodCategoria = ca.CodCategoria and at.NroPlaca = v.Nroplaca "
			+ "and v.NroPlaca = s.NroPlaca and at.IDTrama = ti.ID ";

		#endregion

		public List<temperaturaSerial> listarReporteTemperatura(String fechaini, String horaini, String fechafin, String horafin, String placa)
		{
			List<temperaturaSerial> lista = new List<temperaturaSerial>();
			string rfini;
			string rffin;
			Concatenar(fechaini, horaini, fechafin, horafin, out rfini, out rffin);
			var collection = db.sp_reporteTemperaturaOptimizado(rfini, rffin, placa).ToList();

			foreach (var item in collection)
			{
				temperaturaSerial temp = new temperaturaSerial
				{
					EstadoPuerta = item.EstadoPuerta.ToString(),

					FechaGPS = (DateTime)item.FechaGPS,
					IDButton = item.IDButton,
					Latitud = (float)Convert.ToDouble(item.Latitud),
					Longitud = (float)Convert.ToDouble(item.Longitud),
					Velocidad = item.Velocidad.Value
				};
				if (String.IsNullOrEmpty(item.Nombre))
				{
					temp.Nombre = "No Asignado";
				}
				temp.Nombre = item.Nombre;
				temp.Temperatura = (float)item.Temperatura;
				temp.NroPlaca = item.NroPlaca;
				temp.direcciones = item.direcciones;
				lista.Add(temp);
			}
			return lista;
		}
		public List<Seguimiento> ListaImeis(List<string> listaplacas)
		{
			try
			{
				var list = db.Seguimiento.Where(x => listaplacas.Contains(x.NroPlaca) && x.estado == true).ToList();
				return list;
			}
			catch (Exception ex)
			{

				return null;
			}
		}

		public List<RptTemperaturaViewModel> listarReporteTemperaturaOptimizada(String fechaini, String horaini, String fechafin, String horafin, String placa)
		{
			try
			{
				string rfini;
				string rffin;
				Concatenar(fechaini, horaini, fechafin, horafin, out rfini, out rffin);
				//string consulta = "EXEC sp_reporteTemperatura '" + rfini + "','" + rffin + "','" + placa + "'";
				List<RptTemperaturaViewModel> lista = new List<RptTemperaturaViewModel>();
				var collection = db.sp_reporteTemperaturaOptimizado(rfini, rffin, placa).ToList();
				ControlarAperturayCierre(lista, collection);
				return lista;
			}
			catch (Exception e)
			{
				List<RptTemperaturaViewModel> lista1 = new List<RptTemperaturaViewModel>();
				return lista1;
			}
		}

		private static void ControlarAperturayCierre(List<RptTemperaturaViewModel> lista, List<sp_reporteTemperaturaOptimizado_Result> collection)
		{
			bool bandera = false; //Bandera: Se utiliza para determinar si en la consulta hubo un evento GTDIS que indique cual fue primero (Apertura o Cierre)
			bool okgtdis = false; //okgtdis: Se utiliza para determinar si hubo un evento GTDIS
			var result = collection.Where(s => s.TipoMensaje == "GTDIS").ToList();
			bool buscarGTDIS = false; //Obtener el primer valor GTDIS de la consulta.
			if (result.Count > 0)
			{
				okgtdis = true;
				buscarGTDIS = (bool)collection.Where(s => s.TipoMensaje == "GTDIS").First().EstadoPuerta;
				if (buscarGTDIS == false)
				{
					bandera = true;
				}
				else
				{
					bandera = false;
				}
			}

			int cont = 1;
			foreach (var item in collection)
			{
				RptTemperaturaViewModel rptlista = new RptTemperaturaViewModel();
				if (item.TipoMensaje == "GTDIS")
				{
					bandera = item.EstadoPuerta.Value;
				}
				if (item.TipoMensaje != "GTDIS")
				{
					if (okgtdis == true)
					{
						if (item.Temperatura != -999.00)
						{
							rptlista.direcciones = item.direcciones;
							rptlista.EstadoPuerta = (bandera == true) ? "Cerrado" : "Abierto";
							rptlista.FechaGPS = item.FechaGPS.Value;
							rptlista.IDButton = item.IDButton;
							rptlista.IMEI = item.IMEI;
							rptlista.Latitud = item.Latitud;
							rptlista.Longitud = item.Longitud;
							rptlista.Nombre = item.Nombre;
							rptlista.NroPlaca = item.NroPlaca;
							rptlista.Temperatura = item.Temperatura.Value;
							rptlista.Velocidad = item.Velocidad.Value;
							lista.Add(rptlista);
						}
					}
					else
					{
						bandera = true;
						if (item.Temperatura != -999.00)
						{
							rptlista.direcciones = item.direcciones;
							rptlista.EstadoPuerta = (bandera == true) ? "Cerrado" : "Abierto";
							rptlista.FechaGPS = item.FechaGPS.Value;
							rptlista.IDButton = item.IDButton;
							rptlista.IMEI = item.IMEI;
							rptlista.Latitud = item.Latitud;
							rptlista.Longitud = item.Longitud;
							rptlista.Nombre = item.Nombre;
							rptlista.NroPlaca = item.NroPlaca;
							rptlista.Temperatura = item.Temperatura.Value;
							rptlista.Velocidad = item.Velocidad.Value;
							lista.Add(rptlista);
						}
					}
				}
				cont++;
			}
		}

		public List<DetencionRptDet> GetAllDetenciones(List<string> nroPlacas, string fechaI, string horaI, string fechaF, string horaF, int tiempoDet, int tipoRel)
		{
			List<DetencionRptDet> detenciones = new List<DetencionRptDet>();

			try
			{
				string fechaInicio, fechaFin;
				Concatenar(fechaI, horaI, fechaF, horaF, out fechaInicio, out fechaFin);

				string filtroTiempo = "= 0";

				switch (tipoRel)
				{
					case 0: //IGUAL A
						filtroTiempo = "= " + Convert.ToString(tiempoDet);
						break;

					case 1: //MAYOR A
						filtroTiempo = "> " + Convert.ToString(tiempoDet);
						break;

					case 2: //MENOR A
						filtroTiempo = "< " + Convert.ToString(tiempoDet);
						break;

					case 3: //MAYOR O IGUAL A
						filtroTiempo = ">= " + Convert.ToString(tiempoDet);
						break;

					case 4: //MENOR O IGUAL A
						filtroTiempo = "<= " + Convert.ToString(tiempoDet);
						break;
				}

				string consulta = "SELECT rt.ID AS IDReporte, rt.NroPlaca AS Vehiculo, "
					+ "'No Asignado' AS Conductor, 'No Encontrada' AS Geocerca, "
					+ "(SELECT CONVERT(varchar(10), ti.FechaGPS, 103) + ' ' "
					+ "+ CONVERT(varchar(10), ti.FechaGPS, 108)) AS FechaInicio, "
					+ "CAST(CASE WHEN d.Descripcion is null THEN 'Ver en Mapa' "
					+ "ELSE d.Descripcion END AS VARCHAR(256)) AS Ubicacion, "
					+ "CAST(CAST(ti.Longitud AS decimal(10, 6)) AS varchar(10)) AS Longitud, "
					+ "CAST(CAST(ti.Latitud AS decimal(10, 6)) AS varchar(10)) AS Latitud, "
					+ "(SELECT CONVERT(varchar(10), tf.FechaGPS, 103) + ' ' "
					+ "+ CONVERT(varchar(10), tf.FechaGPS, 108)) AS FechaFin, "
					+ "DATEDIFF(MINUTE, ti.FechaGPS, tf.FechaGPS) AS Tiempo "
					+ "FROM ReporteTemp rt inner join Trama ti ON rt.IDInicio = ti.ID "
					+ "inner join Trama tf ON rt.IDFin = tf.ID "
					+ "left join OSM.dbo.DireccionRound d ON "
					+ "(ROUND(ti.Latitud, 4) = d.Latitud "
					+ "and ROUND(ti.Longitud, 4) = d.Longitud) "
					+ "WHERE rt.CodTipoAlarma = 2 "
					+ "and DATEDIFF(MINUTE, ti.FechaGPS, tf.FechaGPS) " + filtroTiempo + " "
					+ "and ti.FechaGPS >= CONVERT(datetime, '" + fechaInicio + "', 103) "
					+ "and ti.FechaGPS <= CONVERT(datetime, '" + fechaFin + "', 103) "
					+ "and (rt.NroPlaca = '" + nroPlacas[0] + "'";

				for (int i = 1 ; i < nroPlacas.Count ; i++)
					consulta = consulta + " or rt.NroPlaca = '" + nroPlacas[i] + "'";

				consulta = consulta + ") ORDER BY ti.FechaGPS DESC";

				detenciones = db.Database.SqlQuery<DetencionRptDet>(consulta).ToList();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return detenciones;
		}

		public List<VelocidadRptDet> GetAllVelocidades(string nroPlacas, string fechaI, string horaI, string fechaF, string horaF, int velocidad, int tipoRel)
		{
			List<VelocidadRptDet> velocidades = new List<VelocidadRptDet>();

			try
			{
				string consulta, fechaInicio, fechaFin;
				Concatenar(fechaI, horaI, fechaF, horaF, out fechaInicio, out fechaFin);

				string filtroVelocidad = "= 0";

				switch (tipoRel)
				{
					case 0: //IGUAL A
						filtroVelocidad = "= " + Convert.ToString(velocidad);
						break;

					case 1: //MAYOR A
						filtroVelocidad = "> " + Convert.ToString(velocidad);
						break;

					case 2: //MAYOR O IGUAL A
						filtroVelocidad = ">= " + Convert.ToString(velocidad);
						break;
				}

				//foreach (var nroPlaca in nroPlacas)
				//{

				//}

				consulta = "SELECT CONVERT(bigint, ti.ID) AS IDReporte, s.NroPlaca AS Vehiculo, "
						+ "'No Asignado' AS Conductor, 'No Encontrada' AS Geocerca, "
						+ "(SELECT CONVERT(varchar(10), ti.FechaGPS, 103) + ' ' "
						+ "+ CONVERT(varchar(10), ti.FechaGPS, 108)) AS Fecha, "
						+ "CAST(CASE WHEN d.Descripcion is null THEN 'Ver en Mapa' "
						+ "ELSE d.Descripcion END AS VARCHAR(256)) AS Ubicacion, "
						+ "CAST(CAST(ti.Longitud AS decimal(10, 6)) AS varchar(10)) AS Longitud, "
						+ "CAST(CAST(ti.Latitud AS decimal(10, 6)) AS varchar(10)) AS Latitud, "
						+ "CAST(ti.Velocidad AS FLOAT) AS Velocidad "
						+ "FROM Trama2 ti inner join Seguimiento s ON (s.IMEI = ti.IMEI and s.estado = 1) "
						+ "left join OSM.dbo.DireccionRound d ON "
						+ "(ROUND(ti.Latitud, 4) = d.Latitud "
						+ "and ROUND(ti.Longitud, 4) = d.Longitud) "
						+ "WHERE ti.Velocidad " + filtroVelocidad + " "
						+ "and ti.FechaGPS >= CONVERT(datetime, '" + fechaInicio + "', 103) "
						+ "and ti.FechaGPS <= CONVERT(datetime, '" + fechaFin + "', 103) "
						+ "and s.NroPlaca = '" + nroPlacas + "' ORDER BY ti.FechaGPS DESC";

				var velocidades2 = db.Database.SqlQuery<VelocidadRptDet>(consulta).ToList();

				velocidades.AddRange(velocidades2);

				velocidades = velocidades.OrderByDescending(v => v.Fecha).ToList();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return velocidades;
		}


		public List<VelocidadRptDet> GetAllVelocidades(List<string> nroPlacas, string fechaI, string horaI, string fechaF, string horaF, int velocidad, int tipoRel)
		{
			List<VelocidadRptDet> velocidades = new List<VelocidadRptDet>();

			try
			{
				string consulta, fechaInicio, fechaFin;
				Concatenar(fechaI, horaI, fechaF, horaF, out fechaInicio, out fechaFin);

				string filtroVelocidad = "= 0";

				switch (tipoRel)
				{
					case 0: //IGUAL A
						filtroVelocidad = "= " + Convert.ToString(velocidad);
						break;

					case 1: //MAYOR A
						filtroVelocidad = "> " + Convert.ToString(velocidad);
						break;

					case 2: //MENOR A
						filtroVelocidad = "< " + Convert.ToString(velocidad);
						break;
					case 3: //MAYOR A IGUAL
						filtroVelocidad = ">= " + Convert.ToString(velocidad);
						break;
					case 4: //MENOR A IGUAL
						filtroVelocidad = "<= " + Convert.ToString(velocidad);
						break;
				}

				//foreach (var nroPlaca in nroPlacas)
				//{

				//}
				string placas = "";
				foreach (var item in nroPlacas)
				{
					placas = placas + "'" + item + "',";
				}
				if (placas.Length > 1)
				{
					placas = placas.Substring(0, placas.Length - 1);
				}
				consulta = "SELECT CONVERT(bigint, ti.ID) AS IDReporte, s.NroPlaca AS Vehiculo, "
						+ "'No Asignado' AS Conductor, 'No Encontrada' AS Geocerca, "
						+ "(SELECT CONVERT(varchar(10), ti.FechaGPS, 103) + ' ' "
						+ "+ CONVERT(varchar(10), ti.FechaGPS, 108)) AS Fecha, "
						+ "CAST(CASE WHEN d.Descripcion is null THEN 'Ver en Mapa' "
						+ "ELSE d.Descripcion END AS VARCHAR(256)) AS Ubicacion, "
						+ "CAST(CAST(ti.Longitud AS decimal(10, 6)) AS varchar(10)) AS Longitud, "
						+ "CAST(CAST(ti.Latitud AS decimal(10, 6)) AS varchar(10)) AS Latitud, "
						+ "CAST(ti.Velocidad AS FLOAT) AS Velocidad "
						+ "FROM Trama2 ti inner join Seguimiento s ON (s.IMEI = ti.IMEI and s.estado = 1) "
						+ "left join OSM.dbo.DireccionRound d ON "
						+ "(ROUND(ti.Latitud, 4) = d.Latitud "
						+ "and ROUND(ti.Longitud, 4) = d.Longitud) "
						+ "WHERE ti.Velocidad " + filtroVelocidad + " "
						+ "and ti.FechaGPS >= CONVERT(datetime, '" + fechaInicio + "', 103) "
						+ "and ti.FechaGPS <= CONVERT(datetime, '" + fechaFin + "', 103) "
						+ "and s.NroPlaca in(" + placas + ") ORDER BY ti.FechaGPS DESC";

				var velocidades2 = db.Database.SqlQuery<VelocidadRptDet>(consulta).ToList();

				velocidades.AddRange(velocidades2);

				velocidades = velocidades.OrderByDescending(v => v.Fecha).ToList();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return velocidades;
		}

		public List<KilometrajeRptDet> GetAllKilometrajes(string nroPlacas, string fechaI, string horaI, string fechaF, string horaF)
		{
			List<KilometrajeRptDet> kilometrajes = new List<KilometrajeRptDet>();

			try
			{
				string consulta, fechaInicio, fechaFin;
				Concatenar(fechaI, horaI, fechaF, horaF, out fechaInicio, out fechaFin);

				consulta = "SELECT top(1) CONVERT(bigint, ti.ID) AS IDReporte, s.NroPlaca AS Vehiculo, "
						+ "'No Asignado' AS Conductor, 'No Encontrada' AS Geocerca, "
						+ "(SELECT CONVERT(varchar(10), ti.FechaGPS, 103) + ' ' "
						+ "+ CONVERT(varchar(10), ti.FechaGPS, 108)) AS FechaInicio, "
						+ "CAST(CASE WHEN d.Descripcion is null THEN 'Ver en Mapa' "
						+ "ELSE d.Descripcion END AS VARCHAR(256)) AS Ubicacion, "
						+ "CAST(CAST(ti.Longitud AS decimal(10, 6)) AS varchar(10)) AS Longitud, "
						+ "CAST(CAST(ti.Latitud AS decimal(10, 6)) AS varchar(10)) AS Latitud, "
						+ "(SELECT CONVERT(varchar(10), ti.FechaGPS, 103) + ' ' "
						+ "+ CONVERT(varchar(10), ti.FechaGPS, 108)) AS FechaFin, "
						+ "CAST(ti.Kilometraje AS FLOAT) AS Kilometraje "
						+ "FROM Trama2 ti inner join Seguimiento s ON (s.IMEI = ti.IMEI and s.estado = 1) "
						+ "left join OSM.dbo.DireccionRound d ON "
						+ "(ROUND(ti.Latitud, 4) = d.Latitud "
						+ "and ROUND(ti.Longitud, 4) = d.Longitud) "
						+ "WHERE ti.FechaGPS >= CONVERT(datetime, '" + fechaInicio + "', 103) "
						+ "and s.NroPlaca = '" + nroPlacas + "' ORDER BY ti.FechaGPS";

				var kilometrajeI = db.Database.SqlQuery<KilometrajeRptDet>(consulta).FirstOrDefault();

				consulta = "SELECT top(1) CONVERT(bigint, ti.ID) AS IDReporte, s.NroPlaca AS Vehiculo, "
					+ "'No Asignado' AS Conductor, 'No Encontrada' AS Geocerca, "
					+ "(SELECT CONVERT(varchar(10), ti.FechaGPS, 103) + ' ' "
					+ "+ CONVERT(varchar(10), ti.FechaGPS, 108)) AS FechaInicio, "
					+ "CAST(CASE WHEN d.Descripcion is null THEN 'Ver en Mapa' "
					+ "ELSE d.Descripcion END AS VARCHAR(256)) AS Ubicacion, "
					+ "CAST(CAST(ti.Longitud AS decimal(10, 6)) AS varchar(10)) AS Longitud, "
					+ "CAST(CAST(ti.Latitud AS decimal(10, 6)) AS varchar(10)) AS Latitud, "
					+ "(SELECT CONVERT(varchar(10), ti.FechaGPS, 103) + ' ' "
					+ "+ CONVERT(varchar(10), ti.FechaGPS, 108)) AS FechaFin, "
					+ "CAST(ti.Kilometraje AS FLOAT) AS Kilometraje "
					+ "FROM Trama2 ti inner join Seguimiento s ON (s.IMEI = ti.IMEI and s.estado = 1) "
					+ "left join OSM.dbo.DireccionRound d ON "
					+ "(ROUND(ti.Latitud, 4) = d.Latitud "
					+ "and ROUND(ti.Longitud, 4) = d.Longitud) "
					+ "WHERE ti.FechaGPS <= CONVERT(datetime, '" + fechaFin + "', 103) "
					+ "and s.NroPlaca = '" + nroPlacas + "' ORDER BY ti.FechaGPS DESC";

				var kilometrajeF = db.Database.SqlQuery<KilometrajeRptDet>(consulta).FirstOrDefault();

				if (kilometrajeI != null && kilometrajeF != null)
				{
					var kmI = kilometrajeI.Kilometraje;
					var kmF = kilometrajeF.Kilometraje;

					var KmDiff = Math.Round(kmF - kmI, 2);

					if (KmDiff > 0)
					{
						var kilometraje = new KilometrajeRptDet
						{
							IDReporte = kilometrajeI.IDReporte,
							Vehiculo = kilometrajeI.Vehiculo,
							Conductor = kilometrajeI.Conductor,
							Geocerca = kilometrajeI.Geocerca,
							FechaInicio = kilometrajeI.FechaInicio,
							FechaFin = kilometrajeF.FechaInicio,
							Latitud = kilometrajeI.Latitud,
							Longitud = kilometrajeI.Longitud,
							Ubicacion = kilometrajeI.Ubicacion,
							Kilometraje = KmDiff
						};

						kilometrajes.Add(kilometraje);
					}
				}

				kilometrajes = kilometrajes.OrderByDescending(k => k.FechaInicio).ToList();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return kilometrajes;
		}

		public List<sp_ReporteKilometraje_Result> GetAllKilometrajes(string placas, DateTime fromdate, DateTime todate)
		{

			try
			{
				var lista = db.sp_ReporteKilometraje(placas, fromdate, todate);
				return lista.ToList();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return null;
		}


		public List<EstadoPuertaRptDet> GetAllEstadosPuerta(List<string> nroPlacas, int estado, string fechaI, string horaI, string fechaF, string horaF)
		{
			List<EstadoPuertaRptDet> estadosPuerta = new List<EstadoPuertaRptDet>();
			try
			{
				string consulta, fechaInicio, fechaFin;
				Concatenar(fechaI, horaI, fechaF, horaF, out fechaInicio, out fechaFin);

				consulta = "SELECT rt.ID AS IDReporte, rt.NroPlaca AS Vehiculo, "
					+ "'No Asignado' AS Conductor, 'No Encontrada' AS Geocerca, "
					+ "(SELECT CONVERT(varchar(10), ti.FechaGPS, 103) + ' ' "
					+ "+ CONVERT(varchar(10), ti.FechaGPS, 108)) AS FechaApertura, "
					+ "CAST(CASE WHEN d.Descripcion is null THEN 'Ver en Mapa' "
					+ "ELSE d.Descripcion END AS VARCHAR(256)) AS UbicacionA, "
					+ "CAST(CAST(ti.Longitud AS decimal(10, 6)) AS varchar(10)) AS LongitudA, "
					+ "CAST(CAST(ti.Latitud AS decimal(10, 6)) AS varchar(10)) AS LatitudA, "
					+ "(SELECT CONVERT(varchar(10), tf.FechaGPS, 103) + ' ' "
					+ "+ CONVERT(varchar(10), tf.FechaGPS, 108)) AS FechaCierre, "
					+ "CONVERT(varchar, DATEDIFF(MINUTE, ti.FechaGPS, tf.FechaGPS)) AS Tiempo "
					+ "FROM ReporteTemp rt inner join Trama ti ON rt.IDInicio = ti.ID "
					+ "inner join Trama tf ON rt.IDFin = tf.ID "
					+ "left join OSM.dbo.DireccionRound d ON "
					+ "(ROUND(ti.Latitud, 4) = d.Latitud "
					+ "and ROUND(ti.Longitud, 4) = d.Longitud) "
					+ "WHERE rt.CodTipoAlarma = 10 and rt.IDFin > 0 "
					+ "and DATEDIFF(MINUTE, ti.FechaGPS, tf.FechaGPS) > 0 "
					+ "and ti.FechaGPS >= CONVERT(datetime, '" + fechaInicio + "', 103) "
					+ "and ti.FechaGPS <= CONVERT(datetime, '" + fechaFin + "', 103) "
					+ "and (rt.NroPlaca = '" + nroPlacas[0] + "'";

				for (int i = 1 ; i < nroPlacas.Count ; i++)
					consulta = consulta + " or rt.NroPlaca = '" + nroPlacas[i] + "'";

				consulta = consulta + ") ORDER BY ti.FechaGPS DESC";

				estadosPuerta = db.Database.SqlQuery<EstadoPuertaRptDet>(consulta).ToList();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return estadosPuerta;
		}


		//public List<EntradaSalidaRptDet> GetAllEntradaSalida(List<int> codGeos, List<string> nroPlacas, int estado, string fechaI, string horaI, string fechaF, string horaF)
		public List<EntradaSalidaRptDet> GetAllEntradaSalida(int codGeos, string nroPlacas, int estado, string fechaI, string horaI, string fechaF, string horaF)
		{
			List<EntradaSalidaRptDet> entradasSalidas = new List<EntradaSalidaRptDet>();

			try
			{
				string consulta, fechaInicio, fechaFin;
				Concatenar(fechaI, horaI, fechaF, horaF, out fechaInicio, out fechaFin);

				consulta = "SELECT rt.ID AS IDReporte, rt.NroPlaca AS Vehiculo, "
					+ "'No Asignado' AS Conductor, g.Descripcion AS Geocerca, "
					+ "(SELECT CONVERT(varchar(10), ti.FechaGPS, 103) + ' ' "
					+ "+ CONVERT(varchar(10), ti.FechaGPS, 108)) AS FechaEntrada, "
					+ "CAST(CASE WHEN d.Descripcion is null THEN 'Ver en Mapa' "
					+ "ELSE d.Descripcion END AS VARCHAR(256)) AS UbicacionE, "
					+ "CAST(CAST(ti.Longitud AS decimal(10, 6)) AS varchar(10)) AS LongitudE, "
					+ "CAST(CAST(ti.Latitud AS decimal(10, 6)) AS varchar(10)) AS LatitudE, "
					+ "(SELECT CONVERT(varchar(10), tf.FechaGPS, 103) + ' ' "
					+ "+ CONVERT(varchar(10), tf.FechaGPS, 108)) AS FechaSalida, "
					+ "CONVERT(varchar, DATEDIFF(MINUTE, ti.FechaGPS, tf.FechaGPS)) AS Tiempo "
					+ "FROM ReporteTemp rt inner join Trama ti ON rt.IDInicio = ti.ID "
					+ "inner join Trama tf ON rt.IDFin = tf.ID "
					+ "inner join Geocerca g ON rt.CodigoGEO = g.CodigoGEO "
					+ "left join OSM.dbo.DireccionRound d ON "
					+ "(ROUND(ti.Latitud, 4) = d.Latitud "
					+ "and ROUND(ti.Longitud, 4) = d.Longitud) "
					+ "WHERE rt.CodTipoAlarma = 6 and rt.IDFin > 0 "
					+ "and DATEDIFF(MINUTE, ti.FechaGPS, tf.FechaGPS) > 0 "
					+ "and ti.FechaGPS >= CONVERT(datetime, '" + fechaInicio + "', 103) "
					+ "and ti.FechaGPS <= CONVERT(datetime, '" + fechaFin + "', 103) "
					+ "and rt.CodigoGEO = " + codGeos + " and rt.NroPlaca = '" + nroPlacas + "'";
				//+ "and (rt.CodigoGEO = " + codGeos[0].ToString();

				//for (int i = 1; i < codGeos.Count; i++)
				//    consulta = consulta + " or rt.CodigoGEO = " + codGeos[i].ToString();

				//consulta = consulta + ") and (rt.NroPlaca = '" + nroPlacas[0] + "'";

				//for (int i = 1; i < nroPlacas.Count; i++)
				//    consulta = consulta + " or rt.NroPlaca = '" + nroPlacas[i] + "'";

				//consulta = consulta + ") ORDER BY ti.FechaGPS DESC";

				entradasSalidas = db.Database.SqlQuery<EntradaSalidaRptDet>(consulta).ToList();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return entradasSalidas;
		}

		public List<AlarmaRptDet> GetRptAlarmas(List<int> codTipos, string placa, string fechaI, string horaI, string fechaF, string horaF, string sortExpresion, string sortDireccion)
		{
			List<AlarmaRptDet> rptAlarmas = new List<AlarmaRptDet>();
			try
			{
				string fechaInicio, fechaFin;
				Concatenar(fechaI, horaI, fechaF, horaF, out fechaInicio, out fechaFin);

				string consulta = rptAlarmaSelect + rptAlarmaFrom;

				//string consulta2 = "and ti.FechaGPS >= CONVERT(datetime, '" + fechaInicio + "', 103) "
				//    + "and ti.FechaGPS <= CONVERT(datetime, '" + fechaFin + "', 103) "
				//    + "and (ta.CodTipoAlarma = " + Convert.ToString(codTipos[0]);

				string consulta2 = "and at2.FechaInicio>= CONVERT(datetime, '" + fechaInicio + "', 103) "
					+ "and at2.FechaInicio <= CONVERT(datetime, '" + fechaFin + "', 103) "
					+ "and (ta.CodTipoAlarma = " + Convert.ToString(codTipos[0]);

				for (int i = 1 ; i < codTipos.Count ; i++)
					consulta2 = consulta2 + " or ta.CodTipoAlarma = " + Convert.ToString(codTipos[i]);

				consulta2 = consulta2 + ") and v.Patente = '" + placa + "' ";

				string consulta3 = "group by am.CodAlarma, am.NombreAlarma,ta.Descripcion,ca.Descripcion,v.NroPlaca,at.FechaHora,am.CodTipoAlarma, "
				+ "ta.CodTipoAlarma, am.CodCategoria, am.Tiempo, am.Tiempo2, am.Velocidad,"
				+ "am.Velocidad2, am.Distancia, am.Distancia2, am.Temperatura, am.Temperatura2, at.Estado, "
				+ "ti.Velocidad, ti.VoltajeBateria, ti.Kilometraje, ti.Temperatura, ti.Longitud, ti.Latitud, at2.FechaInicio,at2.FechaFin,at.CodigoGEO"
				+ " ORDER BY " + sortExpresion + " " + sortDireccion;

				consulta = consulta + consulta2 + consulta3;
				rptAlarmas = db.Database.SqlQuery<AlarmaRptDet>(consulta).ToList();

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return rptAlarmas;
		}

		public List<AlarmaRptDet> GetRptAlarmas(List<int> codTipos, List<string> placa, string fechaI, string fechaF)
		{
			List<AlarmaRptDet> rptAlarmas = new List<AlarmaRptDet>();
			try
			{

				string consulta = rptAlarmaSelect + rptAlarmaFrom;
				string listaplacas = "";
				string consulta2 = "and at2.FechaInicio>= " + "CONVERT(datetime, '" + fechaI + "', 103) "
					+ "and at2.FechaInicio <= " + "CONVERT(datetime, '" + fechaF + "', 103) " + " and (ta.CodTipoAlarma = " + Convert.ToString(codTipos[0]);

				for (int i = 1 ; i < codTipos.Count ; i++)
					consulta2 = consulta2 + " or ta.CodTipoAlarma = " + Convert.ToString(codTipos[i]);
				listaplacas = "v.NroPlaca='" + placa[0] + "'";
				for (int j = 1 ; j < placa.Count ; j++)
				{
					listaplacas = listaplacas + " or v.NroPlaca='" + placa[j] + "'";
				}

				consulta2 = consulta2 + ") and (" + listaplacas + ")";

				string consulta3 = "group by am.CodAlarma, am.NombreAlarma,ta.Descripcion,ca.Descripcion,v.NroPlaca,at.FechaHora,am.CodTipoAlarma, "
				+ "ta.CodTipoAlarma, am.CodCategoria, am.Tiempo, am.Tiempo2, am.Velocidad,"
				+ "am.Velocidad2, am.Distancia, am.Distancia2, am.Temperatura, am.Temperatura2, at.Estado, "
				+ "ti.Velocidad, ti.VoltajeBateria, ti.Kilometraje, ti.Temperatura, ti.Longitud, ti.Latitud, at2.FechaInicio,at2.FechaFin, at.CodigoGEO"
				+ " ORDER BY at.FechaHora DESC";

				//sortExpresion = "at.FechaHora";
				//sortDireccion = "DESC";
				consulta = consulta + consulta2 + consulta3;
				rptAlarmas = db.Database.SqlQuery<AlarmaRptDet>(consulta).ToList();

			}
			catch (Exception ex)
			{
				throw new Exception("Error en el formulario", ex);
			}

			return rptAlarmas;
		}

		public List<AlarmaRptDet> GetAllRptAlarmas(List<int> codTipos, string nit, string fechaI, string horaI, string fechaF, string horaF, string sortExpresion, string sortDireccion)
		{
			List<AlarmaRptDet> rptAlarmas = new List<AlarmaRptDet>();

			TiposAlarmaDistinct = new List<TipoAlarmaCboDet>();
			CategsAlarmaDistinct = new List<CategAlarmaCboDet>();
			VehiculosDistinct = new List<VehiculoCboDet>();

			try
			{
				string fechaInicio, fechaFin;
				Concatenar(fechaI, horaI, fechaF, horaF, out fechaInicio, out fechaFin);

				string consulta = rptAlarmaSelect + rptAlarmaFrom;

				string consulta2 = "and ti.FechaGPS >= CONVERT(datetime, '" + fechaInicio + "', 103) "
					+ "and ti.FechaGPS <= CONVERT(datetime, '" + fechaFin + "', 103) "
					+ "and (ta.CodTipoAlarma = " + Convert.ToString(codTipos[0]);

				for (int i = 1 ; i < codTipos.Count ; i++)
					consulta2 = consulta2 + " or ta.CodTipoAlarma = " + Convert.ToString(codTipos[i]);

				consulta2 = consulta2 + ") and s.NIT = '" + nit + "' ";

				string consulta3 = "ORDER BY " + sortExpresion + " " + sortDireccion;

				consulta = consulta + consulta2 + consulta3;
				rptAlarmas = db.Database.SqlQuery<AlarmaRptDet>(consulta).ToList();

				consulta = "SELECT DISTINCT ta.CodTipoAlarma, ta.Descripcion ";
				consulta = consulta + rptAlarmaFrom + consulta2;
				TiposAlarmaDistinct = db.Database.SqlQuery<TipoAlarmaCboDet>(consulta).ToList();

				consulta = "SELECT DISTINCT ca.CodCategoria, ca.Descripcion ";
				consulta = consulta + rptAlarmaFrom + consulta2;
				CategsAlarmaDistinct = db.Database.SqlQuery<CategAlarmaCboDet>(consulta).ToList();

				consulta = "SELECT DISTINCT v.NroPlaca ";
				consulta = consulta + rptAlarmaFrom + consulta2;
				VehiculosDistinct = db.Database.SqlQuery<VehiculoCboDet>(consulta).ToList();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return rptAlarmas;
		}

		public List<AlarmaRptDet> GetAllSARptAlarmas(List<int> codTipos, string fechaI, string horaI, string fechaF, string horaF, string sortExpresion, string sortDireccion)
		{
			List<AlarmaRptDet> rptAlarmas = new List<AlarmaRptDet>();

			TiposAlarmaDistinct = new List<TipoAlarmaCboDet>();
			CategsAlarmaDistinct = new List<CategAlarmaCboDet>();
			VehiculosDistinct = new List<VehiculoCboDet>();

			try
			{
				string fechaInicio, fechaFin;
				Concatenar(fechaI, horaI, fechaF, horaF, out fechaInicio, out fechaFin);

				string consulta = rptAlarmaSelect + rptAlarmaFrom;

				string consulta2 = "and ti.FechaGPS >= CONVERT(datetime, '" + fechaInicio + "', 103) "
					+ "and ti.FechaGPS <= CONVERT(datetime, '" + fechaFin + "', 103) "
					+ "and (ta.CodTipoAlarma = " + Convert.ToString(codTipos[0]);

				for (int i = 1 ; i < codTipos.Count ; i++)
					consulta2 = consulta2 + " or ta.CodTipoAlarma = " + Convert.ToString(codTipos[i]);

				string consulta3 = ") ORDER BY " + sortExpresion + " " + sortDireccion;

				consulta = consulta + consulta2 + consulta3;
				rptAlarmas = db.Database.SqlQuery<AlarmaRptDet>(consulta).ToList();

				consulta = "SELECT DISTINCT ta.CodTipoAlarma, ta.Descripcion ";
				consulta = consulta + rptAlarmaFrom + consulta2 + ")";
				TiposAlarmaDistinct = db.Database.SqlQuery<TipoAlarmaCboDet>(consulta).ToList();

				consulta = "SELECT DISTINCT ca.CodCategoria, ca.Descripcion ";
				consulta = consulta + rptAlarmaFrom + consulta2 + ")";
				CategsAlarmaDistinct = db.Database.SqlQuery<CategAlarmaCboDet>(consulta).ToList();

				consulta = "SELECT DISTINCT v.NroPlaca ";
				consulta = consulta + rptAlarmaFrom + consulta2 + ")";
				VehiculosDistinct = db.Database.SqlQuery<VehiculoCboDet>(consulta).ToList();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return rptAlarmas;
		}
		public List<sp_ReporteEncendidoApagado_Result> getListaEncendidoApagado(string placas, DateTime from, DateTime to)
		{
			var list = db.sp_ReporteEncendidoApagado(placas, from, to);
			return list.ToList();
		}
		public List<AlarmaRptDet> GetFltRptAlarmas(List<string> tipos, List<string> categs, List<string> vehs, string nit, string fechaI, string horaI, string fechaF, string horaF, string sortExpresion, string sortDireccion)
		{
			List<AlarmaRptDet> rptAlarmas = new List<AlarmaRptDet>();

			try
			{
				string fechaInicio, fechaFin;
				Concatenar(fechaI, horaI, fechaF, horaF, out fechaInicio, out fechaFin);

				string consulta = rptAlarmaSelect + rptAlarmaFrom;

				string consulta2 = "and ti.FechaGPS >= CONVERT(datetime, '" + fechaInicio + "', 103) "
					+ "and ti.FechaGPS <= CONVERT(datetime, '" + fechaFin + "', 103) "
					+ "and (ta.CodTipoAlarma = " + tipos[0];

				for (int i = 1 ; i < tipos.Count ; i++)
					consulta2 = consulta2 + " or ta.CodTipoAlarma = " + tipos[i];

				consulta2 = consulta2 + ") and (ca.CodCategoria = " + categs[0];

				for (int i = 1 ; i < categs.Count ; i++)
					consulta2 = consulta2 + " or ca.CodCategoria = " + categs[i];

				consulta2 = consulta2 + ") and (v.NroPlaca = " + vehs[0];

				for (int i = 1 ; i < vehs.Count ; i++)
					consulta2 = consulta2 + " or v.NroPlaca = " + vehs[i];

				consulta = consulta + consulta2 + ") and s.NIT = '" + nit
					+ "' ORDER BY " + sortExpresion + " " + sortDireccion;

				rptAlarmas = db.Database.SqlQuery<AlarmaRptDet>(consulta).ToList();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return rptAlarmas;
		}

		public List<AlarmaRptDet> GetFltSARptAlarmas(List<string> tipos, List<string> categs, List<string> vehs, string fechaI, string horaI, string fechaF, string horaF, string sortExpresion, string sortDireccion)
		{
			List<AlarmaRptDet> rptAlarmas = new List<AlarmaRptDet>();

			try
			{
				string fechaInicio, fechaFin;
				Concatenar(fechaI, horaI, fechaF, horaF, out fechaInicio, out fechaFin);

				string consulta = rptAlarmaSelect + rptAlarmaFrom;

				string consulta2 = "and ti.FechaGPS >= CONVERT(datetime, '" + fechaInicio + "', 103) "
					+ "and ti.FechaGPS <= CONVERT(datetime, '" + fechaFin + "', 103) "
					+ "and (ta.CodTipoAlarma = " + tipos[0];

				for (int i = 1 ; i < tipos.Count ; i++)
					consulta2 = consulta2 + " or ta.CodTipoAlarma = " + tipos[i];

				consulta2 = consulta2 + ") and (ca.CodCategoria = " + categs[0];

				for (int i = 1 ; i < categs.Count ; i++)
					consulta2 = consulta2 + " or ca.CodCategoria = " + categs[i];

				consulta2 = consulta2 + ") and (v.NroPlaca = '" + vehs[0] + "'";

				for (int i = 1 ; i < vehs.Count ; i++)
					consulta2 = consulta2 + " or v.NroPlaca = '" + vehs[i] + "'";

				consulta = consulta + consulta2 + ") ORDER BY " + sortExpresion + " " + sortDireccion;

				rptAlarmas = db.Database.SqlQuery<AlarmaRptDet>(consulta).ToList();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return rptAlarmas;
		}

		private static void Concatenar(string fini, string hini, string ffin, string hfin, out string rfini, out string rffin)
		{
			rfini = fini + " " + hini;
			rffin = ffin + " " + hfin;
		}

		/// <summary>
		/// Reporte Dentenciones optimizado GM
		/// </summary>
		/// <param name="imei"></param>
		/// <param name="fini"></param>
		/// <param name="hini"></param>
		/// <param name="ffin"></param>
		/// <param name="hfin"></param>
		/// <returns></returns>
		public List<RptDetencionesViewModel> ListarReporteDetenciones(string placa, string fini, string hini, string ffin, string hfin, int tiporel, int tiempodet)
		{
			string imei = db.Seguimiento.Where(s => s.NroPlaca == placa && s.estado == true).FirstOrDefault().IMEI;
			List<RptDetencionesViewModel> lista = new List<RptDetencionesViewModel>();
			string rfini;
			string rffin;
			Concatenar(fini, hini, ffin, hfin, out rfini, out rffin);
			DateTime f1 = Convert.ToDateTime(rfini);
			DateTime f2 = Convert.ToDateTime(rffin);
			Nullable<System.DateTime> fechagpsini = null;
			Nullable<System.DateTime> fechagpsfin = null;
			string lat = String.Empty;
			string lng = String.Empty;
			string direcciones = String.Empty;
			double cantminutos = 0;
			var collection = db.sp_ListarReporteDetencionesOptimizado(imei, rfini, rffin).ToList();
			//var collection = bd.Trama.Where(t => t.IMEI == imei && t.FechaGPS >= f1 && t.FechaGPS <= f2).OrderBy(t => t.FechaGPS).ToList();
			int c = collection.Count - 1;
			int indice = 1;
			int cont = 0;
			foreach (var item in collection)
			{
				if (item.Velocidad < 1)
				{
					cont++;
					if (cont == 1)
					{
						fechagpsini = item.FechaGPS;
						lat = item.Latitud.ToString();
						lng = item.Longitud.ToString();
					}
					if (cont > 1)
					{
						//DateTime fr = item.FechaGPS;
						cantminutos = item.FechaGPS.Subtract(fechagpsini.Value).TotalMinutes;
						fechagpsfin = item.FechaGPS;
						direcciones = item.direcciones;
					}
				}
				else
				{
					if (fechagpsini != null && fechagpsfin != null)
					{
						RptDetencionesViewModel abc = new RptDetencionesViewModel
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
							Vehiculo = placa
						};
						if (abc.Tiempo >= 1)
						{
							lista.Add(abc);
						}
					}
					cont = 0;
					cantminutos = 0;
					fechagpsini = null;
					fechagpsfin = null;
				}

				if (indice == c)
				{
					if (fechagpsini != null && fechagpsfin != null)
					{
						RptDetencionesViewModel abc = new RptDetencionesViewModel
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
							Vehiculo = placa
						};
						if (abc.Tiempo >= 1)
						{
							lista.Add(abc);
						}

					}
				}
				indice++;
			}
			if (lista.Count > 0)
			{
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
			}


			return lista;
		}

		public List<RptDetencionesViewModel> ListarReporteDetenciones(List<string> placa, string fini, string hini, string ffin, string hfin, int tiporel, int tiempodet)
		{
			//var  imei = bd.Seguimiento.Where(s => placa.Contains(s.NroPlaca) && s.estado == true ).ToList().IMEI;

			var listimeis = db.Seguimiento.Where(x => placa.Contains(x.NroPlaca) && x.estado == true).Select(x => x.IMEI).ToList();
			List<RptDetencionesViewModel> lista = new List<RptDetencionesViewModel>();
			string rfini;
			string rffin;
			Concatenar(fini, hini, ffin, hfin, out rfini, out rffin);
			DateTime f1 = Convert.ToDateTime(rfini);
			DateTime f2 = Convert.ToDateTime(rffin);
			Nullable<System.DateTime> fechagpsini = null;
			Nullable<System.DateTime> fechagpsfin = null;
			string lat = String.Empty;
			string lng = String.Empty;
			string direcciones = String.Empty;
			double cantminutos = 0;
			string placas = "";
			foreach (var item in listimeis)
			{
				placas = placas + "'" + item + "',";
			}
			if (placas.Length > 1)
			{
				placas = placas.Substring(0, placas.Length - 1);
			}
			var collection = db.sp_ListarReporteDetencionesOptimizado(placas, rfini, rffin).ToList();
			//var collection = bd.Trama.Where(t => t.IMEI == imei && t.FechaGPS >= f1 && t.FechaGPS <= f2).OrderBy(t => t.FechaGPS).ToList();
			int c = collection.Count - 1;
			int indice = 1;
			int cont = 0;
			foreach (var item in collection)
			{
				if (item.Velocidad < 1)
				{
					cont++;
					if (cont == 1)
					{
						fechagpsini = item.FechaGPS;
						lat = item.Latitud.ToString();
						lng = item.Longitud.ToString();
					}
					if (cont > 1)
					{
						//DateTime fr = item.FechaGPS;
						cantminutos = item.FechaGPS.Subtract(fechagpsini.Value).TotalMinutes;
						fechagpsfin = item.FechaGPS;
						direcciones = item.direcciones;
					}
				}
				else
				{
					if (fechagpsini != null && fechagpsfin != null)
					{
						RptDetencionesViewModel abc = new RptDetencionesViewModel
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
							Vehiculo = ""
						};
						if (abc.Tiempo >= 1)
						{
							lista.Add(abc);
						}
					}
					cont = 0;
					cantminutos = 0;
					fechagpsini = null;
					fechagpsfin = null;
				}

				if (indice == c)
				{
					if (fechagpsini != null && fechagpsfin != null)
					{
						RptDetencionesViewModel abc = new RptDetencionesViewModel
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
							Vehiculo = "ass"
						};
						if (abc.Tiempo >= 1)
						{
							lista.Add(abc);
						}

					}
				}
				indice++;
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

		public List<EstadoPuertaRptDet> ListarReporteEstadoPuerta(string placa, string fini, string hini, string ffin, string hfin, int estado)
		{
			string imei = db.Seguimiento.Where(s => s.NroPlaca == placa && s.estado == true).FirstOrDefault().IMEI;
			List<EstadoPuertaRptDet> lista = new List<EstadoPuertaRptDet>();
			string rfini;
			string rffin;
			Concatenar(fini, hini, ffin, hfin, out rfini, out rffin);
			DateTime f2 = Convert.ToDateTime(rffin);
			double cantminutos = 0;
			DateTime fechaapertura = new DateTime();
			var collection = db.sp_ListarReporteEPuertaOptimizado(imei, rfini, rffin).ToList();
			int indice = 1;
			int c = collection.Count - 1;
			EstadoPuertaRptDet eprd = new EstadoPuertaRptDet();
			foreach (var item in collection)
			{

				if (item.TipoMensaje == "GTDIS")
				{
					if (item.EstadoPuerta == false) // si estadopuerta = 0 
					{
						eprd.Conductor = "No definido";
						eprd.FechaApertura = Convert.ToString(item.FechaGPS);
						eprd.Geocerca = "No encontrada";
						eprd.IDReporte = 1;
						eprd.LatitudA = item.Latitud;
						eprd.LongitudA = item.Longitud;
						eprd.UbicacionA = item.direcciones;
						eprd.Vehiculo = placa;
						fechaapertura = Convert.ToDateTime(item.FechaGPS);
						eprd.FechaCierre = "0";
					}
					else //si estado puerta = 1
					{
						if (eprd.Vehiculo != null)
						{
							eprd.FechaCierre = Convert.ToString(item.FechaGPS);
							cantminutos = item.FechaGPS.Subtract(fechaapertura).TotalMinutes;

							eprd.Tiempo = Convert.ToString(Math.Round(cantminutos, 0)) + " min";
							if (cantminutos >= 1)
							{
								lista.Add(eprd);
							}
							eprd = new EstadoPuertaRptDet();
							cantminutos = 0;
							fechaapertura = new DateTime();
						}
					}
				}
				if (indice == c)
				{
					if (eprd.FechaCierre == "0")
					{
						eprd.FechaCierre = " -- ";
						cantminutos = f2.Subtract(fechaapertura).TotalMinutes;
						eprd.Tiempo = "-- min";
						lista.Add(eprd);
						eprd = new EstadoPuertaRptDet();
						cantminutos = 0;
						fechaapertura = new DateTime();
					}
				}
				indice++;
			}
			return lista;
		}
		public List<EstadoPuertaRptDet> ListarReporteEstadoPuerta1(string placa, string fini, string hini, string ffin, string hfin, int estado)
		{
			string imei = db.Seguimiento.Where(s => s.NroPlaca == placa && s.estado == true).FirstOrDefault().IMEI;
			List<EstadoPuertaRptDet> lista = new List<EstadoPuertaRptDet>();
			string rfini;
			string rffin;
			Concatenar(fini, hini, ffin, hfin, out rfini, out rffin);
			DateTime f2 = Convert.ToDateTime(rffin);


			var collection = db.sp_ListarReporteEPuertaOptimizado(imei, rfini, rffin).ToList();
			int indice = 1;
			int c = collection.Count - 1;
			int a1 = -1;
			sp_ListarReporteEPuertaOptimizado_Result ant = null;

			foreach (var item in collection)
			{
				if (a1 == -1)
				{
					ant = item;
					//eprd=new EstadoPuertaRptDet(){
					//	Conductor="No asignado",
					//	FechaApertura=item.FechaGPS,
					//	LatitudA=item.Latitud,
					//	LongitudA=item.Longitud,
					//	 Vehiculo=


					//}
				}
			}
			//{

			//	if (item.TipoMensaje == "GTDIS")
			//	{
			//		if (item.EstadoPuerta == false) // si estadopuerta = 0 
			//		{
			//			eprd.Conductor = "No definido";
			//			eprd.FechaApertura = Convert.ToString(item.FechaGPS);
			//			eprd.Geocerca = "No encontrada";
			//			eprd.IDReporte = 1;
			//			eprd.LatitudA = item.Latitud;
			//			eprd.LongitudA = item.Longitud;
			//			eprd.UbicacionA = item.direcciones;
			//			eprd.Vehiculo = placa;
			//			fechaapertura = Convert.ToDateTime(item.FechaGPS);
			//			eprd.FechaCierre = "0";
			//		}
			//		else //si estado puerta = 1
			//		{
			//			if (eprd.Vehiculo != null)
			//			{
			//				eprd.FechaCierre = Convert.ToString(item.FechaGPS);
			//				cantminutos = item.FechaGPS.Subtract(fechaapertura).TotalMinutes;

			//				eprd.Tiempo = Convert.ToString(Math.Round(cantminutos, 0)) + " min";
			//				if (cantminutos >= 1)
			//				{
			//					lista.Add(eprd);
			//				}
			//				eprd = new EstadoPuertaRptDet();
			//				cantminutos = 0;
			//				fechaapertura = new DateTime();
			//			}
			//		}
			//	}
			//	if (indice == c)
			//	{
			//		if (eprd.FechaCierre == "0")
			//		{
			//			eprd.FechaCierre = " -- ";
			//			cantminutos = f2.Subtract(fechaapertura).TotalMinutes;
			//			eprd.Tiempo = "-- min";
			//			lista.Add(eprd);
			//			eprd = new EstadoPuertaRptDet();
			//			cantminutos = 0;
			//			fechaapertura = new DateTime();
			//		}
			//	}
			//	indice++;
			//}
			return lista;
		}

	}
}