using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Models;

namespace WISETRACK.Vistas.Vehiculos
{
    public partial class Index : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    CargarGestionVehiculo();
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        [WebMethod]
        public static string CargarGestionVehiculo()
        {
            string result = String.Empty;
            HomeController homeCtrl = new HomeController();
            VehiculoController vehiculoCtrl = new VehiculoController();
            var user = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(user);
            List<VehiculoDetalle> lista = new List<VehiculoDetalle>();
            if (SitePrincipal.ExisteActiva())
            {
                lista = vehiculoCtrl.GetAll(nit);
                result = JsonConvert.SerializeObject(lista, Formatting.Indented);
            }
            else
            {
                if (HttpContext.Current.User.IsInRole("SA"))
                {
                    lista = vehiculoCtrl.GetAllSA();
                    result = JsonConvert.SerializeObject(lista, Formatting.Indented);
                }
            }
            return result;
        }

        [WebMethod]
        public static string pruebita(string data = "")
        {
            string result = String.Empty;
            VehiculoController ve = new VehiculoController();
            List<VehiculoDetalle> lista = new List<VehiculoDetalle>();
            lista = ve.GetAllSA();
            result = JsonConvert.SerializeObject(lista, Formatting.Indented);
            return result;
        }
        [WebMethod]
        public static string CargarConductores()
        {

            string result = String.Empty;
            VehiculoController vehiculoCtrl = new VehiculoController();
            List<PersonaCboDet> lista = new List<PersonaCboDet>();
            if (SitePrincipal.ExisteActiva())
            {
                HomeController homeCtrl = new HomeController();
                var user = HttpContext.Current.User.Identity.Name;
                var nit = homeCtrl.obtenerNit(user);
                // si esta dentro de una empresas 
                lista = vehiculoCtrl.GetNotPersCondX(nit);//
                result = JsonConvert.SerializeObject(lista, Formatting.Indented);
            }
            else
            {
                if (HttpContext.Current.User.IsInRole("SA"))
                {
                    ///si es super administrador debera traer todos los conductores de las empresas
                    lista = vehiculoCtrl.GetNotPersCondX();
                    result = JsonConvert.SerializeObject(lista, Formatting.Indented);
                }
            }
            //lista = vehiculoCtrl.GetNotPersCond(); 
            //result = JsonConvert.SerializeObject(lista, Formatting.Indented);
            return result;
        }

        private void MensajeAlerta(string mensaje)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(mensaje);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

    }
}