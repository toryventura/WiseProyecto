using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WISETRACK.Datos;

namespace WISETRACK.Controller
{
    public class PrivilegioController
    {
        WISETRACKEntities db = new WISETRACKEntities();
        List<Datos.Privilegios> privilegios = new List<Datos.Privilegios>();

        public List<Datos.Privilegios> GetAll()
        {
            privilegios = (from p in db.Privilegios
                           select p).ToList();

            return privilegios;
        }

        public Datos.Privilegios Get(int codigo)
        {
            return (from p in db.Privilegios
                    where p.CodPrivilegio == codigo
                    select p).SingleOrDefault();
        }

        public void Actualizar(Datos.Privilegios privilegio, string descripcion, string dirPagina)
        {
            privilegio.Descripcion = descripcion;
            privilegio.DirPagina = dirPagina;

            db.SaveChanges();
        }

        public void Eliminar(Datos.Privilegios privilegio)
        {
            db.Privilegios.Remove(privilegio);
            db.SaveChanges();
        }

        public void Add(string descripcion, string dirPagina, string userName)
        {
            var max = db.Privilegios.DefaultIfEmpty().Max(p => p == null ? 0 : p.CodPrivilegio);

            Datos.Privilegios privilegios = new Datos.Privilegios()
            {
                CodPrivilegio = max + 1,
                Descripcion = descripcion,
                DirPagina = dirPagina,
                UsuaReg = userName,
                FechaReg = DateTime.Now
            };

            db.Privilegios.Add(privilegios);
            db.SaveChanges();
        }

        public Datos.Privilegios Get(string dirPagina, string idRol)
        {
            return (from p in db.Privilegios
                    join rp in db.RolesPrivilegios
                    on p.CodPrivilegio equals rp.CodPrivilegio
                    where p.DirPagina == dirPagina && rp.IdRol == idRol
                    select p).SingleOrDefault();
        }

        public Datos.Privilegios Get(string dirPagina)
        {
            if (!dirPagina.Equals("/"))
                return (from p in db.Privilegios
                        where p.DirPagina == dirPagina
                        select p).SingleOrDefault();
            else
                return null;

        }

        public bool IsValidaDirPagina(string dirPagina)
        {
            return true;
        }

    }
}