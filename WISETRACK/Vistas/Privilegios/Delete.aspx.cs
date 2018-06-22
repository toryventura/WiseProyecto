using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;

namespace WISETRACK.Vistas.Privilegios
{
    public partial class Delete : System.Web.UI.Page
    {
        PrivilegioController privilegioCtrl;
        Datos.Privilegios privilegio;
        string codigo;

        protected void Page_Load(object sender, EventArgs e)
        {
            privilegioCtrl = new PrivilegioController();

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                    CargarPrivilegio();
                else
                    Response.Redirect("~/Account/Login");
            }
        }

        private void CargarPrivilegio()
        {
            codigo = Request.QueryString["cod"];
            lblCodigo.Text = codigo;

            privilegio = privilegioCtrl.Get(Convert.ToInt32(codigo));
            lblDescripcion.Text = privilegio.Descripcion;
            lblDirPagina.Text = privilegio.DirPagina;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            codigo = Request.QueryString["cod"];
            privilegio = privilegioCtrl.Get(Convert.ToInt32(codigo));

            try
            {
                privilegioCtrl.Eliminar(privilegio);
                Response.Redirect("~/Vistas/Privilegios/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
        }

    }
}