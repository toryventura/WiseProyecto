using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using WISETRACK.Controller;
using WS.ENTIDADES;

namespace WISETRACK
{
	public partial class FrmZonas1 : System.Web.UI.Page
	{

		//List<sp_ListarPuntosGeo_Result> listPtos;


		protected void Page_Load(object sender, EventArgs e)
		{

			if (!IsPostBack)
			{
				if (!SitePrincipal.IsIntruso())
				{
					//comboZonas();

				}
				else
					Response.Redirect("~/Account/Login");
			}
		}


		[WebMethod]
		public static string getGeocercaAll()
		{
			var user = HttpContext.Current.User.Identity.Name;
			HomeController homeCtrl = new HomeController();
			var nits = homeCtrl.obtenerNit(user);
			ZonasController zn = new ZonasController();
			List<DetalleGeocerca> mlist = new List<DetalleGeocerca>();
			if (!String.IsNullOrEmpty(nits))
			{
				mlist = zn.getGeocercasAll(nits);
			}
			else
			{
				mlist = zn.getGeocercasAll("");
			}
			return JsonConvert.SerializeObject(mlist, Formatting.Indented);
		}



	}
}