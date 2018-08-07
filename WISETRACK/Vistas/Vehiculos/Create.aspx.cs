using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;
using WISETRACK.Models;

namespace WISETRACK.Vistas.Vehiculos
{
    public partial class Create : System.Web.UI.Page
    {

        private VehiculoController cx;
        private   HomeController homerCtrl;

        [WebMethod]
        public static int ObtenerDatos(string NroPlaca = "")
        {

            if (!NroPlaca.Equals(""))
            {
                VehiculoController ver = new VehiculoController();
                VehiculoDetalle ve = new VehiculoDetalle();
              int i =ver.buscarEstado(NroPlaca);
              return i;
            }
           
            return -1 ;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            cx = new VehiculoController();
            homerCtrl = new HomeController();
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    if (SitePrincipal.ExisteActiva())
                    {
                        cargarMarca();
                        cargarTipoVehiculo();
                    }
                    else
                    {
                        SitePrincipal.pagRedireccion = "~/Vistas/Vehiculos/Create";
                        Response.Redirect("~/Vistas/Empresas/Panel");
                    }
                }
                else
                    Response.Redirect("~/Account/Login");
            }
        }

        public void addVehiculo()
        {
             var result = cx.BuscarSiExisteVehiculo(txtplaca.Text);
             if (result == null)
             {
                 if (!cx.BuscarSiEstaRegistradoVehiculo(txtplaca.Text))
                 {

                     var user = HttpContext.Current.User.Identity.Name;
                     string nit = homerCtrl.obtenerNit(user);
					Vehiculo vehiculo = new Vehiculo
					{
						NroPlaca = txtplaca.Text,
						NroChasis = txtchasis.Text,
						NroMotor = txtmotor.Text,
						Modelo = txtmodelo.Text,
						Patente = txtpatente.Text,
						Año = Convert.ToInt32(txtanio.Text),
						CodTipoV = Convert.ToInt32(cbotipov.SelectedValue.ToString()),
						CodMarca = Convert.ToInt32(cbomarca.SelectedValue.ToString())
					};

					byte[] imageBytes = new byte[fileupvehiculo.PostedFile.InputStream.Length + 1];
                     fileupvehiculo.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
                     vehiculo.Foto = imageBytes;
                     vehiculo.Estado = true;
                     vehiculo.UsuaReg = user;
                     vehiculo.FechaReg = DateTime.Now;
                     vehiculo.FechaModif = DateTime.Now;

                     // vehiculo.idempresa = homerCtrl.obtenerNit(user); este se ha quitado

                     bool sw = cx.createVehiculo(vehiculo, nit);

                     if (sw == true)
                     {
                         MensajeAlertaV("Se registro correctamente");
                         Response.Redirect("/Vistas/Vehiculos/Index");
                     }
                     else
                     {
                         MensajeAlertaV("Datos invalidos");
                     }
                 }
                 else
                 {

                     var user = HttpContext.Current.User.Identity.Name;
                     string nit = homerCtrl.obtenerNit(user);
					Vehiculo vehiculo = new Vehiculo
					{
						NroPlaca = txtplaca.Text,
						NroChasis = txtchasis.Text,
						NroMotor = txtmotor.Text,
						Modelo = txtmodelo.Text,
						Patente = txtpatente.Text,
						Año = Convert.ToInt32(txtanio.Text),
						CodTipoV = Convert.ToInt32(cbotipov.SelectedValue.ToString()),
						CodMarca = Convert.ToInt32(cbomarca.SelectedValue.ToString())
					};

					byte[] imageBytes = new byte[fileupvehiculo.PostedFile.InputStream.Length + 1];
                     fileupvehiculo.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
                     vehiculo.Foto = imageBytes;
                     vehiculo.Estado = true;
                     vehiculo.UsuaReg = user;
                     vehiculo.FechaReg = DateTime.Now;
                     vehiculo.FechaModif = DateTime.Now;

                     // vehiculo.idempresa = homerCtrl.obtenerNit(user); este se ha quitado

                    bool sw = cx.update(vehiculo,nit,user);

                     if (sw == true)
                     {
                         MensajeAlertaV("Se registro correctamente");
                         Response.Redirect("/Vistas/Vehiculos/Index");
                     }
                     else
                     {
                         MensajeAlertaV("Datos invalidos");
                     }
                 }
             }
        }
        public void cargarTipoVehiculo()
        {
            cbotipov.DataSource = cx.comboTipoVehiculo();
            cbotipov.DataTextField = "Descripcion";
            cbotipov.DataValueField = "CodTipoV";
            cbotipov.DataBind();
        }

        public void cargarMarca()
        {
            cbomarca.DataSource = cx.comboMarca();
            cbomarca.DataTextField = "Descripcion";
            cbomarca.DataValueField = "CodMarca";
            cbomarca.DataBind();
        }

        private void MensajeAlertaV(string mensaje)
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            addVehiculo();
        }

       

    }
}