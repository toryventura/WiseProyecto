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
    public partial class Index : System.Web.UI.Page
    {
        RolController rolCtrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            rolCtrl = new RolController();

            if (!IsPostBack)
            {
                if(!SitePrincipal.IsIntruso())
                    CargarRoles();
                else
                    Response.Redirect("~/Account/Login");
            }
        }

        private void CargarRoles()
        {
            string userName = HttpContext.Current.User.Identity.Name;

            gdvRoles.DataSource = rolCtrl.GetAll(userName);
            gdvRoles.DataBind();
        }

        protected void gdvPrivilegios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerPrivilegios")
            {
                int pos = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                string rol = Convert.ToString(gdvRoles.Rows[pos].Cells[0].Text);

                Response.Redirect("~/Vistas/Roles/DetailsPrivilegio?rol=" + rol);
            }

        }
    }
}