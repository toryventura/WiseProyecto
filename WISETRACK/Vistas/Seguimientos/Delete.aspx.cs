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
    public partial class Delete : System.Web.UI.Page
    {
        SeguimientoController seguimientoCtrl;
        string id = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            seguimientoCtrl = new SeguimientoController();
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    if (SitePrincipal.ExisteActiva())
                    {
                        id = Request.QueryString["id"];
                        var se = seguimientoCtrl.listar(id);
                        lblid.Text = se.CodSeguimiento.ToString();
                        lblestado.Text = (se.estado.Value) ? "Activo" : "Inactivo";
                        lblfechai.Text = se.FechaInicio.ToString();
                        lblfechaf.Text = se.FechaFin.ToString();
                        lblgps.Text = se.IMEI.ToString();
                        lblplaca.Text = se.NroPlaca.ToString();
                    }
                    else
                    {
                        SitePrincipal.pagRedireccion = "~/Vistas/Seguimiento/Index";
                        SitePrincipal.countRedireccion = 0;
                        Response.Redirect("~/Vistas/Empresas/Panel");
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //Depurar en 30 dias
            //seguimientoCtrl.remove(lblid.Text);
            //Response.Redirect("/Vistas/Seguimientos/Index");
            var user = HttpContext.Current.User.Identity.Name;
			var se = new Seguimiento
			{
				CodSeguimiento = Convert.ToInt32(lblid.Text),
				FechaInicio = Convert.ToDateTime(lblfechai.Text),
				estado = false,
				FechaFin = DateTime.Now,
				IMEI = lblgps.Text,
				NroPlaca = lblplaca.Text,
				UsuaModif = user,
				FechaModif = DateTime.Now
			};

			bool sx = seguimientoCtrl.update(se);
            if (sx == true)
            {
                MensajeAlerta("Se modifico correctamente");
                Response.Redirect("/Vistas/Seguimientos/Index");
            }
            else
            {
                MensajeAlerta("Intente de nuevo");
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