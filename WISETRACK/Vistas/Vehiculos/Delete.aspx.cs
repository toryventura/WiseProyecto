using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;

namespace WISETRACK.Vistas.Vehiculos
{
    public partial class Delete : System.Web.UI.Page
    {
        VehiculoController vehiculoCtrl;
        string placa = String.Empty;
        HomeController homeCtrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            vehiculoCtrl = new VehiculoController();
            homeCtrl = new HomeController();
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    if (SitePrincipal.ExisteActiva())
                    {
                        placa = Request.QueryString["placa"];
                        if (!String.IsNullOrEmpty(placa))
                        {
                            var nplaca = vehiculoCtrl.listar(placa);
                            lblanio.Text = nplaca.Año.Value.ToString();
                            lblchasis.Text = nplaca.NroChasis;
                            lblfechareg.Text = nplaca.FechaReg.ToString();
                            lblmodelo.Text = nplaca.Modelo;
                            lblmotor.Text = nplaca.NroMotor;
                            lblplaca.Text = nplaca.NroPlaca;
                            lblusuarioreg.Text = nplaca.UsuaReg;
                        }
                        else
                        {
                            Response.Redirect("~/Vistas/Vehiculos/Index");
                        }
                    }
                    else
                    {
                        SitePrincipal.pagRedireccion = "~/Vistas/Vehiculos/Create";
                        Response.Redirect("~/Vistas/Empresas/Panel");
                    }
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            var user = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(user);
            bool sw = vehiculoCtrl.RemoverLogicamente(lblplaca.Text, nit, user);
            if (sw)
            {
                MensajeAlerta("Accion realizada satisfactoriamente");
				Response.Redirect("~/Vistas/Vehiculos/Index");
            }
            else
            {
                MensajeAlerta("Favor, Revisar si el Movil esta asignado a un GPS");
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