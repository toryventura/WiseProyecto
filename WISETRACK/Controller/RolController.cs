using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WISETRACK.Datos;

namespace WISETRACK.Controller
{
    public class RolController
    {
        WISETRACKEntities db = new WISETRACKEntities();
        List<AspNetRoles> roles = new List<AspNetRoles>();

        List<Datos.Privilegios> notPrivilegios;

        public List<AspNetRoles> GetAll(string userName)
        {
            var nivel = (from u in db.AspNetUsers
                         where u.UserName == userName
                         select u.AspNetRoles.FirstOrDefault()
                        ).FirstOrDefault().RolNivel.FirstOrDefault().IdNivel;

            roles = (from rn in db.RolNivel
                     where rn.IdNivel >= nivel
                     orderby rn.IdNivel
                     select rn.AspNetRoles).ToList();

            return roles;
        }

        public List<Datos.Privilegios> GetPrivilegios(string rol)
        {
            var idRol = (from r in db.AspNetRoles
                         where r.Name == rol
                         select r.Id).SingleOrDefault();

            var privilegios = (from rp in db.RolesPrivilegios
                           where rp.IdRol == idRol
                           select rp.Privilegios).ToList();

            return privilegios;
        }

        public List<Datos.Privilegios> GetNotPrivilegios(string rol)
        {
            notPrivilegios = new List<Datos.Privilegios>();

            var idRol = (from r in db.AspNetRoles
                         where r.Name == rol
                         select r.Id).SingleOrDefault();

            var privilegios = (from rp in db.RolesPrivilegios
                           where rp.IdRol == idRol
                           select rp.Privilegios).ToList();

            var consulta = (from p in db.Privilegios
                            select p).ToList();

            foreach (var privilegio in consulta)
            {
                if (!privilegios.Contains(privilegio))
                    notPrivilegios.Add(privilegio);
            }

            return notPrivilegios;
        }

        public List<Datos.Privilegios> GetPrivilegios2(string id)
        {
            var privilegios = (from rp in db.RolesPrivilegios
                               where rp.IdRol == id
                               select rp.Privilegios).ToList();

            return privilegios;
        }

        public List<Nivel> GetNiveles(string userName)
        {
            var idRol = (from u in db.AspNetUsers
                         where u.UserName == userName
                         select u.AspNetRoles.FirstOrDefault().Id).FirstOrDefault();

            var idNivel = (from rn in db.RolNivel
                         where rn.IdRol == idRol
                         select rn.IdNivel).FirstOrDefault();

            var niveles = (from n in db.Nivel
                           where n.Id >= idNivel
                           select n).ToList();

            return niveles;
        }

        public void AddNivel(string id, int idNivel, bool ok)
        {
            //var max = db.Nivel.DefaultIfEmpty().Max(n => n == null ? 0 : n.Id);

            if (ok)
            {
                Nivel nivel = new Nivel
                {
                    Id = idNivel,
                    Nombre = "SA"
                };

                db.Nivel.Add(nivel);
                db.SaveChanges();
            }

            RolNivel rolNivel = new RolNivel
            {
                IdRol = id,
                IdNivel = idNivel,
                UsuaReg = "sistemas",
                FechaReg = DateTime.Now
            };

            db.RolNivel.Add(rolNivel);
            db.SaveChanges();
        }

        public void CargarPrivilegiosSA(string id)
        {
            string[] descripPrivs = new string[]
            {
                "Principal", "Acerca", "Contacto",
                "Configuracion", "Privilegios", "Crear Privilegio",
                    "Editar Privilegio", "Eliminar Privilegio",
                "Roles", "Crear Rol", "Ver Privilegios", "Agregar Privilegio",
                "Usuarios", "Registrar Usuario",
                    "Editar Usuario", "Eliminar Usuario",
                "Administracion", "Empresas", "Panel"
            };

            string[] dirPagPrivs = new string[]
            {
                "/Principal", "/Acerca", "/Contacto",
                "/", "/Vistas/Privilegios/Index", "/Vistas/Privilegios/Create", 
                    "/Vistas/Privilegios/Edit", "/Vistas/Privilegios/Delete",
                "/Vistas/Roles/Index", "/Vistas/Roles/Create",
                    "/Vistas/Roles/DetailsPrivilegio", "/Vistas/Roles/AddPrivilegio",
                "/Vistas/Usuarios/Index", "/Account/Register",
                    "/Vistas/Usuarios/Edit", "/Vistas/Usuarios/Delete",
                "/", "/Vistas/Empresas/Index", "/Vistas/Empresas/Panel"
            };

            for (int i = 0; i < descripPrivs.Length; i++)
            {
                Datos.Privilegios privilegio = new Datos.Privilegios()
                {
                    CodPrivilegio = i + 1,
                    Descripcion = descripPrivs[i],
                    DirPagina = dirPagPrivs[i],
                    UsuaReg = "sistemas",
                    FechaReg = DateTime.Now
                };

                db.Privilegios.Add(privilegio);

                RolesPrivilegios rolesPrivs = new RolesPrivilegios()
                {
                    IdRol = id,
                    CodPrivilegio = i + 1,
                    UsuaReg = "sistemas",
                    FechaReg = DateTime.Now
                };

                db.RolesPrivilegios.Add(rolesPrivs);
            }

            db.SaveChanges();
        }

        public void CargarPrivilegios(string id, string userName)
        {
            var privilegios = (from p in db.Privilegios
                               where p.CodPrivilegio <= 3 || p.CodPrivilegio == 17 
                               || p.CodPrivilegio == 18
                               select p).ToList();

            foreach (var privilegio in privilegios)
            {
                RolesPrivilegios rolesPrivs = new RolesPrivilegios()
                {
                    IdRol = id,
                    CodPrivilegio = privilegio.CodPrivilegio,
                    UsuaReg = userName,
                    FechaReg = DateTime.Now
                };

                db.RolesPrivilegios.Add(rolesPrivs);
            }

            db.SaveChanges();
        }

        public void AddPrivilegio(string rol, int codPriv, string userName)
        {
            var idRol = (from r in db.AspNetRoles
                         where r.Name == rol
                         select r.Id).SingleOrDefault();

            RolesPrivilegios rolesPrivs = new RolesPrivilegios()
            {
                IdRol = idRol,
                CodPrivilegio = codPriv,
                UsuaReg = userName,
                FechaReg = DateTime.Now
            };

            db.RolesPrivilegios.Add(rolesPrivs);
            db.SaveChanges();
        }

        public void RemovePrivilegio(string rol, int codPriv)
        {
            var idRol = (from r in db.AspNetRoles
                         where r.Name == rol
                         select r.Id).SingleOrDefault();

            var rolesPrivs = (from rp in db.RolesPrivilegios
                              where rp.IdRol == idRol
                              && rp.CodPrivilegio == codPriv
                              select rp).FirstOrDefault();

            if (rolesPrivs != null)
            {
                db.RolesPrivilegios.Remove(rolesPrivs);
                db.SaveChanges();
            }
        }

        public void AddDefaultPrivilegios(string rol)
        {
            var idRol = (from r in db.AspNetRoles
                         where r.Name == rol
                         select r.Id).SingleOrDefault();

            var privilegios = (from p in db.Privilegios
                               where p.CodPrivilegio <= 3 || p.CodPrivilegio == 17 
                               || p.CodPrivilegio == 18
                               select p).ToList();

            foreach (var privilegio in privilegios)
            {
                RolesPrivilegios rolesPrivs = new RolesPrivilegios()
                {
                    IdRol = idRol,
                    CodPrivilegio = privilegio.CodPrivilegio,
                    UsuaReg = "sistemas",
                    FechaReg = DateTime.Now
                };

                db.RolesPrivilegios.Add(rolesPrivs);
            }

            db.SaveChanges();
        }

    }
}