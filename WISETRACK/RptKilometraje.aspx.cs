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

namespace WISETRACK
{
	public partial class RptKilometraje : System.Web.UI.Page
	{
		HomeController homeCtrl;
		ReporteController reporteCtrl;
		VehiculoController vehiculoCtrl;
		private PersonaController personaCtrl;
		private SeguimientoController seguimientoCtrl;

		string userName, nit;
		
		List<KilometrajeRptDetOptimizado> reporte;

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
					cargarVehiculo();
					CargarDetalle();
					CargarFechas();
				}
				else
					Response.Redirect("~/Account/Login");
			}
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

		//public void cargarVehiculo()
		//{
		//    if (SitePrincipal.ExisteActiva())
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
		//            string nit1 = homeCtrl.obtenerNit(user);
		//            cboplaca.DataSource = seguimientoCtrl.comboVehiculo(nit1);
		//            cboplaca.DataTextField = "Patente";
		//            cboplaca.DataValueField = "NroPlaca";
		//            cboplaca.DataBind();
		//        }
		//    }
		//    else
		//    {
		//        if (HttpContext.Current.User.IsInRole("SA"))
		//        {
		//            cboplaca.DataSource = vehiculoCtrl.cargarDetalleVehiculosSA();
		//            cboplaca.DataTextField = "Patente";
		//            cboplaca.DataValueField = "NroPlaca";
		//            cboplaca.DataBind();
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

		private void CargarDetalle()
		{
			reporte = new List<KilometrajeRptDetOptimizado>();
			ViewState["RptKilometrajes"] = reporte;

			gdvKilometrajes.DataSource = reporte;
			gdvKilometrajes.DataBind();
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

			var rfini = fechaI + " " + horaI;
			var rffin = fechaF + " " + horaF;

			string placas = getlistPlacas();

			reporte = getlistaReporteKilometraje(placas, rfini, rffin);
			ViewState["RptKilometrajes"] = reporte;

			gdvKilometrajes.DataSource = reporte;
			gdvKilometrajes.DataBind();
			upresultado.Update();
		}
		private List<KilometrajeRptDetOptimizado> getlistaReporteKilometraje(string placas, string rfini, string rffin)
		{
		
			List<KilometrajeRptDetOptimizado> list = new List<KilometrajeRptDetOptimizado>();
			DateTime from = Convert.ToDateTime(rfini);
			DateTime toend = Convert.ToDateTime(rffin);


			var listE = reporteCtrl.GetAllKilometrajes(placas, from, toend);
			foreach (var item in listE)
			{
				double dif = item.DMMEncendido.Value;
				dif = Math.Round(dif, 0);

				list.Add(new KilometrajeRptDetOptimizado()
				{
					Vehiculo = item.Nroplaca,
					FechaInicio = Convert.ToString( item.FechaInicio),
					FechaFin = Convert.ToString( item.FechaFin),
					Kilometraje =Convert.ToString(dif)+" km.",
					
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
		protected void btnExportar_Click(object sender, EventArgs e)
		{
			userName = User.Identity.Name;
			nit = homeCtrl.obtenerNit(userName);

			reporte = (List<KilometrajeRptDetOptimizado>)ViewState["RptKilometrajes"];

			if (reporte.Count > 0)
			{
				var empresa = "NO DEFINIDO";
				if (SitePrincipal.ExisteActiva())
				{
					empresa = homeCtrl.nombreEmpresa(nit);
				}

				var fechaInicio = Request["datepicker1"].ToString() + " " + cbohorai.Text;
				var fechaFin = Request["datepicker2"].ToString() + " " + cbohoraf.Text;

				var placa = "Todos";
				//var cboPlacaCheckedItems = cboplaca.CheckedItems.ToList();

				//if (cboPlacaCheckedItems[0].Value != "0")
				//{
				//    placa = cboPlacaCheckedItems[0].Value;

				//    for (int i = 1; i < cboPlacaCheckedItems.Count; i++)
				//        placa = placa + ", " + cboPlacaCheckedItems[i].Value;
				//}

				rptDocument = new ReportDocument();
				rptDocument.Load(Server.MapPath("~/Reporte/reporteKilometraje.rpt"));

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
					"reporteKilometraje_v" + DateTime.Now.ToString("yyyyMMddHHMMss") );
				}
				else
					if (formato == "1")
					{
						rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true,
						"reporteKilometraje_v" + DateTime.Now.ToString("yyyyMMddHHMMss"));
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

		protected void gdvKilometrajes_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			//if (e.CommandName == "VerMapa")
			//{
			//    int index = Convert.ToInt32(e.CommandArgument);
			//    GridViewRow row = gdvKilometrajes.Rows[index];

			//    string longitud = row.Cells[3].Text;
			//    string latitud = row.Cells[4].Text;

			//    string verMapa = "window.open('/FrmUbicacionMapa?longitud=" + longitud + "&latitud=" + latitud + "', '_newtab');";
			//    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), verMapa, true);
			//}
		}
	}
}