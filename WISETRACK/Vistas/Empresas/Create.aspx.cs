using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK
{
    public partial class Create : System.Web.UI.Page
    {
        private EmpresaController emp;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            emp = new EmpresaController();
            if (!IsPostBack)
            {
                if(SitePrincipal.IsIntruso())
                    Response.Redirect("~/Account/Login");
                
            }

        }

        public void add()
        {
            var user = HttpContext.Current.User.Identity.Name;
			Empresa empresa = new Empresa
			{
				NIT = txtNit.Text,
				RazonSocial = txtRazon_social.Text,
				email = txtEmail.Text,
				Contacto = txtContacto.Text,
				emailContacto = txtEmailcontact.Text,
				UsuaReg = user,
				FechaReg = DateTime.Now
			};
			bool sx = emp.createEmpresa(empresa, user);
            if (sx == true)
            {
                MensajeAlerta("Se registro correctamente");
                Response.Redirect("~/Vistas/Empresas/Panel");
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            add();
        }
    }
}