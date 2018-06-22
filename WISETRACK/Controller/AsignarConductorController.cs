using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WISETRACK.Datos;
using WISETRACK.Datos.Auxiliar;
using WISETRACK.Models;

namespace WISETRACK.Controller
{
    public class AsignarConductorController
    {
        WISETRACKEntities db = new WISETRACKEntities();
        public string add(VehiculoConductor o, string user, string nit, List<string> lista)
        {
            var mensaje = new List<KeyValuePair<string, string>>();
            using (var transact = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in lista)
                    {
                        var asig = (from x in db.VehiculoConductor where x.CI == o.CI && x.NroPlaca == o.NroPlaca && x.Asignado == true select x).FirstOrDefault();
                        if (asig == null)
                        {
                            o.keys = item;
                            o.UsuaReg = user;

                            db.VehiculoConductor.Add(o);
                            db.SaveChanges();
                        }
                        else
                        {
                            if (asig.Asignado == false)
                            {

                                o.keys = item;

                                o.UsuaReg = user;
                                db.VehiculoConductor.Add(o);
                                db.SaveChanges();
                            }
                            else
                            {
                                if (asig.Asignado == true)
                                {
                                    var kyss = "Ya encuentra asignado con siguiente : " + asig.keys != null ? asig.keys : " Sin LLave.";
                                    mensaje.Add(Utils.mensaje("ERROR", kyss));
                                }
                            }

                        }
                    }
                    transact.Commit();
                    if (mensaje.Count == 0)
                    {
                        mensaje.Add(Utils.mensaje("OK", "Se realizo corectamente la asignacion"));

                    }

                }
                catch (Exception ex)
                {
                    mensaje.Clear();
                    mensaje.Add(Utils.mensaje("ERROR", ex.Message));

                }
            }
            return JsonConvert.SerializeObject(mensaje);
        }
        public bool existeActiva(VehiculoConductor o)
        {
            var ext = (from x in db.VehiculoConductor where x.Asignado == true select x).FirstOrDefault();
            return ext != null ? true : false;
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

            //var result = conductoreslibre.Where(x => !noasignados.Contains(x.CI)).ToList();
            var result = conductoreslibre;

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

            return conductoreslibre;
        }
        public List<VehiculoCboDet> getVehiculosNoAsiganados(string nit = "")
        {
            var lista = (from emp in db.EmpresaVehiculo
                         join v in db.Vehiculo on emp.nroplaca equals v.NroPlaca
                         where emp.nit == nit && emp.estado == true
                         select new VehiculoCboDet
                         {
                             Id = v.NroPlaca,
                             NroPlaca = v.NroPlaca + "-> " + v.Patente
                         }).ToList();
            var listaAsignado = (from emp in db.EmpresaVehiculo
                                 join v in db.Vehiculo on emp.nroplaca equals v.NroPlaca
                                 join vh in db.VehiculoConductor on v.NroPlaca equals vh.NroPlaca
                                 where emp.nit == nit && vh.Asignado == true
                                 select vh.NroPlaca).ToList();

            var lisfinal = lista.Where(x => !listaAsignado.Contains(x.Id)).ToList();

            return lista;
        }
        public List<ConductorAsiganadoDet> getlistaAsignados(string nit, int tipo)
        {
            var listaConductor = new List<ConductorAsiganadoDet>();
            switch (tipo)
            {
                case 0://SI NO ESTA EN  NINGUNA EMPRESA
                    listaConductor = (from x in db.VehiculoConductor
                                      join
                                          xs in db.Persona
                                          on x.CI equals xs.CI
                                      where x.Asignado == true
                                      select new ConductorAsiganadoDet
                                      {
                                          ID = x.ID,
                                          nroplaca = x.NroPlaca,
                                          keys = x.keys != null ? x.keys : "Sin llave",
                                          fecha = x.Fecha,
                                          nombreConductor = xs.Nombre + " " + xs.ApellidoP + " " + xs.ApellidoM
                                      }).ToList();
                    break;
                case 1:// SI ESTA ACTIVO EN UNA EMPRESA
                    listaConductor = (from s in db.Seguimiento
                                      join
                                          x in db.VehiculoConductor
                                          on s.NroPlaca equals x.NroPlaca
                                      join
                                          xs in db.Persona
                                          on x.CI equals xs.CI
                                      where s.NIT == nit && x.Asignado == true && s.estado == true
                                      select new ConductorAsiganadoDet
                                      {
                                          ID = x.ID,
                                          nroplaca = x.NroPlaca,
                                          keys = x.keys != null ? x.keys : "Sin llave",
                                          fecha = x.Fecha,
                                          nombreConductor = xs.Nombre + " " + xs.ApellidoP + " " + xs.ApellidoM
                                      }).ToList();
                    break;
                default:
                    break;
            }
            return listaConductor;
        }
        public string getkeys(string nroplaca = "")
        {
            var mensaje = new keyDetalles();
            var seguimient = (from x in db.Seguimiento
                              join
                                  xs in db.GPS on x.IMEI equals xs.IMEI
                              where x.NroPlaca == nroplaca && x.estado == true
                              select xs).FirstOrDefault();
            if (seguimient != null)
            {
                mensaje.IMEI = seguimient.IMEI;
                mensaje.modelo = seguimient.Modelo;
                var buts = (from g in db.VehiculoConductor where g.Asignado == true select g.keys).ToList();


                var listaids = (from bt in db.IdButton
                                join
                                    gp in db.IDButtonGps on bt.ID equals gp.CODIDButton
                                where gp.IMEI == seguimient.IMEI && gp.Estado == true
                                select new IDButtonDetalle
                         {
                             ID = bt.ID,
                             Keys = bt.Keys,
                             Alias = bt.Alias,
                             Estado = gp.Estado == true ? "Activo" : "De Baja",
                             Rsocial = "",
                             FechaReg = bt.FechaReg
                         }).ToList();

                if (listaids.Count > 0)
                {
                    //    var listafianal = JsonConvert.SerializeObject(listaids,Formatting.None);
                    //    mensaje.Add(Utils.mensaje("OK", listafianal));
                    var dupurador = listaids.Where(x => buts.Contains(x.Keys)).Select(x => x.Keys).ToList();
                    var list = listaids.Where(s => !dupurador.Contains(s.Keys)).ToList();
                    mensaje.IdButtuns = list;
                }

            }
            return JsonConvert.SerializeObject(mensaje);
        }


        public  string finalizar(int id)
        {
            var mensaje=new List<KeyValuePair<string ,string >>();
            using (var transact = db.Database.BeginTransaction())
            {
                try
                {
                    var o = (from x in db.VehiculoConductor where x.ID == id select x).FirstOrDefault();
                    if (o!=null)
                    {
                        o.Asignado = false;
                        db.SaveChanges();
                        mensaje.Add(Utils.mensaje("OK", "Se finalizo correctamente....."));
                    }else
                    {

                        mensaje.Add(Utils.mensaje("ERROR", "No se pudo finalizar, conectase con su proveedor servicio"));
                    }
                    transact.Commit();

                }
                catch (Exception ex)
                {
                    mensaje.Clear();
                    mensaje.Add(Utils.mensaje("ERROR", ex.Message));
                }
                
            }
            return JsonConvert.SerializeObject( mensaje);
        }
    }
}