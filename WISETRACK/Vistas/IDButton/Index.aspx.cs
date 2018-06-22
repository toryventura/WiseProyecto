using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Models;

namespace WISETRACK.Vistas.IDButton
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    //cargar 
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }

        }

        [WebMethod]
        public static string getDatos(string data = "")
        {
            string result = String.Empty;
            IDButtonController crl = new IDButtonController();
            HomeController homeCtrl = new HomeController();
            List<IDButtonDetalle> lista = new List<IDButtonDetalle>();
            if (SitePrincipal.ExisteActiva())
            {
                var userName = HttpContext.Current.User.Identity.Name;
                var nit = homeCtrl.obtenerNit(userName);

                lista = crl.getListaIdButtuns(nit, 0);

            }
            else
            {
                if (HttpContext.Current.User.IsInRole("SA"))
                {
                    lista = crl.getListaIdButtuns("", 1);

                }
            }

            result = JsonConvert.SerializeObject(lista, Formatting.Indented);
            return result;
        }


        [WebMethod]
        public static string Eliminar(int id = 0)
        {
            string result = String.Empty;
            IDButtonController crl = new IDButtonController();
            HomeController homeCtrl = new HomeController();
            //if (SitePrincipal.ExisteActiva())
            //{
            var userName = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(userName);

            result = crl.delete(id);



            return result;
        }

        [WebMethod]
        public static string Activar(int id = 0)
        {
            string result = String.Empty;
            IDButtonController crl = new IDButtonController();
            HomeController homeCtrl = new HomeController();
            if (SitePrincipal.ExisteActiva())
            {
                var userName = HttpContext.Current.User.Identity.Name;
                var nit = homeCtrl.obtenerNit(userName);

                result = crl.Activar(id);

            }

            return result;
        }

    }
}