using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WISETRACK.Controller;
using WISETRACK.Datos;
using WISETRACK.Datos.Auxiliar;
using WISETRACK.Datos.Serializable;
using WS.DATA;
using WS.LOGICA;

namespace WISETRACK
{
	public partial class RptDetenciones2 : BasePage
	{
		static string userName;
		static string nit;


		protected void Page_Load(object sender, EventArgs e)
		{

			//cboplaca.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
			cbohorai.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
			cbohoraf.Filter = (RadComboBoxFilter)Convert.ToInt32(2);


			if (!IsPostBack)
			{
				if (!SitePrincipal.IsIntruso())
				{
					init();
					CargarFechas();

					//CargarDetalle();
				}
				else
					Response.Redirect("~/Account/Login");
			}
		}
		public void init()
		{
			cbohoraf.DataSource = FuncionesGlobales.getdatosFechas();
			cbohoraf.DataValueField = "Key";
			cbohoraf.DataTextField = "Value";
			cbohoraf.DataBind();

			cbohorai.DataSource = FuncionesGlobales.getdatosFechas();
			cbohorai.DataValueField = "Key";
			cbohorai.DataTextField = "Value";
			cbohorai.DataBind();


		}

		[WebMethod]
		public static string cargarVehiculo()
		{
			SeguimientoController seguimientoCtrl = new SeguimientoController();
			var objs = new List<VehiculoEmpresas>();
			if (SitePrincipal.ExisteActiva())
			{
				if (HttpContext.Current.User.IsInRole("SUPERVISOR"))
				{
					var user = HttpContext.Current.User.Identity.Name;
					objs = seguimientoCtrl.GetVehiculos(3, user);

				}
				else
				{
					var user = HttpContext.Current.User.Identity.Name;
					var homeCtrl = new HomeController();
					string nit1 = homeCtrl.obtenerNit(user);
					objs = seguimientoCtrl.GetVehiculos(2, nit1);

				}
			}
			else
			{
				if (HttpContext.Current.User.IsInRole("SA"))
				{
					objs = seguimientoCtrl.GetVehiculos(1, "");

				}
			}
			return JsonConvert.SerializeObject(objs, Formatting.Indented);
		}
		[WebMethod]
		public static string CargarReporte(string[] lista, string fechaI, string fechaF, string tipoRel, string tiempoDet, int radio)
		{
			string result = String.Empty;

			LReportes lg = new LReportes();
			List<string> placa = new List<string>(lista);
			//int tipoRel = Convert.ToInt32(rcbTipoRel.SelectedValue);
			var reporteCtrl = new ReporteController();
			List<Seguimiento> listobjetos = reporteCtrl.ListaImeis(placa);
			List<string> final = listobjetos.Select(w => w.IMEI).ToList();
			string plac = toimeis(final);
			List<DetencionesRpt> reporte = new List<DetencionesRpt>();
			int tiporel1 = Convert.ToInt32(tipoRel);
			int tiempoDet1 = Convert.ToInt32(tiempoDet);
			reporte = lg.ListarReporteDetenciones(plac, fechaI, fechaF, tiporel1, tiempoDet1, radio);
			foreach (var item in listobjetos)
			{
				reporte.Where(w => w.Vehiculo == item.IMEI).ToList().ForEach(f => f.Vehiculo = item.NroPlaca);
			}
			HttpContext.Current.Session["RptDetenciones"] = reporte;
			result = JsonConvert.SerializeObject(reporte, Formatting.Indented);
			return result;
		}

		public static string toimeis(List<string> lista)
		{
			string placas = "";
			foreach (var item in lista)
			{
				placas = placas + "'" + item + "',";
			}
			if (placas.Length > 1)
			{
				placas = placas.Substring(0, placas.Length - 1);
			}
			return placas;
		}

		public void exprt()
		{
			userName = HttpContext.Current.User.Identity.Name;
			HomeController homeController = new HomeController();
			nit = homeController.obtenerNit(userName);

			List<DetencionesRpt> reporte = new List<DetencionesRpt>();
			reporte = (List<DetencionesRpt>)HttpContext.Current.Session["RptDetenciones"];
			ReportDocument rptDocument = new ReportDocument();
			var empresa = "Todas";
			if (SitePrincipal.ExisteActiva())
			{
				empresa = homeController.nombreEmpresa(nit);
			}
			string placa = "Todas";
			var fechaI = Request["datepicker1"].ToString() + " " + cbohorai.Text;
			var fechaF = Request["datepicker2"].ToString() + " " + cbohoraf.Text;
			rptDocument.Load(Server.MapPath("~/Reporte/reporteDetencion.rpt"));

			rptDocument.SetDataSource(reporte);
			rptDocument.SetParameterValue("Empresa", empresa);
			rptDocument.SetParameterValue("FechaInicio", fechaI);
			rptDocument.SetParameterValue("FechaFin", fechaF);
			rptDocument.SetParameterValue("Placa", "");

			Response.Buffer = false;
			Response.Clear();

			rptDocument.ExportToHttpResponse(ExportFormatType.Excel, Response, true,
			"reporteDetencion.v" + DateTime.Now.ToString("yyyyMMddHHMMss"));

		}

		protected void cbohoraf_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
		{
			e.Item.Text = string.Concat(e.Item.Text.Split(' ')[0], "");
		}

		protected void cbohorai_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
		{
			e.Item.Text = string.Concat(e.Item.Text.Split(' ')[0], "");
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

		protected void btnExportar_Click(object sender, EventArgs e)
		{
			exprt();
		}
	}
}