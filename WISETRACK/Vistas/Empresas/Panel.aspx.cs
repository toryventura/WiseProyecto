using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WISETRACK.Controller;
using WISETRACK.Models;

namespace WISETRACK.Vistas.Empresas
{
	public partial class Panel : System.Web.UI.Page
	{
		EmpresaController empresaCtrl;


		protected void Page_Load(object sender, EventArgs e)
		{
			empresaCtrl = new EmpresaController();

			if (!SitePrincipal.IsIntruso())
			{
				if (!User.IsInRole("SA"))
					Response.Redirect("~/Vistas/Empresas/Index");
				//else
				//    listarEmpresa();
			}
			else
				Response.Redirect("~/Account/Login");
		}
		[WebMethod]
		public static string getDatos(string data = "")
		{
			string result = String.Empty;
			EmpresaController empresaCtrl = new EmpresaController();
			//HomeController homeCtrl = new HomeController();
			List<EmpresaModel> lista = new List<EmpresaModel>();
			//if (SitePrincipal.ExisteActiva())
			//{
			var userName = HttpContext.Current.User.Identity.Name;
			userName = HttpContext.Current.User.Identity.Name;
			lista = empresaCtrl.GetAll22(userName);

			//}
			//else
			//{
			//    //if (HttpContext.Current.User.IsInRole("SA"))
			//    //{
			//    //    lista = alarmaCtrl.GetAll();

			//    //}
			//}

			result = JsonConvert.SerializeObject(lista, Formatting.Indented);

			return result;
		}


		[WebMethod]
		public static string CargarEmpresas(string data = "")
		{
			var userName = HttpContext.Current.User.Identity.Name;
			EmpresaController empresaCtrl = new EmpresaController();
			var empresas = empresaCtrl.GetActivas(userName);

			return empresas.Count > 0 ? empresas.FirstOrDefault().NIT : "-1";

		}


		[WebMethod]
		public static string ActivarEmpresa(string nt = "", int i = -1)
		{
			var userName = HttpContext.Current.User.Identity.Name;
			EmpresaController empresaCtrl = new EmpresaController();
			var empresas = empresaCtrl.GetActivas(userName);
			var url = "";

			if (i == 0 || i == 1)
			{
				//int index = Convert.ToInt32(e.CommandArgument);
				//string accionActual = ((LinkButton)e.CommandSource).Text;

				userName = HttpContext.Current.User.Identity.Name;
				//nit = gdvEmpresa.Rows[index].Cells[0].Text;

				if (i == 0)// si es 0 no es tiene ninguna empresa activa
				{
					if (!empresaCtrl.Activar(nt, userName))
					{
						string pagRedireccion = SitePrincipal.pagRedireccion;

						if (!pagRedireccion.Equals("/"))
						{
							SitePrincipal.pagRedireccion = "/";
							//   Response.Redirect(pagRedireccion);
							var urls = pagRedireccion.Replace("~", "");
							url = urls;

						}
						else
							url = "/Vistas/Empresas/Index";
						//Response.Redirect("~/Vistas/Empresas/Index");
					}
				}
				else
				{
					if (empresaCtrl.Desactivar(nt, userName))
						url = "/Vistas/Empresas/Panel";

					//Response.Redirect("~/Vistas/Empresas/Panel");
				}
			}
			return url;

		}
		//public void listarEmpresa()
		//{
		//    userName = HttpContext.Current.User.Identity.Name;
		//    gdvEmpresa.DataSource = empresaCtrl.GetAll2(userName);
		//    gdvEmpresa.DataBind();

		//    CargarEmpresas();
		//}

		//protected void gdvEmpresa_RowCommand(object sender, GridViewCommandEventArgs e)
		//{
		//    if(e.CommandName == "Ingresar_Salir")
		//    {
		//        int index = Convert.ToInt32(e.CommandArgument);
		//        string accionActual = ((LinkButton)e.CommandSource).Text;

		//        userName = HttpContext.Current.User.Identity.Name;
		//        nit = gdvEmpresa.Rows[index].Cells[0].Text;

		//        if (accionActual.Equals("INGRESAR"))
		//        {
		//            if (!empresaCtrl.Activar(nit, userName))
		//            {
		//                string pagRedireccion = SitePrincipal.pagRedireccion;

		//                if (!pagRedireccion.Equals("/"))
		//                {
		//                    SitePrincipal.pagRedireccion = "/";
		//                    Response.Redirect(pagRedireccion);
		//                }
		//                else
		//                    Response.Redirect("~/Vistas/Empresas/Index");
		//            }
		//        }
		//        else
		//        {
		//            if(empresaCtrl.Desactivar(nit, userName))
		//                Response.Redirect("~/Vistas/Empresas/Panel");
		//        }
		//    }
		//}
	}
}