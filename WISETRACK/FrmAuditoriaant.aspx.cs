using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WISETRACK.Controller;
using WISETRACK.Datos;
using WISETRACK.Datos.Serializable;


namespace WISETRACK
{
    public partial class FrmAuditoria : System.Web.UI.Page
    {
        AuditoriaController auditoriaCtrl;
        HomeController homeCtrl;
        VehiculoController vehiculoCtrl;
        public static List<ListaAuditoria> listaAudit = new List<ListaAuditoria>();

        protected void Page_Load(object sender, EventArgs e)
        {
            auditoriaCtrl = new AuditoriaController();
            homeCtrl = new HomeController();
            vehiculoCtrl = new VehiculoController();
            //listaAudit = new List<ListaAuditoria>();

            cboplaca.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
            cbohorai.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
            cbohoraf.Filter = (RadComboBoxFilter)Convert.ToInt32(2);

            if (!IsPostBack)
            {
                listaAudit = new List<ListaAuditoria>();
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

        public void CargarFechas()
        {
            txtkmh.Text = "" + 0;
            DateTime fechaActual = DateTime.Now;
            //txtfechaini.Text = (fechaActual.Day < 10 ? "0" + fechaActual.Day : "" + fechaActual.Day)
            //    + "/" + (fechaActual.Month < 10 ? "0" + fechaActual.Month : "" + fechaActual.Month)
            //    + "/" + fechaActual.Year;
            cbohorai.Text = "00:00";

            //txtfechafin.Text = (fechaActual.Day < 10 ? "0" + fechaActual.Day : "" + fechaActual.Day)
            //    + "/" + (fechaActual.Month < 10 ? "0" + fechaActual.Month : "" + fechaActual.Month)
            //    + "/" + fechaActual.Year;            

            cbohoraf.Text = "" + (fechaActual.Hour < 10 ? "0" + fechaActual.Hour : "" + fechaActual.Hour)
                + ":" + (fechaActual.Minute < 10 ? "0" + fechaActual.Minute : "" + fechaActual.Minute);

        }

        //public void cargarAuditoria(string fechaini, string horaini, string fechafin, string hfin, string placa)
        //{
        //    gdvAuditoria.DataSource = auditoriaCtrl.obtenerAuditoria(fechaini, horaini, fechafin, hfin, placa);
        //    gdvAuditoria.DataBind();
        //}

        public void cargarVehiculo()
        {
            var user = HttpContext.Current.User.Identity.Name;
            string nit = homeCtrl.obtenerNit(user);
            if (HttpContext.Current.User.IsInRole("SA"))
            {
                cboplaca.DataSource = vehiculoCtrl.cargarDetalleVehiculosSA();
                cboplaca.DataTextField = "NroPlaca";
                cboplaca.DataValueField = "NroPlaca";
                cboplaca.DataBind();
            }
            else
            {
                cboplaca.DataSource = vehiculoCtrl.cargarDetalleVehiculos(nit);
                cboplaca.DataTextField = "NroPlaca";
                cboplaca.DataValueField = "NroPlaca";
                cboplaca.DataBind();
            }
        }

        protected void cbohorai_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = string.Concat(e.Item.Text.ToLower().Split(' ')[0], "");
        }

        protected void cbohoraf_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = string.Concat(e.Item.Text.ToLower().Split(' ')[0], "");
        }

        protected void cboplaca_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = string.Concat(e.Item.Text.Split(' ')[0], "");
        }

        protected void cboplaca_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            #region a
            //List<tramaSerial> lista = new List<tramaSerial>();
            //foreach (GridViewRow row in gdvAuditoria.Rows)
            //{
            //    tramaSerial audi = new tramaSerial();
            //    audi.ID = 0;
            //    //imei
            //    audi.IMEI = row.Cells[1].Text;
            //    //placa
            //    audi.NroPlaca = row.Cells[2].Text;
            //    audi.FechaGPS = row.Cells[3].Text;
            //    audi.Temperatura = (float)Convert.ToDouble(row.Cells[4].Text);
            //    audi.EstadoGPS = row.Cells[5].Text.Equals("Encendido") ? 1 : 0;
            //    audi.Velocidad = (float)Convert.ToDouble(row.Cells[6].Text);                
            //    audi.Latitud = (float)Convert.ToDouble(row.Cells[8].Text);
            //    audi.Longitud = (float)Convert.ToDouble(row.Cells[9].Text);
            //    audi.EstadoMotor = Convert.ToInt32(row.Cells[10].Text);
            //    audi.VoltajeBateria = (float)Convert.ToDouble(row.Cells[11].Text);
            //    audi.EstadoPuerta = row.Cells[12].Text;
            //    lista.Add(audi);
            //}
            #endregion
            //ReporteAuditoria();
        }

        //public void ReporteAuditoria()
        //{
        //    int i = 0;
        //    List<tramaSerial> lista = new List<tramaSerial>();
        //    foreach (GridViewRow row in gdvListaAudtoria.Rows)
        //    {
        //        CheckBox check = (CheckBox)row.FindControl("ckbauditoria");
        //        if (check.Checked == true)
        //        {
        //            i = i + 1;
        //            if (i == 1)
        //            {
        //                lista = auditoriaCtrl.obtenerAuditoria(row.Cells[2].Text, row.Cells[3].Text, row.Cells[4].Text, row.Cells[5].Text, row.Cells[1].Text);
        //            }
        //            else
        //            {
        //                MensajeAlerta("Por favor, Seleccione una Auditoria");
        //            }
        //        }
        //    }

        //    ReportDocument reporte = new ReportDocument();
        //    reporte.Load(Path.Combine(Server.MapPath("~/Reporte"), "reporteAuditoriaDirecciones.rpt"));
        //    reporte.SetDataSource(lista);
        //    Response.Buffer = false;
        //    Response.ClearHeaders();
        //    Response.ClearContent();
            
        //    try
        //    {
        //        reporte.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, true, "reporteAuditoria_v" + DateTime.Now);
        //        Response.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            var txtfechaini = Request["datepicker1"].ToString();
            var txtfechafin = Request["datepicker2"].ToString();
            ListaAuditoria l_auditoria = new ListaAuditoria();
            l_auditoria.NroPlaca = cboplaca.Text;
            l_auditoria.FechaIni = txtfechaini;
            l_auditoria.HoraIni = cbohorai.Text;
            l_auditoria.FechaFin = txtfechafin;
            l_auditoria.HoraFin = cbohoraf.Text;
            l_auditoria.tipo = cbokm.SelectedValue;
            l_auditoria.valor = txtkmh.Text;

            listaAudit.Add(l_auditoria);
            if (listaAudit.Count > 0)
            {
                gdvListaAudtoria.DataSource = listaAudit;
                gdvListaAudtoria.DataBind();
            }
            else
            {
                listaAudit = new List<ListaAuditoria>();
                gdvListaAudtoria.DataSource = listaAudit;
                gdvListaAudtoria.DataBind();
            }
            upgrilla1.Update();
        }

        protected void btnVer_Click(object sender, EventArgs e)
        {
            //VerPageIndex();
        }

        //private void VerPageIndex()
        //{
        //    int i = 0;
        //    foreach (GridViewRow row in gdvListaAudtoria.Rows)
        //    {
        //        CheckBox check = (CheckBox)row.FindControl("ckbauditoria");
        //        if (check.Checked == true)
        //        {
        //            i = i + 1;
        //            if (i == 1)
        //            {
        //                mostrar(row.Cells[2].Text, row.Cells[3].Text, row.Cells[4].Text, row.Cells[5].Text, row.Cells[1].Text, row.Cells[6].Text, row.Cells[7].Text);
        //                cargarAuditoria(row.Cells[2].Text, row.Cells[3].Text, row.Cells[4].Text, row.Cells[5].Text, row.Cells[1].Text);
        //                upauditoria.Update();
        //                udpobtenercadena.Update();
        //            }
        //            else
        //            {
        //                MensajeAlerta("Por favor, Seleccione una Auditoria");
        //            }
        //        }
        //    }
        //}

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

        public void mostrar(string fechaini, string horaini, string fechafin, string horafin, string placa, string km, string kmh)
        {
            //HttpWebRequest wq = (HttpWebRequest) HttpWebRequest.Create(@"http://whatismyip.org/");
            //HttpWebResponse wr = (HttpWebResponse) wq.GetResponse();
            //StreamReader sr = new StreamReader(wr.GetResponseStream(), System.Text.Encoding.UTF8);            
            //IPAddress ip = IPAddress.Parse(sr.ReadToEnd());
            //sr.Close();            
            //wr.Close();

            //local.WisetrackServices aa = new local.WisetrackServices();
            local.WisetrackServices aa = new local.WisetrackServices();
            //pruebita1.Text = aa.obtenerAuditoria2(txtfechaini.Text, cbohorai.Text, txtfechafin.Text, cbohoraf.Text, cboplaca.SelectedValue, cbokm.SelectedValue, txtkmh.Text);
            pruebita1.Text = aa.obtenerAuditoria2(fechaini, horaini, fechafin, horafin, placa, km, kmh);
        }

        protected void btnpruebita_Click(object sender, EventArgs e)
        {
            //mostrar();
        }

        protected void gdvAuditoria_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gdvAuditoria.PageIndex = e.NewPageIndex;
            //VerPageIndex();
        }

        protected void btnlimpiar_Click(object sender, EventArgs e)
        {

        }

        protected void gdvAuditoria_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                switch (e.Row.Cells[5].Text)
                {
                    case "0":
                        e.Row.Cells[5].Text = "Apagado";
                        break;
                    case "1":
                        e.Row.Cells[5].Text = "Encendido";
                        break;
                }
            }
        }

    }
}