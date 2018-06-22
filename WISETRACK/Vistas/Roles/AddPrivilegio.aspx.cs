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
    public partial class AddPrivilegio : System.Web.UI.Page
    {
        private List<Datos.Privilegios> notPrivilegios;
        private string rol;

        RolController rolCtrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            rolCtrl = new RolController();

            if (!IsPostBack)
            {
                if(!SitePrincipal.IsIntruso())
                    CargarNotPrivilegios();
                else
                    Response.Redirect("~/Account/Login");
            }
        }

        private void CargarNotPrivilegios()
        {
            rol = Request.QueryString["rol"];

            notPrivilegios = rolCtrl.GetNotPrivilegios(rol);

            if (notPrivilegios.Count > 0)
                    btnGuardar.Visible = true;

            gdvPrivilegios.DataSource = notPrivilegios;
            gdvPrivilegios.DataBind();
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            int index = 0;
            bool check = false;

            rol = Request.QueryString["rol"];
            notPrivilegios = rolCtrl.GetNotPrivilegios(rol);
            string userName = HttpContext.Current.User.Identity.Name;

            foreach (GridViewRow gvr in gdvPrivilegios.Rows)
            {
                bool selec = ((CheckBox)gvr.FindControl("SelecPriv")).Checked;

                if (selec)
                {
                    if (!check) check = true;

                    int codPriv = notPrivilegios.ElementAt(index).CodPrivilegio;
                        
                    try
                    {
                        rolCtrl.AddPrivilegio(rol, codPriv, userName);
                    }
                    catch (Exception ex)
                    {
                        InfoMessage.Text = ex.Message;
                        return;
                    }
                }

                index++;
            }

            if (check)
                Response.Redirect("~/Vistas/Roles/DetailsPrivilegio?rol=" + rol);
            else
                InfoMessage.Text = "No se selecciono Privilegio";

        }

        protected void gdvPrivilegios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvPrivilegios.PageIndex = e.NewPageIndex;
            CargarNotPrivilegios();
        }

        //protected void SelecAllPriv_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox check = (CheckBox) gdvPrivilegios.HeaderRow.FindControl("SelecAllPriv");

        //    foreach (GridViewRow row in gdvPrivilegios.Rows)
        //    {
        //        CheckBox chkrow = (CheckBox)row.FindControl("SelecPriv");

        //        if (check.Checked)
        //            chkrow.Checked = true;
        //        else
        //            chkrow.Checked = false;
        //    }
        //}
    }
}