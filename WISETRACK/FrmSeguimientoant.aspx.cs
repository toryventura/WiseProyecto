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
    public partial class FrmSeguimiento : System.Web.UI.Page
    {
        private SeguimientoController controler;
        private HomeController homeControl;
        private VehiculoController vehiculoCtrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            cboplaca.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
            controler = new SeguimientoController();
            homeControl = new HomeController();
            vehiculoCtrl = new VehiculoController();

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    cargarVehiculo();
                    grillaSeguimientoLoad("todas");
                    ModificarEstadoGrilla();
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        public void ModificarEstadoGrilla()
        {
            foreach (GridViewRow row in gdvSeguimiento.Rows)
            {
                //CheckBox check = (CheckBox)row.FindControl("ckbseguimiento");
                ImageButton image = (ImageButton)row.FindControl("btnimage");
                string estado = row.Cells[3].Text;
                DateTime fechagps = Convert.ToDateTime(row.Cells[8].Text);
                DateTime fechaActual = DateTime.Now;
                TimeSpan duracion = fechaActual - fechagps;
                double minutosTotales = duracion.TotalMinutes;

                if (minutosTotales < 30)
                {
                    switch (estado)
                    {
                        case "11":
                            image.ImageUrl = "~/Content/img/tools/azul.jpg";
                            //EstadoMotorValue = "Ignicion en Descanso";
                            //url = url + "/azul/";
                            break;
                        case "12":
                            image.ImageUrl = "~/Content/img/tools/verde.jpg";
                            //EstadoMotorValue = "Ignicion en Movimiento";
                            //url = url + "/verde/";
                            break;
                        case "21":
                            //    EstadoMotorValue = "Designicion en Descanso";
                            //    url = url + "/azul/";
                            image.ImageUrl = "~/Content/img/tools/azul.jpg";
                            break;
                        case "22":
                            //    EstadoMotorValue = "Designicion en Movimiento";
                            //    url = url + "/celeste/";
                            image.ImageUrl = "~/Content/img/tools/celeste.jpg";
                            break;
                        case "41":
                            //    EstadoMotorValue = "Descanso con motor Encendido";
                            //    url = url + "/verde/";
                            image.ImageUrl = "~/Content/img/tools/verde.jpg";
                            break;
                        case "42":
                            //EstadoMotorValue = "Movimiento con Motor Encendido";
                            //url = url + "/verde/";
                            image.ImageUrl = "~/Content/img/tools/verde.jpg";
                            break;
                        case "1":
                            image.ImageUrl = "~/Content/img/tools/verde.jpg";
                            break;
                        case "0":
                            image.ImageUrl = "~/Content/img/tools/azul.jpg";
                            break;
                        default:
                            //    EstadoMotorValue = "Revisar";
                            //    url = url + "/coral/";
                            image.ImageUrl = "~/Content/img/tools/coral.jpg";
                            break;
                    }
                }
                else
                {
                    if ((minutosTotales > 30) && (minutosTotales < 60))
                    {
                        image.ImageUrl = "~/Content/img/tools/amarillo.jpg";
                    }
                    else
                    {
                        image.ImageUrl = "~/Content/img/tools/rojo.jpg";
                    }
                }
            }
        }

        public void cargarVehiculo()
        {
            if (HttpContext.Current.User.IsInRole("SA"))
            {
                cboplaca.DataSource = vehiculoCtrl.cargarDetalleVehiculosSA();
                cboplaca.DataTextField = "NroPlaca";
                cboplaca.DataValueField = "NroPlaca";
                cboplaca.DataBind();
                cboplaca.Items.Insert(0, "todas");
            }
            else
            {
                var user = HttpContext.Current.User.Identity.Name;
                string nit = homeControl.obtenerNit(user);
                cboplaca.DataSource = controler.comboVehiculo(nit);
                cboplaca.DataTextField = "NroPlaca";
                cboplaca.DataValueField = "NroPlaca";
                cboplaca.DataBind();
                cboplaca.Items.Insert(0, "todas");
            }
        }

        public void grillaSeguimiento()
        {
            System.Threading.Thread.Sleep(3000);
            string cod = cboplaca.SelectedValue;
            if (String.IsNullOrEmpty(cod))
            {
                cod = cboplaca.Text;
            }
            var user = HttpContext.Current.User.Identity.Name;
            string nit = homeControl.obtenerNit(user);
            if (!cod.Equals(""))
            {
                switch (cod)
                {
                    case "todas":
                        if (HttpContext.Current.User.IsInRole("SA"))
                        {
                            gdvSeguimiento.DataSource = controler.listar_posicion_all();
                            gdvSeguimiento.DataBind();
                        }
                        else
                        {
                            gdvSeguimiento.DataSource = controler.listar_posicion_all(nit);
                            gdvSeguimiento.DataBind();
                        }
                        break;
                    default:
                        if (HttpContext.Current.User.IsInRole("SA"))
                        {
                            gdvSeguimiento.DataSource = controler.cargar_grilla_ultima_posicion_placa(cod);
                            gdvSeguimiento.DataBind();
                        }
                        else
                        {
                            gdvSeguimiento.DataSource = controler.cargar_grilla_ultima_posicion_placa(cod);
                            gdvSeguimiento.DataBind();
                        }
                        break;
                }
            }
            upseguimiento.Update();
        }

        public void grillaSeguimientoLoad(string cadena)
        {
            var user = HttpContext.Current.User.Identity.Name;
            string nit = homeControl.obtenerNit(user);
            if (!cadena.Equals(""))
            {
                switch (cadena)
                {
                    case "todas":
                        if (HttpContext.Current.User.IsInRole("SA"))
                        {
                            gdvSeguimiento.DataSource = controler.listar_posicion_all();
                            gdvSeguimiento.DataBind();
                        }
                        else
                        {
                            gdvSeguimiento.DataSource = controler.listar_posicion_all(nit);
                            gdvSeguimiento.DataBind();
                        }
                        break;
                }
            }
            upseguimiento.Update();
        }

        protected void cboplaca_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = string.Concat(e.Item.Text.Split(' ')[0], "");
        }

        protected void cboplaca_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grillaSeguimiento();
            ModificarEstadoGrilla();
            upseguimiento.Update();
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            List<TempSerial> li = new List<TempSerial>();
            foreach (GridViewRow row in gdvSeguimiento.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("ckbseguimiento");
                if (check.Checked == true)
                {
                    TempSerial rptSeguimiento = new TempSerial();
                    rptSeguimiento.EstadoMotor = Convert.ToInt32(row.Cells[3].Text);
                    rptSeguimiento.NroPlaca = row.Cells[4].Text;
                    rptSeguimiento.Velocidad = (float)Convert.ToDouble(row.Cells[5].Text);
                    rptSeguimiento.Latitud = (float)Convert.ToDouble(row.Cells[6].Text);
                    rptSeguimiento.Longitud = (float)Convert.ToDouble(row.Cells[7].Text);
                    rptSeguimiento.FechaGPS = row.Cells[8].Text;
                    string strestado = row.Cells[9].Text;
                    rptSeguimiento.EstadoGPS = (strestado.Equals("Encendido")) ? 1 : 0;
                    rptSeguimiento.Temperatura = (float)Convert.ToDouble(row.Cells[10].Text);
                    rptSeguimiento.EstadoPuerta = row.Cells[11].Text;
                    rptSeguimiento.VoltajeBateria = (float)Convert.ToDouble(row.Cells[12].Text);
                    if (row.Cells[13].Text != "&nbsp;")
                    {
                        rptSeguimiento.Nombre = row.Cells[13].Text;
                    }
                    else
                    {
                        rptSeguimiento.Nombre = "No Asignado";
                    }

                    
                    li.Add(rptSeguimiento);
                }
            }
            ReporteSeguimiento(li);
        }

        public void ReporteSeguimiento(List<TempSerial> lista)
        {
            lista = lista.OrderByDescending(p => p.FechaGPS).ToList();
            ReportDocument reporte = new ReportDocument();
            reporte.Load(Path.Combine(Server.MapPath("~/Reporte"), "reporteSeguimientoDirecciones.rpt"));
            reporte.SetDataSource(lista);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                reporte.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "reporteSeguimiento" + DateTime.Now);
                Response.End();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void ckbsegclick_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)gdvSeguimiento.HeaderRow.FindControl("ckbsegclick");
            foreach (GridViewRow row in gdvSeguimiento.Rows)
            {
                CheckBox chkrow = (CheckBox)row.FindControl("ckbseguimiento");
                if (check.Checked == true)
                {
                    chkrow.Checked = true;
                }
                else
                {
                    chkrow.Checked = false;
                }
            }
        }

        protected void gdvSeguimiento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvSeguimiento.PageIndex = e.NewPageIndex;
            grillaSeguimiento();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            grillaSeguimiento();
            ModificarEstadoGrilla();
        }

        protected void gdvSeguimiento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                switch (e.Row.Cells[9].Text)
                {
                    case "0":
                        e.Row.Cells[9].Text = "Apagado";
                        break;
                    case "1":
                        e.Row.Cells[9].Text = "Encendido";
                        break;
                }

                switch (e.Row.Cells[11].Text)
                {
                    case "False":
                        e.Row.Cells[11].Text = "Abierto";
                        break;
                    case "True":
                        e.Row.Cells[11].Text = "Cerrado";
                        break;
                }
            }
        }

    }
}