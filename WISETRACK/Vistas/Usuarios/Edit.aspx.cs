using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;
using WISETRACK.Models;

namespace WISETRACK.Vistas.Usuarios
{
    public partial class Edit : System.Web.UI.Page
    {
        UsuarioController usuarioCtrl;
        RolController rolCtrl;
        AspNetUsers usuario;
        string userName;

        protected void Page_Load(object sender, EventArgs e)
        {
            usuarioCtrl = new UsuarioController();
            rolCtrl = new RolController();

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    CargarUsuario();
                    CargarPersonas();
                    CargarRoles();
                }
                else
                    Response.Redirect("~/Account/Login");
            }
        }

        private void CargarUsuario()
        {
            userName = Request.QueryString["user"];
            if (!String.IsNullOrEmpty(userName))
            {
                txbUserName.Text = userName;

                usuario = usuarioCtrl.Get(userName);
                txbEmail.Text = usuario.Email;
            }
            else
            {
                Response.Redirect("~/Vistas/Usuarios/Index");
            }
        }

        private void CargarPersonas()
        {
            dpdPersonas.DataValueField = "CI";
            dpdPersonas.DataTextField = "NombreCompleto";

            var personas = usuarioCtrl.GetNotPersUser(usuario.Id);
            personas.Insert(0, new PersonaCboDet { CI = "0", NombreCompleto = "(Ninguna)" });

            dpdPersonas.DataSource = personas;
            dpdPersonas.DataBind();

            if (usuario.Persona.FirstOrDefault() != null)
                dpdPersonas.SelectedValue = usuario.Persona.FirstOrDefault().CI;
            else
                dpdPersonas.SelectedIndex = 0;
        }

        private void CargarRoles()
        {
            dpdRoles.DataValueField = "Id";
            dpdRoles.DataTextField = "Name";

            var roles = rolCtrl.GetAll(userName);
            dpdRoles.DataSource = roles;
            dpdRoles.DataBind();

            dpdRoles.SelectedValue = usuario.AspNetRoles.FirstOrDefault().Id;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            userName = Request.QueryString["user"];
            usuario = usuarioCtrl.Get(userName);

            try
            {
                string email = txbEmail.Text;
                string ci = dpdPersonas.SelectedValue;
                string rol = Convert.ToString(dpdRoles.SelectedItem);

                usuarioCtrl.Actualizar(usuario, email, ci, rol, userName);
                Response.Redirect("~/Vistas/Usuarios/Index");
            }
            catch(Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
        }
    }
}