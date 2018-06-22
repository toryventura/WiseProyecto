using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WISETRACK.Datos;
using WISETRACK.Datos.Serializable;
using WISETRACK.Models;
using WS.ENTIDADES;

namespace WISETRACK.Controller
{
	public class ZonasController
	{
		WISETRACKEntities db = new WISETRACKEntities();

		private WISETRACK.Datos.Geocerca geo;
		private WISETRACK.Datos.PuntosGeocerca pgeo;
		private HomeController homeCtrl;

		public List<WISETRACK.Datos.TipoGeocerca> comboZonas(string nit)
		{
			var query = db.TipoGeocerca.Where(t => t.NIT == nit);
			return query.ToList();
		}

		public List<sp_ListarGeocerca_Result> grillaGeocerca(int cod)
		{
			var query = db.sp_ListarGeocerca(cod);
			return query.ToList();
		}

		public List<sp_ListarPuntosGeo_Result> grillaPuntosGeocerca(int cod)
		{
			var query = db.sp_ListarPuntosGeo(cod);
			return query.ToList();
		}

		public void guardarGeocerca(List<geocercaSerial> lista, List<puntosgeoSerial> listag)
		{
			try
			{
				homeCtrl = new HomeController();
				var user = HttpContext.Current.User.Identity.Name;
				string nit = homeCtrl.obtenerNit(user);

				foreach (var item in lista)
				{
					geo = new WISETRACK.Datos.Geocerca();
					geo.Descripcion = item.Descripcion;
					geo.ColorLimite = item.ColorLimite;
					geo.ColorRelleno = item.ColorRelleno;
					geo.CodTipoGEO = item.CodTipoGEO;
					geo.UsuaReg = user;
					geo.FechaReg = DateTime.Now;
					geo.NIT = nit;
					db.Geocerca.Add(geo);
					db.SaveChanges();
				}

				var id = db.Geocerca.Where(u => u.CodTipoGEO == geo.CodTipoGEO && u.ColorLimite == geo.ColorLimite && u.ColorRelleno == geo.ColorRelleno && u.NIT == geo.NIT && u.UsuaReg == geo.UsuaReg).Max(u => u.CodigoGEO);

				foreach (var item in listag)
				{
					pgeo = new WISETRACK.Datos.PuntosGeocerca();
					pgeo.CodigoGEO = id;
					pgeo.Latitud = item.Latitud;
					pgeo.Longitud = item.Longitud;
					pgeo.UsuaReg = user;
					pgeo.FechaReg = DateTime.Now;
					db.PuntosGeocerca.Add(pgeo);
					db.SaveChanges();
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public List<sp_reporteGeocerca_Result> exportarGeocerca(int cod)
		{
			List<sp_reporteGeocerca_Result> list = new List<sp_reporteGeocerca_Result>();
			string cod2 = Convert.ToString(cod);
			list = db.sp_reporteGeocerca(cod2).ToList();
			return list;
		}

		public List<GeocercaCboDet> GetAll()
		{
			return (from g in db.Geocerca
					select new GeocercaCboDet
					{
						CodigoGEO = g.CodigoGEO,
						Descripcion = g.Descripcion
					})
					.ToList();
		}

		public List<GeocercaCboDet> GetAll(string nit)
		{
			return (from g in db.Geocerca
					where g.NIT == nit
					select new GeocercaCboDet
					{
						CodigoGEO = g.CodigoGEO,
						Descripcion = g.Descripcion
					})
					.ToList();
		}


		public List<TipoGeocercaDetalle> GetTiposGeocercaSA()
		{
			try
			{
				return (from tg in db.TipoGeocerca
						join em in db.Empresa on tg.NIT equals em.NIT
						orderby em.RazonSocial descending
						select new TipoGeocercaDetalle
						{
							RazonSocial = em.RazonSocial,
							CodTipoGEO = tg.CodTipoGEO,
							Descripcion = tg.Descripcion

						}).ToList();
			}
			catch (Exception ex)
			{
				return null;
			}

		}

		/// <summary>
		/// Listar los Tipo de Geocercas filtradas por empresa.
		/// </summary>
		/// <param name="nit"></param>
		/// <returns></returns>
		public List<TipoGeocercaDetalle> GetTiposGeocerca(string nit)
		{
			try
			{
				return (from tg in db.TipoGeocerca
						join em in db.Empresa on tg.NIT equals em.NIT
						where em.NIT == nit
						orderby tg.CodTipoGEO ascending
						select new TipoGeocercaDetalle
						{
							RazonSocial = em.RazonSocial,
							CodTipoGEO = tg.CodTipoGEO,
							Descripcion = tg.Descripcion

						}).ToList();
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public bool AddTipoGeocerca(string descripcion, string nit, string userName)
		{
			var tipoZona = (from tg in db.TipoGeocerca
							where tg.Descripcion == descripcion
								&& tg.NIT == nit
							select tg).FirstOrDefault();

			if (tipoZona == null)
			{
				var max = db.TipoGeocerca.DefaultIfEmpty().Max(tg => tg == null ? 0 : tg.CodTipoGEO);

				WISETRACK.Datos.TipoGeocerca tipoGeocerca = new WISETRACK.Datos.TipoGeocerca()
				{
					CodTipoGEO = max + 1,
					Descripcion = descripcion,
					NIT = nit,
					UsuaReg = userName,
					FechaReg = DateTime.Now
				};

				db.TipoGeocerca.Add(tipoGeocerca);
				db.SaveChanges();

				return true;
			}
			return false;
		}

		public void ModificarGeocerca(string geocercaID, List<puntosgeoSerial> listag)
		{
			int id = Convert.ToInt32(geocercaID);
			homeCtrl = new HomeController();
			var user = HttpContext.Current.User.Identity.Name;
			string nit = homeCtrl.obtenerNit(user);

			using (var transact = db.Database.BeginTransaction())
			{
				try
				{
					db.PuntosGeocerca.RemoveRange(db.PuntosGeocerca.Where(u => u.CodigoGEO == id));
					db.SaveChanges();

					foreach (var item in listag)
					{
						pgeo = new WISETRACK.Datos.PuntosGeocerca();
						pgeo.CodigoGEO = id;
						pgeo.Latitud = item.Latitud;
						pgeo.Longitud = item.Longitud;
						pgeo.UsuaReg = user;
						pgeo.UsuaModif = user;
						pgeo.FechaReg = DateTime.Now;
						pgeo.FechaModif = DateTime.Now;
						db.PuntosGeocerca.Add(pgeo);
						db.SaveChanges();
					}
					transact.Commit();
				}

				catch (DbEntityValidationException dbEx)
				{
					transact.Rollback();
					foreach (var validationErrors in dbEx.EntityValidationErrors)
					{
						foreach (var validationError in validationErrors.ValidationErrors)
						{
							Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
						}
					}
				}
			}

		}
		public List<DetalleGeocerca> getGeocercasAll(string nit = "")
		{
			if (nit != "")
			{
				var lists = (from x in db.Geocerca join b in db.TipoGeocerca on x.CodTipoGEO equals b.CodTipoGEO where x.NIT == nit select new DetalleGeocerca() { ID = x.CodigoGEO, geocerca = x.Descripcion, zona = b.Descripcion, nit = x.NIT }).ToList();

				return lists;
			}
			else
			{
				var lists = (from x in db.Geocerca join b in db.TipoGeocerca on x.CodTipoGEO equals b.CodTipoGEO select new DetalleGeocerca() { ID = x.CodigoGEO, geocerca = x.Descripcion, zona = b.Descripcion, nit = x.NIT }).ToList();

				return lists;
			}

		}

		public bool EliminarGeocerca(int geocercaID)
		{
			bool sw = false;
			using (var transact = db.Database.BeginTransaction())
			{
				try
				{
					var rpg = db.PuntosGeocerca.Where(u => u.CodigoGEO == geocercaID);
					db.PuntosGeocerca.RemoveRange(rpg);
					db.SaveChanges();

					WISETRACK.Datos.Geocerca rgeocerca = db.Geocerca.Find(geocercaID);
					db.Geocerca.Remove(rgeocerca);
					db.SaveChanges();
					transact.Commit();
					sw = true;
				}
				catch (DbEntityValidationException dbEx)
				{
					transact.Rollback();
					sw = false;
					foreach (var validationErrors in dbEx.EntityValidationErrors)
					{
						foreach (var validationError in validationErrors.ValidationErrors)
						{
							Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
						}
					}
				}
			}
			return sw;
		}

		/// <summary>
		/// SI EL USUARIO ES SA, MOSTRAR TODAS LAS GEOCERCAS DE LAS EMPRESAS
		/// </summary>
		/// <returns></returns>
		public List<Geocerca_Delete> ListarGeocercaSA()
		{
			var collection = (from g in db.Geocerca
							  select new Geocerca_Delete
							  {
								  CodigoGEO = g.CodigoGEO,
								  Descripcion = g.Descripcion,
								  NIT = g.NIT,
								  Zona = g.TipoGeocerca.Descripcion
							  }).ToList();
			return collection;
		}

		/// <summary>
		/// Lista todas las geocercas que tiene una empresa.
		/// </summary>
		/// <param name="nit"></param>
		/// <returns></returns>
		public List<sp_ListarGeocercaEmpresa_Result> ListarGeocerca(string nit)
		{
			var query = db.sp_ListarGeocercaEmpresa(nit);
			return query.ToList();
		}

		public WISETRACK.Datos.Geocerca obtenerGeocerca(string codigoGeo)
		{
			int id = Convert.ToInt32(codigoGeo);
			var rg = db.Geocerca.Find(id);
			return rg;
		}

		public List<sp_ListarGeocercaAll_Result> VisualizarTodasGeocercas(string nit)
		{
			List<sp_ListarGeocercaAll_Result> lista = new List<sp_ListarGeocercaAll_Result>();
			if (nit.Equals("0"))
			{
				lista = db.sp_ListarGeocercaAll().ToList();
			}
			else
			{
				var result = db.sp_ListarGeocercaAll().ToList();
				result = result.Where(p => p.NIT == nit).ToList();
				lista = result;
			}
			return lista;
		}

		public TipoGeocercaDetalle ObtenerTipoGeocercaID(int codtipogeo)
		{
			try
			{
				var collection = from tg in db.TipoGeocerca
								 where tg.CodTipoGEO == codtipogeo
								 select new TipoGeocercaDetalle
								 {
									 CodTipoGEO = tg.CodTipoGEO,
									 Descripcion = tg.Descripcion,
									 RazonSocial = tg.Empresa.RazonSocial
								 };

				return collection.FirstOrDefault();
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public bool EditarTipoGeocerca(string descripcion, int id)
		{
			bool sw = false;
			try
			{
				var aux = db.TipoGeocerca.Where(gp => gp.CodTipoGEO == id);
				WISETRACK.Datos.TipoGeocerca tg = aux.First();
				tg.Descripcion = descripcion;
				db.SaveChanges();
				sw = true;
			}
			catch (Exception ex)
			{
				sw = false;
				throw;
			}
			return sw;
		}

	}
}