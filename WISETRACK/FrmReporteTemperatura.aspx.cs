using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WISETRACK.Controller;
using WISETRACK.Datos;
using WISETRACK.Datos.Serializable;
using WISETRACK.Reporte;

namespace WISETRACK
{
    public partial class FrmReporteTemperatura : System.Web.UI.Page
    {
        ReporteController rptCtrl;
        HomeController homeCtrl;
        VehiculoController vehiculoCtrl;
        private ReportDocument Documento;
        private PersonaController personaCtrl;
        private SeguimientoController seguimientoCtrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            Documento = new ReportDocument();
            rptCtrl = new ReporteController();
            homeCtrl = new HomeController();
            vehiculoCtrl = new VehiculoController();
            personaCtrl = new PersonaController();
            seguimientoCtrl = new SeguimientoController();

            if (!IsPostBack)
            {
                //cargarVehiculo();
                CargarFechas();
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
        //        cboplaca.DataTextField = "NroPlaca";
        //        cboplaca.DataValueField = "NroPlaca";
        //        cboplaca.DataBind();
        //        //cboplaca.Items.Insert(0, "todas");
        //    }
        //    else
        //    {
        //        if (HttpContext.Current.User.IsInRole("CHOFER"))
        //        {
        //            var user = HttpContext.Current.User.Identity.Name;
        //            cboplaca.DataSource = personaCtrl.ObtenerVehiculosAsociadosPersonal(user);
        //            cboplaca.DataTextField = "NroPlaca";
        //            cboplaca.DataValueField = "NroPlaca";
        //            cboplaca.DataBind();
        //            cboplaca.Items.Insert(0, "todas");
        //        }
        //        else
        //        {
        //            var user = HttpContext.Current.User.Identity.Name;
        //            string nit = homeControl.obtenerNit(user);
        //            cboplaca.DataSource = controler.comboVehiculo(nit);
        //            cboplaca.DataTextField = "NroPlaca";
        //            cboplaca.DataValueField = "NroPlaca";
        //            cboplaca.DataBind();
        //            cboplaca.Items.Insert(0, "todas");
        //        }
        //    }
        //}

        public void CargarFechas()
        {
            DateTime fechaActual = DateTime.Now;
            string txtfechaini = String.Empty;
            txtfechaini = (fechaActual.Day < 10 ? "0" + fechaActual.Day : "" + fechaActual.Day)
                + "/" + (fechaActual.Month < 10 ? "0" + fechaActual.Month : "" + fechaActual.Month)
                + "/" + fechaActual.Year;
            cbohorai.Text = "00:00";
            string txtfechafin = String.Empty;

            txtfechafin = (fechaActual.Day < 10 ? "0" + fechaActual.Day : "" + fechaActual.Day)
                + "/" + (fechaActual.Month < 10 ? "0" + fechaActual.Month : "" + fechaActual.Month)
                + "/" + fechaActual.Year;
            cbohorai.Text = "00:00";

            cbohoraf.Text = "" + (fechaActual.Hour < 10 ? "0" + fechaActual.Hour : "" + fechaActual.Hour)
                + ":" + (fechaActual.Minute < 10 ? "0" + fechaActual.Minute : "" + fechaActual.Minute);

        }

        public void listarTemperatura()
        {
            List<temperaturaSerial> reporteList = new List<temperaturaSerial>();
            var txtfechaini = Request["txtfechaini"].ToString();
            var txtfechafin = Request["txtfechafin"].ToString();
            reporteList = rptCtrl.listarReporteTemperatura(txtfechaini, cbohorai.SelectedItem.Text, txtfechafin, cbohoraf.SelectedItem.Text, cboplaca.Text);
            Documento = new ReportDocument();
            Documento.Load(Server.MapPath("~/Reporte/reporteTemperatura.rpt"));

            Documento.SetDataSource(reporteList);
            Documento.SetParameterValue("@fini", txtfechaini.ToString());
            Documento.SetParameterValue("@ffin", txtfechafin.ToString());
            Documento.SetParameterValue("@placa", cboplaca.Text);

            //this.CrystalReportViewer1.ReportSource = Documento;
            //this.CrystalReportViewer1.DataBind();
        }

        public void reporteTemperaturaInterfaz()
        {
            List<temperaturaSerial> reporteList = new List<temperaturaSerial>();
            var txtfechaini = Request["txtfechaini"].ToString();
            var txtfechafin = Request["txtfechafin"].ToString();
            reporteList = rptCtrl.listarReporteTemperatura(txtfechaini, cbohorai.SelectedItem.Text, txtfechafin, cbohoraf.SelectedItem.Text, cboplaca.Text);
            Documento = new ReportDocument();
            Documento.Load(Server.MapPath("~/Reporte/reporteTemperatura.rpt"));
            Documento.SetDataSource(reporteList);
            Response.Buffer = false;
            Response.Clear();

            //Documento.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, true, "reporteTemperatura.v" + DateTime.Now);
            //this.CrystalReportViewer1.DataBind();

        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            listarTemperatura();
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            reporteTemperaturaInterfaz();
        }

    }
}