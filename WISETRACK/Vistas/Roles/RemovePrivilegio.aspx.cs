using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;

namespace WISETRACK.Vistas.Roles
{
    public partial class RemovePrivilegio : System.Web.UI.Page
    {
        private List<Datos.Privilegios> privilegios;
        private string rol;

        RolController rolCtrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            rolCtrl = new RolController();

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                    CargarPrivilegios();
                else
                    Response.Redirect("~/Account/Login");
            }
        }

        private void CargarPrivilegios()
        {
            rol = Request.QueryString["rol"];

            privilegios = rolCtrl.GetPrivilegios(rol);

            gdvPrivilegios.DataSource = privilegios;
            gdvPrivilegios.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int index = 0;
            bool check = false;

            rol = Request.QueryString["rol"];
            privilegios = rolCtrl.GetPrivilegios(rol);
            int count = privilegios.Count;

            string userName = HttpContext.Current.User.Identity.Name;

            foreach (GridViewRow gvr in gdvPrivilegios.Rows)
            {
                bool selec = ((CheckBox)gvr.FindControl("SelecPriv")).Checked;

                if (selec)
                {
                    if (!check) check = true;

                    int codPriv = privilegios.ElementAt(index).CodPrivilegio;

                    try
                    {
                        rolCtrl.RemovePrivilegio(rol, codPriv);
                        count--;
                    }
                    catch (Exception ex)
                    {
                        InfoMessage.Text = ex.Message;
                        return;
                    }
                }

                index++;
            }

            if (count == 0)
                rolCtrl.AddDefaultPrivilegios(rol);

            if (check)
                Response.Redirect("~/Vistas/Roles/DetailsPrivilegio?rol=" + rol);
            else
                InfoMessage.Text = "No se selecciono Privilegio";

        }
    }
}