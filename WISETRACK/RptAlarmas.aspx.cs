using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Telerik.Web.UI;
using WISETRACK.Controller;
using WISETRACK.Datos.Serializable;
using WISETRACK.Models;
using WS.DATA;

namespace WISETRACK
{
	public partial class RptAlarmas2 : System.Web.UI.Page
	{


		//List<TipoAlarmaCboDet> fltTipos;
		//List<CategAlarmaCboDet> fltCategorias;
		//List<VehiculoCboDet> fltVehiculos;

		protected void Page_Load(object sender, EventArgs e)
		{

			if (!IsPostBack)
			{
				if (!SitePrincipal.IsIntruso())
				{
					init();
					//CargarTiposAlarma();ESTO SE VA CARGAR EN HTMAL
					CargarFechas();
					cargarVehiculo();//ESTO TAMBIEN SE VA CARGAR EN HTML
				}
				else
				{
					Response.Redirect("~/Account/Login");
				}
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
		public static string CargarTiposAlarma()
		{
			var alarmaCtrl = new AlarmaController();
			var list = alarmaCtrl.GetAllTiposs();
			return JsonConvert.SerializeObject(list, Formatting.Indented);

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

		protected void cbohorai_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
		{
			e.Item.Text = string.Concat(e.Item.Text.ToLower().Split(' ')[0], "");
		}

		protected void cbohoraf_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
		{
			e.Item.Text = string.Concat(e.Item.Text.ToLower().Split(' ')[0], "");
		}



		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			//sortExpresion = "at.FechaHora";
			//sortDireccion = "DESC";

			//CargarDataAlarma();
			//upresultado.Update();
		}

		protected void btnExportar_Click(object sender, EventArgs e)
		{

		}
		[WebMethod]
		public static string CargarDataAlarma(List<int> tipos, List<string> placa, string fechaI, string fechaF)
		{
			//int index = 0;
			//List<int> codTipos = new List<int>();

			//foreach (GridViewRow gvr in gdvTiposAlarma.Rows)
			//{
			//	bool selecTipo = ((CheckBox)gvr.FindControl("SelecTipoAlarma")).Checked;

			//	if (selecTipo)
			//	{
			//		int codAlarma = Convert.ToInt32(gdvTiposAlarma.Rows[index].Cells[1].Text);
			//		codTipos.Add(codAlarma);
			//	}

			//	index++;
			//}

			//if (codTipos.Count > 0)
			//{
			//	var txtfechaini = Request["datepicker1"].ToString();
			//	var txtfechafin = Request["datepicker2"].ToString();
			//	string fechaI = txtfechaini;
			//	string horaI = cbohorai.Text;

			//	string fechaF = txtfechafin;
			//	string horaF = cbohoraf.Text;

			//	string placa = cboplaca.Text;

			//	if (placa.Equals("todos"))
			//	{
			//		if (!User.IsInRole("SA") || SitePrincipal.ExisteActiva())
			//		{
			//			userName = User.Identity.Name;
			//			nit = homeCtrl.obtenerNit(userName);
			//			rptAlarmas = reporteCtrl.GetAllRptAlarmas(codTipos, nit, fechaI, horaI, fechaF, horaF, sortExpresion, sortDireccion);
			//			gdvAlarmas.DataSource = rptAlarmas;
			//			gdvAlarmas.DataBind();
			//		}
			//		else
			//		{
			//rptAlarmas = reporteCtrl.GetAllSARptAlarmas(codTipos, fechaI, horaI, fechaF, horaF, sortExpresion, sortDireccion);
			//			gdvAlarmas.DataSource = rptAlarmas;
			//			gdvAlarmas.DataBind();
			//		}
			//	}
			//	else
			//	{

			ReporteController reporteCtrl = new ReporteController();
			List<AlarmaRptDet> rptAlarmas = new List<AlarmaRptDet>();
			rptAlarmas = reporteCtrl.GetRptAlarmas(tipos, placa, fechaI, fechaF);//solo necitosesto
			return JsonConvert.SerializeObject(rptAlarmas, Formatting.Indented);
			//		gdvAlarmas.DataSource = rptAlarmas;
			//		gdvAlarmas.DataBind();
			//	}
			//}
			//else
			//{
			//	rptAlarmas = new List<AlarmaRptDet>();
			//	gdvAlarmas.DataSource = rptAlarmas;
			//	gdvAlarmas.DataBind();
			//}
		}

	}
}