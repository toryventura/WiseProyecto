using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;

namespace WISETRACK.Vistas.Privilegios
{
    public partial class Edit : System.Web.UI.Page
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
            txbCodigo.Text = codigo;

            privilegio = privilegioCtrl.Get(Convert.ToInt32(codigo));
            
            txbDescripcion.Text = privilegio.Descripcion;
            txbDirPagina.Text = privilegio.DirPagina;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            codigo = Request.QueryString["cod"];
            privilegio = privilegioCtrl.Get(Convert.ToInt32(codigo));

            try
            {
                string descripcion = txbDescripcion.Text;
                string dirPagina = txbDirPagina.Text;

                privilegioCtrl.Actualizar(privilegio, descripcion, dirPagina);
                Response.Redirect("~/Vistas/Privilegios/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
        }
    }
}