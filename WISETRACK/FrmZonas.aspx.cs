using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK
{
	public partial class FrmZonas : System.Web.UI.Page
	{
		ZonasController zonasControl = new ZonasController();
		List<sp_reporteGeocerca_Result> list = new List<sp_reporteGeocerca_Result>();
		//List<sp_ListarPuntosGeo_Result> listPtos;

		HomeController homeCtrl;
		protected void Page_Load(object sender, EventArgs e)
		{
			homeCtrl = new HomeController();
			if (!IsPostBack)
			{
				if (!SitePrincipal.IsIntruso())
				{
					comboZonas();
					grillaGeocerca();
				}
				else
					Response.Redirect("~/Account/Login");
			}
		}

		public void comboZonas()
		{
			var user = HttpContext.Current.User.Identity.Name;
			var nit = homeCtrl.obtenerNit(user);
			cbotzona.DataSource = zonasControl.comboZonas(nit);
			cbotzona.DataTextField = "Descripcion";
			cbotzona.DataValueField = "CodTipoGEO";
			cbotzona.DataBind();

			cbozonas.DataSource = zonasControl.comboZonas(nit);
			cbozonas.DataTextField = "Descripcion";
			cbozonas.DataValueField = "CodTipoGEO";
			cbozonas.DataBind();
		}

		public void grillaGeocerca()
		{
			var user = HttpContext.Current.User.Identity.Name;
			var nit = homeCtrl.obtenerNit(user);
			string cod = cbotzona.SelectedValue.ToString();
			if (!String.IsNullOrEmpty(cod))
			{
				var query = zonasControl.grillaGeocerca(Convert.ToInt32(cod));
				var result = query.Where(c => c.NIT == nit).ToList();
				gdvGeocerca.DataSource = result;
				gdvGeocerca.DataBind();
			}
		}

		protected void cbotzona_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
			grillaGeocerca();
			List<PuntosGeocerca> list = new List<PuntosGeocerca>();
			gdvPuntosGeo.DataSource = list;
			gdvPuntosGeo.DataBind();
			prueba2.Update();
			upgvpuntosgeo.Update();
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			int count = 0;
			foreach (GridViewRow r in gdvGeocerca.Rows)
			{
				CheckBox check = (CheckBox)r.FindControl("ckboxGeocerca");
				if (check.Checked == true)
				{
					count++;
				}
			}
			if (count == 1)
			{
				RecorrerGrillaChecked();
			}
			else
			{
				List<PuntosGeocerca> list = new List<PuntosGeocerca>();
				gdvPuntosGeo.DataSource = list;
				gdvPuntosGeo.DataBind();
				//AQUI
				lblidgeocerca.Text = "";
				//FIN
				MensajeAlerta("Por favor, Seleccione una Geocerca");
			}

			upgvpuntosgeo.Update();
			//uplabelID.Update();
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

		private void RecorrerGrillaChecked()
		{
			int cod = 0;
			foreach (GridViewRow r in gdvGeocerca.Rows)
			{
				CheckBox check = (CheckBox)r.FindControl("ckboxGeocerca");

				if (check.Checked == true)
				{
					cod = Convert.ToInt32(r.Cells[1].Text);
					gdvPuntosGeo.DataSource = zonasControl.grillaPuntosGeocerca(cod);
					gdvPuntosGeo.DataBind();
					lblidgeocerca.Text = "" + cod.ToString();
				}
			}
		}

		protected void gdvGeocerca_RowDataBound(object sender, GridViewRowEventArgs e)
		{

		}

		protected void btnExportar_Click(object sender, EventArgs e)
		{
			int count = 0;
			int n = 0;
			foreach (GridViewRow r in gdvGeocerca.Rows)
			{
				CheckBox check = (CheckBox)r.FindControl("ckboxGeocerca");
				if (check.Checked == true)
				{
					count++;
				}
			}

			if (count.Equals(1))
			{
				list = asignarIDGeocerca(ref n);
				GenerarReporte();
			}
			else
			{
				MensajeAlerta("Por favor, Seleccione una Geocerca");
			}
		}

		private void GenerarReporte()
		{
			ReportDocument reporte = new ReportDocument();
			reporte.Load(Path.Combine(Server.MapPath("~/Reporte"), "reporteGeocerca.rpt"));
			reporte.SetDataSource(list);
			Response.Buffer = false;
			Response.ClearContent();
			Response.ClearHeaders();

			try
			{
				reporte.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "reporteGeocerca");
				Response.End();
			}
			catch (Exception)
			{
				throw;
			}
		}

		private List<sp_reporteGeocerca_Result> asignarIDGeocerca(ref int n)
		{
			foreach (GridViewRow r in gdvGeocerca.Rows)
			{
				CheckBox check = (CheckBox)r.FindControl("ckboxGeocerca");
				if (check.Checked == true)
				{
					n = Convert.ToInt32(r.Cells[1].Text);
					list = zonasControl.exportarGeocerca(n);
				}
			}
			return list;
		}

		protected void gdvGeocerca_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gdvGeocerca.PageIndex = e.NewPageIndex;
			grillaGeocerca();
		}

		protected void gdvPuntosGeo_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{

		}

		protected void lbleli_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Vistas/Geocercas/Index");
		}


	}
}