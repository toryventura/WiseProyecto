using Newtonsoft.Json;
using WS.LOGICA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WISETRACK.Controller;
using WISETRACK.Datos;
using WISETRACK.Datos.Serializable;
using WS.DATA;

namespace WISETRACK
{
	public partial class FrmAuditoria : System.Web.UI.Page
	{
		AuditoriaController auditoriaCtrl;
		HomeController homeCtrl;
		VehiculoController vehiculoCtrl;
		SeguimientoController seguimientoCtrl;
		private PersonaController personaCtrl;
		private SeguimientoController controller;
		public static List<ListaAuditoria> listaAudit;
		protected void Page_Load(object sender, EventArgs e)
		{
			auditoriaCtrl = new AuditoriaController();
			homeCtrl = new HomeController();
			vehiculoCtrl = new VehiculoController();
			personaCtrl = new PersonaController();
			controller = new SeguimientoController();
			seguimientoCtrl = new SeguimientoController();

			cbohorai.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
			cbohoraf.Filter = (RadComboBoxFilter)Convert.ToInt32(2);

			if (!IsPostBack)
			{
				listaAudit = new List<ListaAuditoria>();
				if (!SitePrincipal.IsIntruso())
				{
					cargarVehiculo();
					CargarFechas();
				}
				else
				{
					Response.Redirect("~/Account/Login");
				}
			}
		}

		public void CargarFechas()
		{
			txtkmh.Text = "" + 0;
			DateTime fechaActual = DateTime.Now;
			cbohorai.Text = "00:00";
			cbohoraf.Text = "" + (fechaActual.Hour < 10 ? "0" + fechaActual.Hour : "" + fechaActual.Hour)
				+ ":" + (fechaActual.Minute < 10 ? "0" + fechaActual.Minute : "" + fechaActual.Minute);
		}
		[WebMethod]
		public static string ObtenerAuditoria(string _fini, string _ffin, string placa)
		{
			LReportes l = new LReportes();
			DateTime fini = Convert.ToDateTime(_fini);
			DateTime ffin = Convert.ToDateTime(_ffin); ;
			List<Auditoria> list = l.getlistAuditoria(fini, ffin, placa);
			return JsonConvert.SerializeObject(list);
		}
		[WebMethod]
		public string obtenerAuditoriaOptimizado(string fini = "", string hini = "", string ffin = "", string hfin = "", string placa = "", string cod = "", string velocidad = "")
		{
			AuditoriaController auditoriaControl = new AuditoriaController();
			List<sp_obtenerAuditoriaOptimizado_Result> list = auditoriaControl.obtenerAuditoriaOptimizada(fini, hini, ffin, hfin, placa).ToList();

			if (!String.IsNullOrEmpty(velocidad))
			{
				switch (cod)
				{
					case "1":
						//igual
						list = list.Where(det => det.Velocidad == Convert.ToDouble(velocidad)).ToList();
						break;
					case "2":
						//mayor
						list = list.Where(det => det.Velocidad > Convert.ToDouble(velocidad)).ToList();
						break;
					case "3":
						//mayor igual
						list = list.Where(det => det.Velocidad >= Convert.ToDouble(velocidad)).ToList();
						break;
					//default:
					//    break;
				}
			}
			string data = JsonConvert.SerializeObject(list, Formatting.Indented);
			return data;
		}

		//public void cargarVehiculo()
		//{
		//    var user = HttpContext.Current.User.Identity.Name;
		//    string nit = homeCtrl.obtenerNit(user);

		//    if (HttpContext.Current.User.IsInRole("SA"))
		//    {
		//        cboplaca.DataSource = vehiculoCtrl.cargarDetalleVehiculosSA();
		//        cboplaca.DataTextField = "Patente";
		//        cboplaca.DataValueField = "NroPlaca";
		//        cboplaca.DataBind();
		//        cboplaca.Items.Insert(0, "todas");
		//    }
		//    else
		//    {
		//        if (HttpContext.Current.User.IsInRole("SUPERVISOR"))
		//        {
		//            cboplaca.DataSource = personaCtrl.ObtenerVehiculosAsociadosPersonal(user);
		//            cboplaca.DataTextField = "Patente";
		//            cboplaca.DataValueField = "NroPlaca";
		//            cboplaca.DataBind();
		//            cboplaca.Items.Insert(0, "todas");
		//        }
		//        else
		//        {
		//            cboplaca.DataSource = controller.comboVehiculo(nit);
		//            cboplaca.DataTextField = "Patente";
		//            cboplaca.DataValueField = "NroPlaca";
		//            cboplaca.DataBind();
		//            cboplaca.Items.Insert(0, "todas");
		//        }
		//    }
		//}

		public void cargarVehiculo()
		{
			if (SitePrincipal.ExisteActiva())
			{
				if (HttpContext.Current.User.IsInRole("SUPERVISOR"))
				{
					var user = HttpContext.Current.User.Identity.Name;
					cboplaca.DataSource = personaCtrl.ObtenerVehiculosAsociadosPersonal(user);
					cboplaca.DataTextField = "Patente";
					cboplaca.DataValueField = "NroPlaca";
					cboplaca.DataBind();
					//cboplaca.Items.Insert(0, "todas");
				}
				else
				{
					var user = HttpContext.Current.User.Identity.Name;
					string nit1 = homeCtrl.obtenerNit(user);
					cboplaca.DataSource = seguimientoCtrl.comboVehiculo(nit1);
					cboplaca.DataTextField = "Patente";
					cboplaca.DataValueField = "NroPlaca";
					cboplaca.DataBind();
				}
			}
			else
			{
				if (HttpContext.Current.User.IsInRole("SA"))
				{
					cboplaca.DataSource = vehiculoCtrl.cargarDetalleVehiculosSA();
					cboplaca.DataTextField = "Patente";
					cboplaca.DataValueField = "NroPlaca";
					cboplaca.DataBind();
				}
			}
		}

		protected void btnCargar_Click(object sender, EventArgs e)
		{
			var txtfechaini = Request["datepicker1"].ToString();
			var txtfechafin = Request["datepicker2"].ToString();
			ListaAuditoria l_auditoria = new ListaAuditoria
			{
				Patente = cboplaca.Text,
				NroPlaca = cboplaca.SelectedValue.ToString(),
				FechaIni = txtfechaini,
				HoraIni = cbohorai.Text,
				FechaFin = txtfechafin,
				HoraFin = cbohoraf.Text,
				tipo = cbokm.SelectedValue,
				valor = txtkmh.Text
			};

			listaAudit.Add(l_auditoria);
			if (listaAudit.Count > 0)
			{
				gdvListaAudtoria.DataSource = listaAudit;
				gdvListaAudtoria.DataBind();
			}
			else
			{
				listaAudit = new List<ListaAuditoria>();
				gdvListaAudtoria.DataSource = listaAudit;
				gdvListaAudtoria.DataBind();
			}
			upgrilla1.Update();
		}

		[WebMethod]
		public string LimpiarTodo()
		{
			listaAudit = new List<ListaAuditoria>();
			gdvListaAudtoria.DataSource = listaAudit;
			gdvListaAudtoria.DataBind();

			upgrilla1.Update();
			return "OK";
		}

	}
}