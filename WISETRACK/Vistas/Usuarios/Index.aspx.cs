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
using WISETRACK.Datos;
using WISETRACK.Datos.Auxiliar;

namespace WISETRACK.Usuarios
{
    public partial class Index : System.Web.UI.Page
    {
        private UsuarioController usuarioCtrl;
        private HomeController homeCtrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            usuarioCtrl = new UsuarioController();
            homeCtrl = new HomeController();

            //((WebScriptManager)ScriptManager.GetCurrent(this.Page)).InfragisticsCDN.EnableCDN = DefaultableBoolean.True;
            //this.WebDataGrid1.DataSource = this.GetDataSource();

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    //CargardetalleUsuario();
                }
                else
                {
                    this.Session.Remove("AllBehaviorsDS");
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        [WebMethod]
        public static string getDatos(string data = "")
        {
            string result = String.Empty;
            UsuarioController gpusuariosCtrl = new UsuarioController();
            HomeController homeCtrl = new HomeController();
            List<ListarAbmUsuario> lista = new List<ListarAbmUsuario>();
            if (SitePrincipal.ExisteActiva())
            {
                var userName = HttpContext.Current.User.Identity.Name;
                var nit = homeCtrl.obtenerNit(userName);

                lista = gpusuariosCtrl.ListarUsuarioPorEmpresa(nit);

            }
            else
            {
                if (HttpContext.Current.User.IsInRole("SA"))
                {
                    lista = gpusuariosCtrl.GetAllSA();

                }
            }

            result = JsonConvert.SerializeObject(lista, Formatting.Indented);
            return result;
        }


        //private ICollection GetDataSource()
        //{
        //    var user = User.Identity.Name;
        //    var nit = homeCtrl.obtenerNit(user);
        //    ICollection datasource = null;
        //    if (this.Session["AllBehaviorsDS"] == null)
        //    {
        //        if (SitePrincipal.ExisteActiva())
        //        {
        //            var detalle_usuario_empresa = usuarioCtrl.ListarUsuarioPorEmpresa(nit);
        //            var dts = detalle_usuario_empresa;
        //            datasource = dts;
        //            this.Session.Add("AllBehaviorsDS", datasource);
        //        }
        //        else
        //        {
        //            if (HttpContext.Current.User.IsInRole("SA"))
        //            {
        //                var detalle_usuario = usuarioCtrl.GetAllSA();
        //                datasource = detalle_usuario;
        //                this.Session.Add("AllBehaviosDS", datasource);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (SitePrincipal.ExisteActiva())
        //        {
        //            var detalle_usuario_empresa = usuarioCtrl.ListarUsuarioPorEmpresa(nit);
        //            var dts = detalle_usuario_empresa;
        //            datasource = dts;
        //            this.Session.Add("AllBehaviorsDS", datasource);
        //        }
        //        else
        //        {
        //            if (HttpContext.Current.User.IsInRole("SA"))
        //            {
        //                var detalle_usuario = usuarioCtrl.GetAllSA();
        //                datasource = detalle_usuario;
        //                this.Session.Add("AllBehaviosDS", datasource);
        //            }
        //        }
        //    }
        //    return datasource;

        //}

        //private void CargarUsuarios()
        //{
        //    string userName = HttpContext.Current.User.Identity.Name;
        //    gdvUsuarios.DataSource = usuarioCtrl.GetAll(userName);
        //    gdvUsuarios.DataBind();
        //}

        //public void CargardetalleUsuario()
        //{
        //    var user = User.Identity.Name;
        //    var nit = homeCtrl.obtenerNit(user);

        //    if (SitePrincipal.ExisteActiva())
        //    {
        //        var detalle_usuario_empresa = usuarioCtrl.ListarUsuarioPorEmpresa(nit);
        //        this.WebDataGrid1.DataSource = detalle_usuario_empresa;
        //        this.WebDataGrid1.DataBind();
        //    }
        //    else
        //    {
        //        if (HttpContext.Current.User.IsInRole("SA"))
        //        {
        //            var detalle_usuario = usuarioCtrl.GetAllSA();

        //            this.WebDataGrid1.DataSource = detalle_usuario;
        //            this.WebDataGrid1.DataBind();
        //        }
        //    }
        //}

        ////protected void gdvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Editar")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        GridViewRow row = gdvUsuarios.Rows[index];
        //        string userName = row.Cells[0].Text;

        //        Response.Redirect("/Vistas/Usuarios/Edit?user=" + userName);
        //    }

        //    if (e.CommandName == "Eliminar")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        GridViewRow row = gdvUsuarios.Rows[index];
        //        string userName = row.Cells[0].Text;

        //        Response.Redirect("/Vistas/Usuarios/Delete?user=" + userName);
        //    }
        //}

        //protected void WebDataGrid1_ItemCommand(object sender, HandleCommandEventArgs e)
        //{
        //    if (e.CommandName == "Editar")
        //    {
        //        Object commandArgument = e.CommandArgument;
        //        var userName = commandArgument.ToString();
        //        Response.Redirect("/Vistas/Usuarios/Edit?user=" + userName);
        //    }

        //    if (e.CommandName == "Eliminar")
        //    {
        //        Object commandArgument = e.CommandArgument;
        //        var userName = commandArgument.ToString();
        //        Response.Redirect("/Vistas/Usuarios/Delete?user=" + userName);
        //    }
        //}
    }
}