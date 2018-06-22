using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WISETRACK.Datos;
using WISETRACK.Datos.Auxiliar;

namespace WISETRACK.Controller
{
    public class GpsController
    {
        private WISETRACKEntities db = new WISETRACKEntities();

        /// <summary>
        /// Listar todos los gps que estan dentro de una empresa
        /// </summary>
        /// <param name="nit"></param>
        /// <returns></returns>
        public List<ListarAbmGPS> listarGps(string nit)
        {
            var collection = (from g in db.GPS
                              join eg in db.EmpresaGPS on g.IMEI equals eg.imei
                              join em in db.Empresa on eg.nit equals em.NIT
                              where eg.nit == nit
                              select new ListarAbmGPS
                              {
                                  ID = g.ID,
                                  IMEI = g.IMEI,
                                  Modelo = g.Modelo,
                                  nit = em.NIT,
                                  NroTelefono = g.NroTelefono,
                                  estado = (eg.estado == true) ? "Activo" : "De Baja",
                                  razon_social = em.RazonSocial
                              });
            return collection.ToList();
        }

        public List<ListarGPSPlaca> listarGpsPlaca(string nit)
        {
            //select g.ID, g.IMEI,g.Modelo,s.NroPlaca from  GPS g inner join Seguimiento s on g.IMEI=s.IMEI  where s.estado=1
            var result = (from g in db.GPS
                              join s in db.Seguimiento on g.IMEI equals s.IMEI 
                              where s.NIT==nit && s.estado ==true
                          select new ListarGPSPlaca
                              {
                                  ID = g.ID,
                                  IMEI = g.IMEI,
                                  Modelo = g.Modelo,
                                  nit = s.NIT,
                                  Placa=s.NroPlaca
                              }).ToList();
            return result;
        }
        /// <summary>
        /// Listar todos los GPS de la Base Datos
        /// </summary>
        /// <returns></returns>
        public List<ListarAbmGPS> listarGps()
        {
            var collection = (from g in db.GPS
                              join eg in db.EmpresaGPS on g.IMEI equals eg.imei
                              join em in db.Empresa on eg.nit equals em.NIT
                              select new ListarAbmGPS
                              {
                                  ID = g.ID,
                                  IMEI = g.IMEI,
                                  Modelo = g.Modelo,
                                  nit = em.NIT,
                                  NroTelefono = g.NroTelefono,
                                  estado = (eg.estado == true) ? "Activo" : "De Baja",
                                  razon_social = em.RazonSocial
                              });
            return collection.ToList();
        }

        public bool Add(GPS g, string user, string nit)
        {
            bool sw;
            using (var transact = db.Database.BeginTransaction())
            {
                try
                {
                    db.GPS.Add(g);
                    db.SaveChanges();

                    var egps = new EmpresaGPS();
                    egps.estado = true;
                    egps.id = db.EmpresaGPS.Max(r => r.id) + 1;
                    egps.fechaReg = DateTime.Now;
                    egps.imei = g.IMEI;
                    egps.nit = nit;
                    egps.usuarioReg = user;
                    db.EmpresaGPS.Add(egps);
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
                return sw;
            }
        }

        public bool update(GPS g)
        {
            bool sw;
            try
            {
                var aux = db.GPS.Where(gp => gp.IMEI == g.IMEI);
                GPS gps = aux.First();
                gps.ID = g.ID;
                gps.NroTelefono = g.NroTelefono;
                gps.Modelo = g.Modelo;
                gps.UsuaModif = g.UsuaModif;
                gps.FechaModif = g.FechaModif;
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

        public bool ActualizarGPSEmpresa(GPS g, string usuario, string nit)
        {
            bool sw;
            using (var transact = db.Database.BeginTransaction())
            {
                try
                {
                    var aux = db.GPS.Find(g.IMEI);
                    aux.ID = g.ID;
                    aux.NroTelefono = g.NroTelefono;
                    aux.Modelo = g.Modelo;
                    aux.UsuaModif = g.UsuaModif;
                    aux.FechaModif = g.FechaModif;
                    aux.Estado = true;
                    db.SaveChanges();

                    var egps = new EmpresaGPS();
                    egps.estado = true;
                    egps.id = db.EmpresaGPS.Max(r => r.id) + 1;
                    egps.fechaReg = DateTime.Now;
                    egps.imei = g.IMEI;
                    egps.nit = nit;
                    egps.usuarioReg = usuario;
                    db.EmpresaGPS.Add(egps);
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
            return sw;
        }
		public bool RemoveGps(string imei, string nit, string usuario)
		{
			bool sw = false;
			var result = db.Seguimiento.Where(s => s.IMEI == imei && s.estado == true).ToList();
			if (result.Count == 0)
			{
				using (var transact = db.Database.BeginTransaction())
				{
					try
					{
						
						var aux2 = db.EmpresaGPS.Where(e => e.imei == imei && e.nit == nit).FirstOrDefault();
						db.EmpresaGPS.Remove(aux2);
						db.SaveChanges();
					

						db.SaveChanges();
						var seg = db.Seguimiento.Where(s => s.IMEI == imei && s.estado==false).ToList();
						foreach (var s in seg)
						{
							db.Seguimiento.Remove(s);
						}
						db.SaveChanges();

						var aux = db.GPS.Where(g => g.IMEI == imei).FirstOrDefault();
						db.GPS.Remove(aux);

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

        public bool RemoverLogicamente(string imei, string nit, string usuario)
        {
            bool sw = false;
            var result = db.Seguimiento.Where(s => s.IMEI == imei && s.estado == true).ToList();
            if (result.Count == 0)
            {
                using (var transact = db.Database.BeginTransaction())
                {
                    try
                    {
                        var aux = db.GPS.Where(g => g.IMEI == imei).FirstOrDefault();
                        aux.Estado = false;
                        aux.UsuaModif = usuario;
                        aux.FechaModif = DateTime.Now;
                        db.SaveChanges();

                        var aux2 = db.EmpresaGPS.Where(e => e.imei == imei && e.nit == nit && e.estado == true).FirstOrDefault();
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

        /// <summary>
        /// Depurar Metodo en 30 dias.
        /// </summary>
        /// <param name="imei"></param>
        /// <returns></returns>
        public bool remove(string imei)
        {
            bool sw = false;
            var gps = db.GPS.Find(imei);
            if (gps != null)
            {
                var seg = db.Seguimiento.Where(s => s.IMEI == gps.IMEI).ToList();
                if (seg.Count == 0)
                {
                    db.GPS.Remove(gps);
                    db.SaveChanges();
                    sw = true;
                }
            }
            return sw;
        }
        public GPS listar(string imei)
        {
            try
            {
                GPS gps = new GPS();
                if (!imei.Equals(""))
                {
                    var aux = db.GPS.Where(gp => gp.IMEI == imei);
                    gps = aux.First();
                }
                return gps;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Busca un GPS Activo en una empresa
        /// </summary>
        /// <param name="imei"></param>
        /// <returns></returns>
        public DataGPS BuscarSiExisteGps(string imei)
        {
            try
            {
                DataGPS aux = new DataGPS();
                if (!imei.Equals(""))
                {
                    aux = (from g in db.GPS
                           join eg in db.EmpresaGPS on
                               g.IMEI equals eg.imei
                           where g.IMEI == imei && eg.estado == true
                           select new DataGPS
                       {
                           ID = g.ID,
                           IMEI = g.IMEI,
                           Modelo = g.Modelo,
                           NroTelefono = g.NroTelefono.ToString()
                       }).FirstOrDefault();
                }
                return aux;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool BuscarSiEstaRegistradoGps(string imei)
        {
            var result = db.GPS.Where(e => e.IMEI == imei).ToList();
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