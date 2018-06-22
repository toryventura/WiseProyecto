using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WISETRACK.Controller;
using WISETRACK.Models;
using WS.DATA;

namespace WISETRACK
{
	public partial class RptVelocidadMaxima : System.Web.UI.Page
	{
		HomeController homeCtrl;
		ReporteController reporteCtrl;
		VehiculoController vehiculoCtrl;
		private PersonaController personaCtrl;
		private SeguimientoController seguimientoCtrl;

		string userName, nit;
		
		List<VelocidadRptDet> reporte;

		//List<string> nroPlacas;

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
					init();
					cargarVehiculo();
					CargarDetalle();
					CargarFechas();
				}
				else
					Response.Redirect("~/Account/Login");
			}
		}
		public void init()
		{
			rcbTipoRel.DataSource = FuncionesGlobales.getCondicional();
			rcbTipoRel.DataValueField = "Key";
			rcbTipoRel.DataTextField = "Value";

			rcbTipoRel.DataBind();
			rcbTipoRel.Items.FindByValue("3").Selected = true;



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

		//public void cargarVehiculo()
		//{
		//    if (HttpContext.Current.User.IsInRole("SA"))
		//    {
		//        cboplaca.DataSource = vehiculoCtrl.cargarDetalleVehiculosSA();
		//        cboplaca.DataTextField = "Patente";
		//        cboplaca.DataValueField = "NroPlaca";
		//        cboplaca.DataBind();
		//        //cboplaca.Items.Insert(0, "todas");
		//    }
		//    else
		//    {
		//        if (HttpContext.Current.User.IsInRole("SUPERVISOR"))
		//        {
		//            var user = HttpContext.Current.User.Identity.Name;
		//            cboplaca.DataSource = personaCtrl.ObtenerVehiculosAsociadosPersonal(user);
		//            cboplaca.DataTextField = "Patente";
		//            cboplaca.DataValueField = "NroPlaca";
		//            cboplaca.DataBind();
		//            //cboplaca.Items.Insert(0, "todas");
		//        }
		//        else
		//        {
		//            var user = HttpContext.Current.User.Identity.Name;
		//            string nit = homeCtrl.obtenerNit(user);
		//            cboplaca.DataSource = seguimientoCtrl.comboVehiculo(nit);
		//            cboplaca.DataTextField = "Patente";
		//            cboplaca.DataValueField = "NroPlaca";
		//            cboplaca.DataBind();
		//            //cboplaca.Items.Insert(0, "todas");
		//        }
		//    }
		//}

		public void cargarVehiculo()
		{
			if (SitePrincipal.ExisteActiva())
			{
				if (HttpContext.Current.User.IsInRole("SUPERVISOR"))
				{
					var user = HttpContext.Current.User.Identity.Name;
					gdvVehiculos.DataSource = personaCtrl.ObtenerVehiculosAsociadosPersonal(user);
					gdvVehiculos.DataBind();
					//cboplaca.DataTextField = "Patente";
					//cboplaca.DataValueField = "NroPlaca";
					//cboplaca.DataBind();
					//cboplaca.Items.Insert(0, "todas");
				}
				else
				{
					var user = HttpContext.Current.User.Identity.Name;
					string nit1 = homeCtrl.obtenerNit(user);
					gdvVehiculos.DataSource = seguimientoCtrl.comboVehiculo(nit1);
					gdvVehiculos.DataBind();
					//cboplaca.DataSource = seguimientoCtrl.comboVehiculo(nit1);
					//cboplaca.DataTextField = "Patente";
					//cboplaca.DataValueField = "NroPlaca";
					//cboplaca.DataBind();
				}
			}
			else
			{
				if (HttpContext.Current.User.IsInRole("SA"))
				{
					gdvVehiculos.DataSource = vehiculoCtrl.cargarDetalleVehiculosSA();
					gdvVehiculos.DataBind();
					//cboplaca.DataSource = vehiculoCtrl.cargarDetalleVehiculosSA();
					//cboplaca.DataTextField = "Patente";
					//cboplaca.DataValueField = "NroPlaca";
					//cboplaca.DataBind();
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

		private void CargarDetalle()
		{
			reporte = new List<VelocidadRptDet>();
			ViewState["RptVelocidades"] = reporte;

			gdvVelocidadesMax.DataSource = reporte;
			gdvVelocidadesMax.DataBind();
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

			var fechaI = Request["datepicker1"].ToString();
			var fechaF = Request["datepicker2"].ToString();

			string horaI = cbohorai.Text;
			string horaF = cbohoraf.Text;

			int velocidad = (txbVelocidad.Text.Equals("") ? 0 : Convert.ToInt32(txbVelocidad.Text));
			int tipoRel = Convert.ToInt32(rcbTipoRel.SelectedValue);
			//string nroplaca = cboplaca.SelectedValue;

			List<string> list = getlistPlacas();
			//'0004-SNP','1170YPH','1539-KFU'

			reporte = reporteCtrl.GetAllVelocidades(list, fechaI, horaI, fechaF, horaF, velocidad, tipoRel);
			var repot = reporte.OrderByDescending(x => x.Vehiculo).ToList();
			ViewState["RptVelocidades"] = repot;

			gdvVelocidadesMax.DataSource = repot;
			gdvVelocidadesMax.DataBind();
			upresultado.Update();
		}
		private List<string> getlistPlacas()
		{
			int index = 0;
			List<string> nroPlacas = new List<string>();

			foreach (GridViewRow gvr in gdvVehiculos.Rows)
			{
				bool selecDest = ((CheckBox)gvr.FindControl("SelecVeh")).Checked;

				if (selecDest)
				{
					string nroPlaca = Convert.ToString(gdvVehiculos.Rows[index].Cells[1].Text);
					nroPlacas.Add(nroPlaca);
				}

				index++;
			}
			return nroPlacas;
		}
		protected void btnExportar_Click(object sender, EventArgs e)
		{
			userName = User.Identity.Name;
			nit = homeCtrl.obtenerNit(userName);

			reporte = (List<VelocidadRptDet>)ViewState["RptVelocidades"];

			if (reporte.Count > 0)
			{
				var empresa = "Todas";

				if (SitePrincipal.ExisteActiva())
					empresa = homeCtrl.nombreEmpresa(nit);

				var fechaInicio = Request["datepicker1"].ToString() + " " + cbohorai.Text;
				var fechaFin = Request["datepicker2"].ToString() + " " + cbohoraf.Text;

				//var placa = cboplaca.Text;
				var placa = "Todas";
				//var cboPlacaCheckedItems = cboplaca.CheckedItems.ToList();

				//if (cboPlacaCheckedItems[0].Value != "0")
				//{
				//    placa = cboPlacaCheckedItems[0].Value;

				//    for (int i = 1; i < cboPlacaCheckedItems.Count; i++)
				//        placa = placa + ", " + cboPlacaCheckedItems[i].Value;
				//}

				rptDocument = new ReportDocument();
				rptDocument.Load(Server.MapPath("~/Reporte/reporteVelocidad.rpt"));

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
					"reporteVelocidad.v" + DateTime.Now.ToString() + ".xlsx");
				}
				else
					if (formato == "1")
					{
						rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true,
						"reporteVelocidad.v" + DateTime.Now.ToString() + ".pdf");
					}
			}
		}

		protected void gdvVelocidadesMax_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "VerMapa")
			{
				int index = Convert.ToInt32(e.CommandArgument);
				GridViewRow row = gdvVelocidadesMax.Rows[index];

				string longitud = row.Cells[4].Text;
				string latitud = row.Cells[5].Text;

				string verMapa = "window.open('/FrmUbicacionMapa?longitud=" + longitud + "&latitud=" + latitud + "', '_newtab');";
				ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), verMapa, true);
			}
		}
	}
}