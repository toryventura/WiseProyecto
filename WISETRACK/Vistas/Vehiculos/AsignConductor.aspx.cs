using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;
using WISETRACK.Models;

namespace WISETRACK.Vistas.Vehiculos
{
    public partial class AsignConductor : System.Web.UI.Page
    {
        VehiculoController vehiculoCtrl;
        Vehiculo vehiculo;
        string placa;

        protected void Page_Load(object sender, EventArgs e)
        {
            vehiculoCtrl = new VehiculoController();
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    if (SitePrincipal.ExisteActiva())
                    {
                        CargarVehiculo();
                        CargarConductores();
                    }
                    else
                    {
                        SitePrincipal.pagRedireccion = "~/Vistas/Vehiculos/AsignConductor";
                        Response.Redirect("~/Vistas/Empresas/Panel");
                    }
                }
                else
                    Response.Redirect("~/Vistas/Account/Login");
            }
        }

        private void CargarVehiculo()
        {
            placa = Request.QueryString["placa"];
            vehiculo = vehiculoCtrl.listar(placa);

            lblplaca.Text = vehiculo.NroPlaca;
            lblmotor.Text = vehiculo.NroMotor;
            lblanio.Text = vehiculo.Año.ToString();
            lblchasis.Text = vehiculo.NroChasis;
            lblmodelo.Text = vehiculo.Modelo;
            lblanio.Text = vehiculo.Año.ToString();
        }

        private void CargarConductores()
        {
            dpdConductores.DataValueField = "CI";
            dpdConductores.DataTextField = "NombreCompleto";

            var conductores = vehiculoCtrl.GetNotPersCond(vehiculo.NroPlaca);
            conductores.Insert(0, new PersonaCboDet { CI = "0", NombreCompleto = "(No Asignado)" });

            dpdConductores.DataSource = conductores;
            dpdConductores.DataBind();

            if (vehiculo.VehiculoConductor.FirstOrDefault() != null)
            {
                dpdConductores.SelectedValue = vehiculo.VehiculoConductor.FirstOrDefault().Persona.CI;

                DateTime fecha = vehiculo.VehiculoConductor.FirstOrDefault().Fecha;
                txtFecha.Text = (fecha.Day < 10 ? "0" + fecha.Day : "" + fecha.Day) + "/"
                    + (fecha.Month < 10 ? "0" + fecha.Month : "" + fecha.Month) + "/"
                    + "" + fecha.Year;
            }
            else
                dpdConductores.SelectedIndex = 0;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            vehiculo = vehiculoCtrl.listar(lblplaca.Text);
            string ci = dpdConductores.SelectedValue;
            string fecha = txtFecha.Text;
            string userName = HttpContext.Current.User.Identity.Name;

            vehiculoCtrl.AsignarConductor(vehiculo, ci, fecha, userName);
            Response.Redirect("~/Vistas/Vehiculos/Index");
        }
    }
}