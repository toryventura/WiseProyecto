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
using WS.LOGICA;

namespace WISETRACK
{
	public partial class RptEntradaSalida : System.Web.UI.Page
	{
		HomeController homeCtrl;
		ReporteController reporteCtrl;
		ZonasController zonasCtrl;
		VehiculoController vehiculoCtrl;
		private PersonaController personaCtrl;
		private SeguimientoController seguimientoCtrl;

		string userName;
		string nit;

		List<GeocercaCboDet> geocercas;
		
		List<WS.DATA.EntradaSalidaRpt> reporte;

		//List<int> codGeos;
		//List<string> nroPlacas;

		ReportDocument rptDocument;

		protected void Page_Load(object sender, EventArgs e)
		{
			homeCtrl = new HomeController();
			reporteCtrl = new ReporteController();
			zonasCtrl = new ZonasController();
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
					CargarGeocercas();
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

		private void CargarGeocercas()
		{
			if (!User.IsInRole("SA") || SitePrincipal.ExisteActiva())
			{
				userName = User.Identity.Name;
				nit = homeCtrl.obtenerNit(userName);

				geocercas = zonasCtrl.GetAll(nit);
			}
			else
				geocercas = zonasCtrl.GetAll();

			//geocercas.Insert(0, new GeocercaCboDet { CodigoGEO = 0, Descripcion = "Todas" });

			gdvGeocercas.DataSource = geocercas;
			gdvGeocercas.DataBind();
		}

		//public void CargarVehiculos()
		//{
		//    if (!User.IsInRole("SA") || SitePrincipal.ExisteActiva())
		//        vehiculos = vehiculoCtrl.GetAllVehiculos2(nit);
		//    else
		//        vehiculos = vehiculoCtrl.GetAllVehiculos2();

		//    vehiculos.Insert(0, new VehiculoCboDet { Id = "0", NroPlaca = "Todos" });
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
		public int MyProperty { get; private set; }
		private void CargarDetalle()
		{
			reporte = new List<WS.DATA.EntradaSalidaRpt>();
			ViewState["RptEntradasSalidas"] = reporte;

			gdvEntradaSalida.DataSource = reporte;
			gdvEntradaSalida.DataBind();
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
		public List<WS.DATA.EntradaSalidaRpt> getReporteEntrada(string fromdate, string todate, string placa, string codgeo)
		{
			LReportes re = new LReportes();
			DateTime _fromdate = Convert.ToDateTime(fromdate);
			DateTime _todate = Convert.ToDateTime(todate);
			var list = re.getlistEntradaSalida(_fromdate, _todate, placa, codgeo);
			return list;
		}
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

			//var geoSelecCount = cboGeocerca.CheckedItems.Count;
			var vehSelecCount = cboplaca.CheckedItems.Count;
			var listageo = getlistGeocercas();

			if (listageo.Length > 0 && vehSelecCount > 0)
			{

				string nroPlacas = cboplaca.SelectedValue;


				string _fromdate = fechaI + " " + horaI;
				string _todate = fechaF + " " + horaF;
				var lis = getReporteEntrada(_fromdate, _todate, nroPlacas, listageo);
				if (lis != null)
				{
					if (lis.Count > 0)
					{
						List<WS.DATA.EntradaSalidaRpt> li = lis.ToList();
						//reporte = reporteCtrl.GetAllEntradaSalida(codGeos, nroPlacas, estado, fechaI, horaI, fechaF, horaF);
						ViewState["RptEntradasSalidas"] = li;
						gdvEntradaSalida.DataSource = li;
						gdvEntradaSalida.DataBind();
						gdvEntradaSalida.Visible = true;
						msnEntradaSalida.Visible = false;
						uprespuesta.Update();

					}
					else
					{
						gdvEntradaSalida.Visible = false;
						msnEntradaSalida.Visible = true;
						uprespuesta.Update();
					}

				}
				else
				{
					gdvEntradaSalida.Visible = false;
					msnEntradaSalida.Visible = true;
					uprespuesta.Update();
				}

			}
		}
		private void alert(string mensaje)
		{
			string funct = "javascript:alertlog('" + mensaje + "');";

			ClientScript.RegisterStartupScript(GetType(), "JavaScript", funct, true);
		}
		private string getlistGeocercas()
		{
			int index = 0;
			string nroPlacas = "";

			foreach (GridViewRow gvr in gdvGeocercas.Rows)
			{
				bool selecDest = ((CheckBox)gvr.FindControl("SelecTipoZona")).Checked;

				if (selecDest)
				{
					nroPlacas = nroPlacas + Convert.ToString(gdvGeocercas.Rows[index].Cells[1].Text) + ",";
				}

				index++;
			}
			return nroPlacas.Substring(0, nroPlacas.Length - 1);
		}

		protected void btnExportar_Click(object sender, EventArgs e)
		{
			userName = User.Identity.Name;
			nit = homeCtrl.obtenerNit(userName);

			reporte = (List<WS.DATA.EntradaSalidaRpt>)ViewState["RptEntradasSalidas"];

			if (reporte.Count > 0)
			{
				var empresa = "Todas";

				if (SitePrincipal.ExisteActiva())
					empresa = homeCtrl.nombreEmpresa(nit);

				var fechaInicio = Request["datepicker1"].ToString() + " " + cbohorai.Text;
				var fechaFin = Request["datepicker2"].ToString() + " " + cbohoraf.Text;

				var placa = "Todas";
				var cboPlacaCheckedItems = cboplaca.CheckedItems.ToList();

				if (cboPlacaCheckedItems[0].Value != "0")
				{
					placa = cboPlacaCheckedItems[0].Value;

					for (int i = 1 ; i < cboPlacaCheckedItems.Count ; i++)
						placa = placa + ", " + cboPlacaCheckedItems[i].Value;
				}


				var geocerca = getlistGeocercas();

				//if (cboGeocercaCheckedItems[0].Value != "0")
				//{
				//	geocerca = cboGeocercaCheckedItems[0].Value;

				//	for (int i = 1 ; i < cboGeocercaCheckedItems.Count ; i++)
				//		geocerca = geocerca + ", " + cboGeocercaCheckedItems[i].Value;
				//}

				rptDocument = new ReportDocument();
				rptDocument.Load(Server.MapPath("~/Reporte/reporteEntradaSalida.rpt"));

				rptDocument.SetDataSource(reporte);
				rptDocument.SetParameterValue("Empresa", empresa);
				rptDocument.SetParameterValue("FechaInicio", fechaInicio);
				rptDocument.SetParameterValue("FechaFin", fechaFin);
				rptDocument.SetParameterValue("Placa", placa);
				rptDocument.SetParameterValue("Geocerca", geocerca);

				Response.Buffer = false;
				Response.Clear();

				var formato = rcbFormato.SelectedValue;

				if (formato == "0")
				{
					rptDocument.ExportToHttpResponse(ExportFormatType.Excel, Response, true,
					"reporteEntradaSalida_v" + DateTime.Now.ToString("yyyyMMddHHMMss"));
				}
				else
					if (formato == "1")
					{
						rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true,
						"reporteEntradaSalida_v" + DateTime.Now.ToString("yyyyMMddHHMMss"));
					}
			}
		}

		protected void gdvEntradaSalida_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "VerMapa")
			{
				int index = Convert.ToInt32(e.CommandArgument);
				GridViewRow row = gdvEntradaSalida.Rows[index];

				string longitud = row.Cells[6].Text;
				string latitud = row.Cells[7].Text;

				string verMapa = "window.open('/FrmUbicacionMapa?longitud=" + longitud + "&latitud=" + latitud + "', '_newtab');";
				ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), verMapa, true);
			}
		}


	}
}