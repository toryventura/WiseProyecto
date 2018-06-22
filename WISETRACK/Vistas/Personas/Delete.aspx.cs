using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;
using Microsoft.AspNet.Identity.Owin;
using Owin;

namespace WISETRACK.Vistas.Personas
{
    public partial class Delete : System.Web.UI.Page
    {
        PersonaController personaCtrl;
        string ci = String.Empty;
        private HomeController homeCtrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            homeCtrl = new HomeController();
            personaCtrl = new PersonaController();
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    if (SitePrincipal.ExisteActiva())
                    {
                        ci = Request.QueryString["ci"];
                        if (!String.IsNullOrEmpty(ci))
                        {
                            lblCi.Text = ci;
                            Persona p = personaCtrl.listar(ci);

                            if (p.Estado)
                            {
                                lblnombre.Text = p.Nombre;
                                lblapellidop.Text = p.ApellidoP;
                                lblapellidom.Text = p.ApellidoM;
                                lbldireccion.Text = p.Direccion;
                                lbltelefono.Text = p.Telefono;
                                lblemail.Text = p.Email;
                                lblcontacto.Text = p.Contacto;
                                lbltelefonoc.Text = p.TelfContacto;
                                lbllicencia.Text = p.CategoriaL;
                            }
                            else
                            {
                                MensajeAlerta("El personal ya ha sido dado de baja");
                                Response.Redirect("~/Vistas/Personas/Index");
                            }
                        }
                        else
                        {
                            Response.Redirect("~/Vistas/Personas/Index");
                        }
                    }
                    else
                    {
                        SitePrincipal.pagRedireccion = "~/Vistas/Personas/Index";
                        SitePrincipal.countRedireccion = 0;
                        Response.Redirect("~/Vistas/Empresas/Panel");
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            var user = User.Identity.Name;
            var nit = homeCtrl.obtenerNit(user);
            var puser = personaCtrl.listar(lblCi.Text);
            bool ok = false;
            if (!String.IsNullOrEmpty(puser.IdUser))
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ok = manager.ClearAllUserRoles(puser.IdUser);
            }

            bool sw = personaCtrl.RemoverLogicamente(lblCi.Text, nit, user);
            if (sw)
            {
                MensajeAlerta("Datos guardados correctamente");
                Response.Redirect("/Vistas/Personas/Index");
            }
            else
            {
                MensajeAlerta("Error en la transaccion, Favor Intente de nuevo");
            }


        }

        private void MensajeAlerta(string mensaje)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(mensaje);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

    }
}