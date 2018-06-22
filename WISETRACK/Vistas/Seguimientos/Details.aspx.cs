using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;

namespace WISETRACK.Vistas.Seguimientos
{
    public partial class Details : System.Web.UI.Page
    {
        GpsController gpsCtrl;
        VehiculoController vehiculoCtrl;
        EmpresaController empresaCtrl;
        SeguimientoController seguimientoCtrl;
        string id = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            gpsCtrl = new GpsController();
            vehiculoCtrl = new VehiculoController();
            empresaCtrl = new EmpresaController();
            seguimientoCtrl = new SeguimientoController();

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    id = Request.QueryString["id"];
                    if (!String.IsNullOrEmpty(id))
                    {
                        lblid.Text = id;
                        var seg = seguimientoCtrl.listar(id);
                        lblfechai.Text = seg.FechaInicio.ToString();
                        lblfechaf.Text = seg.FechaFin.ToString();
                        lblestado.Text = seg.estado.ToString();
                        if (seg.estado == true)
                        {
                            lblestado.Text = "Activo";
                        }
                        else
                        {
                            lblestado.Text = "Inactivo";
                        }
                        lblfechareg.Text = seg.FechaReg.ToString();
                        lblusuareg.Text = seg.UsuaReg;
                        if (!String.IsNullOrEmpty(seg.NIT))
                        {
                            var emp = empresaCtrl.listar(seg.NIT);
                            if (!String.IsNullOrEmpty(emp.NIT))
                            {
                                lblnite.Text = emp.NIT;
                                lblrazonse.Text = emp.RazonSocial;
                                lblemaile.Text = emp.email;
                            }
                        }
                        if (!String.IsNullOrEmpty(seg.IMEI))
                        {
                            var gp = gpsCtrl.listar(seg.IMEI);
                            if (!String.IsNullOrEmpty(gp.IMEI))
                            {
                                lblimeig.Text = gp.IMEI;
                                lblidg.Text = gp.ID;
                                lblmodelog.Text = gp.Modelo;
                            }
                        }
                        if (!String.IsNullOrEmpty(seg.NroPlaca))
                        {
                            var veh = vehiculoCtrl.listar(seg.NroPlaca);
                            if (!String.IsNullOrEmpty(veh.NroPlaca))
                            {
                                lblmodelov.Text = veh.Modelo;
                                lblplacav.Text = veh.NroPlaca;
                                lblaniov.Text = veh.Año.ToString();
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Vistas/Seguimientos/Index");
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            string id = lblid.Text;
            Response.Redirect("/Vistas/Seguimientos/Edit?id=" + id);
        }
    }
}