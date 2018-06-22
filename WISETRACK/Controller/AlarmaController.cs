using System;
using System.Collections.Generic;
using System.Linq;
using WISETRACK.Datos;
using WISETRACK.Datos.Serializable;
using WISETRACK.Models;

namespace WISETRACK.Controller
{
	public class AlarmaController
	{
		WISETRACKEntities db = new WISETRACKEntities();
		List<AlarmaDetalle> alarmas = new List<AlarmaDetalle>();

		public List<AlarmaDetalle> GetAll()
		{
			alarmas = (from a in db.Alarma
					   join
						   em in db.Empresa on a.NIT equals em.NIT
					   select new AlarmaDetalle
					   {
						   CodAlarma = a.CodAlarma,
						   razonSocial = em.RazonSocial,
						   NombreAlarma = a.NombreAlarma,
						   TipoAlarma = a.TipoAlarma.Descripcion,
						   Estado = (a.Activa.Value == true ? "ACTIVA" : "NO ACTIVA"),
						   UsuaReg = a.UsuaReg,
						   FechaReg = a.FechaReg
					   }
					   ).ToList();

			return alarmas;
		}
		public List<AlarmaDetalle> GetAll(bool p)
		{
			alarmas = (from a in db.Alarma
					   join
						   em in db.Empresa on a.NIT equals em.NIT
					   where a.Activa == p
					   select new AlarmaDetalle
					   {
						   CodAlarma = a.CodAlarma,
						   razonSocial = em.RazonSocial,
						   NombreAlarma = a.NombreAlarma,
						   TipoAlarma = a.TipoAlarma.Descripcion,
						   Estado = (a.Activa.Value == true ? "ACTIVA" : "NO ACTIVA"),
						   UsuaReg = a.UsuaReg,
						   FechaReg = a.FechaReg
					   }
					   ).ToList();

			return alarmas;
		}
		public List<AlarmaDetalle> GetAll(string nit)
		{
			alarmas = (from a in db.Alarma
					   join
						   em in db.Empresa on a.NIT equals em.NIT
					   where a.NIT == nit
					   select new AlarmaDetalle
					   {

						   CodAlarma = a.CodAlarma,
						   razonSocial = em.RazonSocial,
						   NombreAlarma = a.NombreAlarma,
						   TipoAlarma = a.TipoAlarma.Descripcion,
						   Estado = (a.Activa.Value == true ? "ACTIVA" : "NO ACTIVA"),
						   UsuaReg = a.UsuaReg,
						   FechaReg = a.FechaReg
					   }
					   ).ToList();

			return alarmas;
		}

		internal object GetAll(string nit, bool p)
		{
			alarmas = (from a in db.Alarma
					   join
						   em in db.Empresa on a.NIT equals em.NIT
					   where a.NIT == nit && a.Activa == p
					   select new AlarmaDetalle
					   {
						   CodAlarma = a.CodAlarma,
						   razonSocial = em.RazonSocial,
						   NombreAlarma = a.NombreAlarma,
						   TipoAlarma = a.TipoAlarma.Descripcion,
						   Estado = (a.Activa.Value == true ? "ACTIVA" : "NO ACTIVA"),
						   UsuaReg = a.UsuaReg,
						   FechaReg = a.FechaReg
					   }
					 ).ToList();

			return alarmas;
		}
		public Alarma Get(int codigo)
		{
			Alarma al = new Alarma();
			al = (from a in db.Alarma
				  where a.CodAlarma == codigo
				  select a).FirstOrDefault();
			return al;
		}

		public List<TipoAlarma> GetAllTipos()
		{
			return (from ta in db.TipoAlarma
					select ta).ToList();
		}
		public List<TipoAlarmas> GetAllTiposs()
		{
			var tolist = new List<TipoAlarmas>();
			var list = (from ta in db.TipoAlarma
						select ta).ToList();
			foreach (var item in list)
			{
				tolist.Add(toTipoAlarma(item));
			}
			return tolist;


		}

		private TipoAlarmas toTipoAlarma(TipoAlarma item)
		{
			return new TipoAlarmas()
			{
				CodTipoAlarma = item.CodTipoAlarma,
				Descripcion = item.Descripcion,
				FechaReg = item.FechaReg,
				UsuaModif = item.UsuaModif,
				UsuaReg = item.UsuaReg

			};
		}

		public List<CategoriaAlarma> GetAllCategorias()
		{
			return (from ca in db.CategoriaAlarma
					select ca).ToList();
		}

		public List<Geocerca> GetGeocercasInicio(string nit)
		{
			return (from g in db.Geocerca
					where g.NIT == nit
					select g).ToList();
		}

		public void AddAlarma(Alarma alarma, List<string> nroPlacas, List<int> codGeos, int codGeoInicio, List<string> ciDest, string userName)
		{
			using (var transation = db.Database.BeginTransaction())
			{
				try
				{
					db.Alarma.Add(alarma);
					db.SaveChanges();

					foreach (var nroPlaca in nroPlacas)
					{
						AlarmaVehiculo alarmaVehiculo = new AlarmaVehiculo
						{
							CodAlarma = alarma.CodAlarma,
							NroPlaca = nroPlaca,
							EstadoReq = true,
							Cantidad = 0,
							UsuaReg = userName,
							FechaReg = DateTime.Now
						};

						db.AlarmaVehiculo.Add(alarmaVehiculo);
					}

					foreach (var codGeo in codGeos)
					{
						AlarmaGeocerca alarmaGeo = new AlarmaGeocerca
						{
							CodAlarma = alarma.CodAlarma,
							CodigoGEO = codGeo,
							Inicio = false,
							UsuaReg = userName,
							FechaReg = DateTime.Now
						};

						if (codGeoInicio > 0)
						{
							if (codGeo == codGeoInicio)
								alarmaGeo.Inicio = true;
						}

						db.AlarmaGeocerca.Add(alarmaGeo);
					}

					foreach (var ci in ciDest)
					{
						AlarmaDestinatarios alarmaDest = new AlarmaDestinatarios
						{
							CodAlarma = alarma.CodAlarma,
							CI = ci,
							UsuaReg = userName,
							FechaReg = DateTime.Now
						};

						db.AlarmaDestinatarios.Add(alarmaDest);
					}

					db.SaveChanges();
					transation.Commit();
				}
				catch (Exception)
				{
					transation.Rollback();

				}

			}

		}

		public void Eliminar(int codigo)
		{
			var alarma = db.Alarma.Find(codigo);

			db.Database.ExecuteSqlCommand("DELETE FROM dbo.Alerta WHERE CodAlarma = " + Convert.ToString(alarma.CodAlarma));
			db.Database.ExecuteSqlCommand("DELETE FROM dbo.AlarmaGeocerca WHERE CodAlarma = " + Convert.ToString(alarma.CodAlarma));
			db.Database.ExecuteSqlCommand("DELETE FROM dbo.AlarmaVehiculo WHERE CodAlarma = " + Convert.ToString(alarma.CodAlarma));
			db.Database.ExecuteSqlCommand("DELETE FROM dbo.AlarmaDestinatarios WHERE CodAlarma = " + Convert.ToString(alarma.CodAlarma));

			db.Alarma.Remove(alarma);
			db.SaveChanges();
		}

		public void Actualizar(Alarma alarma, string nombre, List<string> nroPlacas, List<int> codGeos, int codGeoInicio, List<string> ciDest, string userName)
		{
			Alarma al;
			using (var transation = db.Database.BeginTransaction())
			{
				try
				{

					alarma.NombreAlarma = nombre;

					DelAllAlarmaGeo(alarma.CodAlarma);
					DelAllAlarmaVeh(alarma.CodAlarma);
					DelAllAlarmaDest(alarma.CodAlarma);

					foreach (var nroPlaca in nroPlacas)
					{
						AlarmaVehiculo alarmaVehiculo = new AlarmaVehiculo
						{
							CodAlarma = alarma.CodAlarma,
							NroPlaca = nroPlaca,
							UsuaReg = userName,
							FechaReg = DateTime.Now
						};

						db.AlarmaVehiculo.Add(alarmaVehiculo);
					}
					db.SaveChanges();
					foreach (var codGeo in codGeos)
					{
						AlarmaGeocerca alarmaGeo = new AlarmaGeocerca
						{
							CodAlarma = alarma.CodAlarma,
							CodigoGEO = codGeo,
							UsuaReg = userName,
							FechaReg = DateTime.Now
						};

						if (codGeoInicio > 0)
						{
							if (codGeo == codGeoInicio)
								alarmaGeo.Inicio = true;
						}

						db.AlarmaGeocerca.Add(alarmaGeo);
					}
					db.SaveChanges();
					foreach (var ci in ciDest)
					{
						AlarmaDestinatarios alarmaDest = new AlarmaDestinatarios
						{
							CodAlarma = alarma.CodAlarma,
							CI = ci,
							UsuaReg = userName,
							FechaReg = DateTime.Now
						};

						db.AlarmaDestinatarios.Add(alarmaDest);
					}
					db.SaveChanges();
					al = (from s in db.Alarma where s.CodAlarma == alarma.CodAlarma select s).FirstOrDefault();
					if (al != null)
					{
						al.Activa = alarma.Activa;
						al.CantidadEnvio = alarma.CantidadEnvio;
						//al.CategoriaAlarma = alarma.CategoriaAlarma;
						al.Distancia = alarma.Distancia;
						al.Distancia2 = alarma.Distancia2;
						al.email = alarma.email;
						al.FechaHora = alarma.FechaHora;
						al.FechaHora2 = alarma.FechaHora2;
						al.FechaModif = DateTime.Now;
						al.IntervaloEnvio = alarma.IntervaloEnvio;
						al.NombreAlarma = alarma.NombreAlarma;
						al.Temperatura = alarma.Temperatura;
						al.Temperatura2 = alarma.Temperatura2;
						al.Tiempo = alarma.Tiempo;
						al.Tiempo2 = alarma.Tiempo2;
						al.TiempoEnvio = alarma.TiempoEnvio;
						al.UsuaModif = userName;
						al.Velocidad = alarma.Velocidad;
						al.Velocidad2 = alarma.Velocidad2;
					}

					db.SaveChanges();
					transation.Commit();
					//}
					//catch (DbEntityValidationException ex)
					//{
					//	foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
					//	{
					//		// Get entry

					//		DbEntityEntry entry = item.Entry;
					//		string entityTypeName = entry.Entity.GetType().Name;

					//		// Display or log error messages

					//		foreach (DbValidationError subItem in item.ValidationErrors)
					//		{
					//			string message = string.Format("Error '{0}' occurred in {1} at {2}",
					//					 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
					//			Console.WriteLine(message);
					//		}
					//	}
				}
				catch (Exception)
				{
					transation.Rollback();
					throw;
				}

			}
		}

		private void DelAllAlarmaGeo(int codigo)
		{
			var alarmasGeo = (from ag in db.AlarmaGeocerca
							  where ag.CodAlarma == codigo
							  select ag).ToList();

			foreach (var alarmaGeo in alarmasGeo)
				db.AlarmaGeocerca.Remove(alarmaGeo);

			db.SaveChanges();
		}

		private void DelAllAlarmaVeh(int codigo)
		{
			var alarmasVeh = (from av in db.AlarmaVehiculo
							  where av.CodAlarma == codigo
							  select av).ToList();

			foreach (var alarmaVeh in alarmasVeh)
				db.AlarmaVehiculo.Remove(alarmaVeh);

			db.SaveChanges();
		}

		private void DelAllAlarmaDest(int codigo)
		{
			var alarmasDest = (from ad in db.AlarmaDestinatarios
							   where ad.CodAlarma == codigo
							   select ad).ToList();

			foreach (var alarmaDest in alarmasDest)
				db.AlarmaDestinatarios.Remove(alarmaDest);

			db.SaveChanges();
		}

		public AlarmaGeocerca GetAlarmaGeocerca(int codigo, int codGeo)
		{
			return (from ag in db.AlarmaGeocerca
					where ag.CodAlarma == codigo && ag.CodigoGEO == codGeo
					select ag).SingleOrDefault();
		}

		public AlarmaVehiculo GetAlarmaVehiculo(int codigo, string nroPlaca)
		{
			return (from av in db.AlarmaVehiculo
					where av.CodAlarma == codigo && av.NroPlaca == nroPlaca
					select av).SingleOrDefault();
		}

		public AlarmaDestinatarios GetAlarmaDestinatario(int codigo, string ci)
		{
			return (from ad in db.AlarmaDestinatarios
					where ad.CodAlarma == codigo && ad.CI == ci
					select ad).SingleOrDefault();
		}

		public List<DestinatarioDetalle> GetAllDestinatarios(string nit)
		{
			var destinatarios = (from p in db.Persona
								 join ue in db.UsuarioEmpresa
								 on p.CI equals ue.CI
								 where ue.NIT == nit
								 select new DestinatarioDetalle
								 {
									 CI = p.CI,
									 NombreCompleto = p.Nombre + (p.ApellidoP != null ? " " + p.ApellidoP : "")
														+ (p.ApellidoM != null ? " " + p.ApellidoM : ""),
									 Email = p.Email,
									 Telefono = p.Telefono
								 }
								 ).ToList();
			return destinatarios;
		}

		public List<GeocercaDetalle> GetAllGeocercas(string nit)
		{
			var geocercas = (from g in db.Geocerca
							 where g.NIT == nit
							 select new GeocercaDetalle
							 {
								 CodigoGEO = g.CodigoGEO,
								 Descripcion = g.Descripcion,
								 Tipo = g.TipoGeocerca.Descripcion
							 }
							 ).ToList();

			return geocercas;
		}
		/// <summary>
		/// aqui se esta cambiando  la consulta
		/// </summary>
		/// <param name="nit"></param>
		/// <returns></returns>
		public List<VehConductorDetalle> GetAllVehiculos(string nit)
		{
			var vehiculos = (from v in db.Vehiculo
							 //join vc in db.VehiculoConductor
							 //on v.NroPlaca equals vc.NroPlaca

							 join s in db.Seguimiento
							 on v.NroPlaca equals s.NroPlaca
							 join ve in db.EmpresaVehiculo on v.NroPlaca equals ve.nroplaca
							 join em in db.Empresa on ve.nit equals em.NIT
							 where em.NIT == nit && s.NIT == nit
							 && s.estado == true
							 select new VehConductorDetalle
							 {
								 NroPlaca = v.NroPlaca,
								 Tipo = v.TipoVehiculo.Descripcion,
								 Marca = v.Marca.Descripcion,
								 Conductor = v.VehiculoConductor.FirstOrDefault() != null ? v.VehiculoConductor.FirstOrDefault().Persona.Nombre
												+ (v.VehiculoConductor.FirstOrDefault().Persona.ApellidoP != null ? " " + v.VehiculoConductor.FirstOrDefault().Persona.ApellidoP : "")
												+ (v.VehiculoConductor.FirstOrDefault().Persona.ApellidoM != null ? " " + v.VehiculoConductor.FirstOrDefault().Persona.ApellidoM : "") : "(No Asignado)"
							 }
							 ).ToList();

			return vehiculos;
		}

		public List<GeocercaDetalle> GetGeocercas(int codigo)
		{
			var geocercas = (from g in db.Geocerca
							 join ag in db.AlarmaGeocerca
							 on g.CodigoGEO equals ag.CodigoGEO
							 where ag.CodAlarma == codigo
							 select new GeocercaDetalle
							 {
								 CodigoGEO = g.CodigoGEO,
								 Descripcion = g.Descripcion,
								 Tipo = g.TipoGeocerca.Descripcion
							 }
							 ).ToList();

			return geocercas;
		}

		public List<VehConductorDetalle> GetVehiculos(int codigo)
		{
			var vehiculos = (from v in db.Vehiculo
							 join av in db.AlarmaVehiculo
							 on v.NroPlaca equals av.NroPlaca
							 where av.CodAlarma == codigo
							 select new VehConductorDetalle
							 {
								 NroPlaca = v.NroPlaca,
								 Tipo = v.TipoVehiculo.Descripcion,
								 Marca = v.Marca.Descripcion,
								 Conductor = v.VehiculoConductor.FirstOrDefault() != null ? v.VehiculoConductor.FirstOrDefault().Persona.Nombre
												+ (v.VehiculoConductor.FirstOrDefault().Persona.ApellidoP != null ? " " + v.VehiculoConductor.FirstOrDefault().Persona.ApellidoP : "")
												+ (v.VehiculoConductor.FirstOrDefault().Persona.ApellidoM != null ? " " + v.VehiculoConductor.FirstOrDefault().Persona.ApellidoM : "") : "(No Asignado)"
							 }
							 ).ToList();

			return vehiculos;
		}

		public List<DestinatarioDetalle> GetDestinatarios(int codigo)
		{
			var destinatarios = (from p in db.Persona
								 join ad in db.AlarmaDestinatarios
								 on p.CI equals ad.CI
								 where ad.CodAlarma == codigo
								 select new DestinatarioDetalle
								 {
									 CI = p.CI,
									 NombreCompleto = p.Nombre + (p.ApellidoP != null ? " " + p.ApellidoP : "")
														+ (p.ApellidoM != null ? " " + p.ApellidoM : ""),
									 Email = p.Email,
									 Telefono = p.Telefono
								 }
								 ).ToList();

			return destinatarios;
		}

		public List<VehiculoCboDet> GetAllVehiculos2()
		{
			return (from s in db.Seguimiento
					where s.estado.Value == true
					select new VehiculoCboDet
					{
						NroPlaca = s.NroPlaca
					}
					).ToList();
		}

		public List<VehiculoCboDet> GetAllVehiculos2(string nit)
		{
			return (from s in db.Seguimiento
					where s.estado.Value == true
					&& s.NIT == nit
					select new VehiculoCboDet
					{
						NroPlaca = s.NroPlaca
					}
					).ToList();
		}

	}
}