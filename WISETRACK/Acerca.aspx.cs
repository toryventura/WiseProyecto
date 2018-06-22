using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Models;

namespace WISETRACK
{
    public partial class Acerca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SitePrincipal.IsIntruso())
                Response.Redirect("~/Account/Login");
        }

    }
}