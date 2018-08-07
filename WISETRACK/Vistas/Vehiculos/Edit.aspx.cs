using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK.Vistas.Vehiculos
{
    public partial class Edit : System.Web.UI.Page
    {
        VehiculoController vehiculoCtrl;
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
                        cargarTipoVehiculo();
                        cargarMarca();

                        string placa = Request.QueryString["placa"];
                        if (!String.IsNullOrEmpty(placa))
                        {
                            txtplaca.Text = placa;
                            var ve = vehiculoCtrl.listar(placa);
                            txtplaca.Text = ve.NroPlaca;
                            txtpatente.Text = ve.Patente;
                            txtmotor.Text = ve.NroMotor;
                            txtmodelo.Text = ve.Modelo;
                            txtchasis.Text = ve.NroChasis;
                            txtanio.Text = Convert.ToString(ve.Año);
                            cbomarca.SelectedValue = ve.CodMarca.ToString();
                            cbotipov.Text = ve.CodTipoV.ToString();
                        }
                        else
                        {
                            Response.Redirect("~/Vistas/Vehiculos/Index");
                        }
                    }
                    else
                    {
                        SitePrincipal.pagRedireccion = "~/Vistas/Vehiculos/Edit";
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
            if (validadCampos())
            {
                var user = HttpContext.Current.User.Identity.Name;
                string nits = homeCtrl.obtenerNit(user);
				var vehiculo = new Vehiculo
				{
					NroPlaca = txtplaca.Text,
					NroChasis = txtchasis.Text,
					NroMotor = txtmotor.Text,
					Patente = txtpatente.Text,
					Modelo = txtmodelo.Text,
					Año = Convert.ToInt32(txtanio.Text),
					CodTipoV = Convert.ToInt32(cbotipov.SelectedValue.ToString()),
					CodMarca = Convert.ToInt32(cbomarca.SelectedValue.ToString())
				};

				byte[] imageBytes = new byte[fileupvehiculo.PostedFile.InputStream.Length + 1];
                fileupvehiculo.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
                vehiculo.Foto = imageBytes;
                
                vehiculo.UsuaModif = user;
                vehiculo.FechaModif = DateTime.Now;
                // vehiculo.idempresa = homeCtrl.obtenerNit(user); esto  se ha quedado

                bool sx = vehiculoCtrl.update(vehiculo,nits,user);

                if (sx == true)
                {
                    MensajeAlerta("Se modifico correctamente");
                    Response.Redirect("/Vistas/Vehiculos/Index");
                }
                else
                {
                    MensajeAlerta("Datos invalidos");
                }
            }


        }

        private bool validadCampos()
        {
            if (txtplaca.Text==string.Empty )
            {
                return false;

            }
            return true;
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

        public void cargarTipoVehiculo()
        {
            cbotipov.DataSource = vehiculoCtrl.comboTipoVehiculo();
            cbotipov.DataTextField = "Descripcion";
            cbotipov.DataValueField = "CodTipoV";
            cbotipov.DataBind();
        }

        public void cargarMarca()
        {
            cbomarca.DataSource = vehiculoCtrl.comboMarca();
            cbomarca.DataTextField = "Descripcion";
            cbomarca.DataValueField = "CodMarca";
            cbomarca.DataBind();
        }
    }
}