using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WISETRACK.Datos;

namespace WISETRACK.Controller
{
    public class HomeController
    {
        WISETRACKEntities db = new WISETRACKEntities();

        public string obtenerNit(string user)
        {
            string ci = (from p in db.Persona
                         join u in db.AspNetUsers
                         on p.IdUser equals u.Id
                         where u.UserName == user
                         select p.CI).SingleOrDefault();

            string nit = (from e in db.Empresa
                          join ue in db.UsuarioEmpresa
                          on e.NIT equals ue.NIT
                          where ue.CI == ci && ue.Activo == true
                          select e.NIT).SingleOrDefault();

            return nit;

        }

        public string nombreEmpresa(string nit)
        {
            var result = db.Empresa.Where(s => s.NIT == nit).SingleOrDefault().RazonSocial;
            return result.ToString();
        }
    }
}