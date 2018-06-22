using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos.Auxiliar;
using WISETRACK.Models;

namespace WISETRACK.Vistas.Seguimientos
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {

                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        //public void listarSeguimiento(string tipo, string search)
        //{
        //    var user = HttpContext.Current.User.Identity.Name;
        //    var nit = homeCtrl.obtenerNit(user);
        //    if (SitePrincipal.ExisteActiva())
        //    {
        //        var detalle_asignacion_seguimiento = seguimientoCtrl.listarAsignacionSeguimiento(nit);
        //        if (!String.IsNullOrEmpty(search))
        //        {
        //            switch (tipo)
        //            {
        //                case "1":
        //                    detalle_asignacion_seguimiento = detalle_asignacion_seguimiento.Where(det => det.NroPlaca.Contains(search)).ToList();
        //                    break;
        //                case "2":
        //                    detalle_asignacion_seguimiento = detalle_asignacion_seguimiento.Where(det => det.IMEI.Contains(search)).ToList();
        //                    break;
        //            }
        //        }
        //        gdvseguimiento.DataSource = detalle_asignacion_seguimiento;
        //        gdvseguimiento.DataBind();
        //    }
        //    else
        //    {
        //        var detalle_asignacion_seguimiento1 = seguimientoCtrl.ListarAsignacionSeguimientoSA();
        //        if (User.IsInRole("SA"))
        //        {
        //            if (!String.IsNullOrEmpty(search))
        //            {
        //                switch (tipo)
        //                {
        //                    case "1":
        //                        detalle_asignacion_seguimiento1 = detalle_asignacion_seguimiento1.Where(det => det.NroPlaca.Contains(search)).ToList();
        //                        break;
        //                    case "2":
        //                        detalle_asignacion_seguimiento1 = detalle_asignacion_seguimiento1.Where(det => det.IMEI.Contains(search)).ToList();
        //                        break;
        //                }
        //            }
        //        }
        //        gdvseguimiento.DataSource = detalle_asignacion_seguimiento1;
        //        gdvseguimiento.DataBind();
        //    }
        //}

        //public void listarSeguimientoActivos(string tipo, string search)
        //{
        //    var user = HttpContext.Current.User.Identity.Name;
        //    var nit = homeCtrl.obtenerNit(user);
        //    if (SitePrincipal.ExisteActiva())
        //    {
        //        var detalle_asignacion_seguimiento = seguimientoCtrl.listarAsignacionSeguimiento(nit);
        //        detalle_asignacion_seguimiento = detalle_asignacion_seguimiento.Where(e => e.estado == "Activo").ToList();
        //        if (!String.IsNullOrEmpty(search))
        //        {
        //            switch (tipo)
        //            {
        //                case "1":
        //                    detalle_asignacion_seguimiento = detalle_asignacion_seguimiento.Where(det => det.NroPlaca.Contains(search)).ToList();
        //                    break;
        //                case "2":
        //                    detalle_asignacion_seguimiento = detalle_asignacion_seguimiento.Where(det => det.IMEI.Contains(search)).ToList();
        //                    break;
        //            }
        //        }
        //        gdvseguimiento.DataSource = detalle_asignacion_seguimiento;
        //        gdvseguimiento.DataBind();
        //    }
        //    else
        //    {
        //        if (User.IsInRole("SA"))
        //        {
        //            var detalle_asignacion_seguimiento1 = seguimientoCtrl.ListarAsignacionSeguimientoSA();
        //            detalle_asignacion_seguimiento1 = detalle_asignacion_seguimiento1.Where(r => r.estado == "Activo").ToList();
        //            if (!String.IsNullOrEmpty(search))
        //            {
        //                switch (tipo)
        //                {
        //                    case "1":
        //                        detalle_asignacion_seguimiento1 = detalle_asignacion_seguimiento1.Where(det => det.NroPlaca.Contains(search)).ToList();
        //                        break;
        //                    case "2":
        //                        detalle_asignacion_seguimiento1 = detalle_asignacion_seguimiento1.Where(det => det.IMEI.Contains(search)).ToList();
        //                        break;
        //                }
        //            }
        //            gdvseguimiento.DataSource = detalle_asignacion_seguimiento1;
        //            gdvseguimiento.DataBind();
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

        [WebMethod]
        public static string getDatos(string data = "")
        {
            string result = String.Empty;
            SeguimientoController segCtrl = new SeguimientoController();
            HomeController homeCtrl = new HomeController();
            ////List<ListarAbmAsignacionSeguimiento> lista = new List<ListarAbmAsignacionSeguimiento>();

            List<clsSeguimiento> lista = new List<clsSeguimiento>();

            if (SitePrincipal.ExisteActiva())
            {
                var userName = HttpContext.Current.User.Identity.Name;
                var nit = homeCtrl.obtenerNit(userName);
                lista = segCtrl.listarAsignacionSeguimiento(nit);
            }
            else
            {
                if (HttpContext.Current.User.IsInRole("SA"))
                {
                    lista = segCtrl.ListarAsignacionSeguimientoSA();
                }
            }

            result = JsonConvert.SerializeObject(lista, Formatting.Indented);
            return result;
        }
    }
}