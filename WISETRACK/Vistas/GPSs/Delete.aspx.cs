using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK.Vistas.GPSs
{
    public partial class Delete : System.Web.UI.Page
    {
        private GpsController control;
        private HomeController homeCtrl;
        string imei = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            control = new GpsController();
            homeCtrl = new HomeController();
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    if (SitePrincipal.ExisteActiva())
                    {
                        imei = Request.QueryString["imei"];
                        if (!String.IsNullOrEmpty(imei))
                        {
                            lblimei.Text = imei;
                            GPS g = control.listar(imei);
                            if (g.Estado.Value)
                            {
                                lblid.Text = g.ID;
                                lblmodelo.Text = g.Modelo;
                                lbltelefono.Text = g.NroTelefono.ToString();
                            }
                            else
                            {
								lblid.Text = g.ID;
								lblmodelo.Text = g.Modelo;
								lbltelefono.Text = g.NroTelefono.ToString();
								//MensajeAlerta("El GPSs ya ha sido dado de baja");
								//Response.Redirect("~/Vistas/GPSs/Index");
                            }
                        }
                    }
                    else
                    {
                        SitePrincipal.pagRedireccion = "~/Vistas/GPSs/Delete";
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
            var user = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(user);
            bool sw = control.RemoverLogicamente(lblimei.Text, nit, user);
            if (sw)
            {
                MensajeAlerta("Accion realizada satisfactoriamente");
                Response.Redirect("~/Vistas/GPSs/Index");
            }
            else
            {
                MensajeAlerta("Favor, Revisar si el GPS esta asignado a un Movil");
                Response.Redirect("~/Vistas/GPSs/Index");
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