using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;

namespace WISETRACK.Vistas.Geocercas
{
    public partial class Index : System.Web.UI.Page
    {
        private ZonasController zonasCtrl;
        private HomeController homeCtrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            zonasCtrl = new ZonasController();
            homeCtrl = new HomeController();
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    ListarGeocercaxEmpresa(txtsearchgeocerca.Text);
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        private void ListarGeocercaxEmpresa(string nombre)
        {
            var user = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(user);

            if (SitePrincipal.ExisteActiva())
            {
                var result = zonasCtrl.ListarGeocerca(nit);
                if (!String.IsNullOrEmpty(nombre))
                {
                    result = result.Where(s => s.Descripcion.Contains(nombre)).ToList();
                }
                gdvGeocerca.DataSource = result;
                gdvGeocerca.DataBind();
            }
            else
            {
                if (User.IsInRole("SA"))
                {
                    var result1 = zonasCtrl.ListarGeocercaSA();
                    if (!String.IsNullOrEmpty(nombre))
                    {
                        result1 = result1.Where(s => s.Descripcion.Contains(nombre)).ToList();
                    }
                    gdvGeocerca.DataSource = result1;
                    gdvGeocerca.DataBind();
                }
            }
        }

        protected void gdvGeocerca_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gdvGeocerca.Rows[index];
                string id = row.Cells[0].Text;
                Response.Redirect("/Vistas/Geocercas/Delete?id=" + id);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarGeocercaxEmpresa(txtsearchgeocerca.Text);
        }


    }
}