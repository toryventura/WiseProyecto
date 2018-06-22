using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;

namespace WISETRACK.Vistas.TipoGeocerca
{
    public partial class Create : System.Web.UI.Page
    {
        string userName, nit;

        HomeController homeCtrl;
        ZonasController zonasCtrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            homeCtrl = new HomeController();
            zonasCtrl = new ZonasController();

            if (!IsPostBack)
            {
                if (SitePrincipal.IsIntruso())
                    Response.Redirect("~/Account/Login");
            }
        }

        protected void Crear_Click(object sender, EventArgs e)
        {
            string descripcion = Descripcion.Text.ToUpper();

            userName = User.Identity.Name;
            nit = homeCtrl.obtenerNit(userName);

            if (zonasCtrl.AddTipoGeocerca(descripcion, nit, userName))
                Response.Redirect("~/Vistas/TipoGeocerca/Index");
            else
                ErrorMessage.Text = "TIPO DE ZONA YA EXISTENTE";

        }
    }
}