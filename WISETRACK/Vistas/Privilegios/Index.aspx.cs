using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK.Privilegios
{
    public partial class Index : System.Web.UI.Page
    {
        PrivilegioController privilegioCtrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            privilegioCtrl = new PrivilegioController();

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
            gdvPrivilegios.DataSource = privilegioCtrl.GetAll();
            gdvPrivilegios.DataBind();
        }

        protected void gdvPrivilegios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gdvPrivilegios.Rows[index];
                string codigo = row.Cells[0].Text;

                Response.Redirect("/Vistas/Privilegios/Edit?cod=" + codigo);
            }

            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gdvPrivilegios.Rows[index];
                string codigo = row.Cells[0].Text;

                Response.Redirect("/Vistas/Privilegios/Delete?cod=" + codigo);
            }
        }

        protected void gdvPrivilegios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvPrivilegios.PageIndex = e.NewPageIndex;
            CargarPrivilegios();
        }
    }
}