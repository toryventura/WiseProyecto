using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using WISETRACK.Datos;
using WISETRACK.Datos.Auxiliar;
using WISETRACK.Models;

namespace WISETRACK.Controller
{
    public class IDButtonController
    {
        WISETRACKEntities db = new WISETRACKEntities();

        public string add(IdButton o, string user, string nit, List<string> gps)
        {
            var mensaje = new List<KeyValuePair<string, string>>();
            using (var transact = db.Database.BeginTransaction())
            {
                try
                {
                    if (!existe(o, nit))
                    {
                        if (!existeEmpresa(o, nit))
                        {
                            db.IdButton.Add(o);
                            db.SaveChanges();
                            var idbtton = (from x in db.IdButton where x.Keys == o.Keys select x).FirstOrDefault();
                            EmpresaIDButton emp = new EmpresaIDButton()
                            {
                                CodButton = idbtton.ID,
                                Nit = nit,
                                UsuarioReg = user,
                                Estado = true,
                                FechaReg = DateTime.Now

                            };
                            db.EmpresaIDButton.Add(emp);
                            db.SaveChanges();

                            foreach (var item in gps)
                            {
                                IDButtonGps idgos = new IDButtonGps()
                                {
                                    Estado = true,
                                    IMEI = item,
                                    CODIDButton = idbtton.ID,
                                    FechaReg = DateTime.Now,
                                    UsuReg = user

                                };
                                db.IDButtonGps.Add(idgos);
                                db.SaveChanges();

                            }

                            transact.Commit();

                            mensaje.Add(Utils.mensaje("OK", "Registro corectamente..."));
                        }
                        else
                        {

                            mensaje.Add(Utils.mensaje("ERROR", "No se puede Registra El IDButton contactese con su Proveedro de servicio.."));
                        }
                    }
                    else
                    {

                        mensaje.Add(Utils.mensaje("ERROR", "El IDBUtton ya se encuentra registrado"));
                    }

                }
                catch (Exception ex)
                {
                    transact.Rollback();
                    mensaje.Clear();
                    mensaje.Add(Utils.mensaje("ERROR", ex.Message));
                }

            }

            return JsonConvert.SerializeObject(mensaje);
        }

        private bool existeEmpresa(IdButton o, string nit)
        {
            var exists = (from x in db.IdButton
                          join xs in db.EmpresaIDButton on x.ID equals xs.CodButton
                          where xs.Nit != nit && x.Keys == o.Keys && x.Estado == true
                          select x).FirstOrDefault();
            return exists != null ? true : false;
        }

        public bool existe(IdButton o, string nit)
        {

            if (o.ID == 0)
            {

                var exists = (from x in db.IdButton
                              join xs in db.EmpresaIDButton on x.ID equals xs.CodButton
                              where xs.Nit == nit && x.Keys == o.Keys
                              select x).FirstOrDefault();
                return exists != null ? true : false;
            }
            else
            {

                var exists = (from x in db.IdButton
                              join xs in db.EmpresaIDButton on x.ID equals xs.CodButton
                              where xs.Nit == nit  && x.ID == o.ID
                              select x).FirstOrDefault();
                return exists != null ? true : false;
            }
        }
        public string update(IdButton o, string user, string nit, List<string> gps)
        {
            var mensaje = new List<KeyValuePair<string, string>>();
            using (var transact = db.Database.BeginTransaction())
            {
                try
                {
                    if (existe(o, nit))
                    {
                        if (!existeEmpresa(o, nit))
                        {

                            var idbtton = (from x in db.IdButton where x.ID == o.ID select x).FirstOrDefault();
                            idbtton.Alias = o.Alias;
                            idbtton.FechaMod = o.FechaMod;
                            idbtton.UsuaMod = user;
                            idbtton.Estado = o.Estado;
                            db.SaveChanges();

                            var gpsactuales = (from x in db.IDButtonGps where x.CODIDButton == o.ID select x.IMEI).ToList();
                            var gpsadd = gps.Except(gpsactuales);
                            var gpsremoves = gpsactuales.Except(gps);
                            foreach (var item in gpsadd)
                            {
                                IDButtonGps idgos = new IDButtonGps()
                               {
                                   Estado = true,
                                   IMEI = item,
                                   CODIDButton = idbtton.ID,
                                   FechaReg = DateTime.Now,
                                   UsuReg = user

                               };
                                db.IDButtonGps.Add(idgos);
                                db.SaveChanges();

                            }
                            foreach (var item1 in gpsremoves)
                            {
                                var remov = (from x in db.IDButtonGps where x.IMEI == item1 && x.CODIDButton == idbtton.ID select x).FirstOrDefault();
                                db.IDButtonGps.Remove(remov);
                                db.SaveChanges();
                            }

                            transact.Commit();

                            mensaje.Add(Utils.mensaje("OK", "Se Actualizo Correctamente..."));
                        }
                        else
                        {

                            mensaje.Add(Utils.mensaje("ERROR", "No se puede Registra El IDButton contactese con su Proveedro de servicio.."));
                        }
                    }
                    else
                    {

                        mensaje.Add(Utils.mensaje("ERROR", "El IDBUtton ya se encuentra registrado"));
                    }

                }
                catch (Exception ex)
                {
                    mensaje.Clear();
                    mensaje.Add(Utils.mensaje("ERROR", ex.Message));
                }

            }

            return new JavaScriptSerializer().Serialize(mensaje);
        }
        public string getIDButonXID(int id = 0)
        {
            var mensaje = new List<KeyValuePair<string, string>>();
            var idbuttons = (from x in db.IdButton where x.ID == id select x).FirstOrDefault();

            if (idbuttons != null)
            {
                var getimeis = (from xs in db.IDButtonGps
                                where xs.CODIDButton == idbuttons.ID && xs.Estado == true
                                select xs.IMEI).ToList();
                mensaje.Add(Utils.mensaje("Key", idbuttons.Keys));
                mensaje.Add(Utils.mensaje("Alias", idbuttons.Alias));
                var list = JsonConvert.SerializeObject(getimeis);
                mensaje.Add(Utils.mensaje("Lista", list));
            }

            return JsonConvert.SerializeObject(mensaje);
        }
        public List<IDButtonDetalle> getListaIdButtuns(string nit = "", int tipo = 0)
        {
            List<IDButtonDetalle> list = new List<IDButtonDetalle>();
            switch (tipo)
            {
                case 0:// es SA
                    list = (from x in db.IdButton
                            join xe in db.EmpresaIDButton on x.ID equals xe.CodButton
                            join e in db.Empresa on xe.Nit equals e.NIT
                            where  e.NIT == nit
                            select new IDButtonDetalle
                            {
                                ID = x.ID,
                                Keys = x.Keys,
                                Alias = x.Alias,
                                Estado = x.Estado == true ? "Activo" : "De Baja",
                                Rsocial = e.RazonSocial,
                                FechaReg = xe.FechaReg
                            }).ToList();

                    break;
                case 1:// esto es cuando esta dentro una empresa
                    list = (from x in db.IdButton
                            join xe in db.EmpresaIDButton on x.ID equals xe.CodButton
                            join e in db.Empresa on xe.Nit equals e.NIT
                            
                            select new IDButtonDetalle
                            {
                                ID = x.ID,
                                Keys = x.Keys,
                                Alias = x.Alias,
                                Estado = x.Estado == true ? "Activo" : "De Baja",
                                Rsocial = e.RazonSocial,
                                FechaReg = x.FechaReg
                            }).ToList();

                    break;
                default:
                    break;
            }
            return list;
        }
        public string delete(int id)
        {
            var mensaje = new List<KeyValuePair<string, string>>();
            using (var transact = db.Database.BeginTransaction())
            {
                try
                {
                    var sc = (from x in db.IdButton where x.ID == id select x).FirstOrDefault();
                    sc.Estado = false;
                    db.SaveChanges();
                    var idemp = (from i in db.EmpresaIDButton where i.CodButton == sc.ID && i.Estado == true select i).FirstOrDefault();
                    if (idemp != null)
                    {
                        idemp.Estado = false;
                        db.SaveChanges();
                    }
                    var listagps = (from xs in db.IDButtonGps where xs.CODIDButton == sc.ID && xs.Estado == true select xs).ToList();
                    foreach (var item in listagps)
                    {
                        item.Estado = false;
                    }
                    db.SaveChanges();
                    transact.Commit();
                    mensaje.Add(Utils.mensaje("OK", "Se dio de baja corectamente....."));
                }
                catch (Exception ex)
                {
                    mensaje.Clear();
                    mensaje.Add(Utils.mensaje("ERROR", ex.Message));
                }
            }
            return JsonConvert.SerializeObject(mensaje);
        }
        public string Activar(int id)
        {
            var mensaje = new List<KeyValuePair<string, string>>();
            using (var transact = db.Database.BeginTransaction())
            {
                try
                {
                    var sc = (from x in db.IdButton where x.ID == id && x.Estado == false select x).FirstOrDefault();
                    sc.Estado = true;
                    db.SaveChanges();
                    var idemp = (from i in db.EmpresaIDButton where i.CodButton == sc.ID && i.Estado == false select i).FirstOrDefault();
                    if (idemp != null)
                    {
                        idemp.Estado = true;
                        db.SaveChanges();
                    }
                    var listagps = (from xs in db.IDButtonGps where xs.CODIDButton == sc.ID && xs.Estado == false select xs).ToList();
                    foreach (var item in listagps)
                    {
                        item.Estado = true;
                    }
                    db.SaveChanges();
                    transact.Commit();
                    mensaje.Add(Utils.mensaje("OK", "Se Activo corectamente....."));
                }
                catch (Exception ex)
                {
                    mensaje.Clear();
                    mensaje.Add(Utils.mensaje("ERROR", ex.Message));
                }
            }
            return JsonConvert.SerializeObject(mensaje);
        }


    }
}