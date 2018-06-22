using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK.Roles
{
    public partial class DetailsPrivilegio : System.Web.UI.Page
    {
        RolController rolCtrl;

        private string rol;

        protected void Page_Load(object sender, EventArgs e)
        {
            rolCtrl = new RolController();

            if (!IsPostBack)
            {
                if(!SitePrincipal.IsIntruso())
                    CargarPrivilegios();
                else
                    Response.Redirect("~/Account/Login");
            }
        }

        private void CargarPrivilegios()
        {
            rol = Request.QueryString["rol"];

            gdvPrivilegios.DataSource = rolCtrl.GetPrivilegios(rol);
            gdvPrivilegios.DataBind();
        }

        protected void gdvPrivilegios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Quitar")
            {
                
            }

        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            rol = Request.QueryString["rol"];
            Response.Redirect("~/Vistas/Roles/AddPrivilegio?rol=" + rol);
        }

        protected void gdvPrivilegios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvPrivilegios.PageIndex = e.NewPageIndex;
            CargarPrivilegios();
        }

        protected void Quitar_Click(object sender, EventArgs e)
        {
            rol = Request.QueryString["rol"];
            Response.Redirect("~/Vistas/Roles/RemovePrivilegio?rol=" + rol);
        }
    }
}