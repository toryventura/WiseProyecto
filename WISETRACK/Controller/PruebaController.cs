using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WISETRACK.Datos;

namespace WISETRACK.Controller
{
    public class PruebaController
    {
        WISETRACKEntities db = new WISETRACKEntities();

        public List<Prueba> GetAll()
        {
            try
            {
                return (from p in db.Prueba
                        orderby p.Nro descending
                        select p).Take(15).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new List<Prueba>();
            }
        }

        public List<string> GetMensajes()
        {
            try
            {
                return (from p in db.Prueba
                        orderby p.Nro descending
                        select p.Mensaje).Take(15).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new List<string>();
            }
        }

        public List<int> GetNros()
        {
            try
            {
                return (from p in db.Prueba
                        orderby p.Nro descending
                        select p.Nro).Take(15).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new List<int>();
            }
        }

        public Prueba Get(int nro)
        {
            try
            {
                return (from p in db.Prueba
                        where p.Nro == nro
                        select p).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}