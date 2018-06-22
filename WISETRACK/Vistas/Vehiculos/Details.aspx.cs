using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK.Vistas.Vehiculos
{
    public partial class Details : System.Web.UI.Page
    {
        String placa = String.Empty;
        VehiculoController vehiculoCtrl;
        EmpresaController empresaCtrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            vehiculoCtrl = new VehiculoController();
            empresaCtrl= new EmpresaController();

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    placa = Request.QueryString["placa"];
                    if (!String.IsNullOrEmpty(placa))
                    {
                        var ve = vehiculoCtrl.listar(placa);
                        lblplaca.Text = ve.NroPlaca;
                        lblanio.Text = ve.Año.ToString();
                        lblpatente.Text = ve.Patente.ToString();
                        lblchasis.Text = ve.NroChasis.ToString();
                        lblfechareg.Text = ve.FechaReg.ToShortDateString();
                        lblmodelo.Text = ve.Modelo;
                        lblmotor.Text = ve.NroMotor;
                        lblusuarioreg.Text = ve.UsuaReg;
                        //string idempresa = ve.idempresa;
                        //if (!String.IsNullOrEmpty(idempresa))
                        //{
                        //    var empresa = empresaCtrl.listar(idempresa);
                        //    lblnite.Text = empresa.NIT;
                        //    lblrazonse.Text = empresa.RazonSocial;
                        //    lblemaile.Text = empresa.email;
                        //}
                        string nroplaca = ve.NroPlaca;
                        if (!String.IsNullOrEmpty(nroplaca))
                        {
                            //var empresa = empresaCtrl.listar(idempresa);
                            var empresa = empresaCtrl.getEmpresaXNroPlaca(nroplaca);
                            lblnite.Text = empresa.NIT;
                            lblrazonse.Text = empresa.RazonSocial;
                            lblemaile.Text = empresa.email;
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Vistas/Vehiculos/Index");
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
           
        }

    }
}