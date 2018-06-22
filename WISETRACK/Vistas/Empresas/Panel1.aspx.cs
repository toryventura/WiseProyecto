using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK.Vistas.Empresas
{
    public partial class Panel1 : System.Web.UI.Page
    {
        EmpresaController empresaCtrl;
        string userName;
        string nit;

        protected void Page_Load(object sender, EventArgs e)
        {
            empresaCtrl = new EmpresaController();

            if (!SitePrincipal.IsIntruso())
            {
                if (!User.IsInRole("SA"))
                    Response.Redirect("~/Vistas/Empresas/Index");
                else
                    listarEmpresa();
            }
            else
                Response.Redirect("~/Account/Login");
        }

        public void listarEmpresa()
        {
            userName = HttpContext.Current.User.Identity.Name;
            gdvEmpresa.DataSource = empresaCtrl.GetAll2(userName);
            gdvEmpresa.DataBind();

            CargarEmpresas();
        }

        protected void gdvEmpresa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ingresar_Salir")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string accionActual = ((LinkButton)e.CommandSource).Text;

                userName = HttpContext.Current.User.Identity.Name;
                nit = gdvEmpresa.Rows[index].Cells[0].Text;

                if (accionActual.Equals("INGRESAR"))
                {
                    if (!empresaCtrl.Activar(nit, userName))
                    {
                        string pagRedireccion = SitePrincipal.pagRedireccion;

                        if (!pagRedireccion.Equals("/"))
                        {
                            SitePrincipal.pagRedireccion = "/";
                            Response.Redirect(pagRedireccion);
                        }
                        else
                            Response.Redirect("~/Vistas/Empresas/Index");
                    }
                }
                else
                {
                    if (empresaCtrl.Desactivar(nit, userName))
                        Response.Redirect("~/Vistas/Empresas/Panel");
                }
            }
        }

        private void CargarEmpresas()
        {
            userName = HttpContext.Current.User.Identity.Name;
            var empresas = empresaCtrl.GetActivas(userName);

            if (empresas.Count > 0)
            {
                Empresa empresa = empresas.ElementAt(0);

                foreach (GridViewRow grv in gdvEmpresa.Rows)
                {
                    LinkButton lnkBtn = (LinkButton)grv.FindControl("lkbIngresar");
                    nit = grv.Cells[0].Text;

                    if (empresa.NIT.Equals(nit))
                        //{
                        lnkBtn.Text = "SALIR";
                    //lnkBtn.Enabled = false;
                    //}
                    else
                        //{
                        lnkBtn.Text = "INGRESAR";
                    //lnkBtn.Enabled = true;
                    //}
                }
            }
        }
    }
}