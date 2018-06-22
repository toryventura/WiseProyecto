using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Datos;

namespace WISETRACK
{
    public partial class FrmReporte : System.Web.UI.Page
    {
        WISETRACKEntities db = new WISETRACKEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SitePrincipal.IsIntruso())
            {
                var query = from s in db.UsuarioEmpresa select s;
                grdCustomer.DataSource = query.ToList();
                grdCustomer.DataBind();
            }
            else
                Response.Redirect("~/Account/Login");
        }
    }
}