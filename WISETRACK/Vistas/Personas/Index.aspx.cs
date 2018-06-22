using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos.Auxiliar;

namespace WISETRACK.Vistas.Personas
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    CargarGestionPersona();               
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        [WebMethod]
        public static string CargarGestionPersona()
        {
            string result = String.Empty;
            HomeController homeCtrl = new HomeController();
            PersonaController personaCtrl = new PersonaController();
            var user = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(user);

            List<ListarAbmPersona> lista = new List<ListarAbmPersona>();
            if (SitePrincipal.ExisteActiva())
            {
                //Cargar todos los datos de personas filtradas por el NIT
                var detalle_persona_empresa = personaCtrl.ListarPersonalPorEmpresa(nit);
                var filtardetalle = personaCtrl.ListarSoloPersonalSA();
                var detalle_persona_empresault = detalle_persona_empresa.Where(a => !filtardetalle.Select(b => b.CI).Contains(a.CI));
                lista = detalle_persona_empresault.ToList();
                result = JsonConvert.SerializeObject(lista, Formatting.Indented);
			

				
            }
            else
            {
                if (HttpContext.Current.User.IsInRole("SA"))
                {
                    var detalle_persona = personaCtrl.GetAllSA();
                    lista = detalle_persona.ToList();
                    result = JsonConvert.SerializeObject(lista, Formatting.Indented);
                }
            }
            return result;
        }

        //public void cargarDetalleFinal()
        //{
        //    var user = User.Identity.Name;
        //    var nit = homeCtrl.obtenerNit(user);
        //    if (SitePrincipal.ExisteActiva())
        //    {
        //        //Cargar todos los datos de personas filtradas por el NIT
        //        var detalle_persona_empresa = per.ListarPersonalPorEmpresa(nit);
        //        var filtardetalle = per.ListarSoloPersonalSA();
        //        var detalle_persona_empresault = detalle_persona_empresa.Where(a => !filtardetalle.Select(b => b.CI).Contains(a.CI));

        //        this.WebDataGrid1.DataSource = detalle_persona_empresault;
        //        this.WebDataGrid1.DataBind();
        //    }
        //    else
        //    {
        //        if (HttpContext.Current.User.IsInRole("SA"))
        //        {
        //            var detalle_persona = per.GetAllSA();

        //            this.WebDataGrid1.DataSource = detalle_persona;
        //            this.WebDataGrid1.DataBind();
        //        }
        //    }
        //}

        //public void cargarDetalleFinalActivas(string ci)
        //{
        //    var user = User.Identity.Name;
        //    var nit = homeCtrl.obtenerNit(user);
        //    if (SitePrincipal.ExisteActiva())
        //    {
        //        //Cargar todos los datos de personas filtradas por el NIT
        //        var detalle_persona_empresa = per.ListarPersonalPorEmpresa(nit);
        //        detalle_persona_empresa = detalle_persona_empresa.Where(r => r.estado == "Activo").ToList();

        //        var filtardetalle = per.ListarSoloPersonalSA();
        //        var detalle_persona_empresault = detalle_persona_empresa.Where(a => !filtardetalle.Select(b => b.CI).Contains(a.CI));

        //        if (!String.IsNullOrEmpty(ci))
        //        {
        //            detalle_persona_empresault = detalle_persona_empresault.Where(p => p.CI.Contains(ci)).OrderBy(p => p.ApellidoP).ToList();
        //        }
        //        gdvPersona.DataSource = detalle_persona_empresault;
        //        gdvPersona.DataBind();
        //    }
        //    else
        //    {
        //        if (HttpContext.Current.User.IsInRole("SA"))
        //        {
        //            var detalle_persona = per.GetAllSA();
        //            detalle_persona = detalle_persona.Where(e => e.estado == "Activo").ToList();
        //            if (!String.IsNullOrEmpty(ci))
        //            {
        //                detalle_persona = detalle_persona.Where(p => p.CI.Contains(ci)).OrderBy(p => p.ApellidoP).ToList();
        //            }
        //            gdvPersona.DataSource = detalle_persona;
        //            gdvPersona.DataBind();
        //        }
        //    }
        //}

        //protected void gdvPersona_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "VerDetalles")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        GridViewRow row = gdvPersona.Rows[index];
        //        string ci = row.Cells[2].Text;
        //        Response.Redirect("/Vistas/Personas/Details?ci=" + ci);
        //    }

        //    if (e.CommandName == "Editar")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        GridViewRow row = gdvPersona.Rows[index];
        //        string ci = row.Cells[2].Text;
        //        string estado = row.Cells[8].Text;
        //        if (estado == "Activo")
        //        {
        //            Response.Redirect("/Vistas/Personas/Edit?ci=" + ci);
        //        }
        //        else
        //        {
        //            MensajeAlerta("Acceso Restringido: El personal ya ha sido dado de baja");
        //        }
        //    }

        //    if (e.CommandName == "Eliminar")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        GridViewRow row = gdvPersona.Rows[index];
        //        string ci = row.Cells[2].Text;
        //        string estado = row.Cells[8].Text;
        //        if (estado == "Activo")
        //        {
        //            Response.Redirect("/Vistas/Personas/Delete?ci=" + ci);
        //        }
        //        else
        //        {
        //            MensajeAlerta("Acceso Restringido: El personal ya ha sido dado de baja");
        //        }

        //    }
        //}
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

        //protected void btnBuscar_Click(object sender, EventArgs e)
        //{
        //    //cargarDetalleFinal(txtsearchci.Text);
        //}

        //protected void cboestadook_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cboestadook.SelectedValue == "1")
        //    {
        //        cargarDetalleFinalActivas(txtsearchci.Text);
        //    }
        //    else
        //    {
        //        cargarDetalleFinal(txtsearchci.Text);
        //    }
        //}

        //private ICollection GetDataSource()
        //{
        //    var user = User.Identity.Name;
        //    var nit = homeCtrl.obtenerNit(user);
        //    ICollection datasource = null;
        //    if (this.Session["AllBehaviorsDS"] == null)
        //    {
        //        if (SitePrincipal.ExisteActiva())
        //        {
        //            var detalle_persona_empresa = per.ListarPersonalPorEmpresa(nit);
        //            var filtardetalle = per.ListarSoloPersonalSA();
        //            var detalle_persona_empresault = detalle_persona_empresa.Where(a => !filtardetalle.Select(b => b.CI).Contains(a.CI));

        //            var dts = detalle_persona_empresault.ToList();
        //            datasource = dts;
        //            this.Session.Add("AllBehaviorsDS", datasource);
        //        }
        //        else
        //        {
        //            if (HttpContext.Current.User.IsInRole("SA"))
        //            {
        //                var detalle_persona = per.GetAllSA();
        //                datasource = detalle_persona;
        //                this.Session.Add("AllBehaviosDS", datasource);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (SitePrincipal.ExisteActiva())
        //        {
        //            var detalle_persona_empresa = per.ListarPersonalPorEmpresa(nit);
        //            var filtardetalle = per.ListarSoloPersonalSA();
        //            var detalle_persona_empresault = detalle_persona_empresa.Where(a => !filtardetalle.Select(b => b.CI).Contains(a.CI));

        //            var dts = detalle_persona_empresault.ToList();
        //            datasource = dts;
        //            this.Session.Add("AllBehaviorsDS", datasource);
        //        }
        //        else
        //        {
        //            if (HttpContext.Current.User.IsInRole("SA"))
        //            {
        //                var detalle_persona = per.GetAllSA();
        //                datasource = detalle_persona;
        //                this.Session.Add("AllBehaviosDS", datasource);
        //            }
        //        }

        //        //datasource = (ICollection)this.Session["AllBehaviorsDS"];
        //    }
        //    return datasource;
        //}

        //protected void WebDataGrid1_ItemCommand(object sender, HandleCommandEventArgs e)
        //{
        //    if (e.CommandName == "Editar")
        //    {
        //        Object commandArgument = e.CommandArgument;
        //        var ci = commandArgument.ToString();

        //        Response.Redirect("/Vistas/Personas/Edit?ci=" + ci);
        //    }
        //    if (e.CommandName == "Eliminar")
        //    {
        //        Object commandArgument = e.CommandArgument;
        //        var ci = commandArgument.ToString();

        //        Response.Redirect("/Vistas/Personas/Delete?ci=" + ci);
        //    }
        //}

    }
}