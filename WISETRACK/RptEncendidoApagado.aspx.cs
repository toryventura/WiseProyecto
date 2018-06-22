using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WISETRACK.Controller;
using WISETRACK.Models;

namespace WISETRACK
{
	public partial class RptEncendidoApagado : System.Web.UI.Page
	{
		HomeController homeCtrl;
		ReporteController reporteCtrl;
		VehiculoController vehiculoCtrl;
		private PersonaController personaCtrl;
		private SeguimientoController seguimientoCtrl;

		List<VelocidadRptDet> reporte;
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
					cargarVehiculo();
					CargarDetalle();
					CargarFechas();
				}
				else
					Response.Redirect("~/Account/Login");
			}
		}
		public void cargarVehiculo()
		{
			if (SitePrincipal.ExisteActiva())
			{
				if (HttpContext.Current.User.IsInRole("SUPERVISOR"))
				{
					var user = HttpContext.Current.User.Identity.Name;
					gdvVehiculos.DataSource = personaCtrl.ObtenerVehiculosAsociadosPersonal(user);
					gdvVehiculos.DataBind();

				}
				else
				{
					var user = HttpContext.Current.User.Identity.Name;
					string nit1 = homeCtrl.obtenerNit(user);
					gdvVehiculos.DataSource = seguimientoCtrl.comboVehiculo(nit1);
					gdvVehiculos.DataBind();

				}
			}
			else
			{
				if (HttpContext.Current.User.IsInRole("SA"))
				{
					gdvVehiculos.DataSource = vehiculoCtrl.cargarDetalleVehiculosSA();
					gdvVehiculos.DataBind();

				}
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

		protected void cbohorai_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
		{
			e.Item.Text = string.Concat(e.Item.Text.Split(' ')[0], "");
		}
		protected void cbohoraf_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
		{
			e.Item.Text = string.Concat(e.Item.Text.Split(' ')[0], "");
		}
		private void CargarDetalle()
		{
			reporte = new List<VelocidadRptDet>();
			ViewState["RptVelocidades"] = reporte;

			//gdvVelocidadesMax.DataSource = reporte;
			//gdvVelocidadesMax.DataBind();
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			string nroplacas = getlistPlacas();

			var fechaI = Request["datepicker1"].ToString();
			var fechaF = Request["datepicker2"].ToString();

			string horaI = cbohorai.Text;
			string horaF = cbohoraf.Text;

			var rfini = fechaI + " " + horaI;
			var rffin = fechaF + " " + horaF;


			//var list = getlistaEncendidoApagado(nroplacas, fini, ffin);
			showReporte(nroplacas, rfini, rffin);

		}
		private void showReporte(string placas, string fini, string ffin)
		{
			DateTime dfini = Convert.ToDateTime(fini);
			DateTime dffin = Convert.ToDateTime(ffin);
			//Reset
			rptViewer.Reset();
			//DataSource
			var lis = getlistaEncendidoApagado(placas, dfini, dffin);
			ReportDataSource rds = new ReportDataSource("DS", lis);
			rptViewer.LocalReport.DataSources.Add(rds);
			//Path
			rptViewer.LocalReport.ReportPath = "RDLC/ReportEncendidoApagado.rdlc";
			//Parameters
			ReportParameter[] rptParams = new ReportParameter[]{
				new ReportParameter("fromDate",fini),
				new ReportParameter("toDate",ffin)
			};
			rptViewer.LocalReport.SetParameters(rptParams);
			//Refresh
			rptViewer.LocalReport.Refresh();
		}
		protected void btnExportar_Click(object sender, EventArgs e)
		{

		}
		private List<DetEncendidoApagado> getlistaEncendidoApagado(string placas, DateTime from, DateTime toend)
		{
			List<DetEncendidoApagado> list = new List<DetEncendidoApagado>();
			var listE = reporteCtrl.getListaEncendidoApagado(placas, from, toend);
			foreach (var item in listE)
			{
				list.Add(new DetEncendidoApagado()
					{
						Nroplaca = item.Nroplaca,
						DMinIgnicion = item.DMinIgnicion.Value,
						DMMEncendido = item.DMMEncendido.Value,
						FHApagado = Convert.ToString(item.FHApagado.Value),
						FHMEncendido = (item.FHMEncendido != null ? Convert.ToString(item.FHMEncendido.Value) : ""),
						FHoraIgnicion = Convert.ToString(item.FHoraIgnicion.Value)
					});


			}
			return list;
		}
		private string getlistPlacas()
		{
			int index = 0;
			string nroPlacas = "";

			foreach (GridViewRow gvr in gdvVehiculos.Rows)
			{
				bool selecDest = ((CheckBox)gvr.FindControl("SelecVeh")).Checked;

				if (selecDest)
				{
					nroPlacas = nroPlacas + Convert.ToString(gdvVehiculos.Rows[index].Cells[1].Text) + ",";
				}

				index++;
			}
			return nroPlacas.Substring(0, nroPlacas.Length - 1);
		}

	}
}