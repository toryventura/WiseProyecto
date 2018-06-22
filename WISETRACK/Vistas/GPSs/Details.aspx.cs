using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK.Vistas.GPSs
{
    public partial class Details : System.Web.UI.Page
    {
        GpsController gpsCtrl;
        EmpresaController empresaCtrl;
        string imei = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            gpsCtrl = new GpsController();
            empresaCtrl = new EmpresaController();
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    imei = Request.QueryString["imei"];
                    if (!String.IsNullOrEmpty(imei))
                    {
                        lblimei.Text = imei;
                        GPS g = gpsCtrl.listar(imei);
                        lblid.Text = g.ID;
                        lblmodelo.Text = g.Modelo;
                        lbltelefono.Text = g.NroTelefono.ToString();
                        lblusuarioreg.Text = g.UsuaReg.ToString();
                        lblfechareg.Text = g.FechaReg.ToString();
                    }
                    else
                    {
                        Response.Redirect("~/Vistas/GPSs/Index");
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
            string imei = lblimei.Text;
            Response.Redirect("/Vistas/GPSs/Edit?imei=" + imei);
        }

    }
}