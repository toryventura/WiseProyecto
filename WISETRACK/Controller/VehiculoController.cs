using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WISETRACK.Datos;
using WISETRACK.Datos.Auxiliar;
using WISETRACK.Models;

namespace WISETRACK.Controller
{
	public class VehiculoController
	{
		WISETRACKEntities db = new WISETRACKEntities();

		public bool createVehiculo(Vehiculo data, string nits)
		{
			bool sw;
			using (var transact = db.Database.BeginTransaction())
			{
				try
				{
					db.Vehiculo.Add(data);
					db.SaveChanges();
					db.EmpresaVehiculo.Add(new EmpresaVehiculo()
					{
						estado = data.Estado,
						id = db.EmpresaVehiculo.Max(r => r.id) + 1,
						//fechaMod = data.FechaModif,
						fechaReg = data.FechaReg,
						nroplaca = data.NroPlaca,
						//usuarioMod = data.UsuaModif,
						usuarioReg = data.UsuaReg,
						nit = nits
					});
					db.SaveChanges();
					transact.Commit();
					sw = true;
				}
				catch (Exception)
				{

					transact.Rollback();
					sw = false;
				}
			}

			return sw;
		}

		public List<TipoVehiculo> comboTipoVehiculo()
		{
			var tipo = from c in db.TipoVehiculo select c;
			return tipo.ToList();
		}

		public List<Marca> comboMarca()
		{
			var tipo = from c in db.Marca select c;
			return tipo.ToList();
		}

		public List<sp_listarVehiculosEmpresa_Result> cargarDetalleVehiculos(string nit)
		{
			var query = db.sp_listarVehiculosEmpresa(nit);
			return query.ToList();
		}

		public List<sp_listarVehiculosSA_Result> cargarDetalleVehiculosSA()
		{
			var query = db.sp_listarVehiculosSA();
			return query.ToList();
		}

		/// <summary>
		/// SI UN USUARIO ES SA, MOSTRAR TODOS LOS VEHICULOS REGISTRADOS EN EL SISTEMA. (N EMPRESAS).
		///  es esta modificando la consulta por tory
		/// </summary>
		/// <returns></returns>
		public List<VehiculoDetalle> GetAllSA()
		{
			var collection = (from v in db.Vehiculo
							  join em in db.EmpresaVehiculo on v.NroPlaca equals em.nroplaca
							  join empr in db.Empresa on em.nit equals empr.NIT
							  select new VehiculoDetalle
							  {
								  RazonSocial = empr.RazonSocial,
								  Año = (int)v.Año,
								  Estado = (em.estado == true) ? "Activo" : "De Baja",
								  Conductor = v.VehiculoConductor.FirstOrDefault() != null ? v.VehiculoConductor.FirstOrDefault().Persona.Nombre
												+ (v.VehiculoConductor.FirstOrDefault().Persona.ApellidoP != null ? " " + v.VehiculoConductor.FirstOrDefault().Persona.ApellidoP : "")
												+ (v.VehiculoConductor.FirstOrDefault().Persona.ApellidoM != null ? " " + v.VehiculoConductor.FirstOrDefault().Persona.ApellidoM : "") : "(No Asignado)",

								  // GPS = v.Seguimiento.FirstOrDefault() != null ? v.Seguimiento.FirstOrDefault().IMEI : "(No Asignado)",
								  Modelo = v.Modelo,
								  NroChasis = v.NroChasis,
								  NroPlaca = v.NroPlaca

							  }).OrderBy(e => e.RazonSocial).ToList();

			return collection;
		}

		/// <summary>
		/// Filtrar todos los moviles por el NIT
		/// </summary>
		/// <param name="nit"></param>
		/// <returns></returns>
		/// se esta modificando la consulta 
		/// 

		public List<VehiculoDetalle> GetAll(string nit)
		{
			var vehiculos = (from v in db.Vehiculo
							 join ve in db.EmpresaVehiculo on v.NroPlaca equals ve.nroplaca
							 join em in db.Empresa on ve.nit equals em.NIT
							 where em.NIT == nit
							 select new VehiculoDetalle
							 {
								 NroPlaca = v.NroPlaca,
								 NroChasis = v.NroChasis,
								 Modelo = v.Modelo,
								 Año = v.Año.Value,
								 RazonSocial = em.RazonSocial,
								 Estado = (ve.estado == true) ? "Activo" : "De baja",
								 // GPS = v.Seguimiento.FirstOrDefault() != null ? v.Seguimiento.FirstOrDefault().IMEI : "(No Asignado)",
								 Conductor = v.VehiculoConductor.FirstOrDefault() != null ? v.VehiculoConductor.FirstOrDefault().Persona.Nombre
												+ (v.VehiculoConductor.FirstOrDefault().Persona.ApellidoP != null ? " " + v.VehiculoConductor.FirstOrDefault().Persona.ApellidoP : "")
												+ (v.VehiculoConductor.FirstOrDefault().Persona.ApellidoM != null ? " " + v.VehiculoConductor.FirstOrDefault().Persona.ApellidoM : "") : "(No Asignado)"
							 }
							 ).ToList();

			return vehiculos;
		}

		public List<VehiculoDetalle> GetAllAsign(string nit)
		{
			var vehiculos = (from v in db.Vehiculo
							 join vc in db.VehiculoConductor
							 on v.NroPlaca equals vc.NroPlaca
							 join g in db.GPS
							 on v.Seguimiento.FirstOrDefault().IMEI equals g.IMEI
							 join ve in db.EmpresaVehiculo on v.NroPlaca equals ve.nroplaca
							 join em in db.Empresa on ve.nit equals em.NIT
							 where em.NIT == nit
							 select new VehiculoDetalle
							 {
								 NroPlaca = v.NroPlaca,
								 NroChasis = v.NroChasis,
								 Modelo = v.Modelo,
								 Año = v.Año.Value,
								 // GPS = g.IMEI + " (" + g.ID + " : " + g.Modelo + ")",
								 Conductor = vc.Persona.Nombre
												+ (vc.Persona.ApellidoP != null ? " " + vc.Persona.ApellidoP : "")
												+ (vc.Persona.ApellidoM != null ? " " + vc.Persona.ApellidoM : "")
							 }
							 ).ToList();

			return vehiculos;
		}


		public Vehiculo listar(string placa)
		{
			Vehiculo ve = new Vehiculo();
			if (!String.IsNullOrEmpty(placa))
			{
				var aux = db.Vehiculo.Where(veh => veh.NroPlaca == placa);
				ve = aux.FirstOrDefault();
			}
			return ve;
		}

		public bool update(Vehiculo ve, string nits, string user)
		{
			bool sw;
			using (var transact = db.Database.BeginTransaction())
			{
				try
				{
					var aux = db.Vehiculo.Where(gp => gp.NroPlaca == ve.NroPlaca);
					Vehiculo vehiculo = aux.First();
					vehiculo.NroPlaca = ve.NroPlaca;
					vehiculo.NroMotor = ve.NroMotor;
					vehiculo.NroChasis = ve.NroChasis;
					vehiculo.CodTipoV = ve.CodTipoV;
					vehiculo.Modelo = ve.Modelo;
					vehiculo.Patente = ve.Patente;
					//vehiculo.idempresa = ve.idempresa;
					vehiculo.Foto = ve.Foto;
					//vehiculo.Estado = ve.Estado;
					vehiculo.FechaModif = ve.FechaModif;
					vehiculo.UsuaModif = ve.UsuaModif;
					vehiculo.Año = ve.Año;
					db.SaveChanges();
					db.EmpresaVehiculo.Add(new EmpresaVehiculo()
					{
						estado = vehiculo.Estado,
						fechaMod = ve.FechaModif,
						fechaReg = ve.FechaReg,
						nit = nits,
						nroplaca = ve.NroPlaca,
						usuarioMod = vehiculo.UsuaReg,
						usuarioReg = vehiculo.UsuaReg

					});

					transact.Commit();
					sw = true;
				}
				catch (Exception)
				{
					transact.Rollback();
					sw = false;
				}
			}

			return sw;
		}

		public bool remove(string placa)
		{
			bool sw = false;
			Vehiculo ve = db.Vehiculo.Find(placa);
			if (ve != null)
			{
				var veh = db.Seguimiento.Where(v => v.NroPlaca == placa).ToList();
				if (veh.Count == 0)
				{
					db.Vehiculo.Remove(ve);
					db.SaveChanges();
					sw = true;
				}
			}
			return sw;
		}
		/// <summary>
		/// se esta cambiando la consulta 
		/// </summary>
		/// <param name="nit"></param>
		/// <returns> lista de vehiculos por empresas</returns>
		public List<Vehiculo> listarVehiculo(string nit)
		{
			var collection = from g in db.Vehiculo
							 join ve in db.EmpresaVehiculo on g.NroPlaca equals ve.nroplaca
							 join em in db.Empresa on ve.nit equals em.NIT
							 where em.NIT == nit
							 select g;
			return collection.ToList();
		}

		public List<PersonaCboDet> GetNotPersCond(string nroPlaca)
		{
			var conductores = (from p in db.Persona
							   where p.TipoPersona.CodTipo == 3
							   && (p.VehiculoConductor.FirstOrDefault() == null || p.VehiculoConductor.FirstOrDefault().NroPlaca == nroPlaca)
							   select new PersonaCboDet
							   {
								   CI = p.CI,
								   NombreCompleto = p.Nombre + (p.ApellidoP != null ? " " + p.ApellidoP : "")
													+ (p.ApellidoM != null ? " " + p.ApellidoM : "")
							   }
							   ).ToList();

			return conductores;
		}
		public List<PersonaCboDet> GetNotPersCondX(string nit = "")
		{
			var noasignados = (from p in db.Persona
							   join us in db.UsuarioEmpresa on p.CI equals us.CI
							   where us.NIT == nit
							   join cv in db.VehiculoConductor on p.CI equals cv.CI
							   where cv.Asignado == true
							   select cv.CI).ToList();
			var conductoreslibre = (from p in db.Persona
									join us in db.UsuarioEmpresa on p.CI equals us.CI
									where p.CodTipo == 3 && us.NIT == nit
									select new PersonaCboDet
									{
										CI = p.CI,
										NombreCompleto = p.Nombre + (p.ApellidoP != null ? " " + p.ApellidoP : "")
														 + (p.ApellidoM != null ? " " + p.ApellidoM : "")
									}).ToList();

			var result = conductoreslibre.Where(x => !noasignados.Contains(x.CI)).ToList();
			//var condcutoresAsignados = (from p in db.VehiculoConductor select p).ToList();
			//var listFinal=(from r in conductores  
			//               from re in condcutoresAsignados where r.CI!=re.CI
			//               select new PersonaCboDet
			//                  {
			//                      CI = r.CI,
			//                      NombreCompleto = r.Nombre + (r.ApellidoP != null ? " " + r.ApellidoP : "")
			//                                       + (r.ApellidoM != null ? " " + r.ApellidoM : "")
			//                  }).ToList();

			return result;
		}
		public List<PersonaCboDet> GetNotPersCondX()
		{
			var conductoreslibre = (from c in db.Persona
									from cv in db.VehiculoConductor
									where c.TipoPersona.CodTipo == 3 && c.CI != cv.CI && cv.Asignado == true
									select new PersonaCboDet
									{
										CI = c.CI,
										NombreCompleto = c.Nombre + (c.ApellidoP != null ? " " + c.ApellidoP : "")
														 + (c.ApellidoM != null ? " " + c.ApellidoM : "")
									}).ToList();
			//var condcutoresAsignados = (from p in db.VehiculoConductor select p).ToList();
			//var listFinal=(from r in conductores  
			//               from re in condcutoresAsignados where r.CI!=re.CI
			//               select new PersonaCboDet
			//                  {
			//                      CI = r.CI,
			//                      NombreCompleto = r.Nombre + (r.ApellidoP != null ? " " + r.ApellidoP : "")
			//                                       + (r.ApellidoM != null ? " " + r.ApellidoM : "")
			//                  }).ToList();
			return conductoreslibre;
		}

		public void AsignarConductor(Vehiculo vehiculo, string ci, string fecha, string userName)
		{
			if (!ci.Equals("0"))
			{
				var vehCond = vehiculo.VehiculoConductor.FirstOrDefault();

				if (vehCond == null)
				{
					VehiculoConductor vehConductor = new VehiculoConductor
					{
						NroPlaca = vehiculo.NroPlaca,
						CI = ci,
						Fecha = Convert.ToDateTime(fecha),
						Asignado = true,
						UsuaReg = userName,
						FechaReg = DateTime.Now
					};

					db.VehiculoConductor.Add(vehConductor);
				}
				else
				{
					vehCond.CI = ci;
					vehCond.UsuaModif = userName;
					vehCond.FechaModif = DateTime.Now;
				}
			}
			else
			{
				var vehCond = vehiculo.VehiculoConductor.FirstOrDefault();

				if (vehCond != null)
					db.VehiculoConductor.Remove(vehCond);

			}

			db.SaveChanges();
		}

		public List<VehiculoCboDet> GetAllVehiculos2()
		{
			return (from s in db.Seguimiento
					where s.estado.Value == true
					select new VehiculoCboDet
					{
						Id = s.NroPlaca,
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
						Id = s.NroPlaca,
						NroPlaca = s.NroPlaca
					}
					).ToList();
		}

		/// <summary>
		/// Lista todos los vehiculos que no estan asignados.
		/// </summary>
		/// <param name="ci"></param>
		/// <param name="nit"></param>
		/// <returns></returns>
		public List<Vehiculo> ListarVehiculoSupervisor(string ci, string nit)
		{
			var resultmovil = listarVehiculo(nit);
			var resultusuariomovil = (db.UsuarioVehiculo.Where(uv => uv.ci == ci).ToList());

			var coll = resultmovil.Where(a => !resultusuariomovil.Select(b => b.nroplaca).Contains(a.NroPlaca)).ToList();
			return coll;
		}

		/// <summary>
		/// Listar todos los vehiculos asignados al usuario
		/// </summary>
		/// <param name="ci"></param>
		/// <param name="nit"></param>
		/// <returns></returns>
		public List<ListarUsuarioVehiculo> ListarVehiculoAsignado(string ci, string nit)
		{
			string obtrazonsocial = db.Empresa.Where(e => e.NIT == nit).FirstOrDefault().RazonSocial;
			var user = (from p in db.Persona
						join u in db.AspNetUsers on p.IdUser equals u.Id
						where p.CI == ci
						select u.UserName).FirstOrDefault();
			var rmovilasig = (from uv in db.UsuarioVehiculo.Where(uv => uv.nit == nit && uv.ci == ci)
							  select new ListarUsuarioVehiculo
							  {
								  ci = uv.ci,
								  empresa = obtrazonsocial,
								  id = uv.id,
								  nit = uv.nit,
								  nroplaca = uv.nroplaca,
								  usuario = user
							  }).ToList();
			return rmovilasig;
		}

		public bool GuardarAsignacionVehiculo(string ci, string placa, string nit)
		{
			var user = HttpContext.Current.User.Identity.Name;
			bool sw;
			try
			{
				var uv = new UsuarioVehiculo
				{
					ci = ci,
					estado = true,
					fechaReg = DateTime.Now,
					id = db.UsuarioVehiculo.Max(s => s.id) + 1,
					nit = nit,
					nroplaca = placa,
					usuarioReg = user
				};
				db.UsuarioVehiculo.Add(uv);
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

		public bool RemoverAsignacionVehiculo(int id)
		{
			bool sw = false;
			UsuarioVehiculo ve = db.UsuarioVehiculo.Find(id);
			if (ve != null)
			{
				db.UsuarioVehiculo.Remove(ve);
				db.SaveChanges();
				sw = true;
			}
			return sw;
		}
		/// <summary>
		/// buscar GetAllState por estado a todos los vehiculos 
		/// </summary>
		/// <param name="nit"></param>
		/// <param name="state"></param>
		/// <returns></returns>

		internal object GetAllState(string nit, bool state)
		{
			var vehiculos = (from v in db.Vehiculo
							 join ve in db.EmpresaVehiculo on v.NroPlaca equals ve.nroplaca
							 join em in db.Empresa on ve.nit equals em.NIT
							 where em.NIT == nit && ve.estado == state
							 select new VehiculoDetalle
							 {
								 NroPlaca = v.NroPlaca,
								 NroChasis = v.NroChasis,
								 Modelo = v.Modelo,
								 Año = v.Año.Value,
								 RazonSocial = em.RazonSocial,
								 Estado = ve.estado == true ? "Activo" : "De Baja",
								 // GPS = v.Seguimiento.FirstOrDefault() != null ? v.Seguimiento.FirstOrDefault().IMEI : "(No Asignado)",
								 Conductor = v.VehiculoConductor.FirstOrDefault() != null ? v.VehiculoConductor.FirstOrDefault().Persona.Nombre
												+ (v.VehiculoConductor.FirstOrDefault().Persona.ApellidoP != null ? " " + v.VehiculoConductor.FirstOrDefault().Persona.ApellidoP : "")
												+ (v.VehiculoConductor.FirstOrDefault().Persona.ApellidoM != null ? " " + v.VehiculoConductor.FirstOrDefault().Persona.ApellidoM : "") : "(No Asignado)"
							 }
							).ToList();

			return vehiculos;
		}
		/// <summary>
		/// se busca por estado del vehiculo  de usuarios SA
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		internal object GetAllSAState(bool state)
		{
			var collection = (from v in db.Vehiculo
							  join em in db.EmpresaVehiculo on v.NroPlaca equals em.nroplaca
							  join empr in db.Empresa on em.nit equals empr.NIT
							  where em.estado == state
							  select new VehiculoDetalle
							  {
								  RazonSocial = empr.RazonSocial,
								  Año = (int)v.Año,
								  Estado = (em.estado == true) ? "Activo" : "De Baja",
								  Conductor = v.VehiculoConductor.FirstOrDefault() != null ? v.VehiculoConductor.FirstOrDefault().Persona.Nombre
												+ (v.VehiculoConductor.FirstOrDefault().Persona.ApellidoP != null ? " " + v.VehiculoConductor.FirstOrDefault().Persona.ApellidoP : "")
												+ (v.VehiculoConductor.FirstOrDefault().Persona.ApellidoM != null ? " " + v.VehiculoConductor.FirstOrDefault().Persona.ApellidoM : "") : "(No Asignado)",

								  // GPS = v.Seguimiento.FirstOrDefault() != null ? v.Seguimiento.FirstOrDefault().IMEI : "(No Asignado)",
								  Modelo = v.Modelo,
								  NroChasis = v.NroChasis,
								  NroPlaca = v.NroPlaca

							  }).ToList();

			return collection;
		}

		internal int buscarEstado(string NroPlaca)
		{
			var collection = (from v in db.Vehiculo
							  join ve in db.EmpresaVehiculo on v.NroPlaca equals ve.nroplaca
							  join em in db.Empresa on ve.nit equals em.NIT
							  join m in db.Marca on v.CodMarca equals m.CodMarca
							  join ti in db.TipoVehiculo on v.CodTipoV equals ti.CodTipoV
							  where v.NroPlaca == NroPlaca
							  select new VehiculoDetalle
										{

											Año = (int)v.Año,
											Estado = (ve.estado == true) ? "Activo" : "De Baja",
											Conductor = "(No Asignado)",
											// GPS = v.Seguimiento.FirstOrDefault() != null ? v.Seguimiento.FirstOrDefault().IMEI : "(No Asignado)",
											Modelo = v.Modelo,
											NroChasis = v.NroChasis,
											NroPlaca = v.NroPlaca,

											EstadoVehiculo = v.Estado == null ? true : (bool)v.Estado,
											EstadoEmpresaVehiculo = ve.estado == null ? true : (bool)ve.estado,

										}).FirstOrDefault();

			int pos = -1;
			if (collection != null)
			{
				if (collection.EstadoVehiculo && collection.EstadoEmpresaVehiculo)
				{
					pos = 1;//esto es cuando  estan habilitado los estados del vehiculo y empresaVehiculo
				}
				else
				{
					if (!collection.EstadoVehiculo || !collection.EstadoEmpresaVehiculo)
					{
						pos = 2;
					}
				}

			}
			else
			{
				pos = 3;
			}
			return pos;

		}
		public bool RemoverMovil(string placa, string nit, string usuario)
		{
			bool sw = false;
			var result = db.Seguimiento.Where(s => s.NroPlaca == placa && s.estado == true).ToList();
			if (result.Count == 0)
			{
				using (var transact = db.Database.BeginTransaction())
				{
					try
					{
						var aux2 = db.EmpresaVehiculo.Where(v => v.nroplaca == placa && v.nit == nit).FirstOrDefault();
						db.EmpresaVehiculo.Remove(aux2);
						db.SaveChanges();
						var seg = db.Seguimiento.Where(s => s.NroPlaca == placa && s.estado == false).ToList();
						foreach (var s in seg)
						{
							db.Seguimiento.Remove(s);
						}
						db.SaveChanges();
						var usua = db.UsuarioVehiculo.Where(us => us.nroplaca == placa && us.nit == nit).ToList();
						foreach (var it in usua)
						{
							db.UsuarioVehiculo.Remove(it);
						}
						db.SaveChanges();
						var aux = db.Vehiculo.Where(g => g.NroPlaca == placa).FirstOrDefault();
						db.Vehiculo.Remove(aux);
						db.SaveChanges();


						sw = true;
						transact.Commit();
					}
					catch (Exception e)
					{
						Console.WriteLine("Error encontrado: " + e.ToString());
						transact.Rollback();
						sw = false;
					}
				}
			}
			return sw;
		}
		public bool RemoverLogicamente(string placa, string nit, string usuario)
		{
			bool sw = false;
			var result = db.Seguimiento.Where(s => s.NroPlaca == placa && s.estado == true).ToList();
			if (result.Count == 0)
			{
				using (var transact = db.Database.BeginTransaction())
				{
					try
					{
						var aux = db.Vehiculo.Where(g => g.NroPlaca == placa).FirstOrDefault();
						aux.Estado = false;
						aux.UsuaModif = usuario;
						aux.FechaModif = DateTime.Now;
						db.SaveChanges();

						var aux2 = db.EmpresaVehiculo.Where(e => e.nroplaca == placa && e.nit == nit && e.estado == true).FirstOrDefault();
						aux2.estado = false;
						aux2.usuarioMod = usuario;
						aux2.fechaMod = DateTime.Now;
						db.SaveChanges();
						sw = true;
						transact.Commit();
					}
					catch (Exception e)
					{
						Console.WriteLine("Error encontrado: " + e.ToString());
						transact.Rollback();
						sw = false;
					}
				}
			}
			return sw;
		}

		internal object BuscarSiExisteVehiculo(string p)
		{
			try
			{
				VehiculoDetalle ve = new VehiculoDetalle();
				if (!p.Equals(""))
				{
					ve = (from v in db.Vehiculo
						  join em in db.EmpresaVehiculo on v.NroPlaca equals em.nroplaca
						  where v.NroPlaca == p
						  select new VehiculoDetalle
						  {
							  //RazonSocial = em.RazonSocial,
							  Año = (int)v.Año,
							  //Estado = (ve.estado == true) ? "Activo" : "De Baja",
							  Conductor = "(No Asignado)",
							  // GPS = v.Seguimiento.FirstOrDefault() != null ? v.Seguimiento.FirstOrDefault().IMEI : "(No Asignado)",
							  Modelo = v.Modelo,
							  NroChasis = v.NroChasis,
							  NroPlaca = v.NroPlaca,
							  EstadoVehiculo = (bool)v.Estado == null ? false : true,
							  EstadoEmpresaVehiculo = (bool)v.Estado
							  // marca = m.Descripcion,
							  // tipo = ti.Descripcion
						  }).FirstOrDefault();

				}
				return ve;
			}
			catch (Exception)
			{

				return null;
			}
		}

		internal bool BuscarSiEstaRegistradoVehiculo(string p)
		{
			var result = db.Vehiculo.Where(e => e.NroPlaca == p).ToList();
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