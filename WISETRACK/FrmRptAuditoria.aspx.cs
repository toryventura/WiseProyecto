using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WISETRACK.Controller;
using WISETRACK.Datos.optimizado;

namespace WISETRACK
{
	public partial class FrmRptAuditoria : System.Web.UI.Page
	{
		private HomeController homeCtrl;
		private VehiculoController vehiculoCtrl;
		private List<RptAuditoriaViewModel> reporte;
		private AuditoriaController auditoriaCtrl;
		private ReportDocument rptDocument;
		private PersonaController personaCtrl;
		private SeguimientoController seguimientoCtrl;

		protected void Page_Load(object sender, EventArgs e)
		{
			homeCtrl = new HomeController();
			vehiculoCtrl = new VehiculoController();
			auditoriaCtrl = new AuditoriaController();
			reporte = new List<RptAuditoriaViewModel>();
			personaCtrl = new PersonaController();
			seguimientoCtrl = new SeguimientoController();

			if (!IsPostBack)
			{
				if (!SitePrincipal.IsIntruso())
				{
					cargarVehiculo();
					CargarFechas();
					CargarDetalle();
				}
				else
				{
					Response.Redirect("~/Account/Login");
				}
			}
		}

		private void CargarDetalle()
		{
			reporte = new List<RptAuditoriaViewModel>();
			ViewState["RptAuditoria"] = reporte;
		}
		public void CargarFechas()
		{
			txtkmh.Text = "" + 0;
			DateTime fechaActual = DateTime.Now;
			cbohorai.Text = "00:00";
			cbohoraf.Text = "" + (fechaActual.Hour < 10 ? "0" + fechaActual.Hour : "" + fechaActual.Hour)
				+ ":" + (fechaActual.Minute < 10 ? "0" + fechaActual.Minute : "" + fechaActual.Minute);
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

		protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
		{
			this.CrystalReportViewer1.Dispose();
		}

		protected void btnCargar_Click(object sender, EventArgs e)
		{
			ListarReporteAuditoria();
			upcrystal.Update();
		}

		private void ListarReporteAuditoria()
		{
			string user = User.Identity.Name;
			string nit = homeCtrl.obtenerNit(user);

			var fechaI = Request["datepicker1"].ToString();
			var fechaF = Request["datepicker2"].ToString();
			string horaI = cbohorai.Text;
			string horaF = cbohoraf.Text;
			string placa = cboplaca.SelectedValue;
			string cod = cbokm.Text;
			string velocidad = txtkmh.Text;
			if (!String.IsNullOrEmpty(fechaI) && !String.IsNullOrEmpty(fechaF) && !String.IsNullOrEmpty(horaI) && !String.IsNullOrEmpty(horaF) && !String.IsNullOrEmpty(placa))
			{
				reporte = auditoriaCtrl.ReporteAuditoriaOptimizada(fechaI, horaI, fechaF, horaF, placa);
				if (!String.IsNullOrEmpty(velocidad))
				{
					switch (cod)
					{
						case "1":
							//igual
							reporte = reporte.Where(det => det.Velocidad == Convert.ToDouble(velocidad)).ToList();
							break;
						case "2":
							//mayor
							reporte = reporte.Where(det => det.Velocidad > Convert.ToDouble(velocidad)).ToList();
							break;
						case "3":
							//mayor igual
							reporte = reporte.Where(det => det.Velocidad >= Convert.ToDouble(velocidad)).ToList();
							break;
						//default:
						//    break;
					}
				}

				ViewState["RptAuditoria"] = reporte;

				var empresa = "N";
				if (SitePrincipal.ExisteActiva())
				{
					empresa = homeCtrl.nombreEmpresa(nit);
					rptDocument = new ReportDocument();
					rptDocument.Load(Server.MapPath("~/Reporte/reporteAuditoria.rpt"));
					//Documento.SetDataSource(reporteList);
					rptDocument.Database.Tables[0].SetDataSource(reporte);

					rptDocument.SetParameterValue("fechainicio", fechaI + " " + horaI);
					rptDocument.SetParameterValue("fechafin", fechaF + " " + horaF);
					rptDocument.SetParameterValue("nplaca", cboplaca.Text);
					rptDocument.SetParameterValue("empresa", empresa);

					this.CrystalReportViewer1.ReportSource = rptDocument;
					this.CrystalReportViewer1.DataBind();

				}
				else
				{
					if (HttpContext.Current.User.IsInRole("SA"))
					{
						empresa = "Todas";
						rptDocument = new ReportDocument();
						rptDocument.Load(Server.MapPath("~/Reporte/reporteAuditoria.rpt"));
						//Documento.SetDataSource(reporteList);
						rptDocument.Database.Tables[0].SetDataSource(reporte);

						rptDocument.SetParameterValue("fechainicio", fechaI + " " + horaI);
						rptDocument.SetParameterValue("fechafin", fechaF + " " + horaF);
						rptDocument.SetParameterValue("nplaca", cboplaca.Text);
						rptDocument.SetParameterValue("empresa", empresa);

						this.CrystalReportViewer1.ReportSource = rptDocument;
						this.CrystalReportViewer1.DataBind();
					}
					else
					{
						MensajeAlerta("Favor, Inicie sesion en una empresa");
					}

				}
			}
			else
			{
				MensajeAlerta("Favor, Completar todos los campos");
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

		protected void btnExportar_Click(object sender, EventArgs e)
		{
			string user = User.Identity.Name;
			string nit = homeCtrl.obtenerNit(user);

			reporte = (List<RptAuditoriaViewModel>)ViewState["RptAuditoria"];
			if (reporte.Count > 0)
			{
				var empresa = "No Definido";
				if (SitePrincipal.ExisteActiva())
				{
					empresa = homeCtrl.nombreEmpresa(nit);
				}
				var fechaI = Request["datepicker1"].ToString() + " " + cbohorai.Text;
				var fechaF = Request["datepicker2"].ToString() + " " + cbohoraf.Text;
				string placa = cboplaca.Text;
				rptDocument = new ReportDocument();

				rptDocument.Load(Server.MapPath("~/Reporte/reporteAuditoria.rpt"));

				rptDocument.SetDataSource(reporte);
				rptDocument.SetParameterValue("empresa", empresa);
				rptDocument.SetParameterValue("fechainicio", fechaI);
				rptDocument.SetParameterValue("fechafin", fechaF);
				rptDocument.SetParameterValue("nplaca", placa);

				Response.Buffer = false;
				Response.Clear();

				try
				{
					rptDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, true, "reporteAuditoria" + DateTime.Now);
					Response.Close();
				}
				catch (Exception ex)
				{
					throw;
				}
			}
			else
			{
				MensajeAlerta("Favor, Primero visualizar los datos antes de exportar.");
			}
		}

	}
}