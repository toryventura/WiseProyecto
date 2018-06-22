using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Models;

namespace WISETRACK
{
	public partial class SitePrincipal : System.Web.UI.MasterPage
	{
		private const int PRV_CONFIGURACION = 4;
		private const int PRV_PRIVILEGIOS = 5;
		private const int PRV_ROLES = 9;
		private const int PRV_USUARIOS = 11;
		private const int PRV_ADMINISTRACION = 17;
		private const int PRV_EMPRESA = 18;
		private const int PRV_PERSONAS = 21;
		private const int PRV_ALARMAS = 23;
		private const int PRV_ZONAS = 25;
		private const int PRV_MOVILES = 26;
		private const int PRV_SEGUIMIENTO = 27;
		private const int PRV_AUDITORIA = 28;
		private const int PRV_VEHICULOS = 29;
		private const int PRV_GPS = 31;
		private const int PRV_REPORTES = 33;
		private const int PRV_TEMPERATURA = 34;
		private const int PRV_DETENCIONES = 35;
		private const int PRV_VELOCIDAD_MAX = 36;
		private const int PRV_ALERTAS = 55;
		private const int PRV_APERTURA_CIERRE = 56;
		private const int PRV_TIPOS_ZONA = 58;
		private const int PRV_ENTRADA_SALIDA = 60;
		private const int PRV_KILOMETRAJE = 61;
		private const int PRV_RPTAUDITORIA = 66;
		private const int PRV_ASIGNACION_USUARIO_VEHICULO = 67;
		private const int PRV_IDButton = 71;
		private const int PRV_REncendidoApagado = 76;
		private const int PRV_RPTCONSOLIDADO = 77;


		public static bool ingreso = false;
		public static string pagRedireccion = "/";
		public static int countRedireccion = 0;

		private static string userName;
		private static string idRol;

		public static PrivilegioController privilegioCtrl;
		public static EmpresaController empresaCtrl;

		UsuarioController usuarioCtrl;
		RolController rolCtrl;

		protected void Page_Load(object sender, EventArgs e)
		{
			usuarioCtrl = new UsuarioController();
			rolCtrl = new RolController();
			empresaCtrl = new EmpresaController();

			CargarPrivilegios();
			Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetNoStore();
			var list = HttpContext.Current.Cache;
			foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache)
			{
				HttpContext.Current.Cache.Remove((string)entry.Key);
			}

		}

		public static bool IsIntruso()
		{
			userName = HttpContext.Current.User.Identity.Name;

			if (!(userName.Equals("") || userName == null))
			{
				var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
				var user = um.FindByName(userName);

				idRol = user.Roles.SingleOrDefault().RoleId;

				string dirPagina = HttpContext.Current.Request.FilePath;

				privilegioCtrl = new PrivilegioController();
				var privilegio = privilegioCtrl.Get(dirPagina, idRol);

				if (privilegio != null)
					return false;
			}

			return true;
		}

		public static bool ExisteActiva()
		{
			if (empresaCtrl == null)
				empresaCtrl = new EmpresaController();

			return empresaCtrl.GetActivas(userName).Count > 0;
		}

		private void CargarPrivilegios()
		{
			userName = HttpContext.Current.User.Identity.Name;

			if (countRedireccion > 1)
				pagRedireccion = "/";

			countRedireccion++;

			ingreso = false;
			var empActivas = empresaCtrl.GetActivas(userName);

			if (empActivas.Count > 0)
			{
				ingreso = true;

				string empresaActiva = empActivas.ElementAt(0).RazonSocial;
				lblmensaje.Text = empresaActiva + " (" + userName + ")";
			}
			else
			{
				if (HttpContext.Current.User.IsInRole("SA"))
				{
					ingreso = true;
					lblmensaje.Text = "(" + userName + ")";
				}
				else
				{
					string empresaActiva = empresaCtrl.EmpresaActivada(userName);

					if (!empresaActiva.Equals(""))
					{
						ingreso = true;
						lblmensaje.Text = empresaActiva + " (" + userName + ")";
					}
					else
						lblmensaje.Text = "(" + userName + ")";
				}
			}

			var privilegios = rolCtrl.GetPrivilegios2(idRol);

			if (privilegios.Count > 0)
			{
				foreach (var privilegio in privilegios)
				{
					int codigo = privilegio.CodPrivilegio;

					switch (codigo)
					{
						case PRV_CONFIGURACION:
							if (ingreso)
								prvConfig.Visible = true;
							break;
						case PRV_USUARIOS:
							prvUsuarios.Visible = true;
							break;
						case PRV_ROLES:
							prvRoles.Visible = true;
							break;
						case PRV_PRIVILEGIOS:
							prvPrivilegios.Visible = true;
							break;
						case PRV_ADMINISTRACION:
							if (ingreso)
								prvAdmin.Visible = true;
							break;
						case PRV_EMPRESA:
							prvEmpresa.Visible = true;
							break;
						case PRV_PERSONAS:
							prvPersonas.Visible = true;
							break;
						case PRV_ALARMAS:
							prvAlarmas.Visible = true;
							break;
						case PRV_ZONAS:
							prvZonas.Visible = true;
							break;
						case PRV_TIPOS_ZONA:
							prvTiposZona.Visible = true;
							break;
						case PRV_MOVILES:
							if (ingreso)
								prvMoviles.Visible = true;
							break;
						case PRV_SEGUIMIENTO:
							prvSeguimiento.Visible = true;
							break;
						case PRV_AUDITORIA:
							prvAuditoria.Visible = true;
							break;
						case PRV_VEHICULOS:
							prvVehiculos.Visible = true;
							break;
						case PRV_GPS:
							prvGps.Visible = true;
							break;
						case PRV_REPORTES:
							if (ingreso)
								prvReportes.Visible = true;
							break;
						case PRV_TEMPERATURA:
							prvTemperatura.Visible = true;
							break;
						case PRV_DETENCIONES:
							prvDetenciones.Visible = true;
							break;
						case PRV_VELOCIDAD_MAX:
							prvVelocidadMax.Visible = true;
							break;
						case PRV_ALERTAS:
							prvAlertas.Visible = true;
							break;
						case PRV_APERTURA_CIERRE:
							prvAperturaCierre.Visible = true;
							break;
						case PRV_ENTRADA_SALIDA:
							prvEntradaSalida.Visible = true;
							break;
						case PRV_KILOMETRAJE:
							prvKilometraje.Visible = true;
							break;
						case PRV_RPTAUDITORIA:
							prvRptAuditoria.Visible = true;
							break;
						case PRV_ASIGNACION_USUARIO_VEHICULO:
							prvAsigusuavehiculo.Visible = true;
							break;
						case PRV_IDButton:
							prvIDButton.Visible = true;
							break;
						case PRV_REncendidoApagado:
							prvEncendidoApagado.Visible = true;
							break;
						case PRV_RPTCONSOLIDADO:
							prvConsolidado.Visible = true;
							break;
					}

				}

				if (prvUsuarios.Visible && (prvRoles.Visible || prvPrivilegios.Visible))
					div1.Visible = true;

				if (prvRoles.Visible && prvPrivilegios.Visible)
					div2.Visible = true;

				if (prvEmpresa.Visible && (prvPersonas.Visible || prvAlarmas.Visible || prvZonas.Visible || prvTiposZona.Visible))
					div3.Visible = true;

				if (prvPersonas.Visible && (prvAlarmas.Visible || prvZonas.Visible || prvTiposZona.Visible))
					div4.Visible = true;

				if (prvAlarmas.Visible && (prvZonas.Visible || prvTiposZona.Visible))
					div5.Visible = true;

				if (prvZonas.Visible && prvTiposZona.Visible)
					div6.Visible = true;

				if (prvSeguimiento.Visible && (prvAuditoria.Visible || prvVehiculos.Visible || prvGps.Visible || prvAsigusuavehiculo.Visible))
					div7.Visible = true;

				if (prvAuditoria.Visible && (prvVehiculos.Visible || prvGps.Visible || prvAsigusuavehiculo.Visible))
					div8.Visible = true;

				if (prvVehiculos.Visible && (prvGps.Visible || prvAsigusuavehiculo.Visible))
					div9.Visible = true;

				if (prvGps.Visible && prvAsigusuavehiculo.Visible)
					div17.Visible = true;

				if (prvAsigusuavehiculo.Visible && prvIDButton.Visible)
					div18.Visible = true;

				if (prvAlertas.Visible && (prvAperturaCierre.Visible || prvRptAuditoria.Visible || prvDetenciones.Visible || prvEntradaSalida.Visible || prvKilometraje.Visible || prvTemperatura.Visible || prvVelocidadMax.Visible))
					div10.Visible = true;

				if (prvAperturaCierre.Visible && (prvRptAuditoria.Visible || prvDetenciones.Visible || prvEntradaSalida.Visible || prvKilometraje.Visible || prvTemperatura.Visible || prvVelocidadMax.Visible))
					div11.Visible = true;

				if (prvRptAuditoria.Visible && (prvDetenciones.Visible || prvEntradaSalida.Visible || prvKilometraje.Visible || prvTemperatura.Visible || prvVelocidadMax.Visible))
					div12.Visible = true;

				if (prvDetenciones.Visible && (prvEntradaSalida.Visible || prvKilometraje.Visible || prvTemperatura.Visible || prvVelocidadMax.Visible))
					div13.Visible = true;

				if (prvEntradaSalida.Visible && (prvKilometraje.Visible || prvTemperatura.Visible || prvVelocidadMax.Visible))
					div14.Visible = true;

				if (prvKilometraje.Visible && (prvTemperatura.Visible || prvVelocidadMax.Visible))
					div15.Visible = true;

				if (prvTemperatura.Visible && prvVelocidadMax.Visible)
					div16.Visible = true;
				if (prvEncendidoApagado.Visible)
				{
					div19.Visible = true;
				}
				if (prvConsolidado.Visible)
				{
					div20.Visible = true;
				}
			}
		}

		protected void Usuario_LoggingOut(object sender, LoginCancelEventArgs e)
		{
			userName = HttpContext.Current.User.Identity.Name;
			usuarioCtrl.CerrarSesion(userName);

			Context.GetOwinContext().Authentication.SignOut();
		}

		protected void lblmensaje_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Vistas/Empresas/Panel");
		}
	}
}