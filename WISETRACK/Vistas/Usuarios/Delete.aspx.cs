using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK.Vistas.Usuarios
{
    public partial class Delete : System.Web.UI.Page
    {
        UsuarioController usuarioCtrl;
        AspNetUsers usuario;
        string userName;

        protected void Page_Load(object sender, EventArgs e)
        {
            usuarioCtrl = new UsuarioController();

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                    CargarUsuario();
                else
                    Response.Redirect("~/Account/Login");
            }
        }

        private void CargarUsuario()
        {
            userName = Request.QueryString["user"];
            if (!String.IsNullOrEmpty(userName))
            {
                lblUserName.Text = userName;

                AspNetUsers usuario = usuarioCtrl.Get(userName);
                lblEmail.Text = usuario.Email;
                var resultroles = usuario.AspNetRoles.FirstOrDefault();

                if (resultroles != null)
                {
                    lblRol.Text = usuario.AspNetRoles.FirstOrDefault().Name;
                }
                else
                {
                    lblRol.Text = "Usuario sin Roles, Favor eliminar este usuario";
                }
            }
            else
            {
                Response.Redirect("~/Vistas/Usuarios/Index");
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            userName = Request.QueryString["user"];
            usuario = usuarioCtrl.Get(userName);

            try
            {
                usuarioCtrl.Eliminar(usuario);
                Response.Redirect("~/Vistas/Usuarios/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
        }
    }
}