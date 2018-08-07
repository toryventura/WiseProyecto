using System;
using System.Collections.Generic;
using System.Linq;
using WISETRACK.Datos;
using WISETRACK.Datos.optimizado;
using WISETRACK.Datos.Serializable;
using WISETRACK.Models;


namespace WISETRACK.Controller
{
	public class SeguimientoController
	{
		private WISETRACKEntities db = new WISETRACKEntities();

		public List<VehiculoEmpresas> comboVehiculo(string nit)
		{
			var nplaca = db.sp_listarVehiculosEmpresa(nit);
			List<VehiculoEmpresas> list = new List<VehiculoEmpresas>();
			foreach (var item in nplaca)
			{
				list.Add(toVehiculoEmpresas(item));
			}
			return list;
		}
		public List<VehiculoEmpresas> GetVehiculos(int tipo = 0, string condicion = "")
		{
			var nplaca = db.sp_listarVehculos(tipo, condicion);
			List<VehiculoEmpresas> list = new List<VehiculoEmpresas>();
			foreach (var item in nplaca)
			{
				list.Add(toVehiculo(item));
			}
			return list;
		}

		public VehiculoEmpresas toVehiculoEmpresas(sp_listarVehiculosEmpresa_Result o)
		{
			return new VehiculoEmpresas()
			{
				CodMarca = o.CodMarca,
				CodTipoV = o.CodTipoV,
				Estado = o.Estado.Value,
				FechaModif = o.FechaModif.Value,
				FechaReg = o.FechaReg,
				Foto = o.Foto,
				Marca = o.Marca,
				Modelo = o.Modelo,
				NroChasis = o.Modelo,
				NroMotor = o.NroMotor,
				NroPlaca = o.NroPlaca,
				Patente = o.Patente,
				UsuaModif = o.UsuaModif,
				UsuaReg = o.UsuaReg,
				year = o.Año.Value



			};
		}
		public VehiculoEmpresas toVehiculo(sp_listarVehculos_Result o)
		{
			return new VehiculoEmpresas()
			{
				CodMarca = o.CodMarca,
				CodTipoV = o.CodTipoV,
				Estado = o.Estado.Value,

				FechaReg = o.FechaReg,
				Foto = o.Foto,
				Modelo = o.Modelo,
				NroChasis = o.Modelo,
				NroMotor = o.NroMotor,
				NroPlaca = o.NroPlaca,
				Patente = o.Patente,

				UsuaReg = o.UsuaReg,
				year = o.Año.Value
			};
		}
		////Version para superadminaaaaaaaaaaaaaaaaaa
		public List<TempSerial> listar_ultima_posicion(string placa)
		{
			//List<sp_obtenerUltimaPosicion_Result> lista = new List<sp_obtenerUltimaPosicion_Result>();
			List<TempSerial> lista = new List<TempSerial>();
			var collection = db.sp_obtenerUltimaPosicion(placa);

			foreach (var item in collection)
			{
				TempSerial obt = new TempSerial
				{
					ID = item.ID,
					EstadoGPS = item.EstadoGPS,
					EstadoMotor = item.EstadoMotor,
					Velocidad = (float)item.Velocidad,
					Latitud = (float)item.Latitud,
					Longitud = (float)item.Longitud,
					FechaGPS = item.FechaGPS.Date.ToString(),
					NroPlaca = item.NroPlaca,
					Nombre = item.Nombre
				};
				lista.Add(obt);
			}
			return lista;
		}

		public List<sp_reporteSeguimiento_Result> exportarSeguimiento(string placa, string nit)
		{
			List<sp_reporteSeguimiento_Result> list = new List<sp_reporteSeguimiento_Result>();
			list = db.sp_reporteSeguimiento(placa, nit).ToList();
			return list;
		}

		/// <summary>
		/// SI ES UN USUARIO SA, MOSTRAR TODOS LOS VEHICULOS ASIGNADOS A UN GPS
		/// Metodo usado para asignar un vehiculo a un gps.
		/// </summary>
		/// <returns></returns>
		public List<clsSeguimiento> ListarAsignacionSeguimientoSA()
		{
			var li = (from av in db.Seguimiento
					  where av.estado == true
					  select new clsSeguimiento
					  {
						  CodSeguimiento = av.CodSeguimiento,
						  estado = (av.estado == true) ? "Activo" : "Finalizado",
						  FechaFin = (av.estado == true) ? "En linea" : av.FechaFin.ToString(),
						  FechaInicio = av.FechaInicio,
						  IMEI = av.IMEI,
						  Modelo = av.GPS.Modelo,
						  NroPlaca = av.NroPlaca,
						  RazonSocial = av.Empresa.RazonSocial
					  }).ToList();
			return li;
		}

		/// <summary>
		/// Asignar un gps a un vehiculo filtrado por el NIT de la Empresa
		/// Metodo usado para asignar un vehiculo a un gps.
		/// </summary>
		/// <param name="nit"></param>
		/// <returns></returns>
		public List<clsSeguimiento> listarAsignacionSeguimiento(string nit)
		{
			List<sp_ListarSeguimiento_Result> lista = new List<sp_ListarSeguimiento_Result>();
			List<clsSeguimiento> li = new List<clsSeguimiento>();
			lista = db.sp_ListarSeguimiento(nit).ToList();
			clsSeguimiento se;
			foreach (var item in lista)
			{
				se = new clsSeguimiento
				{
					CodSeguimiento = item.CodSeguimiento
				};
				var estado = item.estado;
				if (estado.Equals(true))
				{
					se.estado = "Activo";
					se.FechaFin = "En linea";
					se.FechaInicio = item.FechaInicio;
					se.IMEI = item.IMEI;
					se.Modelo = item.Modelo;
					se.NroPlaca = item.NroPlaca;
					se.RazonSocial = item.RazonSocial;
					li.Add(se);
				}
				//else
				//{
				//	se.estado = "Finalizado";
				//	se.FechaFin = item.FechaFin.ToString();
				//}
				//li.Add(se);
			}
			return li;
		}

		public List<sp_listar_imei_seguimiento_Result> listarIMEI(string nit)
		{
			var query = db.sp_listar_imei_seguimiento(nit);
			return query.ToList();
		}

		public List<sp_listar_placa_seguimiento_Result> listarPlaca(string nit)
		{
			var query = db.sp_listar_placa_seguimiento(nit);
			return query.ToList();
		}

		public Seguimiento listar(string id)
		{
			int cod = Convert.ToInt32(id);
			Seguimiento se = new Seguimiento();
			if (!String.IsNullOrEmpty(id))
			{
				var aux = db.Seguimiento.Where(gp => gp.CodSeguimiento == cod);
				se = aux.First();
			}
			return se;
		}

		public bool Add(Seguimiento se)
		{
			bool sw;
			try
			{
				db.Seguimiento.Add(se);
				db.SaveChanges();
				sw = true;

			}
			catch (Exception)
			{
				sw = false;
				throw;
			}
			return sw;
		}

		public bool update(Seguimiento se)
		{
			bool sw;

			using (var transaction = db.Database.BeginTransaction())
			{
				try
				{
					var aux = db.Seguimiento.Where(gp => gp.CodSeguimiento == se.CodSeguimiento);
					Seguimiento seg = aux.First();
					seg.estado = se.estado;
					seg.FechaInicio = se.FechaInicio;
					seg.FechaFin = se.FechaFin;
					seg.IMEI = se.IMEI;
					seg.NroPlaca = se.NroPlaca;
					seg.UsuaModif = se.UsuaModif;
					seg.FechaModif = se.FechaModif;
					db.SaveChanges();

					var tmp = db.TramaTemp1.Where(t => t.NroPlaca == seg.NroPlaca && t.NIT == seg.NIT).FirstOrDefault();
					if (tmp != null)
					{
						db.TramaTemp1.Remove(tmp);
						db.SaveChanges();
					}

					sw = true;
					transaction.Commit();
				}
				catch (Exception)
				{
					transaction.Rollback();
					sw = false;
				}

			}
			return sw;
		}

		public void remove(string id)
		{
			if (!String.IsNullOrEmpty(id))
			{
				int cod = Convert.ToInt32(id);
				Seguimiento se = db.Seguimiento.Find(cod);
				db.Seguimiento.Remove(se);
				db.SaveChanges();
			}

		}

		public List<Vehiculo> listarVehiculo()
		{
			var vehiculo = from v in db.Vehiculo select v;
			return vehiculo.ToList();
		}

		////VERSION PARA SUPER ADMIN
		public List<TramaTemp> listar_posicion_all()
		{
			var count_seguimiento = db.Seguimiento.Where(p => p.estado == true).Count();
			var collection = db.TramaTemp.Distinct().OrderByDescending(p => p.Nro).Take(count_seguimiento).ToList();
			return collection;
		}

		//Version para administradores
		public List<TramaTemp> listar_posicion_all(string nit)
		{
			//var placa = from p in db.Vehiculo.Where(s => s.idempresa == nit) select p.NroPlaca;
			//string[] result = placa.ToArray();
			//var collection = from m in db.TramaTemp.OrderByDescending(p=>p.Nro) where (result.Contains((string)m.NroPlaca)) select m;
			//return collection.ToList();
			var coount_seguimiento = db.Seguimiento.Where(p => p.estado == true && p.NIT == nit).Count();
			var collection = db.TramaTemp.Distinct().Where(p => p.NIT == nit).OrderByDescending(s => s.Nro).Take(coount_seguimiento).ToList();
			return collection;
		}

		//EMPIEZA TODO DE NUEVO EN SEGUIMIENTO

		public List<TramaTemp> cargar_grilla_ultima_posicion_placa(string placa)
		{
			var result = db.TramaTemp.Where(p => p.NroPlaca == placa).OrderByDescending(p => p.Nro).Take(1).ToList();
			return result;
		}

		//--------------------------------VERSION 25/07/2016-----------------------------------------------
		public List<TramaTempViewModel> ListarSeguimientoSistema()
		{
			var collection = db.TramaTemp1.ToList();
			List<TramaTempViewModel> lista = new List<TramaTempViewModel>();
			var ListV = db.Vehiculo.Where(s => s.Estado == true).ToList();
			foreach (var item in collection)
			{
				TramaTempViewModel obj = new TramaTempViewModel
				{
					Asimut = item.Asimut.Value,
					direcciones = item.direcciones,
					EstadoGPS = (item.EstadoGPS == 1) ? "Encendido" : "Apagado",
					EstadoMotor = item.EstadoMotor.Value,
					EstadoPuerta = (item.EstadoPuerta == true) ? "Cerrado" : "Abierto",
					FechaGPS = item.FechaGPS.Value,
					ID = item.ID,
					IDButton = item.IDButton,
					IMEI = item.IMEI,
					Kilometraje = item.Kilometraje.Value,
					Latitud = item.Latitud.Value,
					Longitud = item.Longitud.Value,
					NIT = item.NIT,
					Nombre = item.Nombre,
					Nro = item.Nro,
					NroPlaca = item.NroPlaca,
					Temperatura = item.Temperatura.Value,

					tipov = ListV.Count > 0 ? ListV.Where(v => v.NroPlaca == item.NroPlaca).Select(d => d.CodTipoV).FirstOrDefault() : 1,
					Velocidad = item.Velocidad.Value,
					VoltajeBateria = item.VoltajeBateria.Value,
					Patente = ListV.Count > 0 ? ListV.Where(v => v.NroPlaca == item.NroPlaca).Select(d => d.Patente).FirstOrDefault() : ""
				};
				lista.Add(obj);
			}
			return lista;
		}

		public List<TramaTempViewModel> ListarSeguimientoByNit(string nit)
		{
			var collection = db.TramaTemp1.Where(p => p.NIT == nit).ToList();
			List<TramaTempViewModel> lista = new List<TramaTempViewModel>();
			var listV = (from x in db.Seguimiento join s in db.Vehiculo on x.NroPlaca equals s.NroPlaca where x.NIT == nit select s
).ToList();
			foreach (var item in collection)
			{
				TramaTempViewModel obj = new TramaTempViewModel
				{
					Asimut = item.Asimut.Value,
					direcciones = item.direcciones,
					EstadoGPS = (item.EstadoGPS == 1) ? "Encendido" : "Apagado",
					EstadoMotor = item.EstadoMotor.Value,
					EstadoPuerta = (item.EstadoPuerta == true) ? "Cerrado" : "Abierto",
					FechaGPS = item.FechaGPS.Value,
					ID = item.ID,
					IDButton = item.IDButton,
					IMEI = item.IMEI,
					Kilometraje = item.Kilometraje.Value,
					Latitud = item.Latitud.Value,
					Longitud = item.Longitud.Value,
					NIT = item.NIT,
					Nombre = item.Nombre,
					Nro = item.Nro,
					NroPlaca = item.NroPlaca,
					Temperatura = item.Temperatura.Value
				};
				//var rtipov = db.Vehiculo.Where(v => v.NroPlaca == item.NroPlaca).Select(v => v.CodTipoV).SingleOrDefault();
				var rtipovv = listV.Where(g => g.NroPlaca == item.NroPlaca).Select(f => f.CodTipoV).FirstOrDefault();
				obj.tipov = listV.Count > 0 ? rtipovv : 1;
				obj.Velocidad = item.Velocidad.Value;
				obj.VoltajeBateria = item.VoltajeBateria.Value;
				//obj.Patente =listV!=null? db.Vehiculo.Where(e => e.NroPlaca == item.NroPlaca).Select(e => e.Patente).SingleOrDefault().ToString():"";
				obj.Patente = listV.Count > 0 ? listV.Where(h => h.NroPlaca == item.NroPlaca).Select(e => e.Patente).FirstOrDefault().ToString() : "";
				lista.Add(obj);
			}
			return lista;
		}

		public List<TramaTempViewModel> ListarSeguimientoByPlaca(string placa)
		{
			List<TramaTempViewModel> lista = new List<TramaTempViewModel>();
			try
			{
				var nroplaca = db.Vehiculo.Where(s => s.NroPlaca == placa || s.Patente == placa).SingleOrDefault().NroPlaca.ToString();
				var collection = db.TramaTemp1.Where(p => p.NroPlaca == nroplaca).SingleOrDefault();
				TramaTempViewModel obj = new TramaTempViewModel
				{
					Asimut = collection.Asimut.Value,
					direcciones = collection.direcciones,
					EstadoGPS = (collection.EstadoGPS == 1) ? "Encendido" : "Apagado",
					EstadoMotor = collection.EstadoMotor.Value,
					EstadoPuerta = (collection.EstadoPuerta == true) ? "Cerrado" : "Abierto",
					FechaGPS = collection.FechaGPS.Value,
					ID = collection.ID,
					IDButton = collection.IDButton,
					IMEI = collection.IMEI,
					Kilometraje = collection.Kilometraje.Value,
					Latitud = collection.Latitud.Value,
					Longitud = collection.Longitud.Value,
					NIT = collection.NIT,
					Nombre = collection.Nombre,
					Nro = collection.Nro,
					NroPlaca = collection.NroPlaca,
					Temperatura = collection.Temperatura.Value
				};
				var rtipov = db.Vehiculo.Where(v => v.NroPlaca == collection.NroPlaca).Select(v => v.CodTipoV).SingleOrDefault();
				obj.tipov = rtipov;
				obj.Velocidad = collection.Velocidad.Value;
				obj.VoltajeBateria = collection.VoltajeBateria.Value;
				obj.Patente = db.Vehiculo.Where(e => e.NroPlaca == collection.NroPlaca).Select(e => e.Patente).SingleOrDefault().ToString();
				lista.Add(obj);
			}
			catch (Exception)
			{
				lista = null;
				throw;
			}


			return lista;
		}


	}
}
