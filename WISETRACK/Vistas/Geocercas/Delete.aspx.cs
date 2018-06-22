using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;

namespace WISETRACK.Vistas.Geocercas
{
    public partial class Delete : System.Web.UI.Page
    {
        private ZonasController zonasCtrl;
        string id = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            zonasCtrl = new ZonasController();
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    if (SitePrincipal.ExisteActiva())
                    {
                        id = Request.QueryString["id"];
                        if (!String.IsNullOrEmpty(id))
                        {
                            lblcodigoGeo.Text = id;
                            var rg = zonasCtrl.obtenerGeocerca(id);
                            lbldescripcion.Text = rg.Descripcion.ToString();
                            lblcolorlim.Text = rg.ColorLimite.ToString();
                            lblcolorrelleno.Text = rg.ColorRelleno.ToString();
                            lblzona.Text = rg.TipoGeocerca.Descripcion.ToString();
                        }
                        else
                        {
                            Response.Redirect("~/Vistas/Geocercas/Index");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        public void EliminarGeocerca()
        {
            int id = Convert.ToInt32(lblcodigoGeo.Text);
            bool sw = zonasCtrl.EliminarGeocerca(id);
            if (sw)
            {
                MensajeAlerta("Se elimino correctamente");
                Response.Redirect("~/Vistas/Geocercas/Index");
            }
            else
            {
                MensajeAlerta("Por favor, Intente de nuevo");
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarGeocerca();
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