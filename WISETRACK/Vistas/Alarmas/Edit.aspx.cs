using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK.Vistas.Alarmas
{
	public partial class Edit : System.Web.UI.Page
	{
		AlarmaController alarmaCtrl;
		HomeController homeCtrl;
		public static Alarma alarma;

		string codigo;
		string nombre;
		string userName;
		string nit;
		string funct;

		protected void Page_Load(object sender, EventArgs e)
		{
			alarmaCtrl = new AlarmaController();
			homeCtrl = new HomeController();

			if (!IsPostBack)
			{
				if (!SitePrincipal.IsIntruso())
				{
					if (SitePrincipal.ExisteActiva())
					{
						CargarTiposAlarma();
						CargarCategorias();
						CargarGeocercas();

						CargarAlarma();
						CargarGeocercas2();
						CargarVehiculos();
						CargarDestinatarios();
					}
					else
					{
						codigo = Request.QueryString["cod"];
						SitePrincipal.pagRedireccion = "~/Vistas/Alarmas/Edit?cod=" + codigo;
						Response.Redirect("~/Vistas/Empresas/Panel");
					}
				}
				else
					Response.Redirect("~/Account/Login");

			}
		}

		private void CargarTiposAlarma()
		{
			cboTipoA.DataSource = alarmaCtrl.GetAllTipos();
			cboTipoA.DataTextField = "Descripcion";
			cboTipoA.DataValueField = "CodTipoAlarma";
			cboTipoA.DataBind();
		}

		private void CargarCategorias()
		{
			cboCategoriaA.DataSource = alarmaCtrl.GetAllCategorias();
			cboCategoriaA.DataTextField = "Descripcion";
			cboCategoriaA.DataValueField = "CodCategoria";
			cboCategoriaA.DataBind();
		}

		private void CargarGeocercas()
		{
			userName = HttpContext.Current.User.Identity.Name;
			nit = homeCtrl.obtenerNit(userName);

			dpdGeocercas.DataSource = alarmaCtrl.GetGeocercasInicio(nit);
			dpdGeocercas.DataTextField = "Descripcion";
			dpdGeocercas.DataValueField = "CodigoGEO";
			dpdGeocercas.DataBind();
			dpdGeocercas.SelectedIndex = 0;
		}

		private void CargarAlarma()
		{
			codigo = Request.QueryString["cod"];
			if (String.IsNullOrEmpty(codigo))
			{
				Response.Redirect("~/Vistas/Alarmas/Index");
			}

			txtCodigo.Text = codigo;
			alarma = alarmaCtrl.Get(Convert.ToInt32(codigo));
			txtNombre.Text = alarma.NombreAlarma;

			int tipoAlarma = alarma.CodTipoAlarma;
			int categAlarma = alarma.CodCategoria;

			DateTime fechaHora, fechaHora2;

			switch (tipoAlarma)
			{
				case 1:
					divGeocerca.Visible = false;

					divVelocidad.Visible = false;
					divTiempo.Visible = false;
					divDistancia.Visible = false;
					divTemperatura.Visible = false;

					divVelocidad2.Visible = false;
					divTiempo2.Visible = false;
					divDistancia2.Visible = false;
					divTemperatura2.Visible = false;

					divFechaHora.Visible = true;
					divFechaHora2.Visible = true;

					fechaHora = alarma.FechaHora.Value;
					fechaHora2 = alarma.FechaHora2.Value;

					cboFechaHora.Text = (fechaHora.Hour < 10 ? "0" + Convert.ToString(fechaHora.Hour) : Convert.ToString(fechaHora.Hour)) + ":"
						+ (fechaHora.Minute < 10 ? "0" + Convert.ToString(fechaHora.Minute) : Convert.ToString(fechaHora.Minute));

					cboFechaHora2.Text = (fechaHora2.Hour < 10 ? "0" + Convert.ToString(fechaHora2.Hour) : Convert.ToString(fechaHora2.Hour)) + ":"
						+ (fechaHora2.Minute < 10 ? "0" + Convert.ToString(fechaHora2.Minute) : Convert.ToString(fechaHora2.Minute));

					break;
				case 2:
					divGeocerca.Visible = false;

					divVelocidad.Visible = false;
					divDistancia.Visible = false;
					divTemperatura.Visible = false;

					divVelocidad2.Visible = false;
					divDistancia2.Visible = false;
					divTemperatura2.Visible = false;

					txtTiempo.Text = Convert.ToString(alarma.Tiempo);

					if (categAlarma > 3)
						txtTiempo2.Text = Convert.ToString(alarma.Tiempo2);
					else
						divTiempo2.Visible = false;

					divFechaHora.Visible = false;
					divFechaHora2.Visible = false;
					break;
				case 3:
					divGeocerca.Visible = false;

					divVelocidad.Visible = false;
					divDistancia.Visible = false;
					divTemperatura.Visible = false;

					divVelocidad2.Visible = false;
					divDistancia2.Visible = false;
					divTemperatura2.Visible = false;

					txtTiempo2.Text = Convert.ToString(alarma.Tiempo2);

					if (categAlarma > 3)
						txtTiempo.Text = Convert.ToString(alarma.Tiempo);
					else
						divTiempo.Visible = false;

					divFechaHora.Visible = false;
					divFechaHora2.Visible = false;
					break;
				case 4:
					divGeocerca.Visible = false;

					divVelocidad.Visible = false;
					divTiempo.Visible = false;
					divDistancia.Visible = false;
					divTemperatura.Visible = false;

					divVelocidad2.Visible = false;
					divTiempo2.Visible = false;
					divDistancia2.Visible = false;
					divTemperatura2.Visible = false;

					divFechaHora.Visible = false;
					divFechaHora2.Visible = false;
					break;
				case 5:
					divGeocerca.Visible = false;

					divTiempo.Visible = false;
					divDistancia.Visible = false;
					divTemperatura.Visible = false;

					divTiempo2.Visible = false;
					divDistancia2.Visible = false;
					divTemperatura2.Visible = false;

					txtVelocidad.Text = Convert.ToString(alarma.Velocidad);

					if (categAlarma > 3)
						txtVelocidad2.Text = Convert.ToString(alarma.Velocidad2);
					else
						divVelocidad2.Visible = false;

					divFechaHora.Visible = false;
					divFechaHora2.Visible = false;
					break;
				case 6:
					divGeocerca.Visible = false;

					divVelocidad.Visible = false;
					divTiempo.Visible = false;
					divDistancia.Visible = false;
					divTemperatura.Visible = false;

					divVelocidad2.Visible = false;
					divTiempo2.Visible = false;
					divDistancia2.Visible = false;
					divTemperatura2.Visible = false;
					txtCantidadEnvio.Text = Convert.ToString(alarma.CantidadEnvio);
					txtIntervaloEnvio.Text = Convert.ToString(alarma.IntervaloEnvio);
					switch (categAlarma)
					{
						case 1:
							divTiempoEnvio.Visible = false;
							break;
						case 2:
							divTiempo.Visible = false;
							divFechaHora.Visible = false;
							divFechaHora2.Visible = false;
							txtTiempo.Text = Convert.ToString(alarma.Tiempo);

							break;
						case 3:
							divFechaHora.Visible = true;
							divFechaHora2.Visible = true;
							divTiempo.Visible = false;
							divTiempoEnvio.Visible = false;
							fechaHora = alarma.FechaHora.Value;
							fechaHora2 = alarma.FechaHora2.Value;

							cboFechaHora.Text = (fechaHora.Hour < 10 ? "0" + Convert.ToString(fechaHora.Hour) : Convert.ToString(fechaHora.Hour)) + ":"
								+ (fechaHora.Minute < 10 ? "0" + Convert.ToString(fechaHora.Minute) : Convert.ToString(fechaHora.Minute));

							cboFechaHora2.Text = (fechaHora2.Hour < 10 ? "0" + Convert.ToString(fechaHora2.Hour) : Convert.ToString(fechaHora2.Hour)) + ":"
								+ (fechaHora2.Minute < 10 ? "0" + Convert.ToString(fechaHora2.Minute) : Convert.ToString(fechaHora2.Minute));

							break;
						default:
							break;
					}


					break;
				case 7:
					divGeocerca.Visible = false;
					divVelocidad.Visible = false;
					divTiempo.Visible = false;
					divTemperatura.Visible = false;

					divVelocidad2.Visible = false;
					divTiempo2.Visible = false;
					divTemperatura2.Visible = false;

					txtDistancia.Text = Convert.ToString(alarma.Distancia);

					if (categAlarma > 3)
						txtDistancia2.Text = Convert.ToString(alarma.Distancia2);
					else
						divDistancia2.Visible = false;

					divFechaHora.Visible = false;
					divFechaHora2.Visible = false;
					break;
				case 8:
					divGeocerca.Visible = false;
					divVelocidad.Visible = false;
					divTiempo.Visible = false;
					divDistancia.Visible = false;

					divVelocidad2.Visible = false;
					divTiempo2.Visible = false;
					divDistancia2.Visible = false;

					txtTemperatura.Text = Convert.ToString(alarma.Temperatura);

					if (categAlarma > 3)
						txtTemperatura2.Text = Convert.ToString(alarma.Temperatura2);
					else
						divTemperatura2.Visible = false;

					divFechaHora.Visible = false;
					divFechaHora2.Visible = false;
					break;
				case 9:
					divGeocerca.Visible = false;
					divVelocidad.Visible = false;
					divTiempo.Visible = false;
					divDistancia.Visible = false;

					divVelocidad2.Visible = false;
					divTiempo2.Visible = false;
					divDistancia2.Visible = false;

					txtTemperatura2.Text = Convert.ToString(alarma.Temperatura2);

					if (categAlarma > 3)
						txtTemperatura.Text = Convert.ToString(alarma.Temperatura);
					else
						divTemperatura.Visible = false;

					divFechaHora.Visible = false;
					divFechaHora2.Visible = false;
					break;
				case 10:
					divGeocerca.Visible = false;

					divVelocidad.Visible = false;
					divTiempo.Visible = false;
					divDistancia.Visible = false;
					divTemperatura.Visible = false;

					divVelocidad2.Visible = false;
					divTiempo2.Visible = false;
					divDistancia2.Visible = false;
					divTemperatura2.Visible = false;

					if (categAlarma > 3)
					{
						divFechaHora.Visible = true;
						divFechaHora2.Visible = true;

						fechaHora = alarma.FechaHora.Value;
						fechaHora2 = alarma.FechaHora2.Value;

						cboFechaHora.Text = (fechaHora.Hour < 10 ? "0" + Convert.ToString(fechaHora.Hour) : Convert.ToString(fechaHora.Hour)) + ":"
							+ (fechaHora.Minute < 10 ? "0" + Convert.ToString(fechaHora.Minute) : Convert.ToString(fechaHora.Minute));

						cboFechaHora2.Text = (fechaHora2.Hour < 10 ? "0" + Convert.ToString(fechaHora2.Hour) : Convert.ToString(fechaHora2.Hour)) + ":"
							+ (fechaHora2.Minute < 10 ? "0" + Convert.ToString(fechaHora2.Minute) : Convert.ToString(fechaHora2.Minute));
					}
					else
					{
						divFechaHora.Visible = false;
						divFechaHora2.Visible = false;
					}

					break;
				case 11:
					divGeocerca.Visible = false;
					gdvGeocercas.Visible = false;

					divVelocidad.Visible = false;
					divTiempo.Visible = false;
					divDistancia.Visible = false;
					divTemperatura.Visible = false;

					divVelocidad2.Visible = false;
					divTiempo2.Visible = false;
					divDistancia2.Visible = false;
					divTemperatura2.Visible = false;

					lblTiempoEnvio.Visible = false;
					txtTiempoEnvio.Visible = false;


					if (categAlarma > 2)
					{
						divFechaHora.Visible = true;
						divFechaHora2.Visible = true;

						fechaHora = alarma.FechaHora.Value;
						fechaHora2 = alarma.FechaHora2.Value;

						cboFechaHora.Text = (fechaHora.Hour < 10 ? "0" + Convert.ToString(fechaHora.Hour) : Convert.ToString(fechaHora.Hour)) + ":"
							+ (fechaHora.Minute < 10 ? "0" + Convert.ToString(fechaHora.Minute) : Convert.ToString(fechaHora.Minute));

						cboFechaHora2.Text = (fechaHora2.Hour < 10 ? "0" + Convert.ToString(fechaHora2.Hour) : Convert.ToString(fechaHora2.Hour)) + ":"
							+ (fechaHora2.Minute < 10 ? "0" + Convert.ToString(fechaHora2.Minute) : Convert.ToString(fechaHora2.Minute));
					}
					else
					{
						divFechaHora.Visible = false;
						divFechaHora2.Visible = false;
					}

					break;
				case 12:
					divGeocerca.Visible = false;

					divVelocidad.Visible = false;
					divTiempo.Visible = false;
					divDistancia.Visible = false;
					divTemperatura.Visible = false;

					divVelocidad2.Visible = false;
					divTiempo2.Visible = false;
					divDistancia2.Visible = false;
					divTemperatura2.Visible = false;

					lblTiempoEnvio.Visible = false;
					txtTiempoEnvio.Visible = false;

					if (categAlarma > 2)
					{
						divFechaHora.Visible = true;
						divFechaHora2.Visible = true;

						fechaHora = alarma.FechaHora.Value;
						fechaHora2 = alarma.FechaHora2.Value;

						cboFechaHora.Text = (fechaHora.Hour < 10 ? "0" + Convert.ToString(fechaHora.Hour) : Convert.ToString(fechaHora.Hour)) + ":"
							+ (fechaHora.Minute < 10 ? "0" + Convert.ToString(fechaHora.Minute) : Convert.ToString(fechaHora.Minute));

						cboFechaHora2.Text = (fechaHora2.Hour < 10 ? "0" + Convert.ToString(fechaHora2.Hour) : Convert.ToString(fechaHora2.Hour)) + ":"
							+ (fechaHora2.Minute < 10 ? "0" + Convert.ToString(fechaHora2.Minute) : Convert.ToString(fechaHora2.Minute));
					}
					else
					{
						divFechaHora.Visible = false;
						divFechaHora2.Visible = false;
					}

					break;
				case 13:
					divGeocerca.Visible = false;

					divVelocidad.Visible = false;
					divTiempo.Visible = false;
					divDistancia.Visible = false;
					divTemperatura.Visible = false;

					divVelocidad2.Visible = false;
					divTiempo2.Visible = false;
					divDistancia2.Visible = false;
					divTemperatura2.Visible = false;

					lblTiempoEnvio.Visible = false;
					txtTiempoEnvio.Visible = false;

					if (categAlarma > 3)
					{
						divFechaHora.Visible = true;
						divFechaHora2.Visible = true;

						fechaHora = alarma.FechaHora.Value;
						fechaHora2 = alarma.FechaHora2.Value;

						cboFechaHora.Text = (fechaHora.Hour < 10 ? "0" + Convert.ToString(fechaHora.Hour) : Convert.ToString(fechaHora.Hour)) + ":"
							+ (fechaHora.Minute < 10 ? "0" + Convert.ToString(fechaHora.Minute) : Convert.ToString(fechaHora.Minute));

						cboFechaHora2.Text = (fechaHora2.Hour < 10 ? "0" + Convert.ToString(fechaHora2.Hour) : Convert.ToString(fechaHora2.Hour)) + ":"
							+ (fechaHora2.Minute < 10 ? "0" + Convert.ToString(fechaHora2.Minute) : Convert.ToString(fechaHora2.Minute));
					}
					else
					{
						divFechaHora.Visible = false;
						divFechaHora2.Visible = false;
					}

					break;

			}

			cboTipoA.SelectedValue = Convert.ToString(tipoAlarma);
			cboCategoriaA.SelectedValue = Convert.ToString(alarma.CodCategoria);

			ckbEstado.Checked = alarma.Activa.Value;
			ckbEmail.Checked = alarma.email;

			txtTiempoEnvio.Text = Convert.ToString(alarma.TiempoEnvio);
			txtIntervaloEnvio.Text = Convert.ToString(alarma.IntervaloEnvio);
			txtCantidadEnvio.Text = Convert.ToString(alarma.CantidadEnvio);
		}

		private void CargarGeocercas2()
		{
			gdvGeocercas.DataSource = alarmaCtrl.GetAllGeocercas(nit);
			gdvGeocercas.DataBind();

			bool allOk = true;
			int index = 0;

			foreach (GridViewRow row in gdvGeocercas.Rows)
			{
				int codGeo = Convert.ToInt32(gdvGeocercas.Rows[index].Cells[1].Text);

				if (alarmaCtrl.GetAlarmaGeocerca(alarma.CodAlarma, codGeo) != null)
				{
					CheckBox chkrow = (CheckBox)row.FindControl("SelecGeo");
					chkrow.Checked = true;
				}
				else
				{
					if (allOk)
						allOk = false;
				}

				index++;
			}

			if (allOk && index > 0)
			{
				CheckBox chk = (CheckBox)gdvGeocercas.HeaderRow.FindControl("SelecAllGeo");
				chk.Checked = true;
			}
		}

		private void CargarVehiculos()
		{
			gdvVehiculos.DataSource = alarmaCtrl.GetAllVehiculos(nit);
			gdvVehiculos.DataBind();

			bool allOk = true;
			int index = 0;

			foreach (GridViewRow row in gdvVehiculos.Rows)
			{
				string nroPlaca = Convert.ToString(gdvVehiculos.Rows[index].Cells[1].Text);

				if (alarmaCtrl.GetAlarmaVehiculo(alarma.CodAlarma, nroPlaca) != null)
				{
					CheckBox chkrow = (CheckBox)row.FindControl("SelecVeh");
					chkrow.Checked = true;
				}
				else
				{
					if (allOk)
						allOk = false;
				}

				index++;
			}

			if (allOk && index > 0)
			{
				CheckBox chk = (CheckBox)gdvVehiculos.HeaderRow.FindControl("SelecAllVeh");
				chk.Checked = true;
			}
		}

		private void CargarDestinatarios()
		{
			gdvDestinatarios.DataSource = alarmaCtrl.GetAllDestinatarios(nit);
			gdvDestinatarios.DataBind();

			bool allOk = true;
			int index = 0;

			foreach (GridViewRow row in gdvDestinatarios.Rows)
			{
				string ci = Convert.ToString(gdvDestinatarios.Rows[index].Cells[1].Text);

				if (alarmaCtrl.GetAlarmaDestinatario(alarma.CodAlarma, ci) != null)
				{
					CheckBox chkrow = (CheckBox)row.FindControl("SelecDest");
					chkrow.Checked = true;
				}
				else
				{
					if (allOk)
						allOk = false;
				}

				index++;
			}

			if (allOk && index > 0)
			{
				CheckBox chk = (CheckBox)gdvDestinatarios.HeaderRow.FindControl("SelecAllDest");
				chk.Checked = true;
			}
		}

		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			guardarfinal();
			//userName = User.Identity.Name;

			//codigo = txtCodigo.Text;
			//alarma = alarmaCtrl.Get(Convert.ToInt32(codigo));

			//try
			//{
			//	nombre = txtNombre.Text;
			//	int tipoAlarma = alarma.CodTipoAlarma;
			//	int categAlarma = alarma.CodCategoria;

			//	switch (tipoAlarma)
			//	{
			//		case 1:
			//			alarma.FechaHora = Convert.ToDateTime("01/01/2016 " + cboFechaHora.Text);
			//			alarma.FechaHora2 = Convert.ToDateTime("01/01/2016 " + cboFechaHora2.Text);
			//			break;
			//		case 2:
			//			alarma.Tiempo = Convert.ToInt32(txtTiempo.Text);

			//			if (categAlarma > 3)
			//				alarma.Tiempo2 = Convert.ToInt32(txtTiempo2.Text);

			//			break;
			//		case 3:
			//			alarma.Tiempo2 = Convert.ToInt32(txtTiempo2.Text);

			//			if (categAlarma > 3)
			//				alarma.Tiempo = Convert.ToInt32(txtTiempo.Text);

			//			break;
			//		case 4:
			//			break;
			//		case 5:
			//			alarma.Velocidad = Convert.ToInt32(txtVelocidad.Text);

			//			if (categAlarma > 3)
			//				alarma.Velocidad2 = Convert.ToInt32(txtVelocidad2.Text);

			//			break;
			//		case 6:
			//			if (categAlarma > 3)
			//			{
			//				alarma.FechaHora = Convert.ToDateTime("01/01/2016 " + cboFechaHora.Text);
			//				alarma.FechaHora2 = Convert.ToDateTime("01/01/2016 " + cboFechaHora2.Text);
			//			}
			//			break;
			//		case 7:
			//			alarma.Distancia = Convert.ToInt32(txtDistancia.Text);

			//			if (categAlarma > 3)
			//				alarma.Distancia2 = Convert.ToInt32(txtDistancia2.Text);

			//			break;
			//		case 8:
			//			alarma.Temperatura = Convert.ToInt32(txtTemperatura.Text);

			//			if (categAlarma > 3)
			//				alarma.Temperatura2 = Convert.ToInt32(txtTemperatura2.Text);

			//			break;
			//		case 9:
			//			alarma.Temperatura2 = Convert.ToInt32(txtTemperatura2.Text);

			//			if (categAlarma > 3)
			//				alarma.Temperatura = Convert.ToInt32(txtTemperatura.Text);

			//			break;
			//		case 10:
			//			if (categAlarma > 3)
			//			{
			//				alarma.FechaHora = Convert.ToDateTime("01/01/2016 " + cboFechaHora.Text);
			//				alarma.FechaHora2 = Convert.ToDateTime("01/01/2016 " + cboFechaHora2.Text);
			//			}
			//			break;
			//	}

			//	alarma.TiempoEnvio = Convert.ToInt32(txtTiempoEnvio.Text);
			//	alarma.IntervaloEnvio = Convert.ToInt32(txtIntervaloEnvio.Text);
			//	alarma.CantidadEnvio = Convert.ToInt32(txtCantidadEnvio.Text);

			//	alarma.Activa = ckbEstado.Checked;
			//	alarma.email = ckbEmail.Checked;

			//	alarma.UsuaModif = userName;
			//	alarma.FechaModif = DateTime.Now;

			//	bool ok = true;
			//	int index = 0;

			//	List<string> nroPlacas = new List<string>();

			//	foreach (GridViewRow gvr in gdvVehiculos.Rows)
			//	{
			//		bool selecDest = ((CheckBox)gvr.FindControl("SelecVeh")).Checked;

			//		if (selecDest)
			//		{
			//			string nroPlaca = Convert.ToString(gdvVehiculos.Rows[index].Cells[1].Text);
			//			nroPlacas.Add(nroPlaca);
			//		}

			//		index++;
			//	}

			//	if (nroPlacas.Count > 0)
			//	{
			//		index = 0;
			//		List<int> codGeos = new List<int>();

			//		if (tipoAlarma == 4 || tipoAlarma == 6 || tipoAlarma == 5 || tipoAlarma == 7||tipoAlarma==12 ||tipoAlarma==13)
			//		{
			//			foreach (GridViewRow gvr in gdvGeocercas.Rows)
			//			{
			//				bool selecGeo = ((CheckBox)gvr.FindControl("SelecGeo")).Checked;

			//				if (selecGeo)
			//				{
			//					int codGeo = Convert.ToInt32(gdvGeocercas.Rows[index].Cells[1].Text);
			//					codGeos.Add(codGeo);
			//				}

			//				index++;
			//			}

			//			//if (codGeos.Count == 0)
			//			//    ok = false;

			//		}

			//		if (ok)
			//		{
			//			index = 0;
			//			List<string> ciDest = new List<string>();

			//			foreach (GridViewRow gvr in gdvDestinatarios.Rows)
			//			{
			//				bool selecDest = ((CheckBox)gvr.FindControl("SelecDest")).Checked;

			//				if (selecDest)
			//				{
			//					string ci = Convert.ToString(gdvDestinatarios.Rows[index].Cells[1].Text);
			//					ciDest.Add(ci);
			//				}

			//				index++;
			//			}

			//			if (tipoAlarma != 4)
			//				alarmaCtrl.Actualizar(alarma, nombre, nroPlacas, codGeos, 0, ciDest, userName);
			//			else
			//			{
			//				int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);

			//				alarmaCtrl.Actualizar(alarma, nombre, nroPlacas, codGeos, codGeoInicio, ciDest, userName);
			//			}

			//			Response.Redirect("~/Vistas/Alarmas/Index");
			//		}
			//		else
			//			ErrorMessage.Text = "Seleccione uno o varias Geocercas de Alarma";

			//	}
			//	else
			//		ErrorMessage.Text = "Seleccione una o varios Vehiculos de Alarma";

			//}
			//catch (Exception ex)
			//{
			//	ErrorMessage.Text = ex.Message;
			//}
		}

		protected void cboFechaHora2_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
		{
			e.Item.Text = string.Concat(e.Item.Text.ToLower().Split(' ')[0], "");
		}

		protected void cboFechaHora_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
		{
			e.Item.Text = string.Concat(e.Item.Text.ToLower().Split(' ')[0], "");
		}

		protected void gdvDestinatarios_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		protected void txtCantidadEnvio_TextChanged(object sender, EventArgs e)
		{

		}
		//private void CargarVehiculos()
		//{
		//	gdvVehiculos.DataSource = alarmaCtrl.GetAllVehiculos(nit);
		//	gdvVehiculos.DataBind();
		//}

		//private void CargarDestinatarios()
		//{
		//	gdvDestinatarios.DataSource = alarmaCtrl.GetAllDestinatarios(nit);
		//	gdvDestinatarios.DataBind();
		//}
		private void guardarfinal()
		{
			userName = HttpContext.Current.User.Identity.Name;
			//Alarma alarma = new Alarma()
			//{
			//	NombreAlarma = txtNombre.Text.ToUpper(),
			//	CodTipoAlarma = Convert.ToInt32(cboTipoA.SelectedValue),
			//	CodCategoria = Convert.ToInt32(cboCategoriaA.SelectedValue),
			//	Activa = ckbEstado.Checked,
			//	email = ckbEmail.Checked,
			//	NIT = homeCtrl.obtenerNit(userName),
			//	UsuaReg = userName,
			//	FechaReg = DateTime.Now
			//};

			nombre = txtNombre.Text;
			int tipoAlarma = Convert.ToInt32(cboTipoA.SelectedValue);
			int categAlarma = Convert.ToInt32(cboCategoriaA.SelectedValue);
			alarma.Activa = ckbEstado.Checked;
			alarma.email = ckbEmail.Checked;
			alarma.CodTipoAlarma = tipoAlarma;
			alarma.CodCategoria = categAlarma;
			switch (tipoAlarma)
			{
				case 1://control
					guardarControl(userName, alarma, tipoAlarma, categAlarma);
					break;
				case 2:///
					guardarDeteccionMaxima(userName, alarma, tipoAlarma, categAlarma);
					break;
				case 3:
					guardarDetecionMinima(userName, alarma, tipoAlarma, categAlarma);
					break;
				case 4:
					guardarItiranario(userName, alarma, tipoAlarma, categAlarma);
					break;
				case 5:
					guardarVelocidadMaxina(userName, alarma, tipoAlarma, categAlarma);
					break;
				case 6:
					guardarEntradaSalida(userName, alarma, tipoAlarma, categAlarma);
					break;
				case 7:
					guardarKilometraje(userName, alarma, tipoAlarma, categAlarma);
					break;
				case 8:
					guardarTemperaturaMax(userName, alarma, tipoAlarma, categAlarma);
					break;
				case 9:
					guardarTemperaturaMin(userName, alarma, tipoAlarma, categAlarma);
					break;
				case 10:
					guardarAperturaCierre(userName, alarma, tipoAlarma, categAlarma);
					break;
				case 11:
					guardarVoltaje(userName, alarma, tipoAlarma, categAlarma);
					break;
				case 12:
					guardarEntrada(userName, alarma, tipoAlarma, categAlarma);
					break;
				case 13:
					guardarSalida(userName, alarma, tipoAlarma, categAlarma);
					break;
				default:
					break;
			}
		}
		private void error(string mensaje)
		{

			funct = "javascript:errorlog('" + mensaje + "');";
			ClientScript.RegisterStartupScript(GetType(), "JavaScript", funct, true);
		}
		private void alert(string mensaje)
		{
			funct = "javascript:alertlog('" + mensaje + "');";

			ClientScript.RegisterStartupScript(GetType(), "JavaScript", funct, true);
		}
		private void guardarSalida(string userName, Alarma alarma, int tipoAlarma, int categAlarma)
		{
			List<String> nroplaca = getlistPlacas();
			List<int> geocerca = getlistGeos();
			List<String> destino = getDesnatarios();
			try
			{
				switch (categAlarma)
				{
					case 1:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{
									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");
						break;

					case 2:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{

									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");
						break;
					case 3:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{

									string fechaHora = "01/01/2017 " + cboFechaHora.Text;
									string fechaHora2 = "01/01/2017 " + cboFechaHora2.Text;
									alarma.FechaHora = Convert.ToDateTime(fechaHora);
									alarma.FechaHora2 = Convert.ToDateTime(fechaHora2);
									if (alarma.FechaHora < alarma.FechaHora2)
									{
										int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
										//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
										alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
										alert("Se ha creado corectamente Alarma");
										Response.Redirect("~/Vistas/Alarmas/Index");
									}
									else
										error("La hora inicio de la alarma debera ser mayor hora de fin");

								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");

						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				error(ex.Message);
			}
		}

		private void guardarEntrada(string userName, Alarma alarma, int tipoAlarma, int categAlarma)
		{

			List<String> nroplaca = getlistPlacas();
			List<int> geocerca = getlistGeos();
			List<String> destino = getDesnatarios();
			try
			{
				switch (categAlarma)
				{
					case 1:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{


									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");
						break;

					case 2:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{
									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");
						break;
					case 3:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{
									string fechaHora = "01/01/2017 " + cboFechaHora.Text;
									string fechaHora2 = "01/01/2017 " + cboFechaHora2.Text;

									alarma.FechaHora = Convert.ToDateTime(fechaHora);
									alarma.FechaHora2 = Convert.ToDateTime(fechaHora2);
									if (alarma.FechaHora < alarma.FechaHora2)
									{
										int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
										//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
										alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
										alert("Se ha creado corectamente Alarma");
										Response.Redirect("~/Vistas/Alarmas/Index");
									}
									else
										error("La hora inicio de la alarma debera ser mayor hora de fin");

								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");

						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				error(ex.Message);
			}
		}

		private void guardarVoltaje(string userName, Alarma alarma, int tipoAlarma, int categAlarma)
		{
			List<String> nroplaca = getlistPlacas();
			List<int> geocerca = getlistGeos();
			List<String> destino = getDesnatarios();
			try
			{
				switch (categAlarma)
				{
					case 1:

						if (nroplaca.Count > 0)
						{
							if (destino.Count > 0)
							{

								int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
								//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
								alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
								alert("Se ha creado corectamente Alarma");
								Response.Redirect("~/Vistas/Alarmas/Index");

							}
							error("Selecione por lo menos un destinatario.... ");
						}
						else
							error("Selecione por lo menos una Vehiculo...");

						break;

					default:
						break;
				}
			}
			catch (Exception ex)
			{
				error(ex.Message);
			}
		}

		private void guardarAperturaCierre(string userName, Alarma alarma, int tipoAlarma, int categAlarma)
		{
			List<String> nroplaca = getlistPlacas();
			List<int> geocerca = getlistGeos();
			List<String> destino = getDesnatarios();
			try
			{
				switch (categAlarma)
				{
					case 1:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{
									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");
						break;

					case 2:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{
									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");
						break;
					case 3:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{
									string fechaHora = "01/01/2017 " + cboFechaHora.Text;
									string fechaHora2 = "01/01/2017 " + cboFechaHora2.Text;
									alarma.FechaHora = Convert.ToDateTime(fechaHora);
									alarma.FechaHora2 = Convert.ToDateTime(fechaHora2);
									if (alarma.FechaHora < alarma.FechaHora2)
									{
										int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
										//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
										alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
										alert("Se ha creado corectamente Alarma");
										Response.Redirect("~/Vistas/Alarmas/Index");
									}
									else
										error("La hora inicio de la alarma debera ser mayor hora de fin");

								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");

						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				error(ex.Message);
			}
		}

		private void guardarTemperaturaMin(string userName, Alarma alarma, int tipoAlarma, int categAlarma)
		{
			List<String> nroplaca = getlistPlacas();
			List<int> geocerca = getlistGeos();
			List<String> destino = getDesnatarios();
			try
			{
				switch (categAlarma)
				{
					case 1:

						if (nroplaca.Count > 0)
						{
							if (destino.Count > 0)
							{
								if (this.txtTemperatura2.Text != "")
								{
									alarma.Temperatura2 = Convert.ToInt32(txtTemperatura2.Text);
									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								else
								{
									error("Introduzca el Temperatura min...");

								}

							}
							error("Selecione por lo menos un destinatario.... ");
						}
						else
							error("Selecione por lo menos una Vehiculo...");


						break;

					case 2:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{
									if (this.txtTemperatura2.Text != "")
									{
										alarma.Temperatura2 = Convert.ToInt32(txtTemperatura2.Text);
										int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
										//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
										alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
										alert("Se ha creado corectamente Alarma");
										Response.Redirect("~/Vistas/Alarmas/Index");
									}
									else
									{
										error("Introduzca el Temperatura min...");

									}
								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");
						break;
					case 3:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{
									if (this.txtTemperatura.Text != "")
									{
										if (this.txtTemperatura2.Text != "")
										{

											alarma.Temperatura = Convert.ToInt32(txtTemperatura.Text);
											alarma.Temperatura2 = Convert.ToInt32(txtTemperatura2.Text);
											if (alarma.Temperatura > alarma.Temperatura2)
											{

												int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
												//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
												alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
												alert("Se ha creado corectamente Alarma");
												Response.Redirect("~/Vistas/Alarmas/Index");
											}
											else
												error("Error La Temperatura Max debe ser MAYOR  a Temperatura menor");
										}
										else
											error("Introduzca el Distancia Min");

									}
									else
									{
										error("Introduzca el Distancia Max...");

									}
								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");

						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				error(ex.Message);
			}
		}
		private void guardarTemperaturaMax(string userName, Alarma alarma, int tipoAlarma, int categAlarma)
		{
			List<String> nroplaca = getlistPlacas();
			List<int> geocerca = getlistGeos();
			List<String> destino = getDesnatarios();
			try
			{
				switch (categAlarma)
				{
					case 1:

						if (nroplaca.Count > 0)
						{
							if (destino.Count > 0)
							{
								if (this.txtTemperatura.Text != "")
								{
									alarma.Temperatura = Convert.ToInt32(txtTemperatura.Text);
									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								else
								{
									error("Introduzca el Temperatura...");

								}

							}
							error("Selecione por lo menos un destinatario.... ");
						}
						else
							error("Selecione por lo menos una Vehiculo...");


						break;

					case 2:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{
									if (this.txtTemperatura.Text != "")
									{
										alarma.Temperatura = Convert.ToInt32(txtTemperatura.Text);
										int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
										//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
										alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
										alert("Se ha creado corectamente Alarma");
										Response.Redirect("~/Vistas/Alarmas/Index");
									}
									else
									{
										error("Introduzca el Temperatura...");

									}
								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");
						break;
					case 3:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{
									if (this.txtTemperatura.Text != "")
									{
										if (this.txtTemperatura2.Text != "")
										{

											alarma.Temperatura = Convert.ToInt32(txtTemperatura.Text);
											alarma.Temperatura2 = Convert.ToInt32(txtTemperatura2.Text);
											if (alarma.Temperatura > alarma.Temperatura2)
											{

												int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
												//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
												alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
												alert("Se ha creado corectamente Alarma");
												Response.Redirect("~/Vistas/Alarmas/Index");
											}
											else
												error("Error La Temperatura Max debe ser MAYOR  a Temperatura menor");
										}
										else
											error("Introduzca el Distancia Min");

									}
									else
									{
										error("Introduzca el Distancia Max...");

									}
								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");

						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				error(ex.Message);
			}
		}

		private void guardarKilometraje(string userName, Alarma alarma, int tipoAlarma, int categAlarma)
		{
			List<String> nroplaca = getlistPlacas();
			List<int> geocerca = getlistGeos();
			List<String> destino = getDesnatarios();
			try
			{
				switch (categAlarma)
				{
					case 1:

						if (nroplaca.Count > 0)
						{
							if (destino.Count > 0)
							{
								if (this.txtDistancia.Text != "")
								{
									alarma.Distancia = Convert.ToInt32(txtDistancia.Text);
									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								else
								{
									error("Introduzca el Dsitancia...");

								}

							}
							error("Selecione por lo menos un destinatario.... ");
						}
						else
							error("Selecione por lo menos una Vehiculo...");


						break;

					case 2:

						if (nroplaca.Count > 0)
						{
							if (destino.Count > 0)
							{
								if (this.txtDistancia.Text != "")
								{
									alarma.Distancia = Convert.ToInt32(txtDistancia.Text);
									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								else
								{
									error("Introduzca el Dsitancia...");

								}
							}
							error("Selecione por lo menos un destinatario.... ");
						}
						else
							error("Selecione por lo menos una Vehiculo...");

						break;
					case 3:

						if (nroplaca.Count > 0)
						{
							if (destino.Count > 0)
							{
								if (this.txtDistancia.Text != "")
								{
									if (this.txtDistancia2.Text != "")
									{

										alarma.Distancia = Convert.ToInt32(txtDistancia.Text);
										alarma.Distancia2 = Convert.ToInt32(txtDistancia2.Text);
										if (alarma.Distancia > alarma.Distancia2)
										{

											int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
											//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
											alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
											alert("Se ha creado corectamente Alarma");
											Response.Redirect("~/Vistas/Alarmas/Index");
										}
										else
											error("Error La Dsitancia Max debe ser MAYOR  a Distancia menor");
									}
									else
										error("Introduzca el Distancia Min");

								}
								else
								{
									error("Introduzca el Distancia Max...");

								}
							}
							error("Selecione por lo menos un destinatario.... ");
						}
						else
							error("Selecione por lo menos una Vehiculo...");


						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				error(ex.Message);
			}
		}

		private void guardarEntradaSalida(string userName, Alarma alarma, int tipoAlarma, int categAlarma)
		{

			List<String> nroplaca = getlistPlacas();
			List<int> geocerca = getlistGeos();
			List<String> destino = getDesnatarios();
			try
			{
				switch (categAlarma)
				{
					case 1:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{

									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");
						break;

					case 2:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{
									alarma.Tiempo = !String.IsNullOrEmpty(txtTiempo.Text) ? Convert.ToInt32(txtTiempo.Text) : 0;
									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");
						break;
					case 3:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{
									string fechaHora = "01/01/2017 " + cboFechaHora.Text;
									string fechaHora2 = "01/01/2017 " + cboFechaHora2.Text;


									alarma.FechaHora = DateTime.Parse(fechaHora);
									alarma.FechaHora2 = DateTime.Parse(fechaHora2);
									if (alarma.FechaHora < alarma.FechaHora2)
									{
										int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
										//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
										alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
										alert("Se ha creado corectamente Alarma");
										Response.Redirect("~/Vistas/Alarmas/Index");
									}
									else
										error("La hora inicio de la alarma debera ser mayor hora de fin");

								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");

						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				error(ex.Message);
			}
		}

		private void guardarVelocidadMaxina(string userName, Alarma alarma, int tipoAlarma, int categAlarma)
		{
			List<String> nroplaca = getlistPlacas();
			List<int> geocerca = getlistGeos();
			List<String> destino = getDesnatarios();
			try
			{
				switch (categAlarma)
				{
					case 1:

						if (nroplaca.Count > 0)
						{
							if (destino.Count > 0)
							{
								if (this.txtVelocidad.Text != "")
								{
									alarma.Velocidad = Convert.ToInt32(txtVelocidad.Text);
									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								else
								{
									error("Introduzca el velociad ...");

								}

							}
							error("Selecione por lo menos un destinatario.... ");
						}
						else
							error("Selecione por lo menos una Vehiculo...");


						break;

					case 2:

						if (nroplaca.Count > 0)
						{
							if (destino.Count > 0)
							{
								if (this.txtVelocidad.Text != "")
								{
									alarma.Velocidad = Convert.ToInt32(txtVelocidad.Text);
									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								else
								{
									error("Introduzca el Velocidad...");

								}
							}
							error("Selecione por lo menos un destinatario.... ");
						}
						else
							error("Selecione por lo menos una Vehiculo...");

						break;
					case 3:

						if (nroplaca.Count > 0)
						{
							if (destino.Count > 0)
							{
								if (this.txtVelocidad.Text != "")
								{
									if (this.txtVelocidad2.Text != "")
									{

										alarma.Velocidad = Convert.ToInt32(txtVelocidad.Text);
										alarma.Velocidad2 = Convert.ToInt32(txtVelocidad2.Text);
										if (alarma.Velocidad > alarma.Velocidad2)
										{

											int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
											//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
											alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
											alert("Se ha creado corectamente Alarma");
											Response.Redirect("~/Vistas/Alarmas/Index");
										}
										else
											error("Error La velocidad Max debe ser MAYOR  a velocidad menor");
									}
									else
										error("Introduzca el Velocidad Min");

								}
								else
								{
									error("Introduzca el Velicidad Max...");

								}
							}
							error("Selecione por lo menos un destinatario.... ");
						}
						else
							error("Selecione por lo menos una Vehiculo...");

						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				error(ex.Message);
			}
		}

		private void guardarItiranario(string userName, Alarma alarma, int tipoAlarma, int categAlarma)
		{

		}

		private void guardarDetecionMinima(string userName, Alarma alarma, int tipoAlarma, int categAlarma)
		{
			List<String> nroplaca = getlistPlacas();
			List<int> geocerca = getlistGeos();
			List<String> destino = getDesnatarios();
			try
			{
				switch (categAlarma)
				{
					case 1:

						if (nroplaca.Count > 0)
						{
							if (destino.Count > 0)
							{
								if (this.txtTiempo.Text != "")
								{
									alarma.Tiempo = Convert.ToInt32(txtTiempo.Text);
									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								else
								{
									error("Introduzca el tiempo...");

								}

							}
							error("Selecione por lo menos un destinatario.... ");
						}
						else
							error("Selecione por lo menos una Vehiculo...");


						break;

					case 2:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{
									if (this.txtTiempo.Text != "")
									{
										alarma.Tiempo = Convert.ToInt32(txtTiempo.Text);
										int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
										//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
										alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
										alert("Se ha creado corectamente Alarma");
										Response.Redirect("~/Vistas/Alarmas/Index");
									}
									else
									{
										error("Introduzca el tiempo...");

									}
								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");
						break;
					case 3:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{
									if (this.txtTiempo.Text != "")
									{
										if (this.txtTiempo2.Text != "")
										{

											alarma.Tiempo = Convert.ToInt32(txtTiempo.Text);
											alarma.Tiempo2 = Convert.ToInt32(txtTiempo2.Text);
											int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
											//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
											alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
											alert("Se ha creado corectamente Alarma");
											Response.Redirect("~/Vistas/Alarmas/Index");
										}
										else
											error("Introduzca el Tiempo Min");

									}
									else
									{
										error("Introduzca el tiempo Max...");

									}
								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");

						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				error(ex.Message);
			}
		}

		private void guardarDeteccionMaxima(string userName, Alarma alarma, int tipoAlarma, int categAlarma)
		{
			List<String> nroplaca = getlistPlacas();
			List<int> geocerca = getlistGeos();
			List<String> destino = getDesnatarios();
			try
			{
				switch (categAlarma)
				{
					case 1:

						if (nroplaca.Count > 0)
						{
							if (destino.Count > 0)
							{
								if (this.txtTiempo.Text != "")
								{
									alarma.Tiempo = Convert.ToInt32(txtTiempo.Text);
									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								else
								{
									error("Introduzca el tiempo...");

								}

							}
							error("Selecione por lo menos un destinatario.... ");
						}
						else
							error("Selecione por lo menos una Vehiculo...");


						break;

					case 2:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{
									if (this.txtTiempo.Text != "")
									{
										alarma.Tiempo = Convert.ToInt32(txtTiempo.Text);
										int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
										//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
										alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
										alert("Se ha creado corectamente Alarma");
										Response.Redirect("~/Vistas/Alarmas/Index");
									}
									else
									{
										error("Introduzca el tiempo...");

									}
								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");
						break;
					case 3:
						if (geocerca.Count > 0)
						{
							if (nroplaca.Count > 0)
							{
								if (destino.Count > 0)
								{
									if (this.txtTiempo.Text != "")
									{
										if (this.txtTiempo2.Text != "")
										{

											alarma.Tiempo = Convert.ToInt32(txtTiempo.Text);
											alarma.Tiempo2 = Convert.ToInt32(txtTiempo2.Text);
											int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
											//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
											alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
											alert("Se ha creado corectamente Alarma");
											Response.Redirect("~/Vistas/Alarmas/Index");
										}
										else
											error("Introduzca el Tiempo Min");

									}
									else
									{
										error("Introduzca el tiempo Max...");

									}
								}
								error("Selecione por lo menos un destinatario.... ");
							}
							else
								error("Selecione por lo menos una Vehiculo...");
						}
						else
							error("Seleccione por los menos una Geocerca...");

						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				error(ex.Message);
			}
		}

		private void guardarControl(string userName, Alarma alarma, int tipoAlarma, int categAlarma)
		{
			List<String> nroplaca = getlistPlacas();
			List<int> geocerca = getlistGeos();
			List<String> destino = getDesnatarios();
			try
			{
				switch (categAlarma)
				{
					case 1:

						if (nroplaca.Count > 0)
						{
							if (destino.Count > 0)
							{
								string fechaHora = "01/01/2017 " + cboFechaHora.Text;
								string fechaHora2 = "01/01/2017 " + cboFechaHora2.Text;

								alarma.FechaHora = Convert.ToDateTime(fechaHora);
								alarma.FechaHora2 = Convert.ToDateTime(fechaHora2);
								if (alarma.FechaHora > alarma.FechaHora2)
								{

									int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);
									//alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
									alarmaCtrl.Actualizar(alarma, nombre, nroplaca, geocerca, codGeoInicio, destino, userName);
									alert("Se ha creado corectamente Alarma");
									Response.Redirect("~/Vistas/Alarmas/Index");
								}
								else
									error("La hora inicio debera ser menor hora fin");

							}
							error("Selecione por lo menos un destinatario.... ");
						}
						else
							error("Selecione por lo menos una Vehiculo...");

						break;

					default:
						break;
				}
			}
			catch (Exception ex)
			{
				error(ex.Message);
			}

		}

		private List<string> getDesnatarios()
		{
			List<String> lista = new List<string>();
			int index = 0;
			foreach (GridViewRow gvr in gdvDestinatarios.Rows)
			{
				bool selecDest = ((CheckBox)gvr.FindControl("SelecDest")).Checked;

				if (selecDest)
				{
					string ci = Convert.ToString(gdvDestinatarios.Rows[index].Cells[1].Text);
					lista.Add(ci);
				}

				index++;
			}
			return lista;

		}
		private List<string> getlistPlacas()
		{
			int index = 0;
			List<string> nroPlacas = new List<string>();

			foreach (GridViewRow gvr in gdvVehiculos.Rows)
			{
				bool selecDest = ((CheckBox)gvr.FindControl("SelecVeh")).Checked;

				if (selecDest)
				{
					string nroPlaca = Convert.ToString(gdvVehiculos.Rows[index].Cells[1].Text);
					nroPlacas.Add(nroPlaca);
				}

				index++;
			}
			return nroPlacas;
		}
		private List<int> getlistGeos()
		{
			int index = 0;
			List<int> lista = new List<int>();
			foreach (GridViewRow gvr in gdvGeocercas.Rows)
			{
				bool selecGeo = ((CheckBox)gvr.FindControl("SelecGeo")).Checked;

				if (selecGeo)
				{
					int codGeo = Convert.ToInt32(gdvGeocercas.Rows[index].Cells[1].Text);
					lista.Add(codGeo);
				}

				index++;
			}
			return lista;
		}


		//protected void SelecAllDest_CheckedChanged(object sender, EventArgs e)
		//{
		//    CheckBox check = (CheckBox)gdvDestinatarios.HeaderRow.FindControl("SelecAllDest");

		//    foreach (GridViewRow row in gdvDestinatarios.Rows)
		//    {
		//        CheckBox chkrow = (CheckBox)row.FindControl("SelecDest");

		//        if (check.Checked)
		//            chkrow.Checked = true;
		//        else
		//            chkrow.Checked = false;

		//    }
		//}

		//protected void SelecAllGeo_CheckedChanged(object sender, EventArgs e)
		//{
		//    CheckBox check = (CheckBox)gdvGeocercas.HeaderRow.FindControl("SelecAllGeo");

		//    foreach (GridViewRow row in gdvGeocercas.Rows)
		//    {
		//        CheckBox chkrow = (CheckBox)row.FindControl("SelecGeo");

		//        if (check.Checked)
		//            chkrow.Checked = true;
		//        else
		//            chkrow.Checked = false;

		//    }
		//}

		//protected void SelecAllVeh_CheckedChanged(object sender, EventArgs e)
		//{
		//    CheckBox check = (CheckBox)gdvVehiculos.HeaderRow.FindControl("SelecAllVeh");

		//    foreach (GridViewRow row in gdvVehiculos.Rows)
		//    {
		//        CheckBox chkrow = (CheckBox)row.FindControl("SelecVeh");

		//        if (check.Checked)
		//            chkrow.Checked = true;
		//        else
		//            chkrow.Checked = false;

		//    }
		//}
	}
}