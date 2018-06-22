using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;
using WISETRACK.Models;

namespace WISETRACK.Roles
{
    public partial class Create : System.Web.UI.Page
    {
        RolController rolCtrl;
        string userName;

        protected void Page_Load(object sender, EventArgs e)
        {
            rolCtrl = new RolController();

            if (!SitePrincipal.IsIntruso())
            {
                CargarNiveles();
            }
            else
                Response.Redirect("~/Account/Login");
        }

        private void CargarNiveles()
        {
            userName = User.Identity.Name;

            dpdNiveles.DataValueField = "Id";
            dpdNiveles.DataTextField = "Nombre";

            dpdNiveles.DataSource = rolCtrl.GetNiveles(userName);
            dpdNiveles.DataBind();
        }

        protected void CrearRol_Click(object sender, EventArgs e)
        {
            string nombre = Nombre.Text.ToUpper();
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            if (!rm.RoleExists(nombre))
            {
                rm.Create(new IdentityRole(nombre));
                var rol = rm.FindByName(nombre);
                
                int idNivel = Convert.ToInt32(dpdNiveles.SelectedValue);
                rolCtrl.AddNivel(rol.Id, idNivel, false);

                userName = HttpContext.Current.User.Identity.Name;
                rolCtrl.CargarPrivilegios(rol.Id, userName);

                Response.Redirect("~/Vistas/Roles/Index");
            }
            else
            {
                ErrorMessage.Text = "ROL YA EXISTENTE";
            }
        }

    }
}