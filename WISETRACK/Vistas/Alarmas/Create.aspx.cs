using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK.Vistas.Alarmas
{
	public partial class Create : System.Web.UI.Page
	{
		AlarmaController alarmaCtrl;
		HomeController homeCtrl;

		//public static string selecValue = "0";
		//public static bool okGeocerca = true;

		string userName;
		string nit;
		string funct;

		protected void Page_Load(object sender, EventArgs e)
		{
			alarmaCtrl = new AlarmaController();
			homeCtrl = new HomeController();

			cboFechaHora.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
			cboFechaHora2.Filter = (RadComboBoxFilter)Convert.ToInt32(2);

			if (!IsPostBack)
			{
				if (!SitePrincipal.IsIntruso())
				{
					if (SitePrincipal.ExisteActiva())
					{
						CargarGeocercas();
						CargarGeocercas2();
						CargarVehiculos();
						CargarDestinatarios();

						CargarTiposAlarma();
						CargarCategorias();

						ckbEstado.Checked = true;
						ckbEmail.Checked = true;

					}
					else
					{
						SitePrincipal.pagRedireccion = "~/Vistas/Alarmas/Create";
						SitePrincipal.countRedireccion = 0;
						Response.Redirect("~/Vistas/Empresas/Panel");
					}
				}
				else
					Response.Redirect("~/Account/Login");

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
		private void CargarTiposAlarma()
		{
			cboTipoA.DataSource = alarmaCtrl.GetAllTipos();
			cboTipoA.DataTextField = "Descripcion";
			cboTipoA.DataValueField = "CodTipoAlarma";
			cboTipoA.DataBind();

			//cboTipoA.SelectedValue = selecValue;
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

		private void CargarGeocercas2()
		{
			gdvGeocercas.DataSource = alarmaCtrl.GetAllGeocercas(nit);
			gdvGeocercas.DataBind();
		}

		private void CargarVehiculos()
		{
			gdvVehiculos.DataSource = alarmaCtrl.GetAllVehiculos(nit);
			gdvVehiculos.DataBind();
		}

		private void CargarDestinatarios()
		{
			gdvDestinatarios.DataSource = alarmaCtrl.GetAllDestinatarios(nit);
			gdvDestinatarios.DataBind();
		}
		private void guardarfinal()
		{
			userName = HttpContext.Current.User.Identity.Name;
			Alarma alarma = new Alarma()
			{
				NombreAlarma = txtNombre.Text.ToUpper(),
				CodTipoAlarma = Convert.ToInt32(cboTipoA.SelectedValue),
				CodCategoria = Convert.ToInt32(cboCategoriaA.SelectedValue),
				Activa = ckbEstado.Checked,
				email = ckbEmail.Checked,
				NIT = homeCtrl.obtenerNit(userName),
				UsuaReg = userName,
				FechaReg = DateTime.Now
			};

			alarma.CantidadEnvio = Convert.ToInt32(txtCantidadEnvio.Text);
			alarma.IntervaloEnvio = Convert.ToInt32(txtIntervaloEnvio.Text);
			int tipoAlarma = Convert.ToInt32(cboTipoA.SelectedValue);
			int categAlarma = Convert.ToInt32(cboCategoriaA.SelectedValue);
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

									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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

									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
										alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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


									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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

									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
										alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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

								alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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

									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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

									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
										alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
										alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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

												alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
										alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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

												alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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

											alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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

									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
									alarma.TiempoEnvio = !String.IsNullOrEmpty(txtTiempoEnvio.Text) ? Convert.ToInt32(txtTiempoEnvio.Text) : 0;
									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
										alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
									alarma.TiempoEnvio = 2;
									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
									alarma.TiempoEnvio = Convert.ToInt32(txtTiempoEnvio.Text);
									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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

											alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
										alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
											alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
										alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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
											alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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

									alarmaCtrl.AddAlarma(alarma, nroplaca, geocerca, 0, destino, userName);
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

		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			guardarfinal();
			//userName = HttpContext.Current.User.Identity.Name;

			//string errorMsj = String.Empty;
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

			//int tipoAlarma = Convert.ToInt32(cboTipoA.SelectedValue);
			//int categAlarma = Convert.ToInt32(cboCategoriaA.SelectedValue);

			//bool ok = false;

			//switch (tipoAlarma)
			//{
			//	case 1:
			//		alarma.CodCategoria = 1;

			//		try
			//		{

			//			string fechaHora = DateTime.Now.Date.ToString("M/d/yyyy") + " " + cboFechaHora.Text;
			//			string fechaHora2 = DateTime.Now.Date.ToString("M/d/yyyy") + " " + cboFechaHora2.Text;


			//			alarma.FechaHora = Convert.ToDateTime(fechaHora);
			//			alarma.FechaHora2 = Convert.ToDateTime(fechaHora2);

			//			ok = true;
			//		}
			//		catch (Exception ex)
			//		{
			//			ErrorMessage.Text = ex.Message;
			//			ok = false;
			//		}

			//		break;
			//	case 2:
			//		try
			//		{
			//			alarma.Tiempo = Convert.ToInt32(txtTiempo.Text);

			//			if (categAlarma > 1)
			//				alarma.Tiempo2 = Convert.ToInt32(txtTiempo2.Text);

			//			ok = true;
			//		}
			//		catch (Exception ex)
			//		{
			//			ErrorMessage.Text = "Tiempo: " + ex.Message;
			//			ok = false;
			//		}

			//		break;
			//	case 3:
			//		try
			//		{
			//			alarma.Tiempo2 = Convert.ToInt32(txtTiempo2.Text);

			//			if (categAlarma > 1)
			//				alarma.Tiempo = Convert.ToInt32(txtTiempo.Text);

			//			ok = true;
			//		}
			//		catch (Exception ex)
			//		{
			//			ErrorMessage.Text = "Tiempo: " + ex.Message;
			//			ok = false;
			//		}

			//		break;
			//	case 4:
			//		alarma.CodCategoria = 1;
			//		ok = true;
			//		break;
			//	case 5:
			//		try
			//		{
			//			alarma.Velocidad = Convert.ToInt32(txtVelocidad.Text);
			//			ok = true;
			//		}
			//		catch (Exception ex)
			//		{
			//			ErrorMessage.Text = "Velocidad: " + ex.Message;
			//			ok = false;
			//		}

			//		break;
			//	case 6:
			//		try
			//		{
			//			if (categAlarma > 1)
			//			{
			//				string fechaHora = DateTime.Now.Date.ToString("M/d/yyyy") + " " + cboFechaHora.Text;
			//				string fechaHora2 = DateTime.Now.Date.ToString("M/d/yyyy") + " " + cboFechaHora2.Text;

			//				alarma.FechaHora = Convert.ToDateTime(fechaHora);
			//				alarma.FechaHora2 = Convert.ToDateTime(fechaHora2);
			//			}

			//			ok = true;
			//		}
			//		catch (Exception ex)
			//		{
			//			ErrorMessage.Text = ex.Message;
			//			ok = false;
			//		}

			//		break;
			//	case 7:
			//		try
			//		{
			//			alarma.Distancia = Convert.ToInt32(txtDistancia.Text);
			//			ok = true;
			//		}
			//		catch (Exception ex)
			//		{
			//			ErrorMessage.Text = "Distancia: " + ex.Message;
			//			ok = false;
			//		}

			//		break;
			//	case 8:
			//		try
			//		{
			//			alarma.Temperatura = Convert.ToInt32(txtTemperatura.Text);

			//			if (categAlarma > 1)
			//				alarma.Temperatura2 = Convert.ToInt32(txtTemperatura2.Text);

			//			ok = true;
			//		}
			//		catch (Exception ex)
			//		{
			//			ErrorMessage.Text = "Temperatura: " + ex.Message;
			//			ok = false;
			//		}

			//		break;
			//	case 9:
			//		try
			//		{
			//			alarma.Temperatura2 = Convert.ToInt32(txtTemperatura2.Text);

			//			if (categAlarma > 1)
			//				alarma.Temperatura = Convert.ToInt32(txtTemperatura.Text);

			//			ok = true;
			//		}
			//		catch (Exception ex)
			//		{
			//			ErrorMessage.Text = "Temperatura: " + ex.Message;
			//			ok = false;
			//		}

			//		break;
			//	case 10:
			//		try
			//		{
			//			if (categAlarma > 1)
			//			{
			//				string fechaHora = DateTime.Now.Date.ToString("M/d/yyyy") + " " + cboFechaHora.Text;
			//				string fechaHora2 = DateTime.Now.Date.ToString("M/d/yyyy") + " " + cboFechaHora2.Text;

			//				alarma.FechaHora = Convert.ToDateTime(fechaHora);
			//				alarma.FechaHora2 = Convert.ToDateTime(fechaHora2);
			//			}

			//			ok = true;
			//		}
			//		catch (Exception ex)
			//		{
			//			//ErrorMessage.Text = ex.Message;
			//			error(ex.Message);
			//			ok = false;
			//		}

			//		break;
			//	case 12:
			//		try
			//		{
			//			if (categAlarma > 1)
			//			{
			//				string fechaHora = DateTime.Now.Date.ToString("M/d/yyyy") + " " + cboFechaHora.Text;
			//				string fechaHora2 = DateTime.Now.Date.ToString("M/d/yyyy") + " " + cboFechaHora2.Text;

			//				alarma.FechaHora = Convert.ToDateTime(fechaHora);
			//				alarma.FechaHora2 = Convert.ToDateTime(fechaHora2);
			//			}

			//			ok = true;
			//		}
			//		catch (Exception ex)
			//		{
			//			//ErrorMessage.Text = ex.Message;
			//			error(ex.Message);
			//			ok = false;
			//		}

			//		break;
			//	case 13:
			//		try
			//		{
			//			if (categAlarma > 1)
			//			{
			//				string fechaHora = DateTime.Now.Date.ToString("M/d/yyyy") + " " + cboFechaHora.Text;
			//				string fechaHora2 = DateTime.Now.Date.ToString("M/d/yyyy") + " " + cboFechaHora2.Text;

			//				alarma.FechaHora = Convert.ToDateTime(fechaHora);
			//				alarma.FechaHora2 = Convert.ToDateTime(fechaHora2);
			//			}

			//			ok = true;
			//		}
			//		catch (Exception ex)
			//		{
			//			ErrorMessage.Text = ex.Message;
			//			ok = false;
			//		}

			//		break;
			//}

			//try
			//{
			//	alarma.TiempoEnvio = Convert.ToInt32(txtTiempoEnvio.Text);
			//	alarma.IntervaloEnvio = Convert.ToInt32(txtIntervaloEnvio.Text);
			//	alarma.CantidadEnvio = Convert.ToInt32(txtCantidadEnvio.Text);

			//	ok = true;
			//}
			//catch (Exception ex)
			//{
			//	ErrorMessage.Text = ex.Message;
			//	ok = false;
			//}

			//if (ok)
			//{
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

			//	//if (nroPlacas.Count > 0)
			//	//{

			//	index = 0;
			//	List<int> codGeos = new List<int>();

			//	if (tipoAlarma == 4 || tipoAlarma == 6 || tipoAlarma == 12 || tipoAlarma == 13 || tipoAlarma == 11 || tipoAlarma == 10)
			//	{
			//		if (tipoAlarma == 12 || tipoAlarma == 13 || tipoAlarma == 11)
			//		{
			//			alarma.TiempoEnvio = 0;
			//		}
			//		foreach (GridViewRow gvr in gdvGeocercas.Rows)
			//		{
			//			bool selecGeo = ((CheckBox)gvr.FindControl("SelecGeo")).Checked;

			//			if (selecGeo)
			//			{
			//				int codGeo = Convert.ToInt32(gdvGeocercas.Rows[index].Cells[1].Text);
			//				codGeos.Add(codGeo);
			//			}

			//			index++;
			//		}

			//		if (codGeos.Count == 0)
			//		{
			//			if (tipoAlarma == 4)
			//				errorMsj = "Seleccione una o varias Geocercas de Alarma";
			//			//error("Seleccione una o varias Geocercas de Alarma");

			//			if (tipoAlarma == 6 || tipoAlarma == 12 || tipoAlarma == 13 || tipoAlarma == 10)
			//				errorMsj = "Seleccione una Geocerca de Alarma";
			//			//error("Seleccione una Geocerca de Alarma");

			//			if (tipoAlarma != 1)
			//			{
			//				ok = false;
			//			}

			//		}
			//		else
			//		{
			//			if (tipoAlarma == 6)
			//			{
			//				if (codGeos.Count < 1)
			//				{
			//					errorMsj = "Seleccione solo una Geocerca de Alarma";
			//					//error("Seleccione solo una Geocerca de Alarma");
			//					ok = false;
			//				}
			//			}
			//		}
			//	}

			//	if (ok)
			//	{
			//		index = 0;
			//		List<string> ciDest = new List<string>();

			//		foreach (GridViewRow gvr in gdvDestinatarios.Rows)
			//		{
			//			bool selecDest = ((CheckBox)gvr.FindControl("SelecDest")).Checked;

			//			if (selecDest)
			//			{
			//				string ci = Convert.ToString(gdvDestinatarios.Rows[index].Cells[1].Text);
			//				ciDest.Add(ci);
			//			}

			//			index++;
			//		}

			//		try
			//		{
			//			if (tipoAlarma != 4)
			//			{
			//				if (alarma.CodCategoria > 1)
			//				{
			//					if (codGeos.Count > 0)
			//					{

			//						alarmaCtrl.AddAlarma(alarma, nroPlacas, codGeos, 0, ciDest, userName);
			//						alert("Se ha creado corectamente Alarma");
			//					}
			//					else
			//					{
			//						error("Seleccione uno o varios Vehiculos");
			//					}

			//				}
			//				else
			//				{
			//					alarmaCtrl.AddAlarma(alarma, nroPlacas, codGeos, 0, ciDest, userName);
			//					alert("Se ha creado corectamente Alarma");
			//				}
			//			}
			//			else
			//			{
			//				int codGeoInicio = Convert.ToInt32(dpdGeocercas.SelectedValue);

			//				alarmaCtrl.AddAlarma(alarma, nroPlacas, codGeos, codGeoInicio, ciDest, userName);
			//				alert("Se ha creado corectamente Alarma");
			//			}

			//			Response.Redirect("~/Vistas/Alarmas/Index");
			//		}
			//		catch (Exception ex)
			//		{
			//			error(ex.Message);
			//			//ErrorMessage.Text = ex.Message;z
			//		}
			//	}
			//	else
			//		error(errorMsj);
			//	//ErrorMessage.Text = errorMsj;

			//	//}
			//	//else
			//	//{

			//	//	//ErrorMessage.Text = "Seleccione uno o varios Vehiculos";
			//	//	error("Seleccione uno o varios Vehiculos");
			//	//}
			//}
			//else
			//	error("Datos no validos");
			////ErrorMessage.Text = "Datos no validos";
		}

		protected void cboFechaHora_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
		{
			e.Item.Text = string.Concat(e.Item.Text.ToLower().Split(' ')[0], "");
		}


		protected void cboFechaHora2_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
		{
			e.Item.Text = string.Concat(e.Item.Text.ToLower().Split(' ')[0], "");
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

		//    selecValue = cboTipoA.SelectedValue;
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

		//    selecValue = cboTipoA.SelectedValue;
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

		//    selecValue = cboTipoA.SelectedValue;
		//}

		//[WebMethod]
		//public static void SetSelecGeocerca(string value)
		//{

		//    //if (!value.Equals("0"))
		//    //    okGeocerca = true;
		//    //else
		//    //    okGeocerca = false;
		//}

		//[WebMethod]
		//public static string GetSelecGeo()
		//{
		//    if (okGeocerca)
		//        return "1";
		//    else
		//        return "0";
		//}
	}
}