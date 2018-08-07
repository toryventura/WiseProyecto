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
    public partial class Edit : System.Web.UI.Page
    {
        GpsController control;
        protected void Page_Load(object sender, EventArgs e)
        {
            control = new GpsController();
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    if (SitePrincipal.ExisteActiva())
                    {
                        string imei = Request.QueryString["imei"];
                        if (!String.IsNullOrEmpty(imei))
                        {
                            txtimei.Text = imei;
                            GPS g = control.listar(imei);
                            if (g.Estado.Value)
                            {
                                txtid.Text = g.ID;
                                cbomodelo.SelectedValue = g.Modelo;
                                txttelefono.Text = g.NroTelefono.ToString();
                            }
                            else
                            {
                                MensajeAlerta("El GPSs ya ha sido dado de baja");
                                Response.Redirect("~/Vistas/GPSs/Index");
                            }
                        }
                        else
                        {
                            MensajeAlerta("Informacion: Datos Invalidos");
                            Response.Redirect("~/Vistas/GPSs/Index");
                        }
                    }
                    else
                    {
                        SitePrincipal.pagRedireccion = "~/Vistas/GPSs/Edit";
                        Response.Redirect("~/Vistas/Empresas/Panel");
                    }
                }
                else
                { 
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            var user = HttpContext.Current.User.Identity.Name;
			GPS modelo = new GPS
			{
				IMEI = txtimei.Text,
				ID = txtid.Text,
				NroTelefono = Convert.ToDecimal(txttelefono.Text),
				Modelo = cbomodelo.SelectedValue,
				UsuaModif = user,
				FechaModif = DateTime.Now
			};
			bool sx = control.update(modelo);
            if (sx == true)
            {
                MensajeAlerta("Se modifico correctamente");
                Response.Redirect("/Vistas/GPSs/Index");
            }
            else
            {
                MensajeAlerta("Datos invalidos");
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