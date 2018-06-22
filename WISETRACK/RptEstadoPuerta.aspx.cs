using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WISETRACK.Controller;
using WISETRACK.Models;

namespace WISETRACK
{
	public partial class RptEstadoPuerta : System.Web.UI.Page
	{
		HomeController homeCtrl;
		ReporteController reporteCtrl;
		VehiculoController vehiculoCtrl;
		private PersonaController personaCtrl;
		private SeguimientoController seguimientoCtrl;

		string userName;
		string nit;

		List<EstadoPuertaRptDet> reporte;

		ReportDocument rptDocument;

		protected void Page_Load(object sender, EventArgs e)
		{
			homeCtrl = new HomeController();
			reporteCtrl = new ReporteController();
			vehiculoCtrl = new VehiculoController();
			personaCtrl = new PersonaController();
			seguimientoCtrl = new SeguimientoController();

			//cboplaca.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
			cbohorai.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
			cbohoraf.Filter = (RadComboBoxFilter)Convert.ToInt32(2);

			if (!IsPostBack)
			{
				if (!SitePrincipal.IsIntruso())
				{
					CargarFechas();
					cargarVehiculo();
					CargarDetalle();
				}
				else
					Response.Redirect("~/Account/Login");
			}
		}

		private void CargarFechas()
		{
			DateTime fechaActual = DateTime.Now;

			//txtfechaini.Text = (fechaActual.Day < 10 ? "0" + fechaActual.Day : "" + fechaActual.Day)
			//    + "/" + (fechaActual.Month < 10 ? "0" + fechaActual.Month : "" + fechaActual.Month)
			//    + "/" + fechaActual.Year;

			cbohorai.Text = "00:00";

			//txtfechafin.Text = (fechaActual.Day < 10 ? "0" + fechaActual.Day : "" + fechaActual.Day)
			//    + "/" + (fechaActual.Month < 10 ? "0" + fechaActual.Month : "" + fechaActual.Month)
			//    + "/" + fechaActual.Year;

			cbohoraf.Text = (fechaActual.Hour < 10 ? "0" + fechaActual.Hour : "" + fechaActual.Hour)
				+ ":" + (fechaActual.Minute < 10 ? "0" + fechaActual.Minute : "" + fechaActual.Minute);
		}

		//public void CargarVehiculos()
		//{
		//    if (!User.IsInRole("SA") || SitePrincipal.ExisteActiva())
		//    {
		//        userName = User.Identity.Name;
		//        nit = homeCtrl.obtenerNit(userName);

		//        vehiculos = vehiculoCtrl.GetAllVehiculos2(nit);
		//    }
		//    else
		//        vehiculos = vehiculoCtrl.GetAllVehiculos2();

		//    //vehiculos.Insert(0, new VehiculoCboDet { Id = "0", NroPlaca = "Todos" });
		//    cboplaca.DataValueField = "Id";
		//    cboplaca.DataTextField = "NroPlaca";

		//    cboplaca.DataSource = vehiculos;
		//    cboplaca.DataBind();
		//}
		public void cargarVehiculo()
		{
			if (SitePrincipal.ExisteActiva())
			{
				if (HttpContext.Current.User.IsInRole("SUPERVISOR"))
				{
					var user = HttpContext.Current.User.Identity.Name;
					cboplaca.DataSource = personaCtrl.ObtenerVehiculosAsociadosPersonal(user);
					cboplaca.DataTextField = "Patente";
					cboplaca.DataValueField = "NroPlaca";
					cboplaca.DataBind();
					//cboplaca.Items.Insert(0, "todas");
				}
				else
				{
					var user = HttpContext.Current.User.Identity.Name;
					string nit1 = homeCtrl.obtenerNit(user);
					cboplaca.DataSource = seguimientoCtrl.comboVehiculo(nit1);
					cboplaca.DataTextField = "Patente";
					cboplaca.DataValueField = "NroPlaca";
					cboplaca.DataBind();
				}
			}
			else
			{
				if (HttpContext.Current.User.IsInRole("SA"))
				{
					cboplaca.DataSource = vehiculoCtrl.cargarDetalleVehiculosSA();
					cboplaca.DataTextField = "Patente";
					cboplaca.DataValueField = "NroPlaca";
					cboplaca.DataBind();
				}
			}
		}

		private void CargarDetalle()
		{
			reporte = new List<EstadoPuertaRptDet>();
			ViewState["RptEstadosPuerta"] = reporte;

			gdvEstadosPuerta.DataSource = reporte;
			gdvEstadosPuerta.DataBind();
		}

		protected void cbohorai_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
		{
			e.Item.Text = string.Concat(e.Item.Text.Split(' ')[0], "");
		}

		protected void cbohoraf_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
		{
			e.Item.Text = string.Concat(e.Item.Text.Split(' ')[0], "");
		}

		//protected void cboplaca_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
		//{
		//    e.Item.Text = string.Concat(e.Item.Text.Split(' ')[0], "");
		//}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			userName = HttpContext.Current.User.Identity.Name;
			nit = homeCtrl.obtenerNit(userName);
			string usuario = userName + ":" + nit;
			var txtfechaini = Request["datepicker1"].ToString();
			var txtfechafin = Request["datepicker2"].ToString();

			int estado = Convert.ToInt32(rcbEstado.SelectedValue);
			string fechaI = txtfechaini;
			string horaI = cbohorai.Text;

			string fechaF = txtfechafin;
			string horaF = cbohoraf.Text;

			string placa = cboplaca.SelectedValue; //Buscar la placa
			reporte = reporteCtrl.ListarReporteEstadoPuerta(placa, fechaI, horaI, fechaF, horaF, estado);
			ViewState["RptEstadosPuerta"] = reporte;

			gdvEstadosPuerta.DataSource = reporte;
			gdvEstadosPuerta.DataBind();
			uprespuesta.Update();
		}

		protected void btnExportar_Click(object sender, EventArgs e)
		{
			userName = User.Identity.Name;
			nit = homeCtrl.obtenerNit(userName);
			reporte = (List<EstadoPuertaRptDet>)ViewState["RptEstadosPuerta"];

			if (reporte.Count > 0)
			{
				var empresa = "Todas";

				if (SitePrincipal.ExisteActiva())
				{
					empresa = homeCtrl.nombreEmpresa(nit);
				}
				var fechaInicio = Request["datepicker1"].ToString() + " " + cbohorai.Text;
				var fechaFin = Request["datepicker2"].ToString() + " " + cbohoraf.Text;
				var placa = cboplaca.Text;

				rptDocument = new ReportDocument();
				rptDocument.Load(Server.MapPath("~/Reporte/reporteEstadoPuerta.rpt"));

				rptDocument.SetDataSource(reporte);
				rptDocument.SetParameterValue("Empresa", empresa);
				rptDocument.SetParameterValue("FechaInicio", fechaInicio);
				rptDocument.SetParameterValue("FechaFin", fechaFin);
				rptDocument.SetParameterValue("Placa", placa);

				Response.Buffer = false;
				Response.Clear();

				var formato = rcbFormato.SelectedValue;

				if (formato == "0")
				{
					rptDocument.ExportToHttpResponse(ExportFormatType.Excel, Response, true,
					"reporteEstadoPuerta.v" + DateTime.Now.ToString() + ".xlsx");
				}
				else
					if (formato == "1")
					{
						rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true,
						"reporteEstadoPuerta.v" + DateTime.Now.ToString() + ".pdf");
					}
			}
			else
			{
				MensajeAlerta("Favor, Primero visualizar los datos antes de exportar.");
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

		protected void gdvEstadosPuerta_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "VerMapa")
			{
				int index = Convert.ToInt32(e.CommandArgument);
				GridViewRow row = gdvEstadosPuerta.Rows[index];

				string longitud = row.Cells[5].Text;
				string latitud = row.Cells[6].Text;

				string verMapa = "window.open('/FrmUbicacionMapa?longitud=" + longitud + "&latitud=" + latitud + "', '_newtab');";
				ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), verMapa, true);
			}
		}
	}
}