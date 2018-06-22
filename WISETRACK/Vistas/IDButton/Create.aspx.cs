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
using WISETRACK.Datos;
using WISETRACK.Datos.Auxiliar;

namespace WISETRACK.Vistas.IDButton
{
    public partial class Create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    if (!SitePrincipal.ExisteActiva())
                    {
                        //       listarEmpresa();

                        Response.Redirect("~/Vistas/Empresas/Panel");

                    }
                }
                else
                {
                    this.Session.Remove("AllBehaviorsDS");
                    Response.Redirect("~/Account/Login");
                }
            }

        }
        [WebMethod]
        public static string getDatos(string data = "")
        {
            string result = String.Empty;
            GpsController gpsCtrl = new GpsController();
            HomeController homeCtrl = new HomeController();
            List<ListarGPSPlaca> lista = new List<ListarGPSPlaca>();
            if (SitePrincipal.ExisteActiva())
            {
                var userName = HttpContext.Current.User.Identity.Name;
                var nit = homeCtrl.obtenerNit(userName);

                lista = gpsCtrl.listarGpsPlaca(nit);

            }
           

            result = JsonConvert.SerializeObject(lista, Formatting.Indented);
            return result;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] 
        public static string setguardar(object list, string key = "", string alias = "")
        {
            string result = String.Empty;
            string[] lis = list.ToString().Split(',');
            List<string> lista = lis.Select(x => x).ToList<string>();
            HomeController homeCtrl = new HomeController();
            var userName = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(userName);

            IDButtonController button = new IDButtonController();
            IdButton o = new IdButton()
            {
                Estado = true,
                Alias = alias,
                Keys = key,
                FechaReg = DateTime.Now,
                UsuaReg = userName
            };
            result = button.add(o, userName, nit, lista);
            return result;
        }
    }
}