using ClosedXML.Excel;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos.optimizado;
using WISETRACK.Models;
using WS.DATA;
using WS.LOGICA;





namespace WISETRACK
{
	public partial class RptConsolidado : System.Web.UI.Page
	{
		ReporteController rptCtrl;
		HomeController homeCtrl;
		VehiculoController vehiculoCtrl;
		CrystalDecisions.CrystalReports.Engine.ReportDocument Documento;
		private PersonaController personaCtrl;
		private SeguimientoController seguimientoCtrl;

		protected void Page_Load(object sender, EventArgs e)
		{
			rptCtrl = new ReporteController();
			homeCtrl = new HomeController();
			vehiculoCtrl = new VehiculoController();
			Documento = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
			personaCtrl = new PersonaController();
			seguimientoCtrl = new SeguimientoController();


			if (!IsPostBack)
			{
				//if (!SitePrincipal.IsIntruso())
				//{
				init();
				cargarVehiculo();
				CargarFechas();

				FristParametres();
				CargarParameters();
				//btnBuscar.Attributes["onclick"] = "javascript:gfProceso()";

				//this.btnExportar.Attributes.Add("OnClick", "javascript:return fnAceptar();");
				//RegistrarScript();

				//}
				//else
				//{
				//	Response.Redirect("~/Account/Login");
				//}
			}
			//Menu1.Items[0].Selected = true;
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

		public void CargarFechas()
		{
			DateTime fechaActual = DateTime.Now;
			cbohorai.Text = "00:00";
			cbohoraf.Text = "" + (fechaActual.Hour < 10 ? "0" + fechaActual.Hour : "" + fechaActual.Hour)
				+ ":" + (fechaActual.Minute < 10 ? "0" + fechaActual.Minute : "" + fechaActual.Minute);
		}



		protected void gdvDetenciones_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "VerMapa")
			{
				int index = Convert.ToInt32(e.CommandArgument);
				GridViewRow row = gdvDetenciones.Rows[index];

				string longitud = row.Cells[5].Text;
				string latitud = row.Cells[6].Text;

				string verMapa = "window.open('/FrmUbicacionMapa?longitud=" + longitud + "&latitud=" + latitud + "', '_newtab');";

				//ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), verMapa, true);
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
				//ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), verMapa, true);
			}

		}
		//protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
		//{
		//	this.CrystalReportViewer1.Dispose();
		//}
		protected void gdvVelocidadesMax_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "VerMapa")
			{
				int index = Convert.ToInt32(e.CommandArgument);
				GridViewRow row = gdvVelocidadesMax.Rows[index];

				string longitud = row.Cells[4].Text;
				string latitud = row.Cells[5].Text;

				string verMapa = "window.open('/FrmUbicacionMapa?longitud=" + longitud + "&latitud=" + latitud + "', '_newtab');";
				//ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), verMapa, true);
			}
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


		private void FristParametres()
		{
			DataTable dt = new DataTable();
			DataRow dr = null;
			dt.Columns.Add(new DataColumn("Col1", typeof(string)));
			dt.Columns.Add(new DataColumn("Col2", typeof(string)));
			dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
			dt.Columns.Add(new DataColumn("Col3", typeof(string)));

			//dr = dt.NewRow();
			//dr["Col1"] = string.Empty;
			//dr["Col2"] = string.Empty;

			//dr["RowNumber"] = "Auditoria";
			//dr["Col3"] = string.Empty;
			//dt.Rows.Add(dr);

			dr = dt.NewRow();
			dr["Col1"] = string.Empty;
			dr["Col2"] = string.Empty;

			dr["RowNumber"] = "Detencion";
			dr["Col3"] = string.Empty;
			dt.Rows.Add(dr);
			dr = dt.NewRow();
			dr["Col1"] = string.Empty;
			dr["Col2"] = string.Empty;

			dr["RowNumber"] = "Entradas/Sailda";
			dr["Col3"] = string.Empty;
			dt.Rows.Add(dr);
			dr = dt.NewRow();
			dr["Col1"] = string.Empty;
			dr["Col2"] = string.Empty;

			dr["RowNumber"] = "VolicidadMax";
			dr["Col3"] = string.Empty;
			dt.Rows.Add(dr);
			ViewState["CurrentTable"] = dt;
			grvParametersDetails.DataSource = dt;
			grvParametersDetails.DataBind();
			CheckBox chk = (CheckBox)grvParametersDetails.Rows[0].Cells[0].FindControl("chkVerificar");
			chk.Focus();
			for (int i = 0 ; i < dt.Rows.Count ; i++)
			{
				DropDownList DrpQualification = (DropDownList)grvParametersDetails.Rows[i].Cells[2].FindControl("drpCondition");
				TextBox txt = (TextBox)grvParametersDetails.Rows[i].Cells[2].FindControl("txtParametro");
				CheckBox chk1 = (CheckBox)grvParametersDetails.Rows[0].Cells[0].FindControl("chkVerificar");
				chk1.Checked = false;
				switch (i)
				{
					case 0:

						txt.Text = "1";
						DrpQualification.DataSource = getdatos();
						DrpQualification.DataValueField = "Key";

						//Definimos el campo que contendrá los textos que se verán en el control

						DrpQualification.DataTextField = "Value";

						//Enlazamos los valores de los datos con el contenido del Control

						DrpQualification.DataBind();
						DrpQualification.Items.FindByValue("3").Selected = true;
						break;
					case 1:
						txt.Text = "0";
						DrpQualification.DataSource = CargarGeocercas();
						DrpQualification.DataValueField = "Key";

						//Definimos el campo que contendrá los textos que se verán en el control

						DrpQualification.DataTextField = "Value";

						//Enlazamos los valores de los datos con el contenido del Control

						DrpQualification.DataBind();

						break;
					case 2:
						txt.Text = "50";

						DrpQualification.DataSource = getdatos();
						DrpQualification.DataValueField = "Key";

						//Definimos el campo que contendrá los textos que se verán en el control

						DrpQualification.DataTextField = "Value";

						//Enlazamos los valores de los datos con el contenido del Control

						DrpQualification.DataBind();
						DrpQualification.Items.FindByValue("3").Selected = true;
						break;


					default:
						break;
				}

				//DrpQualification.Items.FindByValue("3").Selected = true;



			}
		}

		private void CargarParameters()
		{
			DataTable dt = new DataTable();
			DataRow dr = null;
			dt.Columns.Add(new DataColumn("Col1", typeof(string)));
			dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));


			dr = dt.NewRow();
			dr["Col1"] = string.Empty;


			dr["RowNumber"] = "Kilometro";

			dt.Rows.Add(dr);

			dr = dt.NewRow();
			dr["Col1"] = string.Empty;

			dr["RowNumber"] = "Tempetura";

			dt.Rows.Add(dr);
			dr = dt.NewRow();
			dr["Col1"] = string.Empty;


			dr["RowNumber"] = "Encendido y Apagado";

			dt.Rows.Add(dr);

			grvParameters.DataSource = dt;
			grvParameters.DataBind();
			for (int i = 0 ; i < grvParameters.Rows.Count ; i++)
			{
				CheckBox chk1 = (CheckBox)grvParameters.Rows[0].Cells[0].FindControl("chekVerificar1");
				chk1.Checked = false;
			}

		}
		private List<KeyValuePair<int, string>> CargarGeocercas()
		{
			ZonasController zonasCtrl = new ZonasController();
			List<GeocercaCboDet> geocercas = new List<GeocercaCboDet>();

			if (!User.IsInRole("SA") || SitePrincipal.ExisteActiva())
			{
				var userName = User.Identity.Name;
				var nit = homeCtrl.obtenerNit(userName);

				geocercas = zonasCtrl.GetAll(nit);
			}
			else
				geocercas = zonasCtrl.GetAll();


			List<KeyValuePair<int, string>> datos = new List<KeyValuePair<int, string>>();
			foreach (var geo in geocercas)
			{
				datos.Add(new KeyValuePair<int, string>(geo.CodigoGEO, geo.Descripcion));
			}
			return datos;
		}
		public List<KeyValuePair<int, string>> getdatos()
		{
			List<KeyValuePair<int, string>> datos = new List<KeyValuePair<int, string>>()

				{
					new KeyValuePair<int, string> (0, "Igual A"),
					new KeyValuePair<int, string> (1, "Menor A"),
					new KeyValuePair<int, string> (2, "Mayor A"),
					new KeyValuePair<int, string> (3, "Mayor o Igual A"),
					new KeyValuePair<int, string> (4, "Menor o Igual A"),
					
				};
			return datos;
		}


		//Thread th1 = new Thread(new ThreadStart(cargarDetencion));
		//Thread th2 = new Thread(new ThreadStart(CargarEntradaSalida));
		//Thread th3 = new Thread(new ThreadStart(CargarVelocidadMaxima));
		//th1.Start();
		//th2.Start();
		//th3.Start();
		//th1.Join();
		//th2.Join();
		//th3.Join();

		protected void btnExportar_Click(object sender, EventArgs e)
		{
			//ClientScript.RegisterStartupScript(GetType(), "mostrar", "diHola();", true);
			//Response.Clear();
			//Response.Buffer = true;

			//Response.AddHeader("content-disposition",
			// "attachment;filename=GridViewExport.xls");
			//Response.Charset = "";
			//Response.ContentType = "application/vnd.ms-excel";
			//StringWriter sw = new StringWriter();
			//HtmlTextWriter hw = new HtmlTextWriter(sw);

			//PrepareForExport(gdvDetenciones);
			//PrepareForExport(gdvKilometrajes);

			//Table tb = new Table();
			//TableRow tr1 = new TableRow();
			//TableCell cell1 = new TableCell();
			//cell1.Controls.Add(gdvKilometrajes);
			//tr1.Cells.Add(cell1);
			//TableCell cell3 = new TableCell();
			//cell3.Controls.Add(gdvDetenciones);
			//TableCell cell2 = new TableCell();
			//cell2.Text = "&nbsp;";
			////if (rbPreference.SelectedValue == "2")
			////{
			////	tr1.Cells.Add(cell2);
			////	tr1.Cells.Add(cell3);
			////	tb.Rows.Add(tr1);
			////}
			////else
			////{
			//TableRow tr2 = new TableRow();
			//tr2.Cells.Add(cell2);
			//TableRow tr3 = new TableRow();
			//tr3.Cells.Add(cell3);
			//tb.Rows.Add(tr1);
			//tb.Rows.Add(tr2);
			//tb.Rows.Add(tr3);

			//tb.RenderControl(hw);

			////style to format numbers to string
			//string style = @"<style> .textmode { mso-number-format:\@; } </style>";
			//Response.Write(style);
			//Response.Output.Write(sw.ToString());
			//Response.Flush();
			//Response.End();
			exportToExcel();

		}
		protected void PrepareForExport(GridView Gridview)
		{
			//	Gridview.AllowPaging = Convert.ToBoolean(rbPaging.SelectedItem.Value);
			//	Gridview.DataBind();

			//Change the Header Row back to white color
			Gridview.HeaderRow.Style.Add("background-color", "#FFFFFF");

			//Apply style to Individual Cells
			for (int k = 0 ; k < Gridview.HeaderRow.Cells.Count ; k++)
			{
				Gridview.HeaderRow.Cells[k].Style.Add("background-color", "green");
			}

			for (int i = 0 ; i < Gridview.Rows.Count ; i++)
			{
				GridViewRow row = Gridview.Rows[i];

				//Change Color back to white
				row.BackColor = System.Drawing.Color.White;

				//Apply text style to each Row
				row.Attributes.Add("class", "textmode");

				//Apply style to Individual Cells of Alternating Row
				if (i % 2 != 0)
				{
					for (int j = 0 ; j < Gridview.Rows[i].Cells.Count ; j++)
					{
						row.Cells[j].Style.Add("background-color", "#C2D69B");
					}
				}
			}
		}
		protected void grvStudentDetails1_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{

		}
		private void RegistrarScript()
		{
			const string ScriptKey = "ScriptKey";
			if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
			{
				StringBuilder fn = new StringBuilder();
				fn.Append("function fnAceptar() { ");
				fn.Append("alert('El Contenido del TextBox es:'); ");

				fn.Append("}");
				ClientScript.RegisterStartupScript(this.GetType(),
		ScriptKey, fn.ToString(), true);
			}
		}

		protected void grvParameters_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{

		}
		public void cargarDetencion()
		{
			var fechaI = Request["datepicker1"].ToString();
			var fechaF = Request["datepicker2"].ToString();
			string horaI = cbohorai.Text;
			string horaF = cbohoraf.Text;
			string placa = cboplaca.SelectedValue;
			ReporteController reporteCtrl = new ReporteController();
			List<RptDetencionesViewModel> reporte = new List<RptDetencionesViewModel>();

			GridViewRow dt = grvParametersDetails.Rows[0];
			CheckBox ck = (CheckBox)dt.FindControl("chkVerificar");
			if (ck.Checked)
			{
				DropDownList dp = (DropDownList)dt.FindControl("drpCondition");
				int tipoRel = Convert.ToInt32(dp.SelectedValue);
				TextBox txbTiempoDet = (TextBox)dt.FindControl("txtParametro");
				int tiempoDet = (txbTiempoDet.Text.Equals("") ? 0 : Convert.ToInt32(txbTiempoDet.Text));
				if (!String.IsNullOrEmpty(fechaI) && !String.IsNullOrEmpty(fechaF) && !String.IsNullOrEmpty(horaI) && !String.IsNullOrEmpty(horaF) && !String.IsNullOrEmpty(placa))
				{
					reporte = reporteCtrl.ListarReporteDetenciones(placa, fechaI, horaI, fechaF, horaF, tipoRel, tiempoDet);
					if (reporte.Count > 0)
					{
						ViewState["RptDetenciones"] = reporte;
						gdvDetenciones.DataSource = reporte;
						gdvDetenciones.DataBind();

						gdvDetenciones.Visible = true;
						msnDetencion.Visible = false;
						UpdatePanelDetencion.Update();


					}
					else
					{
						ViewState["RptDetenciones"] = reporte;
						gdvDetenciones.DataSource = reporte;
						gdvDetenciones.DataBind();

						gdvDetenciones.Visible = false;
						msnDetencion.Visible = true;
						UpdatePanelDetencion.Update();

					}

				}
				else
				{
					//MensajeAlerta("Favor, Completar todos los campos");
				}
			}

		}
		public void CargarEntradaSalida()
		{
			//userName = HttpContext.Current.User.Identity.Name;
			//nit = homeCtrl.obtenerNit(userName);

			//string usuario = userName + ":" + nit;

			var txtfechaini = Request["datepicker1"].ToString();
			var txtfechafin = Request["datepicker2"].ToString();

			//int estado = Convert.ToInt32(rcbEstado.SelectedValue);

			string fechaI = txtfechaini;
			string horaI = cbohorai.Text;

			string fechaF = txtfechafin;
			string horaF = cbohoraf.Text;
			GridViewRow gr = grvParametersDetails.Rows[1];
			CheckBox ck = (CheckBox)gr.FindControl("chkVerificar");
			if (ck.Checked)
			{

				DropDownList dp = (DropDownList)gr.FindControl("drpCondition");
				var geoSelecCount = dp.Items.Count;
				var vehSelecCount = cboplaca.Items.Count;

				if (geoSelecCount > 0 && vehSelecCount > 0)
				{
					//codGeos = new List<int>();
					string codGeos = dp.SelectedValue;
					//foreach (var item in cboGeocerca.CheckedItems.ToList())
					//{
					//    var codGeo = item.Value;

					//    if (codGeo != "0")
					//        codGeos.Add(Convert.ToInt32(codGeo));
					//}

					//nroPlacas = new List<string>();
					string nroPlacas = cboplaca.SelectedValue;
					//foreach (var item in cboplaca.CheckedItems.ToList())
					//{
					//    var nroPlaca = item.Value;

					//    if (nroPlaca != "0")
					//        nroPlacas.Add(nroPlaca);
					//}
					string _fromdate = fechaI + " " + horaI;
					string _todate = fechaF + " " + horaF;
					var lis = getReporteEntrada(_fromdate, _todate, nroPlacas, codGeos);
					if (lis != null)
					{
						if (lis.Count > 0)
						{
							List<WS.DATA.EntradaSalidaRpt> li = lis;
							//reporte = reporteCtrl.GetAllEntradaSalida(codGeos, nroPlacas, estado, fechaI, horaI, fechaF, horaF);
							ViewState["RptEntradasSalidas"] = li;
							gdvEntradaSalida.DataSource = li;
							gdvEntradaSalida.DataBind();

							gdvEntradaSalida.Visible = true;
							msnEntradaSalida.Visible = false;

						}
						else
						{
							//alert("No hay datos para mostrar");
							gdvEntradaSalida.Visible = false;
							msnEntradaSalida.Visible = true;
						}

					}
					else
					{
						//alert("No hay datos para mostrar");
						gdvEntradaSalida.Visible = false;
						msnEntradaSalida.Visible = true;
					}

				}
				UpdatePanelEntradaSalida.Update();

			}
		}
		public void CargarVelocidadMaxima()
		{
			//userName = HttpContext.Current.User.Identity.Name;
			//nit = homeCtrl.obtenerNit(userName);

			//string usuario = userName + ":" + nit;

			var fechaI = Request["datepicker1"].ToString();
			var fechaF = Request["datepicker2"].ToString();

			string horaI = cbohorai.Text;
			string horaF = cbohoraf.Text;
			ReporteController reporteCtrl = new ReporteController();
			GridViewRow dt = grvParametersDetails.Rows[2];
			CheckBox ck = (CheckBox)dt.FindControl("chkVerificar");
			if (ck.Checked)
			{
				DropDownList dp = (DropDownList)dt.FindControl("drpCondition");
				int tipoRel = Convert.ToInt32(dp.SelectedValue);
				TextBox txbVelocidad = (TextBox)dt.FindControl("txtParametro");

				int velocidad = (txbVelocidad.Text.Equals("") ? 0 : Convert.ToInt32(txbVelocidad.Text));

				string nroplaca = cboplaca.SelectedValue;

				List<string> list = new List<string>() { nroplaca };
				//'0004-SNP','1170YPH','1539-KFU'
				List<VelocidadRptDet> reporte = new List<VelocidadRptDet>();
				reporte = reporteCtrl.GetAllVelocidades(list, fechaI, horaI, fechaF, horaF, velocidad, tipoRel);
				var repot = reporte.OrderByDescending(x => x.Vehiculo).ToList();
				if (repot.Count > 0)
				{
					ViewState["RptVelocidades"] = repot;

					gdvVelocidadesMax.DataSource = repot;
					gdvVelocidadesMax.DataBind();
					gdvVelocidadesMax.Visible = true;
					msnVelocidadMax.Visible = false;

				}
				else
				{
					ViewState["RptVelocidades"] = repot;

					gdvVelocidadesMax.DataSource = repot;
					gdvVelocidadesMax.DataBind();
					gdvVelocidadesMax.Visible = false;
					msnVelocidadMax.Visible = true;

				}
				UpdatePanelVelocidadMax.Update();

			}

		}
		public void CargarKilometraje()
		{

			var fechaI = Request["datepicker1"].ToString();
			var fechaF = Request["datepicker2"].ToString();
			string horaI = cbohorai.Text;
			string horaF = cbohoraf.Text;

			var rfini = fechaI + " " + horaI;
			var rffin = fechaF + " " + horaF;
			GridViewRow dt = grvParameters.Rows[0];
			CheckBox ck = (CheckBox)dt.FindControl("chekVerificar1");
			List<KilometrajeRptDetOptimizado> reporte = new List<KilometrajeRptDetOptimizado>();
			if (ck.Checked)
			{

				string placas = cboplaca.SelectedValue;

				reporte = getlistaReporteKilometraje(placas, rfini, rffin);
				if (reporte.Count > 0)
				{
					ViewState["RptKilometrajes"] = reporte;

					gdvKilometrajes.DataSource = reporte;
					gdvKilometrajes.DataBind();
					gdvKilometrajes.Visible = true;
					msnKilometraje.Visible = false;

				}
				else
				{
					gdvKilometrajes.Visible = false;
					msnKilometraje.Visible = true;
				}


			}
			else
			{
				gdvKilometrajes.DataSource = reporte;
				gdvKilometrajes.DataBind();

				tabKilometraje.Visible = false;
				msnKilometraje.Visible = true;
			}
			UpdatePanelKilometraje.Update();
		}
		private List<KilometrajeRptDetOptimizado> getlistaReporteKilometraje(string placas, string rfini, string rffin)
		{

			List<KilometrajeRptDetOptimizado> list = new List<KilometrajeRptDetOptimizado>();
			DateTime from = Convert.ToDateTime(rfini);
			DateTime toend = Convert.ToDateTime(rffin);

			ReporteController reporteCtrl = new ReporteController();
			var listE = reporteCtrl.GetAllKilometrajes(placas, from, toend);
			foreach (var item in listE)
			{
				double dif = item.DMMEncendido.Value;
				dif = Math.Round(dif, 0);

				list.Add(new KilometrajeRptDetOptimizado()
				{
					Vehiculo = item.Nroplaca,
					FechaInicio = Convert.ToString(item.FechaInicio),
					FechaFin = Convert.ToString(item.FechaFin),
					Kilometraje = Convert.ToString(dif) + " km.",

				});


			}
			return list;
		}
		public List<WS.DATA.EntradaSalidaRpt> getReporteEntrada(string fromdate, string todate, string placa, string codgeo)
		{
			LReportes re = new LReportes();
			DateTime _fromdate = Convert.ToDateTime(fromdate);
			DateTime _todate = Convert.ToDateTime(todate);
			var list = re.getlistEntradaSalida(_fromdate, _todate, placa, codgeo);
			return list;
		}
		public void CargarTemperatura()
		{
			try
			{
				GridViewRow dt = grvParameters.Rows[1];
				CheckBox ck = (CheckBox)dt.FindControl("chekVerificar1");
				if (ck.Checked)
				{
					listarTemperatura();
					lblf.Text = DateTime.Now.ToString();
					UpdatePanelTemperatura.Update();
				}

				//mensajeAlerta();

			}
			catch (Exception ex)
			{

			}
		}
		public void listarTemperatura()
		{

			string txtfechaini;
			string txtfechafin;
			string f1;
			string f2;
			string nombreempresa;
			CargarDatosTemperatura(out txtfechaini, out txtfechafin, out f1, out f2, out nombreempresa);
			WS.LOGICA.LReportes logica = new WS.LOGICA.LReportes();


			string placa = cboplaca.SelectedValue;
			DateTime datefrom = Convert.ToDateTime(f1);
			DateTime dateto = Convert.ToDateTime(f2);
			if (CalcularDiasDeDiferencia(datefrom, dateto) <= 15 && !string.IsNullOrEmpty(placa))
			{
				List<WS.DATA.ReporteTempuratura> lis = logica.getlistTemperatura(datefrom, dateto, placa);
				List<RptTemperaturaViewModel> reporteList = new List<RptTemperaturaViewModel>();
				if (lis != null)
				{
					ControlarAperturayCierre(ref reporteList, lis);

					if (reporteList.Count() > 0)
					{
						ViewState["RptTemperatura"] = reporteList;


						Documento.Load(Server.MapPath("~/Reporte/reporteTemperaturaOptimizado.rpt"));
						//Documento.SetDataSource(reporteList);
						Documento.Database.Tables[0].SetDataSource(reporteList);

						Documento.SetParameterValue("fechainicio", f1);
						Documento.SetParameterValue("fechafin", f2);
						Documento.SetParameterValue("nplaca", cboplaca.Text);
						Documento.SetParameterValue("empresa", nombreempresa);

						this.CrystalReportViewer1.ReportSource = Documento;
						this.CrystalReportViewer1.DataBind();
						CrystalReportViewer1.Visible = true;
						msnTemperatura.Visible = false;
						//success("Se cargaron los datos Corectamente");
					}
					else
					{
						//MensajeAlertaV("No hay datos en el servidor");
						//MensajeAlertaV("No hay datos en el Servidor");
						CrystalReportViewer1.Visible = false;
						msnTemperatura.Visible = true;
					}
				}
			}
			else
			{
				//if (string.IsNullOrEmpty(placa))
				//	MensajeAlertaV("Seleccione alguna placa del vehiculo");
				//else
				//	MensajeAlertaV("El reporte de temperatura solo puede sacar menores a  15 dias");

				//mensajeAlerta();
			}


		}

		private static void ControlarAperturayCierre(ref List<RptTemperaturaViewModel> lista, List<WS.DATA.ReporteTempuratura> collection)
		{
			bool bandera = false; //Bandera: Se utiliza para determinar si en la consulta hubo un evento GTDIS que indique cual fue primero (Apertura o Cierre)
			bool okgtdis = false; //okgtdis: Se utiliza para determinar si hubo un evento GTDIS
			var result = collection.Where(s => s.TipoMensaje == "GTDIS").ToList();
			bool buscarGTDIS = false; //Obtener el primer valor GTDIS de la consulta.
			if (result.Count > 0)
			{
				okgtdis = true;
				buscarGTDIS = (bool)collection.Where(s => s.TipoMensaje == "GTDIS").First().EstadoPuerta;
				if (buscarGTDIS == false)
				{
					bandera = true;
				}
				else
				{
					bandera = false;
				}
			}

			int cont = 1;
			foreach (var item in collection.OrderBy(s => s.FechaGPS))
			{
				RptTemperaturaViewModel rptlista = new RptTemperaturaViewModel();
				if (item.TipoMensaje == "GTDIS")
				{
					bandera = item.EstadoPuerta;
				}
				if (item.TipoMensaje != "GTDIS")
				{
					if (okgtdis == true)
					{
						if (item.Temperatura != -999.00)
						{
							rptlista.direcciones = item.direcciones;
							rptlista.EstadoPuerta = (bandera == true) ? "Cerrado" : "Abierto";
							rptlista.FechaGPS = item.FechaGPS;
							rptlista.IDButton = item.IDButton;
							rptlista.IMEI = item.IMEI;
							rptlista.Latitud = item.Latitud;
							rptlista.Longitud = item.Longitud;
							rptlista.Nombre = item.Nombre;
							rptlista.NroPlaca = item.NroPlaca;
							rptlista.Temperatura = item.Temperatura;
							rptlista.Velocidad = item.Velocidad;
							lista.Add(rptlista);
						}
					}
					else
					{
						bandera = true;
						if (item.Temperatura != -999.00)
						{
							rptlista.direcciones = item.direcciones;
							rptlista.EstadoPuerta = (bandera == true) ? "Cerrado" : "Abierto";
							rptlista.FechaGPS = item.FechaGPS;
							rptlista.IDButton = item.IDButton;
							rptlista.IMEI = item.IMEI;
							rptlista.Latitud = item.Latitud;
							rptlista.Longitud = item.Longitud;
							rptlista.Nombre = item.Nombre;
							rptlista.NroPlaca = item.NroPlaca;
							rptlista.Temperatura = item.Temperatura;
							rptlista.Velocidad = item.Velocidad;
							lista.Add(rptlista);
						}
					}
				}
				cont++;
			}
		}

		public double CalcularDiasDeDiferencia(DateTime primerFecha, DateTime segundaFecha)
		{
			TimeSpan diferencia;
			diferencia = segundaFecha - primerFecha;

			return diferencia.Days;
		}
		private void CargarDatosTemperatura(out string txtfechaini, out string txtfechafin, out string f1, out string f2, out string nombreempresa)
		{
			txtfechaini = Request["datepicker1"].ToString();
			txtfechafin = Request["datepicker2"].ToString();
			f1 = txtfechaini + " " + cbohorai.Text;
			f2 = txtfechafin + " " + cbohoraf.Text;
			var user = HttpContext.Current.User.Identity.Name;
			var nit = homeCtrl.obtenerNit(user);
			if (nit != null)
			{
				nombreempresa = homeCtrl.nombreEmpresa(nit);
			}
			else
			{
				nombreempresa = "No definido";
			}
		}
		public void CargarEncendidoApadado()
		{
			string nroplacas = cboplaca.SelectedValue;

			var fechaI = Request["datepicker1"].ToString();
			var fechaF = Request["datepicker2"].ToString();

			string horaI = cbohorai.Text;
			string horaF = cbohoraf.Text;

			var rfini = fechaI + " " + horaI;
			var rffin = fechaF + " " + horaF;

			GridViewRow dt = grvParameters.Rows[2];
			CheckBox ck = (CheckBox)dt.FindControl("chekVerificar1");
			if (ck.Checked)
			{
				showReporte(nroplacas, rfini, rffin);
			}
			//var list = getlistaEncendidoApagado(nroplacas, fini, ffin);

		}
		private void showReporte(string placas, string fini, string ffin)
		{
			DateTime dfini = Convert.ToDateTime(fini);
			DateTime dffin = Convert.ToDateTime(ffin);
			//Reset
			rptViewer1.Reset();
			//DataSource
			var lis = getlistaEncendidoApagado(placas, dfini, dffin);
			ReportDataSource rds = new ReportDataSource("DS", lis);
			rptViewer1.LocalReport.DataSources.Add(rds);
			//Path
			rptViewer1.LocalReport.ReportPath = "RDLC/ReportEncendidoApagado.rdlc";

			//Parameters
			ReportParameter[] rptParams = new ReportParameter[]{
				new ReportParameter("fromDate",fini),
				new ReportParameter("toDate",ffin)
			};
			rptViewer1.ProcessingMode = ProcessingMode.Local;
			rptViewer1.LocalReport.SetParameters(rptParams);
			//Refresh
			rptViewer1.LocalReport.Refresh();
		}
		private List<DetEncendidoApagado> getlistaEncendidoApagado(string placas, DateTime from, DateTime toend)
		{
			List<DetEncendidoApagado> list = new List<DetEncendidoApagado>();
			ReporteController reporteCtrl = new ReporteController();
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

		protected void grvParametersDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{

		}

		protected void btnBuscar_Click1(object sender, EventArgs e)
		{
			//cargarAuditoria();
			cargarDetencion();
			CargarEntradaSalida();
			CargarVelocidadMaxima();
			CargarKilometraje();
			CargarTemperatura();

			//RegisterAsyncTask(new PageAsyncTask(async () =>
			//{
			//	var client = new DataServiceClient();
			//	var gettingCities = await client.GetCitiesAsync();
			//	gvCities.DataSource = gettingCities;
			//	gvCities.DataBind();
			//})); 

			//PageAsyncTask aks = new PageAsyncTask(
			//	delegate(Object s, EventArgs ev, AsyncCallback cb, object state)
			//	{
			//		Response.Write("<br> Method Fired at" + DateTime.Now.ToLongTimeString());
			//		//return LongRunner.BeginLongRunningTransaction(cb, state);
			//		return null;
			//	},

			//delegate(IAsyncResult asyncResult)
			//{
			//	Response.Write("<br> Response Fired at " + DateTime.Now.ToLongTimeString());
			//},

			//delegate(IAsyncResult asyncResult)
			//{
			//	Response.Write("<br> Timeout Fired at" + DateTime.Now.ToLongTimeString());
			//},

			//null
			//	);
			//Page.RegisterAsyncTask(aks);
			//Thread th1 = new Thread(new ThreadStart(cargarDetencion));
			//Thread th2 = new Thread(new ThreadStart(CargarEntradaSalida));
			//Thread th3 = new Thread(new ThreadStart(CargarVelocidadMaxima));
			//Thread th4 = new Thread(new ThreadStart(CargarKilometraje));
			//Thread th5 = new Thread(new ThreadStart(CargarTemperatura));
			//Thread th6 = new Thread(new ThreadStart(CargarEncendidoApadado));
			//th1.Start();
			//th2.Start();
			//th3.Start();
			//th4.Start();

			//th5.Start();
			//th6.Start();
			//th1.Join();
			//th2.Join();
			//th3.Join();
			//th4.Join();
			//th4.Join();
			//th6.Join();
		}

		public void exportToExcel()
		{
			List<RptTemperaturaViewModel> reporteList = (List<RptTemperaturaViewModel>)ViewState["RptTemperatura"];
			//GridView gvTemperatura = new GridView();
			//gdv.DataSource = reporteList;
			//gvTemperatura.DataBind();
			List<KilometrajeRptDetOptimizado> reporte = (List<KilometrajeRptDetOptimizado>)ViewState["RptKilometrajes"];
			//GridView gvKilometraje = new GridView();
			gdvKilometrajes.DataSource = reporte;
			gdvKilometrajes.DataBind();
			List<WS.DATA.EntradaSalidaRpt> li = (List<WS.DATA.EntradaSalidaRpt>)ViewState["RptEntradasSalidas"];
			//GridView gvEntradaSlida = new GridView();
			gdvEntradaSalida.DataSource = li;
			gdvEntradaSalida.DataBind();

			List<RptDetencionesViewModel> reportes = (List<RptDetencionesViewModel>)ViewState["RptDetenciones"];
			//GridView gvDetenciones = new GridView();
			gdvDetenciones.DataSource = reportes;
			gdvDetenciones.DataBind();
			List<VelocidadRptDet> reportess = (List<VelocidadRptDet>)ViewState["RptVelocidades"];
			//GridView gvVelocidad = new GridView();
			gdvVelocidadesMax.DataSource = reportess;
			gdvVelocidadesMax.DataBind();
			XLWorkbook wb = new XLWorkbook();

			GridView[] gvExcel = new GridView[] { gdvKilometrajes, gdvEntradaSalida, gdvDetenciones, gdvVelocidadesMax };
			string[] name = new string[] { "Kilometraje", "EntradaSalida", "Detenciones", "Velocidad" };

			for (int i = 0 ; i < gvExcel.Length ; i++)
			{
				if (gvExcel[i].Visible)
				{
					gvExcel[i].AllowPaging = false;
					gvExcel[i].DataBind();
					DataTable dt = new DataTable(name[i].ToString());
					for (int z = 0 ; z < gvExcel[i].Columns.Count ; z++)
					{
						dt.Columns.Add(gvExcel[i].Columns[z].HeaderText);
					}

					foreach (GridViewRow row in gvExcel[i].Rows)
					{
						dt.Rows.Add();
						for (int c = 0 ; c < row.Cells.Count ; c++)
						{
							dt.Rows[dt.Rows.Count - 1][c] = row.Cells[c].Text;
						}
					}

					wb.Worksheets.Add(dt);
					gvExcel[i].AllowPaging = true;

				}

			}
			Response.Clear();
			Response.Buffer = true;
			Response.Charset = "";
			Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

			var filename = "attachment;filename=ReporteConsolido.v" + DateTime.Now.ToString() + ".xlsx";
			Response.AddHeader("content-disposition", filename);
			using (MemoryStream MyMemoryStream = new MemoryStream())
			{
				wb.SaveAs(MyMemoryStream);
				MyMemoryStream.WriteTo(Response.OutputStream);
				Response.Flush();
				Response.End();
			}
		}
		protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
		{
			this.CrystalReportViewer1.Dispose();
		}

	}
}
