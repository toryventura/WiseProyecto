using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;

namespace WISETRACK.Vistas.TipoGeocerca
{
    public partial class Edit : System.Web.UI.Page
    {
        private HomeController homeCtrl;
        private ZonasController zonasCtrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            homeCtrl = new HomeController();
            zonasCtrl = new ZonasController();

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    if (SitePrincipal.ExisteActiva())
                    {
                        string CodTipoGEO = Request.QueryString["CodTipoGEO"];
                        if (!String.IsNullOrEmpty(CodTipoGEO))
                        {
                            int cod = Convert.ToInt32(CodTipoGEO);
                            txtID.Text = CodTipoGEO;
                            var tipogeocerca = zonasCtrl.ObtenerTipoGeocercaID(cod);
                            Descripcion.Text = tipogeocerca.Descripcion;
                        }
                        else
                        {
                            Response.Redirect("~/Vistas/TipoGeocerca/Index");
                        }
                    }
                    else
                    {
                        SitePrincipal.pagRedireccion = "~/Vistas/TipoGeocerca/Index";
                        Response.Redirect("~/Vistas/Empresas/Panel");
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
            string descrip = Descripcion.Text;
            int id = Convert.ToInt32(txtID.Text);

            bool sw = zonasCtrl.EditarTipoGeocerca(descrip, id);
            if (sw)
            {
                MensajeAlerta("Se modifico correctamente");
                Response.Redirect("/Vistas/TipoGeocerca/Index");
            }
            else
            {
                MensajeAlerta("Datos invalidos");
            }
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