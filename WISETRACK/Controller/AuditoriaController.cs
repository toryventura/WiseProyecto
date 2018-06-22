using System;
using System.Collections.Generic;
using System.Linq;
using WISETRACK.Datos;
using WISETRACK.Datos.optimizado;

namespace WISETRACK.Controller
{
	public class AuditoriaController
	{
		WISETRACKEntities db = new WISETRACKEntities();

		public List<sp_listarVehiculosEmpresa_Result> comboVehiculo(string nit)
		{
			var nplaca = db.sp_listarVehiculosEmpresa(nit);
			return nplaca.ToList();
		}

		//Metodo usado para mostrar datos en el MAPA DE AUDITORIA 
		public List<sp_obtenerAuditoriaOptimizado_Result> obtenerAuditoriaOptimizada(string fini, string hini, string ffin, string hfin, string placa)
		{
			List<sp_obtenerAuditoriaOptimizado_Result> lista = new List<sp_obtenerAuditoriaOptimizado_Result>();
			string rfini;
			string rffin;
			Concatenar(fini, hini, ffin, hfin, out rfini, out rffin);
			lista = db.sp_obtenerAuditoriaOptimizado(rfini, rffin, placa).ToList();
			return lista;
		}

		private static void Concatenar(string fini, string hini, string ffin, string hfin, out string rfini, out string rffin)
		{
			rfini = fini + " " + hini;
			rffin = ffin + " " + hfin;
		}


		//-------------------------------REPORTES---------------------------------
		//Metodo usado para mostrar datos en el reporte consolidado de REPORTE DE AUDITORIA
		public List<RptAuditoriaViewModel> ReporteAuditoriaOptimizada(string fini, string hini, string ffin, string hfin, string placa)
		{
			List<RptAuditoriaViewModel> lista = new List<RptAuditoriaViewModel>();
			string rfini;
			string rffin;
			Concatenar(fini, hini, ffin, hfin, out rfini, out rffin);
			var collection = db.sp_ListarReporteAuditoriaOptimizado(rfini, rffin, placa).ToList();
			ControlarAperturayCierre(lista, collection);
			return lista;
		}

		private static void ControlarAperturayCierre(List<RptAuditoriaViewModel> lista, List<sp_ListarReporteAuditoriaOptimizado_Result> collection)
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
				RptAuditoriaViewModel rptlista = new RptAuditoriaViewModel();
				if (item.TipoMensaje == "GTDIS")
				{
					bandera = item.EstadoPuerta;
				}
				if (item.TipoMensaje != "GTDIS")
				{
					if (okgtdis == true)
					{
						if (item.Temperatura != -999.00)
						{
							rptlista.direcciones = item.direcciones;
							rptlista.EstadoPuerta = (bandera == true) ? "Cerrado" : "Abierto";
							rptlista.FechaGPS = item.FechaGPS;
							rptlista.IDButton = item.IDButton;
							rptlista.IMEI = item.IMEI;
							rptlista.Latitud = item.Latitud;
							rptlista.Longitud = item.Longitud;
							rptlista.Nombre = "No definido";
							rptlista.NroPlaca = item.NroPlaca;
							rptlista.Temperatura = Convert.ToString(item.Temperatura);
							rptlista.Velocidad = (float)item.Velocidad;
							rptlista.VoltajeBateria = (float)item.VoltajeBateria;
							lista.Add(rptlista);
						}
					}
					else
					{
						bandera = true;
						if (item.Temperatura != -999.00)
						{
							rptlista.direcciones = item.direcciones;
							rptlista.EstadoPuerta = (item.EstadoPuerta == true) ? "Cerrado" : "Abierto";
							rptlista.FechaGPS = item.FechaGPS;
							rptlista.IDButton = item.IDButton;
							rptlista.IMEI = item.IMEI;
							rptlista.Latitud = item.Latitud;
							rptlista.Longitud = item.Longitud;
							rptlista.Nombre = "No definido";
							rptlista.NroPlaca = item.NroPlaca;
							rptlista.Temperatura = Convert.ToString(item.Temperatura);
							rptlista.Velocidad = (float)item.Velocidad;
							rptlista.VoltajeBateria = (float)item.VoltajeBateria;
							lista.Add(rptlista);
						}
						else
						{
							rptlista.direcciones = item.direcciones;
							rptlista.EstadoPuerta = (item.EstadoPuerta == true) ? "Cerrado" : "Abierto";
							rptlista.FechaGPS = item.FechaGPS;
							rptlista.IDButton = item.IDButton;
							rptlista.IMEI = item.IMEI;
							rptlista.Latitud = item.Latitud;
							rptlista.Longitud = item.Longitud;
							rptlista.Nombre = "No definido";
							rptlista.NroPlaca = item.NroPlaca;
							rptlista.Temperatura = "S/Temp";
							rptlista.Velocidad = (float)item.Velocidad;
							rptlista.VoltajeBateria = (float)item.VoltajeBateria;
							lista.Add(rptlista);
						}
					}
				}
				cont++;
			}
		}
	}
}