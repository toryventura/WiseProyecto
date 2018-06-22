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

namespace WISETRACK.Vistas.AsignarConductor
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    //       listarEmpresa();
                }
                else
                {
                    this.Session.Remove("AllBehaviorsDS");
                    Response.Redirect("~/Account/Login");
                }
            }

        }

        [WebMethod]
        public static string  getDatos(string data = "")
        {
            string result = String.Empty;
            AsignarConductorController gpsCtrl = new AsignarConductorController();
            HomeController homeCtrl = new HomeController();
            List<ConductorAsiganadoDet> lista = new List<ConductorAsiganadoDet>();
            if (SitePrincipal.ExisteActiva())
            {
                var userName = HttpContext.Current.User.Identity.Name;
                var nit = homeCtrl.obtenerNit(userName);

                lista = gpsCtrl.getlistaAsignados(nit, 1);

            }
            else
            {
                if (HttpContext.Current.User.IsInRole("SA"))
                {
                    lista = gpsCtrl.getlistaAsignados("", 0);

                }
            }

            result = JsonConvert.SerializeObject(lista, Formatting.Indented);
            return result;
        }


        [WebMethod]
        public static string Finalizar(int id = 0)
        {
            string result = String.Empty;
            AsignarConductorController gpsCtrl = new AsignarConductorController();
            HomeController homeCtrl = new HomeController();
            List<ConductorAsiganadoDet> lista = new List<ConductorAsiganadoDet>();
            if (SitePrincipal.ExisteActiva())
            {
                var userName = HttpContext.Current.User.Identity.Name;
                var nit = homeCtrl.obtenerNit(userName);

                //lista = gpsCtrl.getlistaAsignados(nit, 1);
                result = gpsCtrl.finalizar(id);

            }

            return result;
        }
    }
}