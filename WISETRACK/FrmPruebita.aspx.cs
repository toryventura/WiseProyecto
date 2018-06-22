using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WISETRACK
{
    public partial class FrmPruebita : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnver_Click(object sender, EventArgs e)
        {
            var result = Request["datepicker1"].ToString();
            Response.Write(result);
        }
    }
}