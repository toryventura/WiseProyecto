using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WISETRACK.Datos;
using WISETRACK.Datos.Auxiliar;
using WISETRACK.Models;

namespace WISETRACK.Controller
{
    public class UsuarioController
    {
        WISETRACKEntities db = new WISETRACKEntities();
        List<UsuarioDetalle> usuarios = new List<UsuarioDetalle>();

        public List<ListarAbmUsuario> ListarUsuarioPorEmpresa(string nit)
        {
            try
            {
                var usuariosnit = (from aspu in db.AspNetUsers
                                   join p in db.Persona on aspu.Id equals p.IdUser
                                   join ue in db.UsuarioEmpresa on p.CI equals ue.CI
                                   join e in db.Empresa on ue.NIT equals e.NIT
                                   where e.NIT == nit
                                   select new ListarAbmUsuario
                                   {
                                       Email = aspu.Email,
                                       Persona = aspu.Persona.FirstOrDefault() != null ? aspu.Persona.FirstOrDefault().Nombre
                                                    + (aspu.Persona.FirstOrDefault().ApellidoP != null ? " " + aspu.Persona.FirstOrDefault().ApellidoP : "")
                                                    + (aspu.Persona.FirstOrDefault().ApellidoM != null ? " " + aspu.Persona.FirstOrDefault().ApellidoM : "") : "(Ninguna)",
                                       UserName = aspu.UserName,
                                       UserRole = aspu.AspNetRoles.FirstOrDefault().Name
                                   }).ToList();

                usuariosnit = usuariosnit.Where(r => r.UserRole != "SA").ToList();
                return usuariosnit;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ListarAbmUsuario> GetAllSA()
        {
            try
            {
                var usuariossa = (from aspu in db.AspNetUsers
                                  //join p in db.Persona on aspu.Id equals p.IdUser
                                  //join ue in db.UsuarioEmpresa on p.CI equals ue.CI
                                  //join e in db.Empresa on ue.NIT equals e.NIT
                                  select new ListarAbmUsuario
                                  {
                                      Email = aspu.Email,
                                      Persona = aspu.Persona.FirstOrDefault() != null ? aspu.Persona.FirstOrDefault().Nombre
                                                   + (aspu.Persona.FirstOrDefault().ApellidoP != null ? " " + aspu.Persona.FirstOrDefault().ApellidoP : "")
                                                   + (aspu.Persona.FirstOrDefault().ApellidoM != null ? " " + aspu.Persona.FirstOrDefault().ApellidoM : "") : "(Ninguna)",
                                      UserName = aspu.UserName,
                                      UserRole = aspu.AspNetRoles.FirstOrDefault().Name
                                  });
                return usuariossa.OrderBy(q => q.UserName).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<UsuarioDetalle> GetAll(string userName)
        {
            try
            {
                if (!(userName.Equals("sistemas") && HttpContext.Current.User.IsInRole("SA")))
                {
                    usuarios = (from u in db.AspNetUsers
                                where u.UserName != "sistemas"
                                select new UsuarioDetalle
                                {
                                    Id = u.Id,
                                    UserName = u.UserName,
                                    Email = u.Email,
                                    UserRole = u.AspNetRoles.FirstOrDefault().Name,
                                    Persona = u.Persona.FirstOrDefault() != null ? u.Persona.FirstOrDefault().Nombre
                                                + (u.Persona.FirstOrDefault().ApellidoP != null ? " " + u.Persona.FirstOrDefault().ApellidoP : "")
                                                + (u.Persona.FirstOrDefault().ApellidoM != null ? " " + u.Persona.FirstOrDefault().ApellidoM : "") : "(Ninguna)"
                                }).ToList();
                }
                else
                {
                    usuarios = (from u in db.AspNetUsers
                                select new UsuarioDetalle
                                {
                                    Id = u.Id,
                                    UserName = u.UserName,
                                    Email = u.Email,
                                    UserRole = u.AspNetRoles.FirstOrDefault().Name,
                                    Persona = u.Persona.FirstOrDefault() != null ? u.Persona.FirstOrDefault().Nombre
                                                + (u.Persona.FirstOrDefault().ApellidoP != null ? " " + u.Persona.FirstOrDefault().ApellidoP : "")
                                                + (u.Persona.FirstOrDefault().ApellidoM != null ? " " + u.Persona.FirstOrDefault().ApellidoM : "") : "(Ninguna)"
                                }).ToList();
                }

                return usuarios.OrderBy(e => e.UserName).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public AspNetUsers Get(string userName)
        {
            return (from u in db.AspNetUsers
                    where u.UserName == userName
                    select u).SingleOrDefault();
        }

        public void Actualizar(AspNetUsers usuario, string email, string ci, string rol, string userName)
        {
            usuario.Email = email;

            if (usuario.Persona.FirstOrDefault() != null)
            {
                var persona = (from p in db.Persona
                               where p.IdUser == usuario.Id
                               select p).FirstOrDefault();

                if (!ci.Equals("0"))
                {
                    persona.IdUser = usuario.Id;
                    persona.CodTipo = 2;

                    if (rol.Equals("SA"))
                        AddUsuarioEmpresaSA(persona.CI, userName);

                }
                else
                {
                    persona.IdUser = null;
                    persona.CodTipo = 1;

                    if (rol.Equals("SA"))
                        DelUsuarioEmpresaSA(persona.CI);

                }
            }
            else
            {
                if (!ci.Equals("0"))
                {
                    var persona = db.Persona.Where(p => p.CI == ci).FirstOrDefault();

                    usuario.Email = persona.Email;
                    usuario.PhoneNumber = persona.Telefono;

                    persona.IdUser = usuario.Id;
                    persona.CodTipo = 2;

                    if (rol.Equals("SA"))
                        AddUsuarioEmpresaSA(persona.CI, userName);

                }
            }

            db.SaveChanges();
        }

        /// <summary>
        /// Cambio en la tabla UsuarioEmpresa, campo agregado Estado
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="userName"></param>
        private void AddUsuarioEmpresaSA(string ci, string userName)
        {
            var idEmpresa = (from p in db.Persona
                             join ue in db.UsuarioEmpresa on p.CI equals ue.CI
                             where p.CI == ci && ue.Estado == true
                             select ue.NIT).FirstOrDefault();

            var nitEmpresas = (from e in db.Empresa
                               select e.NIT).ToList();

            foreach (var nit in nitEmpresas)
            {
                if (!nit.Equals(idEmpresa))
                {
                    UsuarioEmpresa usuarioEmpresa = new UsuarioEmpresa
                    {
                        NIT = nit,
                        CI = ci,
                        UsuaReg = userName,
                        FechaReg = DateTime.Now,
                        Activo = false
                    };

                    db.UsuarioEmpresa.Add(usuarioEmpresa);
                }
            }
        }

        /// <summary>
        /// Cambio en la tabla UsuarioEmpresa, campo agregado Estado
        /// </summary>
        /// <param name="ci"></param>
        private void DelUsuarioEmpresaSA(string ci)
        {
            var idEmpresa = (from p in db.Persona
                             join ue in db.UsuarioEmpresa on
                             p.CI equals ue.CI
                             where p.CI == ci && ue.Estado == true
                             select ue.NIT).FirstOrDefault();

            var nitEmpresas = (from e in db.Empresa
                               select e.NIT).ToList();

            foreach (var nit in nitEmpresas)
            {
                if (!nit.Equals(idEmpresa))
                {
                    var usuarioEmpresa = (from ue in db.UsuarioEmpresa
                                          where ue.CI == ci && ue.NIT == nit
                                          select ue).FirstOrDefault();

                    db.UsuarioEmpresa.Remove(usuarioEmpresa);
                }
            }
        }

        public void Actualizar2(AspNetUsers usuario, string email, string telefono)
        {
            usuario.Email = email;
            usuario.PhoneNumber = telefono;

            db.SaveChanges();
        }

        public void Eliminar(AspNetUsers usuario)
        {
            var persona = (from p in db.Persona
                           where p.IdUser == usuario.Id
                           select p).FirstOrDefault();

            if (persona != null)
            {
                persona.IdUser = null;
                persona.CodTipo = 1;
            }

            db.Database.ExecuteSqlCommand("DELETE FROM dbo.AspNetUserRoles WHERE UserId = '" + Convert.ToString(usuario.Id) + "'");

            db.AspNetUsers.Remove(usuario);
            db.SaveChanges();
        }

        public void CerrarSesion(string userName)
        {
            string ci = (from p in db.Persona
                         join u in db.AspNetUsers
                         on p.IdUser equals u.Id
                         where u.UserName == userName
                         select p.CI).SingleOrDefault();

            var userEmpresa = (from ue in db.UsuarioEmpresa
                               where ue.CI == ci && ue.Activo == true
                               select ue).SingleOrDefault();

            if (userEmpresa != null)
            {
                userEmpresa.Activo = false;
                db.SaveChanges();
            }
        }

        public bool AsignPersona(string userName)
        {

            var persona = (from u in db.AspNetUsers
                           join p in db.Persona on
                           u.Id equals p.IdUser
                           where p.Estado == true && u.UserName == userName
                           select p).SingleOrDefault();

            return persona != null;
        }

        public List<PersonaCboDet> GetNotPersUser(string id)
        {
            var personas = (from p in db.Persona
                            where (p.TipoPersona.CodTipo == 1 || p.TipoPersona.CodTipo == 2)
                            && (p.AspNetUsers == null || p.AspNetUsers.Id == id)
                            select new PersonaCboDet
                            {
                                CI = p.CI,
                                NombreCompleto = p.Nombre + (p.ApellidoP != null ? " " + p.ApellidoP : "")
                                                    + (p.ApellidoM != null ? " " + p.ApellidoM : "")
                            }).ToList();

            return personas;
        }
    }
}