using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Models;

namespace WISETRACK.Vistas.TipoGeocerca
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    CargarGestionTipoZona();
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        [WebMethod]
        public static string CargarGestionTipoZona()
        {
            string result = String.Empty;
            HomeController homeCtrl = new HomeController();
            ZonasController zonaCtrl = new ZonasController();
            var user = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(user);
            List<TipoGeocercaDetalle> lista = new List<TipoGeocercaDetalle>();
            if (SitePrincipal.ExisteActiva())
            {
                lista = zonaCtrl.GetTiposGeocerca(nit);
                result = JsonConvert.SerializeObject(lista, Formatting.Indented);
            }
            else
            {
                if (HttpContext.Current.User.IsInRole("SA"))
                {
                    lista = zonaCtrl.GetTiposGeocercaSA();
                    result = JsonConvert.SerializeObject(lista, Formatting.Indented);
                }
            }
            return result;
        }

        //private void CargarTiposGeocerca()
        //{
        //    userName = User.Identity.Name;
        //    nit = homeCtrl.obtenerNit(userName);
        //    if (SitePrincipal.ExisteActiva())
        //    {
        //        this.WebDataGrid1.DataSource = zonasCtrl.GetTiposGeocerca(nit);
        //        this.WebDataGrid1.DataBind();
        //    }
        //    else
        //    {
        //        if (User.IsInRole("SA"))
        //        {
        //            this.WebDataGrid1.DataSource = zonasCtrl.GetTiposGeocercaSA();
        //            this.WebDataGrid1.DataBind();
        //        }
        //    }
        //}

        //protected void WebDataGrid1_ItemCommand(object sender, HandleCommandEventArgs e)
        //{
        //    if (e.CommandName == "Editar")
        //    {
        //        Object commandArgument = e.CommandArgument;
        //        string CodTipoGEO = commandArgument.ToString();
        //        Response.Redirect("/Vistas/TipoGeocerca/Edit?CodTipoGEO=" + CodTipoGEO);
        //    }
        //}

        //private ICollection GetDataSource()
        //{
        //    userName = User.Identity.Name;
        //    nit = homeCtrl.obtenerNit(userName);
        //    ICollection datasource = null;
        //    if (this.Session["AllBehaviorsDS"] == null)
        //    {
        //        if (SitePrincipal.ExisteActiva())
        //        {
        //           var dts = zonasCtrl.GetTiposGeocerca(nit);
        //           datasource=dts;
        //            this.Session.Add("AllBehaviorsDS", datasource);
        //        }
        //        else
        //        {
        //            if (User.IsInRole("SA"))
        //            {
        //               var dts = zonasCtrl.GetTiposGeocercaSA();
        //               datasource = dts;
        //                this.Session.Add("AllBehaviorsDS", datasource);
        //            }
        //        }

        //    }
        //    else
        //    {
        //        if (SitePrincipal.ExisteActiva())
        //        {
        //            var dts = zonasCtrl.GetTiposGeocerca(nit);
        //            datasource = dts;
        //            this.Session.Add("AllBehaviorsDS", datasource);
        //        }
        //        else
        //        {
        //            if (User.IsInRole("SA"))
        //            {
        //                var dts = zonasCtrl.GetTiposGeocercaSA();
        //                datasource = dts;
        //                this.Session.Add("AllBehaviorsDS", datasource);
        //            }
        //        }
        //        //datasource = (ICollection)this.Session["AllBehaviorsDS"];
        //    }
        //    return datasource;
        //}
    }
}