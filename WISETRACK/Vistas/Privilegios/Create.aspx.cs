using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK.Privilegios
{
    public partial class Create : System.Web.UI.Page
    {
        PrivilegioController privilegioCtrl;
        Datos.Privilegios privilegio;

        protected void Page_Load(object sender, EventArgs e)
        {
            privilegioCtrl = new PrivilegioController();

            if(SitePrincipal.IsIntruso())
                Response.Redirect("~/Account/Login");
        }

        protected void CrearPrivilegio_Click(object sender, EventArgs e)
        {
            try
            {
                string dirPagina = DirPagina.Text;

                privilegio = privilegioCtrl.Get(dirPagina);

                if (privilegio == null)
                {
                    if (privilegioCtrl.IsValidaDirPagina(dirPagina))
                    {
                        string descripcion = Descripcion.Text;
                        string userName = HttpContext.Current.User.Identity.Name;

                        privilegioCtrl.Add(descripcion, dirPagina, userName);

                        Response.Redirect("~/Vistas/Privilegios/Index");
                    }
                    else
                        ErrorMessage.Text = "La Dirección de Página no es valida";

                }
                else
                    ErrorMessage.Text = "Ya existe un Privilegio con esta Dirección de Página";
                
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
        }
    }
}