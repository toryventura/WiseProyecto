using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WISETRACK.Datos;
using WISETRACK.Models;

namespace WISETRACK.Controller
{
    public class EmpresaController
    {
        WISETRACKEntities db = new WISETRACKEntities();
        List<Empresa> empresas = new List<Empresa>(); 
         
        public List<sp_ListarEmpresa_Result> cargarEmpresa(string username)
        {
            var colletion = db.sp_ListarEmpresa(username);
            return colletion.ToList();
        }

        public Empresa listar(string id)
        {
            Empresa emp = new Empresa();

            if (!String.IsNullOrEmpty(id))
            {
                var aux = db.Empresa.Where(e => e.NIT == id);
                if (aux.Count() > 0)
                {
                    emp = aux.First();
                }
            }
            return emp;
        }
        /// <summary>
        /// getEmpresaXNroPlaca se obtiene 
        /// </summary>
        /// <param name="nroplaca"></param>
        /// <returns></returns>
        public Empresa getEmpresaXNroPlaca(string nroplaca)
        {
            Empresa emp = new Empresa();

            if (!String.IsNullOrEmpty(nroplaca ))
            {
                //var aux = db.Empresa.Where(e => e.NIT == id);
                var aux=(from ve in db.EmpresaVehiculo 
                             join em in db.Empresa on ve.nit equals em.NIT
                         where ve.nroplaca ==nroplaca  select em).FirstOrDefault ();
                if (aux != null )
                {
                    emp = aux;
                }
            }
            return emp;
        }
        public bool createEmpresa(Empresa emp, string userName)
        {
            bool sw;
            try
            {
                db.Empresa.Add(emp);
                db.SaveChanges();

                string ci = (from p in db.Persona
                         join u in db.AspNetUsers
                         on p.IdUser equals u.Id
                         where u.UserName == userName
                         select p.CI).SingleOrDefault();

                UsuarioEmpresa usuarioEmpresa = new UsuarioEmpresa
                {
                    NIT = emp.NIT,
                    CI = ci,
                    UsuaReg = userName,
                    FechaReg = DateTime.Now,
                    Activo = false
                };

                db.UsuarioEmpresa.Add(usuarioEmpresa);

                var CIsSA = (from u in db.AspNetUsers
                            join p in db.Persona
                            on u.Id equals p.IdUser
                            where u.AspNetRoles.FirstOrDefault().Name == "SA"
                            && u.UserName != userName
                            select p.CI).ToList();

                foreach (var ciSA in CIsSA)
                {
                    UsuarioEmpresa usuarioEmpresaSA = new UsuarioEmpresa
                    {
                        NIT = emp.NIT,
                        CI = ciSA,
                        UsuaReg = "sistemas",
                        FechaReg = DateTime.Now,
                        Activo = false
                    };

                    db.UsuarioEmpresa.Add(usuarioEmpresaSA);
                }

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

        public List<Empresa> GetAll2(string userName)
        {
            string ci = (from p in db.Persona
                         join u in db.AspNetUsers
                         on p.IdUser equals u.Id
                         where u.UserName == userName
                         select p.CI).SingleOrDefault();

            empresas = (from e in db.Empresa
                        join ue in db.UsuarioEmpresa
                        on e.NIT equals ue.NIT
                        where ue.CI == ci
                        select e).Distinct().ToList();

            return empresas;
        }
        public List<EmpresaModel> GetAll22(string userName)
        {
            List<EmpresaModel> lista = new List<EmpresaModel>();
            string ci = (from p in db.Persona
                         join u in db.AspNetUsers
                         on p.IdUser equals u.Id
                         where u.UserName == userName
                         select p.CI).SingleOrDefault();

            lista = (from e in db.Empresa
                        join ue in db.UsuarioEmpresa
                        on e.NIT equals ue.NIT
                        where ue.CI == ci
                        select new EmpresaModel(){NIT= e.NIT ,RazonSocial=e.RazonSocial ,email=e.email ,FechaReg =e.FechaReg}).Distinct().ToList();

            return lista ;
        }
        public List<Empresa> GetActivas(string userName)
        {
            string ci = (from p in db.Persona
                         join u in db.AspNetUsers
                         on p.IdUser equals u.Id
                         where u.UserName == userName
                         select p.CI).SingleOrDefault();

            return (from e in db.Empresa
                    join ue in db.UsuarioEmpresa
                    on e.NIT equals ue.NIT
                    where ue.CI == ci && ue.Activo == true
                    select e).ToList();
        }

        public string EmpresaActivada(string userName)
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

            var userEmpresa = (from up in db.UsuarioEmpresa
                                where up.NIT == nit && up.CI == ci
                                && up.Activo == false
                                select up).SingleOrDefault();

            if (userEmpresa != null)
            {
                userEmpresa.Activo = true;
                db.SaveChanges();

                return userEmpresa.Empresa.RazonSocial;
            }

            return "";
        }

        public bool Activar(string nit, string userName)
        {
            string ci = (from p in db.Persona
                         join u in db.AspNetUsers
                         on p.IdUser equals u.Id
                         where u.UserName == userName
                         select p.CI).SingleOrDefault();

            var usersEmpresa = (from up in db.UsuarioEmpresa
                                where up.NIT == nit && up.CI == ci
                                && up.Activo == false
                                select up).ToList();

            if (usersEmpresa.Count > 0)
            {
                foreach (var userEmpresa in usersEmpresa)
                    userEmpresa.Activo = true;

                var usersEmpresa2 = (from up in db.UsuarioEmpresa
                                     where up.NIT != nit && up.CI == ci
                                     && up.Activo == true
                                     select up).ToList();

                foreach (var userEmpresa in usersEmpresa2)
                    userEmpresa.Activo = false;

                db.SaveChanges();
                return false;
            }

            return true;
        }

        public bool Desactivar(string nit, string userName)
        {
            string ci = (from p in db.Persona
                         join u in db.AspNetUsers
                         on p.IdUser equals u.Id
                         where u.UserName == userName
                         select p.CI).SingleOrDefault();

            var usuarioEmpresa = (from ue in db.UsuarioEmpresa
                                  where ue.NIT == nit && ue.CI == ci
                                  && ue.Activo == true
                                  select ue).SingleOrDefault();

            if (usuarioEmpresa != null)
            {
                usuarioEmpresa.Activo = false;
                db.SaveChanges();

                return true;
            }

            return false;
        }


    }
}