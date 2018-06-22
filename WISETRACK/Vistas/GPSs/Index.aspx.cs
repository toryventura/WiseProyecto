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
using WISETRACK.Datos.Auxiliar;

namespace WISETRACK.Vistas.GPSs
{
    public partial class Index : System.Web.UI.Page
    {
        GpsController gps;
        EmpresaController empresaCtrl;
        HomeController homeCtrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            gps = new GpsController();
            homeCtrl = new HomeController();
            empresaCtrl = new EmpresaController();

          //  ((WebScriptManager)ScriptManager.GetCurrent(this.Page)).InfragisticsCDN.EnableCDN = DefaultableBoolean.True;
           // this.WebDataGrid1.DataSource = this.GetDataSource();

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
        public static string getDatos(string data = "")
        {
            string result = String.Empty;
            GpsController gpsCtrl = new GpsController();
            HomeController homeCtrl = new HomeController();
            List<ListarAbmGPS> lista = new List<ListarAbmGPS>();
            if (SitePrincipal.ExisteActiva())
            {
                var userName = HttpContext.Current.User.Identity.Name;
                var nit = homeCtrl.obtenerNit(userName);

                lista = gpsCtrl.listarGps(nit);

            }
            else
            {
                if (HttpContext.Current.User.IsInRole("SA"))
                {
                    lista = gpsCtrl.listarGps();

                }
            }

            result = JsonConvert.SerializeObject(lista, Formatting.Indented);
            return result;
        }

        //public ICollection GetDataSource()
        //{
        //    var user = User.Identity.Name;
        //    var nit = homeCtrl.obtenerNit(user);
        //    ICollection datasource = null;
        //    if (this.Session["AllBehaviorsDS"] == null)
        //    {
        //        if (SitePrincipal.ExisteActiva())
        //        {
        //            var detalle_imei = gps.listarGps(nit);
        //            var dts = detalle_imei.ToList();
        //            datasource = dts;
        //            this.Session.Add("AllBehaviorsDS", datasource);

        //            //this.WebDataGrid1.DataSource = detalle_imei;
        //            //this.WebDataGrid1.DataBind();
        //        }
        //        else
        //        {
        //            if (HttpContext.Current.User.IsInRole("SA"))
        //            {
        //                var detalle_imei1 = gps.listarGps();
        //                datasource = detalle_imei1;
        //                this.Session.Add("AllBehaviosDS", datasource);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (SitePrincipal.ExisteActiva())
        //        {
        //            var detalle_imei = gps.listarGps(nit);
        //            var dts = detalle_imei.ToList();
        //            datasource = dts;
        //            this.Session.Add("AllBehaviorsDS", datasource);

        //            //this.WebDataGrid1.DataSource = detalle_imei;
        //            //this.WebDataGrid1.DataBind();
        //        }
        //        else
        //        {
        //            if (HttpContext.Current.User.IsInRole("SA"))
        //            {
        //                var detalle_imei1 = gps.listarGps();
        //                datasource = detalle_imei1;
        //                this.Session.Add("AllBehaviosDS", datasource);
        //            }
        //        }
        //        //datasource = (ICollection)this.Session["AllBehaviorsDS"];
        //    }
        //    return datasource;
        //}

        //public void listarEmpresa()
        //{
        //    var user = User.Identity.Name;
        //    var nit = homeCtrl.obtenerNit(user);
        //    if (SitePrincipal.ExisteActiva())
        //    {
        //        var detalle_imei = gps.listarGps(nit);
        //        this.WebDataGrid1.DataSource = detalle_imei;
        //        this.WebDataGrid1.DataBind();
        //    }
        //    else
        //    {
        //        if (HttpContext.Current.User.IsInRole("SA"))
        //        {
        //            var detalle_imei1 = gps.listarGps();
        //            this.WebDataGrid1.DataSource = detalle_imei1;
        //            this.WebDataGrid1.DataBind();
        //        }
        //    }
        //}

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

        //protected void WebDataGrid1_ItemCommand(object sender, HandleCommandEventArgs e)
        //{
        //    if (e.CommandName == "Editar")
        //    {
        //        Object commandArgument = e.CommandArgument;
        //        var imei = commandArgument.ToString();
        //        Response.Redirect("/Vistas/GPSs/Edit?imei=" + imei);
        //    }
        //    if (e.CommandName == "Eliminar")
        //    {
        //        Object commandArgument = e.CommandArgument;
        //        var imei = commandArgument.ToString();

        //        Response.Redirect("/Vistas/GPSs/Delete?imei=" + imei);
        //    }
        //}


    }
}