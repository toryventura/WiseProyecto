using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK.Vistas.GPSs
{
    public partial class Create : System.Web.UI.Page
    {
        private GpsController negocio;
        private HomeController homeCtrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            negocio = new GpsController();
            homeCtrl = new HomeController();
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    if (!SitePrincipal.ExisteActiva())
                    {
                        SitePrincipal.pagRedireccion = "~/Vistas/GPSs/Create";
                        Response.Redirect("~/Vistas/Empresas/Panel");
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var result = negocio.BuscarSiExisteGps(txtimei.Text);
            if (result == null)
            {
                if (!negocio.BuscarSiEstaRegistradoGps(txtimei.Text))
                {
                    var user = HttpContext.Current.User.Identity.Name;
                    string nit = homeCtrl.obtenerNit(user);
                    GPS modelo = new GPS();
                    modelo.IMEI = txtimei.Text;
                    modelo.ID = txtid.Text;
                    modelo.NroTelefono = Convert.ToDecimal(txttelefono.Text);
                    modelo.Modelo = cbomodelo.Text;
                    modelo.UsuaReg = user;
                    modelo.Estado = true;
                    modelo.FechaReg = DateTime.Now;
                    modelo.EstadoPuerta = true;
                    modelo.TiempoEspera = 90;

                    bool sx = negocio.Add(modelo, user, nit);
                    if (sx == true)
                    {
                        MensajeAlerta("Se registro satisfactoriamente");
                        Response.Redirect("/Vistas/GPSs/Index");
                    }
                    else
                    {
                        MensajeAlerta("Datos invalidos");
                    }
                }
                else
                {
                    var user = HttpContext.Current.User.Identity.Name;
                    string nit = homeCtrl.obtenerNit(user);
                    GPS modelo = new GPS();
                    modelo.IMEI = txtimei.Text;
                    modelo.ID = txtid.Text;
                    modelo.NroTelefono = Convert.ToDecimal(txttelefono.Text);
                    modelo.Modelo = cbomodelo.Text;
                    modelo.UsuaModif = user;
                    modelo.FechaModif = DateTime.Now;
                    bool sy = negocio.ActualizarGPSEmpresa(modelo, user, nit);
                    if (sy == true)
                    {
                        MensajeAlerta("Se registro satisfactoriamente");
                        Response.Redirect("/Vistas/GPSs/Index");
                    }
                    else
                    {
                        MensajeAlerta("Datos invalidos, Favor intente de nuevo");
                    }
                }
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