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

namespace WISETRACK.Vistas.Alarmas
{
    public partial class Index : System.Web.UI.Page
    {
        AlarmaController alarmaCtrl;
        HomeController homeCtrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            alarmaCtrl = new AlarmaController();
            homeCtrl = new HomeController();

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    //cargar 
                }
                else { 
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        [WebMethod]
        public static string getDatos(string data = "")
        {
            string result = String.Empty;
            AlarmaController alarmaCtrl = new AlarmaController();
            HomeController homeCtrl = new HomeController();
            List<AlarmaDetalle> lista = new List<AlarmaDetalle>();
            if (SitePrincipal.ExisteActiva())
            {
                var userName = HttpContext.Current.User.Identity.Name;
                var nit = homeCtrl.obtenerNit(userName);

                lista = alarmaCtrl.GetAll(nit);

            }
            else
            {
                if (HttpContext.Current.User.IsInRole("SA"))
                {
                    lista = alarmaCtrl.GetAll();

                }
            }

            result = JsonConvert.SerializeObject(lista, Formatting.Indented);
            return result;
        }

        protected void cboestado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //private void CargarAlarmas()
        //{
        //    if (SitePrincipal.ExisteActiva())
        //    {
        //        var userName = HttpContext.Current.User.Identity.Name;
        //        var nit = homeCtrl.obtenerNit(userName);

        //        gdvAlarmas.DataSource = alarmaCtrl.GetAll(nit);
        //        gdvAlarmas.DataBind();
        //    }
        //    else
        //    {
        //        if (HttpContext.Current.User.IsInRole("SA"))
        //        {
        //            gdvAlarmas.DataSource = alarmaCtrl.GetAll();
        //            gdvAlarmas.DataBind();
        //        }
        //    }
        //}
        //private void CargarAlarmasActivas(bool p)
        //{
        //    if (SitePrincipal.ExisteActiva())
        //    {
        //        var userName = HttpContext.Current.User.Identity.Name;
        //        var nit = homeCtrl.obtenerNit(userName);

        //        gdvAlarmas.DataSource = alarmaCtrl.GetAll(nit, p);
        //        gdvAlarmas.DataBind();
        //    }
        //    else
        //    {
        //        if (HttpContext.Current.User.IsInRole("SA"))
        //        {
        //            gdvAlarmas.DataSource = alarmaCtrl.GetAll(p);
        //            gdvAlarmas.DataBind();
        //        }
        //    }
        //}

        //protected void gdvAlarmas_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "VerDetalles")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        GridViewRow row = gdvAlarmas.Rows[index];
        //        string codigo = row.Cells[0].Text;

        //        Response.Redirect("/Vistas/Alarmas/Details?cod=" + codigo);
        //    }

        //    if (e.CommandName == "Editar")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        GridViewRow row = gdvAlarmas.Rows[index];
        //        string codigo = row.Cells[0].Text;

        //        Response.Redirect("/Vistas/Alarmas/Edit?cod=" + codigo);
        //    }

        //    if (e.CommandName == "Eliminar")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        GridViewRow row = gdvAlarmas.Rows[index];
        //        string codigo = row.Cells[0].Text;

        //        Response.Redirect("/Vistas/Alarmas/Delete?cod=" + codigo);
        //    }
        //}

        //protected void gdvAlarmas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gdvAlarmas.PageIndex = e.NewPageIndex;
        //    CargarAlarmas();
        //}

        //protected void cboestado_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int estasdo = Convert.ToInt32(cboestado.SelectedValue);
        //    switch (estasdo)
        //    {
        //        case 0:
        //            CargarAlarmas();
        //            break;
        //        case 1:
        //            CargarAlarmasActivas(true);
        //            break;

        //        default:
        //            break;
        //    }
        //}
    }
}