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
	public partial class RptAlarmas : System.Web.UI.Page
	{
		AlarmaController alarmaCtrl;
		HomeController homeCtrl;
		ReporteController reporteCtrl;
		private VehiculoController vehiculoCtrl;
		private PersonaController personaCtrl;
		private SeguimientoController seguimientoCtrl;

		static List<AlarmaRptDet> rptAlarmas;


		List<TipoAlarmaCboDet> fltTipos;
		List<CategAlarmaCboDet> fltCategorias;
		List<VehiculoCboDet> fltVehiculos;

		string userName, nit;
		bool okFiltro;
		static string sortExpresion, sortDireccion;

		protected void Page_Load(object sender, EventArgs e)
		{
			alarmaCtrl = new AlarmaController();
			homeCtrl = new HomeController();
			reporteCtrl = new ReporteController();
			vehiculoCtrl = new VehiculoController();
			rptAlarmas = new List<AlarmaRptDet>();
			personaCtrl = new PersonaController();
			seguimientoCtrl = new SeguimientoController();

			//cboplaca.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
			cbohorai.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
			cbohoraf.Filter = (RadComboBoxFilter)Convert.ToInt32(2);

			if (!IsPostBack)
			{
				if (!SitePrincipal.IsIntruso())
				{
					CargarTiposAlarma();
					CargarFechas();
					cargarVehiculo();
					CargarColumnas();
					CargarDetalle();
				}
				else
					Response.Redirect("~/Account/Login");
			}
		}

		private void CargarTiposAlarma()
		{
			gdvTiposAlarma.DataSource = alarmaCtrl.GetAllTipos();
			gdvTiposAlarma.DataBind();
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

		//private void CargarVehiculos()
		//{
		//    if (!User.IsInRole("SA"))
		//    {
		//        userName = User.Identity.Name;
		//        nit = homeCtrl.obtenerNit(userName);
		//        vehiculos = alarmaCtrl.GetAllVehiculos2(nit);
		//    }
		//    else
		//    {
		//        vehiculos = alarmaCtrl.GetAllVehiculos2();
		//    }


		//    vehiculos.Insert(0, new VehiculoCboDet { NroPlaca = "todos" });
		//    cboplaca.DataValueField = "NroPlaca";
		//    cboplaca.DataTextField = "NroPlaca";
		//    cboplaca.DataSource = vehiculos;
		//    cboplaca.DataBind();

		//    cboplaca.SelectedIndex = 0;
		//}

		public void cargarVehiculo()
		{
			if (HttpContext.Current.User.IsInRole("SA"))
			{
				cboplaca.DataSource = vehiculoCtrl.cargarDetalleVehiculosSA();
				cboplaca.DataTextField = "Patente";
				cboplaca.DataValueField = "NroPlaca";
				cboplaca.DataBind();
				//cboplaca.Items.Insert(0, "todas");
			}
			else
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
					string nit = homeCtrl.obtenerNit(user);
					cboplaca.DataSource = seguimientoCtrl.comboVehiculo(nit);
					cboplaca.DataTextField = "Patente";
					cboplaca.DataValueField = "NroPlaca";
					cboplaca.DataBind();
					//cboplaca.Items.Insert(0, "todas");
				}
			}
		}

		private void CargarColumnas()
		{
			int index = 0;

			foreach (var item in cboFltColumnas.Items.ToList())
			{
				if (index < 7)
					item.Checked = true;

				index++;
			}
		}

		private void CargarDetalle()
		{
			gdvAlarmas.DataSource = rptAlarmas;
			gdvAlarmas.DataBind();

			if (rptAlarmas.Count > 0)
			{
				cboFltTipos.DataValueField = "CodTipoAlarma";
				cboFltTipos.DataTextField = "Descripcion";
				cboFltTipos.DataSource = fltTipos;
				cboFltTipos.DataBind();

				cboFltCategorias.DataValueField = "CodCategoria";
				cboFltCategorias.DataTextField = "Descripcion";
				cboFltCategorias.DataSource = fltCategorias;
				cboFltCategorias.DataBind();

				cboFltVehiculos.DataValueField = "NroPlaca";
				cboFltVehiculos.DataTextField = "NroPlaca";
				cboFltVehiculos.DataSource = fltVehiculos;
				cboFltVehiculos.DataBind();

				foreach (var item in cboFltTipos.Items.ToList())
					item.Checked = true;

				foreach (var item in cboFltCategorias.Items.ToList())
					item.Checked = true;

				foreach (var item in cboFltVehiculos.Items.ToList())
					item.Checked = true;

				cboFltTipos.Enabled = true;
				cboFltCategorias.Enabled = true;
				cboFltVehiculos.Enabled = true;

				btnFiltrar.Enabled = true;
			}
			else
			{
				cboFltTipos.Enabled = false;
				cboFltCategorias.Enabled = false;
				cboFltVehiculos.Enabled = false;

				btnFiltrar.Enabled = false;
			}
		}

		private void CargarFltDetalle()
		{
			gdvAlarmas.DataSource = rptAlarmas;
			gdvAlarmas.DataBind();
		}

		private void CargarDataAlarma()
		{
			int index = 0;
			List<int> codTipos = new List<int>();

			foreach (GridViewRow gvr in gdvTiposAlarma.Rows)
			{
				bool selecTipo = ((CheckBox)gvr.FindControl("SelecTipoAlarma")).Checked;

				if (selecTipo)
				{
					int codAlarma = Convert.ToInt32(gdvTiposAlarma.Rows[index].Cells[1].Text);
					codTipos.Add(codAlarma);
				}

				index++;
			}

			if (codTipos.Count > 0)
			{
				var txtfechaini = Request["datepicker1"].ToString();
				var txtfechafin = Request["datepicker2"].ToString();
				string fechaI = txtfechaini;
				string horaI = cbohorai.Text;

				string fechaF = txtfechafin;
				string horaF = cbohoraf.Text;

				string placa = cboplaca.Text;

				if (placa.Equals("todos"))
				{
					if (!User.IsInRole("SA") || SitePrincipal.ExisteActiva())
					{
						userName = User.Identity.Name;
						nit = homeCtrl.obtenerNit(userName);

						rptAlarmas = reporteCtrl.GetAllRptAlarmas(codTipos, nit, fechaI, horaI, fechaF, horaF, sortExpresion, sortDireccion);

						fltTipos = reporteCtrl.TiposAlarmaDistinct;
						fltCategorias = reporteCtrl.CategsAlarmaDistinct;
						fltVehiculos = reporteCtrl.VehiculosDistinct;
					}
					else
					{
						rptAlarmas = reporteCtrl.GetAllSARptAlarmas(codTipos, fechaI, horaI, fechaF, horaF, sortExpresion, sortDireccion);

						fltTipos = reporteCtrl.TiposAlarmaDistinct;
						fltCategorias = reporteCtrl.CategsAlarmaDistinct;
						fltVehiculos = reporteCtrl.VehiculosDistinct;
					}
				}
				else
				{
					rptAlarmas = reporteCtrl.GetRptAlarmas(codTipos, placa, fechaI, horaI, fechaF, horaF, sortExpresion, sortDireccion);

					//fltTipos = reporteCtrl.TiposAlarmaDistinct;
					//fltCategorias = reporteCtrl.CategsAlarmaDistinct;
					//fltVehiculos = reporteCtrl.VehiculosDistinct;
				}
			}
			else
			{
				rptAlarmas = new List<AlarmaRptDet>();

				fltTipos = new List<TipoAlarmaCboDet>();
				fltCategorias = new List<CategAlarmaCboDet>();
				fltVehiculos = new List<VehiculoCboDet>();
			}
		}

		private void CargarFltDataAlarma()
		{
			List<string> tipos = new List<string>();
			List<string> categs = new List<string>();
			List<string> vehs = new List<string>();

			foreach (var item in cboFltTipos.CheckedItems)
				tipos.Add(item.Value);

			foreach (var item in cboFltCategorias.CheckedItems)
				categs.Add(item.Value);

			foreach (var item in cboFltVehiculos.CheckedItems)
				vehs.Add(item.Value);

			if (tipos.Count == 0)
			{
				foreach (var item in cboFltTipos.Items.ToList())
				{
					item.Checked = true;
					tipos.Add(item.Value);
				}
			}

			if (categs.Count == 0)
			{
				foreach (var item in cboFltCategorias.Items.ToList())
				{
					item.Checked = true;
					categs.Add(item.Value);
				}
			}

			if (vehs.Count == 0)
			{
				foreach (var item in cboFltVehiculos.Items.ToList())
				{
					item.Checked = true;
					vehs.Add(item.Value);
				}
			}

			var txtfechaini = Request["datepicker1"].ToString();
			var txtfechafin = Request["datepicker2"].ToString();
			string fechaI = txtfechaini;
			string horaI = cbohorai.Text;

			string fechaF = txtfechafin;
			string horaF = cbohoraf.Text;

			if (!User.IsInRole("SA") || SitePrincipal.ExisteActiva())
			{
				userName = User.Identity.Name;
				nit = homeCtrl.obtenerNit(userName);

				rptAlarmas = reporteCtrl.GetFltRptAlarmas(tipos, categs, vehs, nit, fechaI, horaI, fechaF, horaF, sortExpresion, sortDireccion);
			}
			else
				rptAlarmas = reporteCtrl.GetFltSARptAlarmas(tipos, categs, vehs, fechaI, horaI, fechaF, horaF, sortExpresion, sortDireccion);
		}

		private void Exportar()
		{

		}

		public SortDirection SortDir
		{
			get
			{
				if (ViewState["SortDirState"] == null)
					ViewState["SortDirState"] = SortDirection.Ascending;

				return (SortDirection)ViewState["SortDirState"];
			}

			set { ViewState["SortDirState"] = value; }
		}

		protected void cbohorai_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
		{
			e.Item.Text = string.Concat(e.Item.Text.ToLower().Split(' ')[0], "");
		}

		protected void cbohoraf_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
		{
			e.Item.Text = string.Concat(e.Item.Text.ToLower().Split(' ')[0], "");
		}

		//protected void cboplaca_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
		//{
		//    e.Item.Text = string.Concat(e.Item.Text.ToLower().Split(' ')[0], "");
		//}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			//int index = 0;

			//foreach (var item in cboFltColumnas.Items.ToList())
			//{
			//    if (!item.Checked)
			//        gdvAlarmas.Columns[index].Visible = false;
			//    else
			//        gdvAlarmas.Columns[index].Visible = true;

			//    index++;
			//}

			sortExpresion = "at.FechaHora";
			sortDireccion = "DESC";

			CargarDataAlarma();
			//CargarDetalle();

			okFiltro = false;
			upresultado.Update();
		}

		protected void btnExportar_Click(object sender, EventArgs e)
		{
			Exportar();
		}

		protected void gdvAlarmas_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (!okFiltro)
				CargarDataAlarma();
			else
				CargarFltDataAlarma();

			if (e.CommandName == "VerMapa")
			{
				int index = Convert.ToInt32(e.CommandArgument);

				string longitud = Convert.ToString(rptAlarmas[index].Longitud);
				string latitud = Convert.ToString(rptAlarmas[index].Latitud);

				string verMapa = "window.open('/FrmUbicacionMapa?longitud=" + longitud + "&latitud=" + latitud + "', '_newtab');";
				ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), verMapa, true);
			}
		}

		protected void gdvTiposAlarma_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gdvTiposAlarma.PageIndex = e.NewPageIndex;
			CargarTiposAlarma();
		}

		protected void gdvAlarmas_Sorting(object sender, GridViewSortEventArgs e)
		{
			bool ok = false;
			sortExpresion = String.Empty;

			switch (e.SortExpression)
			{
				case "Nombre":
					sortExpresion = "am.NombreAlarma";
					ok = true;
					break;

				case "Tipo":
					sortExpresion = "ta.Descripcion";
					ok = true;
					break;

				case "Categoria":
					sortExpresion = "ca.Descripcion";
					ok = true;
					break;

				case "Vehiculo":
					sortExpresion = "v.NroPlaca";
					ok = true;
					break;

				case "Fecha":
					sortExpresion = "ti.FechaGPS";
					ok = true;
					break;
			}

			if (ok)
			{
				sortDireccion = String.Empty;

				if (SortDir == SortDirection.Ascending)
				{
					SortDir = SortDirection.Descending;
					sortDireccion = "DESC";
				}
				else
				{
					SortDir = SortDirection.Ascending;
					sortDireccion = "ASC";
				}

				CargarFltDataAlarma();
				CargarFltDetalle();

				okFiltro = true;
			}
		}

		protected void btnFiltrar_Click(object sender, EventArgs e)
		{
			int rowCount = gdvAlarmas.Rows.Count;

			if (rowCount > 0)
			{
				int index = 0;

				foreach (var item in cboFltColumnas.Items.ToList())
				{
					if (!item.Checked)
						gdvAlarmas.Columns[index].Visible = false;
					else
						gdvAlarmas.Columns[index].Visible = true;

					index++;
				}

				CargarFltDataAlarma();
				CargarFltDetalle();
			}
		}
	}
}