using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using WS.DATA;


namespace WS.ENTIDADES
{
	public class Util
	{
		public static string OK = "OK";
		public static string OKMENSAJE = "SE HA REALIZADO LA OPERACION CORRECTAMENTE";
		public static string ERROR = "ERROR";
		public static string ERRORMENSAJE = "NO SE REALIAZO LA OPERACION";
		public static Usuario Usuario
		{
			get
			{
				if (HttpContext.Current != null)
				{
					if (HttpContext.Current.Session != null)
					{
						if (HttpContext.Current.Session["UsuarioSesion"] != null)
						{
							return (Usuario)HttpContext.Current.Session["UsuarioSesion"];
						}
					}
				}
				return null;
			}
			set
			{
				HttpContext.Current.Session["UsuarioSesion"] = value;
			}
		}


		public static string ContrasenaPorDefecto
		{
			get
			{
				try
				{
					string contra = WebConfigurationManager.AppSettings["ContrasenaDefecto"];
					return contra;
				}
				catch
				{
					return "Abc_12345678";
				}
			}
		}

		public static HandleErrorInfo Error(Exception ex)
		{
			string ActualController = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
			string ActualAction = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
			return new HandleErrorInfo(ex, ActualController, ActualAction);
		}

		public static KeyValuePair<string, string> mensaje(string key, string value, bool reemplazarEspeciales = true)
		{
			if (!reemplazarEspeciales)
			{
				return new KeyValuePair<string, string>(key, value);
			}
			else
			{
				return new KeyValuePair<string, string>(key, value.Replace("\"", "").Replace("\\", "").Replace("'", ""));
			}
		}




	}
}