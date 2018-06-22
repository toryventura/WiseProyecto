using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WISETRACK.Datos;
using WISETRACK.Datos.Auxiliar;
using WISETRACK.Models;

namespace WISETRACK.Controller
{
	public class PersonaController
	{
		WISETRACKEntities db = new WISETRACKEntities();
		private HomeController homeCtrl = new HomeController();
		public List<TipoPersona> comboTipoPersona()
		{
			var tipo = from c in db.TipoPersona select c;
			return tipo.ToList();
		}

		public bool addUsuarioEmpresa(UsuarioEmpresa ue)
		{
			try
			{
				db.UsuarioEmpresa.Add(ue);
				db.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("controller negocio", ex);
			}
		}

		public void AddAllUsuarioEmpresa(string ci, string userName)
		{
			var nitEmpresas = (from e in db.Empresa
							   select e.NIT).ToList();

			foreach (var nit in nitEmpresas)
			{
				UsuarioEmpresa usuarioEmpresa = new UsuarioEmpresa
				{
					NIT = nit,
					CI = ci,
					UsuaReg = userName,
					FechaReg = DateTime.Now,
					Activo = false,
					Estado = true
				};

				db.UsuarioEmpresa.Add(usuarioEmpresa);
			}

			db.SaveChanges();
		}

		public bool add(Persona p)
		{
			try
			{
				db.Persona.Add(p);
				db.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("Personacontroller negocio", ex);
			}
		}

		public bool ActualizarDatosPersona(Persona p)
		{
			bool sw;
			try
			{

			}
			catch (Exception)
			{

				throw;
			}
			using (var transact = db.Database.BeginTransaction())
			{
				try
				{
					var aux = db.Persona.Find(p.CI);
					aux.ApellidoM = p.ApellidoM;
					aux.ApellidoP = p.ApellidoP;
					aux.CategoriaL = p.CategoriaL;
					aux.CodTipo = p.CodTipo;
					aux.Contacto = p.Contacto;
					aux.Direccion = p.Direccion;
					aux.Email = p.Email;
					aux.Estado = p.Estado;
					aux.FechaModif = p.FechaModif;
					aux.FechaVigDefL = p.FechaVigDefL;
					aux.FechaVigL = p.FechaVigL;
					aux.IdUser = p.IdUser;
					aux.Nombre = p.Nombre;
					aux.Telefono = p.Telefono;
					aux.TelfContacto = p.TelfContacto;
					aux.UsuaModif = p.UsuaModif;
					aux.Estado = true;
					db.SaveChanges();

					sw = true;
					transact.Commit();
				}
				catch (Exception ex)
				{

					transact.Rollback();
					throw new Exception("controller negocio", ex);
				}
			}
			return sw;
		}

		public List<sp_listarPersona_Result> cargarDetallePersona(string userName)
		{
			string ci = (from p in db.Persona
						 join u in db.AspNetUsers
						 on p.IdUser equals u.Id
						 where u.UserName == userName
						 select p.CI).SingleOrDefault();

			string nit = (from p in db.Persona
						  where p.CI == ci
						  select p.UsuarioEmpresa.FirstOrDefault()
						  ).SingleOrDefault().NIT;

			var collection = db.sp_listarPersona(nit);
			return collection.ToList();
		}

		public bool update(Persona p)
		{
			bool sw;
			try
			{
				var aux = db.Persona.Where(gp => gp.CI == p.CI);
				Persona per = aux.First();
				per.Nombre = p.Nombre;
				per.ApellidoP = p.ApellidoP;
				per.ApellidoM = p.ApellidoM;
				per.Direccion = p.Direccion;
				per.Telefono = p.Telefono;
				per.Email = p.Email;
				per.Contacto = p.Contacto;
				per.Estado = true;
				per.TelfContacto = p.TelfContacto;
				per.CodTipo = p.CodTipo;
				per.CategoriaL = p.CategoriaL;
				per.IdUser = p.IdUser;
				per.UsuaModif = p.UsuaModif;
				per.FechaModif = p.FechaModif;
				db.SaveChanges();
				sw = true;

			}
			catch (Exception ex)
			{
				sw = false;
				throw new Exception("controller negocio", ex);
			}
			return sw;
		}

		public void remove(string ci)
		{
			try
			{
				Persona p = db.Persona.Find(ci);
				p.Estado = false;
				db.SaveChanges();

				UsuarioEmpresa ue = db.UsuarioEmpresa.Find(ci);
				ue.Estado = false;
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception("controller negocio", ex);
			}
		}

		public bool RemoverLogicamente(string ci, string nit, string user)
		{
			bool sw = false;
			using (var transac = db.Database.BeginTransaction())
			{
				try
				{
					Persona p = db.Persona.Find(ci);

					p.IdUser = null;
					p.Estado = false;
					p.UsuaModif = user;
					p.FechaModif = DateTime.Now;
					db.SaveChanges();

					UsuarioEmpresa ue = db.UsuarioEmpresa.Where(e => e.CI == ci && e.NIT == nit).FirstOrDefault();
					ue.Estado = false;
					ue.UsuaModif = user;
					ue.FechaModif = DateTime.Now;
					db.SaveChanges();

					sw = true;
					transac.Commit();
				}
				catch (Exception ex)
				{
					sw = false;
					transac.Rollback();
					throw new Exception("controller negocio", ex);
				}
			}
			return sw;
		}

		public Persona listar(string ci)
		{
			try
			{
				Persona p = new Persona();
				if (!ci.Equals(""))
				{
					var aux = db.Persona.Where(gp => gp.CI == ci);
					p = aux.First();
				}
				return p;
			}
			catch (Exception e)
			{
				throw new Exception("controller negocio", e);
			}
		}

		public List<AspNetUsers> listarUsuario()
		{
			var collection = from u in db.AspNetUsers select u;
			return collection.ToList();
		}

		public List<ListarAbmPersona> GetAllSA()
		{
			return (from p in db.Persona
					join ue in db.UsuarioEmpresa
					on p.CI equals ue.CI
					join em in db.Empresa on ue.NIT equals em.NIT
					select new ListarAbmPersona
					{
						CI = p.CI,
						Nombre = p.Nombre,
						ApellidoP = p.ApellidoP,
						ApellidoM = p.ApellidoM,
						Direccion = p.Direccion,
						Telefono = p.Telefono,
						Email = p.Email,
						nit = ue.NIT,
						estado = (ue.Estado == true) ? "Activo" : "De Baja",
						razon_social = em.RazonSocial
					}).ToList();
		}

		/// <summary>
		/// Eliminar en 30 dias si no se utiliza
		/// Fecha 14/09/2016
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		public List<PersonaDetalle> GetAll(string userName)
		{
			string nit = homeCtrl.obtenerNit(userName);

			List<PersonaDetalle> personas = new List<PersonaDetalle>();

			if (!(userName.Equals("sistemas") && HttpContext.Current.User.IsInRole("SA")))
			{
				personas = (from p in db.Persona
							join ue in db.UsuarioEmpresa on
							p.CI equals ue.CI
							where ue.NIT == nit && p.CI != "1" && ue.Estado == true
							select new PersonaDetalle
							{
								CI = p.CI,
								Nombre = p.Nombre,
								ApellidoP = p.ApellidoP,
								ApellidoM = p.ApellidoM,
								Direccion = p.Direccion,
								Telefono = p.Telefono,
								Email = p.Email,
								FechaReg = p.FechaReg,
								UserName = p.UsuaReg
							}
							).ToList();
			}
			else
			{
				personas = (from p in db.Persona
							join ue in db.UsuarioEmpresa on
							p.CI equals ue.CI
							where ue.NIT == nit && ue.Estado == true
							select new PersonaDetalle
							{
								CI = p.CI,
								Nombre = p.Nombre,
								ApellidoP = p.ApellidoP,
								ApellidoM = p.ApellidoM,
								Direccion = p.Direccion,
								Telefono = p.Telefono,
								Email = p.Email,
								FechaReg = p.FechaReg,
								UserName = p.UsuaReg
							}
							).ToList();
			}

			return personas;
		}

		/// <summary>
		/// Listar todas las personas filtradas por el Nit siempre y cuando no haiga empresa activa
		/// </summary>
		/// <param name="nit"></param>
		/// <returns></returns>
		public List<ListarAbmPersona> ListarPersonalPorEmpresa(string nit)
		{
			var personas = (from p in db.Persona
							join ue in db.UsuarioEmpresa on p.CI equals ue.CI
							join em in db.Empresa on ue.NIT equals em.NIT
							where ue.NIT == nit
							select new ListarAbmPersona
							{
								CI = p.CI,
								Nombre = p.Nombre,
								ApellidoP = p.ApellidoP,
								ApellidoM = p.ApellidoM,
								Direccion = p.Direccion,
								Telefono = p.Telefono,
								Email = p.Email,
								estado = (ue.Estado == true) ? "Activo" : "De Baja",
								nit = ue.NIT,
								razon_social = em.RazonSocial
							}).ToList();

			return personas;
		}

		public List<Vehiculo> ObtenerVehiculosAsociadosPersonal(string user)
		{
			string nit = homeCtrl.obtenerNit(user);
			var collection = db.AspNetUsers.Where(u => u.UserName == user).FirstOrDefault();
			var resultpersona = db.Persona.Where(p => p.IdUser == collection.Id).FirstOrDefault();

			if (resultpersona.CI != null)
			{
				try
				{
					var resultvehiculo = (from uv in db.UsuarioVehiculo
										  join v in db.Vehiculo on uv.nroplaca equals v.NroPlaca
										  join s in db.Seguimiento on v.NroPlaca equals s.NroPlaca
										  where uv.ci == resultpersona.CI && uv.nit == nit && s.estado == true
										  select v).ToList();

					return resultvehiculo;
				}
				catch (Exception ex)
				{
					throw new Exception("controller negocio", ex);
				}
			}
			return null;
		}

		/// <summary>
		/// SI EL ROL ES SUPERVISOR LISTAR TODAS LAS PERSONAS DE ESA EMPRESA
		/// </summary>
		/// <returns></returns>
		public List<sp_ListarPersonalAsignacionVehiculo_Result> ListarUsuarioSupervisor()
		{
			var user = HttpContext.Current.User.Identity.Name;
			var nit = homeCtrl.obtenerNit(user);
			var result = db.sp_ListarPersonalAsignacionVehiculo(nit).ToList();
			return result;
		}

		public List<ListarAbmPersona> ListarSoloPersonalSA()
		{
			var collection = db.sp_ListarSoloPersonalSA().ToList();
			var resul = (from p in collection
						 select new ListarAbmPersona
						 {
							 ApellidoM = p.ApellidoM,
							 ApellidoP = p.ApellidoP,
							 CI = p.CI,
							 Direccion = p.Direccion,
							 Email = p.Email,
							 estado = (p.Estado == true) ? "Activo" : "De Baja"
						 }).ToList();
			return resul;
		}


		/// <summary>
		/// Busca una persona Activa en una empresa
		/// </summary>
		/// <param name="imei"></param>
		/// <returns></returns>
		public DataPersona BuscarSiExistePersona(string ci)
		{
			try
			{
				DataPersona aux = new DataPersona();
				if (!ci.Equals(""))
				{
					aux = (from p in db.Persona
						   join ep in db.UsuarioEmpresa on
							   p.CI equals ep.CI
						   where p.CI == ci && ep.Estado == true
						   select new DataPersona
						   {
							   ApellidoM = p.ApellidoM,
							   ApellidoP = p.ApellidoP,
							   CI = p.CI,
							   Nombre = p.Nombre
						   }).FirstOrDefault();
				}
				return aux;
			}
			catch (Exception ex)
			{
				throw new Exception("controller negocio", ex);
			}
		}

		public bool BuscarSiEstaRegistradoPersona(string ci)
		{
			var result = db.Persona.Where(r => r.CI == ci).ToList();
			if (result.Count > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	}
}






