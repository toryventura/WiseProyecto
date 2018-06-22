using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WISETRACK.Controller;
using WISETRACK.Datos;
using WISETRACK.Models;

namespace WISETRACK
{
    public partial class RptDetenciones : System.Web.UI.Page
    {
        HomeController homeCtrl;
        ReporteController reporteCtrl;
        VehiculoController vehiculoCtrl;

        string userName;
        string nit;

        List<VehiculoCboDet> vehiculos;
        List<DetencionRptDet> reporte;

        List<string> nroPlacas;

        ReportDocument rptDocument;

        protected void Page_Load(object sender, EventArgs e)
        {
            homeCtrl = new HomeController();
            reporteCtrl = new ReporteController();
            vehiculoCtrl = new VehiculoController();

            cboplaca.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
            cbohorai.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
            cbohoraf.Filter = (RadComboBoxFilter)Convert.ToInt32(2);

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    CargarFechas();
                    CargarVehiculos();
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

        public void CargarVehiculos()
        {
            if (!User.IsInRole("SA") || SitePrincipal.ExisteActiva())
            {
                userName = User.Identity.Name;
                nit = homeCtrl.obtenerNit(userName);

                vehiculos = vehiculoCtrl.GetAllVehiculos2(nit);
            }
            else
                vehiculos = vehiculoCtrl.GetAllVehiculos2();

            vehiculos.Insert(0, new VehiculoCboDet { Id = "0", NroPlaca = "Todos" });
            cboplaca.DataValueField = "Id";
            cboplaca.DataTextField = "NroPlaca";

            cboplaca.DataSource = vehiculos;
            cboplaca.DataBind();
        }

        private void CargarDetalle()
        {
            reporte = new List<DetencionRptDet>();
            ViewState["RptDetenciones"] = reporte;

            gdvDetenciones.DataSource = reporte;
            gdvDetenciones.DataBind();
        }

        protected void cbohorai_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = string.Concat(e.Item.Text.Split(' ')[0], "");
        }

        protected void cbohoraf_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = string.Concat(e.Item.Text.Split(' ')[0], "");
        }

        protected void cboplaca_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = string.Concat(e.Item.Text.Split(' ')[0], "");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            userName = User.Identity.Name;
            nit = homeCtrl.obtenerNit(userName);

            string usuario = userName + ":" + nit;

            var fechaI = Request["datepicker1"].ToString();
            var fechaF = Request["datepicker2"].ToString();

            string horaI = cbohorai.Text;
            string horaF = cbohoraf.Text;

            int tiempoDet = (txbTiempoDet.Text.Equals("") ? 0 : Convert.ToInt32(txbTiempoDet.Text));
            int tipoRel = Convert.ToInt32(rcbTipoRel.SelectedValue);

            var vehSelecCount = cboplaca.CheckedItems.Count;

            if (vehSelecCount > 0)
            {
                nroPlacas = new List<string>();

                foreach (var item in cboplaca.CheckedItems.ToList())
                {
                    var nroPlaca = item.Value;

                    if (nroPlaca != "0")
                        nroPlacas.Add(nroPlaca);
                }

                reporte = reporteCtrl.GetAllDetenciones(nroPlacas, fechaI, horaI, fechaF, horaF, tiempoDet, tipoRel);
                ViewState["RptDetenciones"] = reporte;

                gdvDetenciones.DataSource = reporte;
                gdvDetenciones.DataBind();

                upresultado.Update();
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            userName = User.Identity.Name;
            nit = homeCtrl.obtenerNit(userName);

            reporte = (List<DetencionRptDet>) ViewState["RptDetenciones"];

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

                    for (int i = 1; i < cboPlacaCheckedItems.Count; i++)
                        placa = placa + ", " + cboPlacaCheckedItems[i].Value;
                }

                rptDocument = new ReportDocument();
                rptDocument.Load(Server.MapPath("~/Reporte/reporteDetencion.rpt"));
                
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
                    "reporteDetencion.v" + DateTime.Now.ToString() + ".xlsx");
                }
                else
                if (formato == "1")
                {
                    rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true,
                    "reporteDetencion.v" + DateTime.Now.ToString() + ".pdf");
                }
            }
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), verMapa, true);
            }
        }

    }
}