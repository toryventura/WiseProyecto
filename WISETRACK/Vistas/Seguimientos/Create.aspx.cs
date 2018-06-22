using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK.Vistas.Seguimientos
{
    public partial class Create : System.Web.UI.Page
    {
        SeguimientoController seguimientoCtrl;
        HomeController homeCtrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            seguimientoCtrl = new SeguimientoController();
            homeCtrl = new HomeController();
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    if (!SitePrincipal.ExisteActiva())
                    {
                        SitePrincipal.pagRedireccion = "~/Vistas/GPSs/Create";
                        Response.Redirect("~/Vistas/Empresas/Panel");
                    }
                    else
                    {
                        ListarIMEI();
                        ListarPlaca();
                        chkestado.Checked = true;
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var user = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(user);

            var fechaI = Request["datepicker1"].ToString();

            var seg = new Seguimiento();
            seg.estado = chkestado.Checked;
            seg.FechaInicio = Convert.ToDateTime(fechaI);
            seg.FechaReg = DateTime.Now;
            seg.IMEI = cboimei.SelectedValue;
            seg.NIT = nit;
            seg.NroPlaca = cboplaca.SelectedValue;
            seg.UsuaReg = user;

            bool sx = seguimientoCtrl.Add(seg);
            if (sx == true)
            {
                MensajeAlerta("Se registro satisfactoriamente");
                Response.Redirect("/Vistas/Seguimientos/Index");
            }
            else
            {
                MensajeAlerta("Datos invalidos");
            }
        }

        public void ListarIMEI()
        {
            var user = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(user);
            cboimei.DataSource = seguimientoCtrl.listarIMEI(nit);
            cboimei.DataTextField = "IMEI";
            cboimei.DataValueField = "IMEI";
            cboimei.DataBind();
        }

        public void ListarPlaca()
        {
            var user = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(user);

            cboplaca.DataSource = seguimientoCtrl.listarPlaca(nit);
            cboplaca.DataTextField = "NroPlaca";
            cboplaca.DataValueField = "NroPlaca";
            cboplaca.DataBind();
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

    }
}