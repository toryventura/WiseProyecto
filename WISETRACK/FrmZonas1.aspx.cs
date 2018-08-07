using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using WISETRACK.Controller;
using WISETRACK.Datos.Serializable;
using WS.ENTIDADES;

namespace WISETRACK
{
	public partial class FrmZonas1 : System.Web.UI.Page
	{

		//List<sp_ListarPuntosGeo_Result> listPtos;

		ZonasController zonasControl = new ZonasController();
		HomeController homeCtrl;
		protected void Page_Load(object sender, EventArgs e)
		{
			homeCtrl = new HomeController();
			if (!IsPostBack)
			{
				if (!SitePrincipal.IsIntruso())
				{
					comboZonas();

				}
				else
					Response.Redirect("~/Account/Login");
			}
		}

		private void comboZonas()
		{

			var user = HttpContext.Current.User.Identity.Name;
			var nit = homeCtrl.obtenerNit(user);

			cbozonas.DataSource = zonasControl.comboZonas(nit);
			cbozonas.DataTextField = "Descripcion";
			cbozonas.DataValueField = "CodTipoGEO";
			cbozonas.DataBind();
		}

		[WebMethod]
		public static string getGeocercaAll()
		{
			var user = HttpContext.Current.User.Identity.Name;
			HomeController homeCtrl = new HomeController();
			var nits = homeCtrl.obtenerNit(user);
			ZonasController zn = new ZonasController();
			List<Geocerca> mlist = new List<Geocerca>();
			if (!String.IsNullOrEmpty(nits))
			{
				mlist = zn.getGeocercasAllM(nits);
			}
			else
			{
				mlist = zn.getGeocercasAllM("");
			}
			return JsonConvert.SerializeObject(mlist, Formatting.Indented);
		}
		[WebMethod]
		public static string GetPuntos(int idgeocerca)
		{
			ZonasController zn = new ZonasController();
			WS.ENTIDADES.Geocerca geocerca = zn.GetPuntos(idgeocerca);
			return JsonConvert.SerializeObject(geocerca, Formatting.Indented); ;
		}

		[WebMethod]
		public static string AddUpdate(geocercaSerial geocerca, List<puntosgeoSerial> puntosGeocercas, int accion)
		{
			var user = HttpContext.Current.User.Identity.Name;
			HomeController homeCtrl = new HomeController();
			var nits = homeCtrl.obtenerNit(user);
			ZonasController zn = new ZonasController();
			switch (accion)
			{
				case 0:
					//aqui hacemos el codigo para guardar
					zn.AddGeocerca(geocerca, puntosGeocercas);
					break;
				case 1:
					zn.UpdateGeocerca(geocerca, puntosGeocercas);
					//aqui hacemos el codigo para actulizar 
					break;
				default:
					break;
			}
			return JsonConvert.SerializeObject("", Formatting.Indented);
		}


	}
}