using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using WISETRACK.Controller;
using WISETRACK.Datos.optimizado;

namespace WISETRACK
{
	public partial class FrmRptTemperatura : System.Web.UI.Page
	{
		ReporteController rptCtrl;
		HomeController homeCtrl;
		VehiculoController vehiculoCtrl;
		ReportDocument Documento;
		private PersonaController personaCtrl;
		private SeguimientoController seguimientoCtrl;

		protected void Page_Load(object sender, EventArgs e)
		{
			rptCtrl = new ReporteController();
			homeCtrl = new HomeController();
			vehiculoCtrl = new VehiculoController();
			Documento = new ReportDocument();
			personaCtrl = new PersonaController();
			seguimientoCtrl = new SeguimientoController();

			if (!IsPostBack)
			{
				if (!SitePrincipal.IsIntruso())
				{
					cargarVehiculo();
					CargarFechas();
				}
				else
				{
					Response.Redirect("~/Account/Login");
				}
			}
		}

		//public void cargarVehiculo()
		//{
		//    var user = HttpContext.Current.User.Identity.Name;
		//    string nit = homeCtrl.obtenerNit(user);
		//    cboplaca.DataSource = vehiculoCtrl.cargarDetalleVehiculos(nit);
		//    cboplaca.DataTextField = "NroPlaca";
		//    cboplaca.DataValueField = "NroPlaca";
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
		string funct;
		private void error(string mensaje)
		{

			funct = "javascript:errorlog('" + mensaje + "');";
			ClientScript.RegisterStartupScript(GetType(), "JavaScript", funct, false);
		}
		private void success(string mensaje)
		{
			funct = "javascript:successlog('" + mensaje + "');";

			ClientScript.RegisterStartupScript(GetType(), "JavaScript", funct, true);
		}
		private void bloquear()
		{

			funct = "javascript:bloquear();";

			ClientScript.RegisterStartupScript(GetType(), "JavaScript", funct, true);

		}
		private void desbloquear()
		{

			funct = "javascript:desbloquear();";

			ClientScript.RegisterStartupScript(GetType(), "JavaScript", funct, true);
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
						Documento = new ReportDocument();
						Documento.Load(Server.MapPath("~/Reporte/reporteTemperaturaOptimizado.rpt"));
						//Documento.SetDataSource(reporteList);
						Documento.Database.Tables[0].SetDataSource(reporteList);

						Documento.SetParameterValue("fechainicio", f1);
						Documento.SetParameterValue("fechafin", f2);
						Documento.SetParameterValue("nplaca", cboplaca.Text);
						Documento.SetParameterValue("empresa", nombreempresa);

						this.CrystalReportViewer1.ReportSource = Documento;
						this.CrystalReportViewer1.DataBind();
						upcrystal.Update();
						success("Se cargaron los datos Corectamente");
					}
					else
					{
						//MensajeAlertaV("No hay datos en el servidor");
						MensajeAlertaV("No hay datos en el Servidor");
					}
				}
			}
			else
			{
				if (string.IsNullOrEmpty(placa))
					MensajeAlertaV("Seleccione alguna placa del vehiculo");
				else
					MensajeAlertaV("El reporte de temperatura solo puede sacar menores a  15 dias");

				mensajeAlerta();
			}


		}
		public double CalcularDiasDeDiferencia(DateTime primerFecha, DateTime segundaFecha)
		{
			TimeSpan diferencia;
			diferencia = segundaFecha - primerFecha;

			return diferencia.Days;
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

		protected void btnCargar_Click(object sender, EventArgs e)
		{
			try
			{

				listarTemperatura();
				lblf.Text = DateTime.Now.ToString();
				mensajeAlerta();

			}
			catch (Exception ex)
			{

			}
		}

		protected void btnExportar_Click(object sender, EventArgs e)
		{
			ListarTemperaturaExcel();
		}

		public void ListarTemperaturaExcel()
		{
			string txtfechaini;
			string txtfechafin;
			string f1;
			string f2;
			string nombreempresa;
			CargarDatosTemperatura(out txtfechaini, out txtfechafin, out f1, out f2, out nombreempresa);
			List<RptTemperaturaViewModel> reporteList = (List<RptTemperaturaViewModel>)ViewState["RptTemperatura"];



			if (reporteList.Count() > 0)
			{
				ReportDocument doc = new ReportDocument();
				doc.Load(Path.Combine(Server.MapPath("~/Reporte"), "reporteTemperaturaOptimizado.rpt"));
				doc.SetDataSource(reporteList);
				Response.Buffer = false;
				Response.ClearHeaders();
				Response.ClearContent();

				doc.SetParameterValue("fechainicio", f1);
				doc.SetParameterValue("fechafin", f2);
				doc.SetParameterValue("nplaca", cboplaca.Text);
				doc.SetParameterValue("empresa", nombreempresa);

				try
				{
					doc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, true, "reporteTemperaturaExcel" + DateTime.Now);
					Response.Close();
				}
				catch (Exception ex)
				{
					throw;
				}
			}
			else
			{
				MensajeAlertaV("No hay datos en el servidor");
			}
		}

		protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
		{
			this.CrystalReportViewer1.Dispose();
		}

		protected void btnExportarPDF_Click(object sender, EventArgs e)
		{
			ListarTemperaturaPDF();
		}

		public void ListarTemperaturaPDF()
		{
			string txtfechaini;
			string txtfechafin;
			string f1;
			string f2;
			string nombreempresa;
			CargarDatosTemperatura(out txtfechaini, out txtfechafin, out f1, out f2, out nombreempresa);
			List<RptTemperaturaViewModel> reporteList = (List<RptTemperaturaViewModel>)ViewState["RptTemperatura"];

			//reporteList = rptCtrl.listarReporteTemperaturaOptimizada(txtfechaini, cbohorai.SelectedItem.Text, txtfechafin, cbohoraf.SelectedItem.Text, cboplaca.Text);

			if (reporteList.Count() > 0)
			{
				ReportDocument doc = new ReportDocument();
				doc.Load(Path.Combine(Server.MapPath("~/Reporte"), "reporteTemperaturaOptimizado.rpt"));
				doc.SetDataSource(reporteList);
				Response.Buffer = false;
				Response.ClearHeaders();
				Response.ClearContent();

				doc.SetParameterValue("fechainicio", f1);
				doc.SetParameterValue("fechafin", f2);
				doc.SetParameterValue("nplaca", cboplaca.Text);
				doc.SetParameterValue("empresa", nombreempresa);

				try
				{
					doc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "reporteTemperaturaPdf" + DateTime.Now);
					Response.Close();
				}
				catch (Exception ex)
				{
					throw;
				}
			}
			else
			{
				MensajeAlertaV("No hay datos en el servidor");
			}
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
		public void mensajeAlerta()
		{
			String csname1 = "PopupScript";
			String csname2 = "ButtonClickScript";
			Type cstype = this.GetType();

			// Get a ClientScriptManager reference from the Page class.
			ClientScriptManager cs = Page.ClientScript;

			// Check to see if the startup script is already registered.
			if (!cs.IsStartupScriptRegistered(cstype, csname1))
			{
				String cstext1 = "alert('Hello World');";
				cs.RegisterStartupScript(cstype, csname1, cstext1, true);
			}

			// Check to see if the client script is already registered.
			if (!cs.IsClientScriptBlockRegistered(cstype, csname2))
			{
				StringBuilder cstext2 = new StringBuilder();
				cstext2.Append("<script type=\"text/javascript\"> function DoClick() {");
				cstext2.Append("Form1.Message.value='Text from client script.'} </");
				cstext2.Append("script>");
				cs.RegisterClientScriptBlock(cstype, csname2, cstext2.ToString(), false);
			}
		}

		//No usado... Borrar en 30 dias FechaCreacion: 03/07/2016 21:00
		public void ListarTemperaturaExcelNoDirecciones()
		{
			string txtfechaini;
			string txtfechafin;
			string f1;
			string f2;
			string nombreempresa;
			List<RptTemperaturaViewModel> reporteList = new List<RptTemperaturaViewModel>();
			CargarDatosTemperatura(out txtfechaini, out txtfechafin, out f1, out f2, out nombreempresa);
			//reporteList = rptCtrl.listarReporteTemperaturaOptimizada(txtfechaini, cbohorai.SelectedItem.Text, txtfechafin, cbohoraf.SelectedItem.Text, cboplaca.Text);
			reporteList = rptCtrl.listarReporteTemperaturaOptimizada(txtfechaini, cbohorai.Text, txtfechafin, cbohoraf.Text, cboplaca.SelectedValue);
			if (reporteList.Count() > 0)
			{
				ReportDocument doc = new ReportDocument();
				doc.Load(Path.Combine(Server.MapPath("~/Reporte"), "reporteTemperaturaOptimizado.rpt"));
				doc.SetDataSource(reporteList);
				Response.Buffer = false;
				Response.ClearHeaders();
				Response.ClearContent();

				doc.SetParameterValue("fechainicio", f1);
				doc.SetParameterValue("fechafin", f2);
				doc.SetParameterValue("nplaca", cboplaca.Text);
				doc.SetParameterValue("empresa", nombreempresa);

				try
				{
					doc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, true, "reporteTemperaturaExcelNoDirecciones" + DateTime.Now);
					Response.Close();
				}
				catch (Exception ex)
				{
					throw;
				}
			}
			else
			{
				MensajeAlertaV("No hay datos en el servidor");
			}
		}
	}
}