using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Models;
using WISETRACK.Datos;

namespace WISETRACK.Vistas.AsignarConductor
{
    public partial class Create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    if (SitePrincipal.ExisteActiva())
                    {

                    }
                    else
                    {

                        Response.Redirect("~/Vistas/Empresas/Panel");
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }

        }
        [WebMethod]
        public static string CargarConductores()
        {

            string result = String.Empty;
            AsignarConductorController asigctl = new AsignarConductorController();
            List<PersonaCboDet> lista = new List<PersonaCboDet>();
            HomeController homeCtrl = new HomeController();
            var user = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(user);
            // si esta dentro de una empresas 
            lista = asigctl.GetNotPersCondX(nit);//
            result = JsonConvert.SerializeObject(lista, Formatting.Indented);
            return result;
        }
        [WebMethod]
        public static string CargarVehiculos()
        {

            string result = String.Empty;
            AsignarConductorController asigctrl = new AsignarConductorController();
            List<VehiculoCboDet> lista = new List<VehiculoCboDet>();
            HomeController homeCtrl = new HomeController();
            var user = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(user);
            // si esta dentro de una empresas 
            lista = asigctrl.getVehiculosNoAsiganados(nit);//
            result = JsonConvert.SerializeObject(lista, Formatting.Indented);
            return result;
        }
        [WebMethod]
        public static string getKeys(string nroplaca = "")
        {

            string result = String.Empty;
            AsignarConductorController asigctrl = new AsignarConductorController();
            HomeController homeCtrl = new HomeController();
            var user = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(user);
            // si esta dentro de una empresas 
            result = asigctrl.getkeys(nroplaca);//
            return result;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string setguardar(object list, string ci = "", string nroplaca = "")
        {
            string result = String.Empty;
            string[] lis = list.ToString().Split(',');
            List<string> lista = lis.Select(x => x).ToList<string>();
            HomeController homeCtrl = new HomeController();
            var userName = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(userName);

            AsignarConductorController Asign = new AsignarConductorController();
            VehiculoConductor o = new VehiculoConductor
            {
                CI = ci,
                Asignado = true,
                Fecha = DateTime.Now,
                FechaReg = DateTime.Now,
                NroPlaca = nroplaca

            };
            result = Asign.add(o, userName, nit, lista);
            return result;
        }
    }
}