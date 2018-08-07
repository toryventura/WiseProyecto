using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK.Vistas.Seguimientos
{
    public partial class Edit : System.Web.UI.Page
    {
        SeguimientoController seguimientoCtrl;
        GpsController gpsCtrl;
        VehiculoController vehiculoCtrl;
        HomeController homeCtrl;
        String id = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            gpsCtrl = new GpsController();
            vehiculoCtrl = new VehiculoController();
            seguimientoCtrl = new SeguimientoController();
            homeCtrl = new HomeController();

            if (!SitePrincipal.IsIntruso())
            {
                ListarIMEI();
                ListarPlaca();

                id = Request.QueryString["id"];
                var seg = seguimientoCtrl.listar(id);
                txtid.Text = seg.CodSeguimiento.ToString();
                txtFechaI.Text = seg.FechaInicio.ToShortDateString().ToString();
                chkestado.Checked = seg.estado.Value;
                cboplaca.SelectedValue = seg.NroPlaca;
                cboimei.SelectedValue = seg.IMEI;
            }
            else
            {
                Response.Redirect("~/Account/Login");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var user = HttpContext.Current.User.Identity.Name;
            var fechaI = Request["datepicker1"].ToString();

			var se = new Seguimiento
			{
				CodSeguimiento = Convert.ToInt32(txtid.Text),
				FechaInicio = Convert.ToDateTime(fechaI),
				estado = chkestado.Checked
			};
			if (se.estado == false)
            {
                se.FechaFin = DateTime.Now;
            }

            se.IMEI = cboimei.SelectedValue;
            se.NroPlaca = cboplaca.SelectedValue;
            se.UsuaModif = user;
            se.FechaModif = DateTime.Now;
            
            bool sx = seguimientoCtrl.update(se);
            if (sx == true)
            {
                MensajeAlerta("Se modifico correctamente");
                Response.Redirect("/Vistas/Seguimientos/Index");
            }
            else
            {
                MensajeAlerta("Datos invalidos");
            }
        }

        public void ListarIMEI()
        {
            var user = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(user);
            cboimei.DataSource = gpsCtrl.listarGps(nit);
            cboimei.DataTextField = "IMEI";
            cboimei.DataValueField = "IMEI";
            cboimei.DataBind();
        }

        public void ListarPlaca()
        {
            var user = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(user);

            cboplaca.DataSource = vehiculoCtrl.listarVehiculo(nit);
            cboplaca.DataTextField = "NroPlaca";
            cboplaca.DataValueField = "NroPlaca";
            cboplaca.DataBind();
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