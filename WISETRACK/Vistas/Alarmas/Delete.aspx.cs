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
    public partial class Delete : System.Web.UI.Page
    {
        AlarmaController alarmaCtrl;
        Alarma alarma;
        string codigo;

        protected void Page_Load(object sender, EventArgs e)
        {
            alarmaCtrl = new AlarmaController();

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                    CargarAlarma();
                else
                    Response.Redirect("~/Account/Login");
            }
        }

        private void CargarAlarma()
        {
            codigo = Request.QueryString["cod"];
            if (String.IsNullOrEmpty(codigo))
            {
                Response.Redirect("~/Vistas/Alarmas/Index");
            }
            lblCodigo.Text = codigo;

            alarma = alarmaCtrl.Get(Convert.ToInt32(codigo));
            lblNombre.Text = alarma.NombreAlarma;
            lblTipoAlarma.Text = alarma.TipoAlarma.Descripcion;
            lblCategoria.Text = alarma.CategoriaAlarma.Descripcion;

            int tipoAlarma = alarma.CodTipoAlarma;
            int categAlarma = alarma.CodCategoria;

            DateTime fechaHora, fechaHora2;

            switch (tipoAlarma)
            {
                case 1:
                    dtVelocidad.Visible = false;
                    lblVelocidad.Visible = false;

                    dtTiempo.Visible = false;
                    lblTiempo.Visible = false;

                    dtDistancia.Visible = false;
                    lblDistancia.Visible = false;

                    dtTemperatura.Visible = false;
                    lblTemperatura.Visible = false;

                    dtVelocidad2.Visible = false;
                    lblVelocidad2.Visible = false;

                    dtTiempo2.Visible = false;
                    lblTiempo2.Visible = false;

                    dtDistancia2.Visible = false;
                    lblDistancia2.Visible = false;

                    dtTemperatura2.Visible = false;
                    lblTemperatura2.Visible = false;

                    fechaHora = alarma.FechaHora.Value;
                    fechaHora2 = alarma.FechaHora2.Value;

                    lblFechaHora.Text = (fechaHora.Hour < 10 ? "0" + Convert.ToString(fechaHora.Hour) : Convert.ToString(fechaHora.Hour)) + ":"
                        + (fechaHora.Minute < 10 ? "0" + Convert.ToString(fechaHora.Minute) : Convert.ToString(fechaHora.Minute));

                    lblFechaHora2.Text = (fechaHora2.Hour < 10 ? "0" + Convert.ToString(fechaHora2.Hour) : Convert.ToString(fechaHora2.Hour)) + ":"
                        + (fechaHora2.Minute < 10 ? "0" + Convert.ToString(fechaHora2.Minute) : Convert.ToString(fechaHora2.Minute));

                    break;
                case 2:
                    dtVelocidad.Visible = false;
                    lblVelocidad.Visible = false;

                    dtTiempo.Visible = true;
                    lblTiempo.Text = alarma.Tiempo.ToString();

                    dtDistancia.Visible = false;
                    lblDistancia.Visible = false;

                    dtTemperatura.Visible = false;
                    lblTemperatura.Visible = false;

                    dtVelocidad2.Visible = false;
                    lblVelocidad2.Visible = false;

                    if (categAlarma <= 3)
                    {
                        dtTiempo2.Visible = false;
                        lblTiempo2.Visible = false;
                    }
                    else
                    {
                        dtTiempo2.Visible = true;
                        lblTiempo2.Text = alarma.Tiempo2.ToString();
                    }

                    dtDistancia2.Visible = false;
                    lblDistancia2.Visible = false;
                    
                    dtTemperatura2.Visible = false;
                    lblTemperatura2.Visible = false;

                    dtFechaHora.Visible = false;
                    lblFechaHora.Visible = false;

                    dtFechaHora2.Visible = false;
                    lblFechaHora2.Visible = false;
                    break;
                case 3:
                    dtVelocidad.Visible = false;
                    lblVelocidad.Visible = false;

                    dtTiempo2.Visible = true;
                    lblTiempo2.Text = alarma.Tiempo2.ToString();

                    dtDistancia.Visible = false;
                    lblDistancia.Visible = false;

                    dtTemperatura.Visible = false;
                    lblTemperatura.Visible = false;

                    dtVelocidad2.Visible = false;
                    lblVelocidad2.Visible = false;

                    if (categAlarma <= 3)
                    {
                        dtTiempo.Visible = false;
                        lblTiempo.Visible = false;
                    }
                    else
                    {
                        dtTiempo.Visible = true;
                        lblTiempo.Text = alarma.Tiempo.ToString();
                    }

                    dtDistancia2.Visible = false;
                    lblDistancia2.Visible = false;

                    dtTemperatura2.Visible = false;
                    lblTemperatura2.Visible = false;

                    dtFechaHora.Visible = false;
                    lblFechaHora.Visible = false;

                    dtFechaHora2.Visible = false;
                    lblFechaHora2.Visible = false;
                    break;
                case 4:
                    dtVelocidad.Visible = false;
                    lblVelocidad.Visible = false;

                    dtTiempo.Visible = false;
                    lblTiempo.Visible = false;

                    dtDistancia.Visible = false;
                    lblDistancia.Visible = false;

                    dtTemperatura.Visible = false;
                    lblTemperatura.Visible = false;

                    dtVelocidad2.Visible = false;
                    lblVelocidad2.Visible = false;

                    dtTiempo2.Visible = false;
                    lblTiempo2.Visible = false;

                    dtDistancia2.Visible = false;
                    lblDistancia2.Visible = false;

                    dtTemperatura2.Visible = false;
                    lblTemperatura2.Visible = false;

                    dtFechaHora.Visible = false;
                    lblFechaHora.Visible = false;

                    dtFechaHora2.Visible = false;
                    lblFechaHora2.Visible = false;
                    break;
                case 5:
                    dtVelocidad.Visible = true;
                    lblVelocidad.Text = alarma.Velocidad.ToString();

                    dtTiempo.Visible = false;
                    lblTiempo.Visible = false;

                    dtDistancia.Visible = false;
                    lblDistancia.Visible = false;

                    dtTemperatura.Visible = false;
                    lblTemperatura.Visible = false;

                    if (categAlarma <= 3)
                    {
                        dtVelocidad2.Visible = false;
                        lblVelocidad2.Visible = false;
                    }
                    else
                    {
                        dtVelocidad2.Visible = true;
                        lblVelocidad2.Text = alarma.Velocidad2.ToString();
                    }

                    dtTiempo2.Visible = false;
                    lblTiempo2.Visible = false;

                    dtDistancia2.Visible = false;
                    lblDistancia2.Visible = false;

                    dtTemperatura2.Visible = false;
                    lblTemperatura2.Visible = false;

                    dtFechaHora.Visible = false;
                    lblFechaHora.Visible = false;

                    dtFechaHora2.Visible = false;
                    lblFechaHora2.Visible = false;
                    break;
                case 6:
                    dtVelocidad.Visible = false;
                    lblVelocidad.Visible = false;

                    dtTiempo.Visible = false;
                    lblTiempo.Visible = false;

                    dtDistancia.Visible = false;
                    lblDistancia.Visible = false;

                    dtTemperatura.Visible = false;
                    lblTemperatura.Visible = false;

                    dtVelocidad2.Visible = false;
                    lblVelocidad2.Visible = false;

                    dtTiempo2.Visible = false;
                    lblTiempo2.Visible = false;

                    dtDistancia2.Visible = false;
                    lblDistancia2.Visible = false;

                    dtTemperatura2.Visible = false;
                    lblTemperatura2.Visible = false;

                    if (categAlarma <= 3)
                    {
                        dtFechaHora.Visible = false;
                        lblFechaHora.Visible = false;

                        dtFechaHora2.Visible = false;
                        lblFechaHora2.Visible = false;
                    }
                    else
                    {
                        fechaHora = alarma.FechaHora.Value;
                        fechaHora2 = alarma.FechaHora2.Value;

                        lblFechaHora.Text = (fechaHora.Hour < 10 ? "0" + Convert.ToString(fechaHora.Hour) : Convert.ToString(fechaHora.Hour)) + ":"
                            + (fechaHora.Minute < 10 ? "0" + Convert.ToString(fechaHora.Minute) : Convert.ToString(fechaHora.Minute));

                        lblFechaHora2.Text = (fechaHora2.Hour < 10 ? "0" + Convert.ToString(fechaHora2.Hour) : Convert.ToString(fechaHora2.Hour)) + ":"
                            + (fechaHora2.Minute < 10 ? "0" + Convert.ToString(fechaHora2.Minute) : Convert.ToString(fechaHora2.Minute));
                    }

                    break;
                case 7:
                    dtVelocidad.Visible = false;
                    lblVelocidad.Visible = false;

                    dtTiempo.Visible = false;
                    lblTiempo.Visible = false;

                    dtDistancia.Visible = true;
                    lblDistancia.Text = alarma.Distancia.ToString();

                    dtTemperatura.Visible = false;
                    lblTemperatura.Visible = false;

                    dtVelocidad2.Visible = false;
                    lblVelocidad2.Visible = false;

                    dtTiempo2.Visible = false;
                    lblTiempo2.Visible = false;

                    if (categAlarma <= 3)
                    {
                        dtDistancia2.Visible = false;
                        lblDistancia2.Visible = false;
                    }
                    else
                    {
                        dtDistancia2.Visible = true;
                        lblDistancia2.Text = alarma.Distancia2.ToString();
                    }

                    dtTemperatura2.Visible = false;
                    lblTemperatura2.Visible = false;

                    dtFechaHora.Visible = false;
                    lblFechaHora.Visible = false;

                    dtFechaHora2.Visible = false;
                    lblFechaHora2.Visible = false;
                    break;
                case 8:
                    dtVelocidad.Visible = false;
                    lblVelocidad.Visible = false;

                    dtTiempo.Visible = false;
                    lblTiempo.Visible = false;

                    dtDistancia.Visible = false;
                    lblDistancia.Visible = false;

                    dtTemperatura.Visible = true;
                    lblTemperatura.Text = alarma.Temperatura.ToString();

                    dtVelocidad2.Visible = false;
                    lblVelocidad2.Visible = false;

                    dtTiempo2.Visible = false;
                    lblTiempo2.Visible = false;

                    dtDistancia2.Visible = false;
                    lblDistancia2.Visible = false;

                    if (categAlarma <= 3)
                    {
                        dtTemperatura2.Visible = false;
                        lblTemperatura2.Visible = false;
                    }
                    else
                    {
                        dtTemperatura2.Visible = true;
                        lblTemperatura2.Text = alarma.Temperatura2.ToString();
                    }

                    dtFechaHora.Visible = false;
                    lblFechaHora.Visible = false;

                    dtFechaHora2.Visible = false;
                    lblFechaHora2.Visible = false;
                    break;
                case 9:
                    dtVelocidad.Visible = false;
                    lblVelocidad.Visible = false;

                    dtTiempo.Visible = false;
                    lblTiempo.Visible = false;

                    dtDistancia.Visible = false;
                    lblDistancia.Visible = false;

                    dtTemperatura2.Visible = true;
                    lblTemperatura2.Text = alarma.Temperatura2.ToString();

                    dtVelocidad2.Visible = false;
                    lblVelocidad2.Visible = false;

                    dtTiempo2.Visible = false;
                    lblTiempo2.Visible = false;

                    dtDistancia2.Visible = false;
                    lblDistancia2.Visible = false;

                    if (categAlarma <= 3)
                    {
                        dtTemperatura.Visible = false;
                        lblTemperatura.Visible = false;
                    }
                    else
                    {
                        dtTemperatura.Visible = true;
                        lblTemperatura.Text = alarma.Temperatura.ToString();
                    }

                    dtFechaHora.Visible = false;
                    lblFechaHora.Visible = false;

                    dtFechaHora2.Visible = false;
                    lblFechaHora2.Visible = false;
                    break;
                case 10:
                    dtVelocidad.Visible = false;
                    lblVelocidad.Visible = false;

                    dtTiempo.Visible = false;
                    lblTiempo.Visible = false;

                    dtDistancia.Visible = false;
                    lblDistancia.Visible = false;

                    dtTemperatura.Visible = false;
                    lblTemperatura.Visible = false;

                    dtVelocidad2.Visible = false;
                    lblVelocidad2.Visible = false;

                    dtTiempo2.Visible = false;
                    lblTiempo2.Visible = false;

                    dtDistancia2.Visible = false;
                    lblDistancia2.Visible = false;

                    dtTemperatura2.Visible = false;
                    lblTemperatura2.Visible = false;

                    if (categAlarma <= 3)
                    {
                        dtFechaHora.Visible = false;
                        lblFechaHora.Visible = false;

                        dtFechaHora2.Visible = false;
                        lblFechaHora2.Visible = false;
                    }
                    else
                    {
                        fechaHora = alarma.FechaHora.Value;
                        fechaHora2 = alarma.FechaHora2.Value;

                        lblFechaHora.Text = (fechaHora.Hour < 10 ? "0" + Convert.ToString(fechaHora.Hour) : Convert.ToString(fechaHora.Hour)) + ":"
                            + (fechaHora.Minute < 10 ? "0" + Convert.ToString(fechaHora.Minute) : Convert.ToString(fechaHora.Minute));

                        lblFechaHora2.Text = (fechaHora2.Hour < 10 ? "0" + Convert.ToString(fechaHora2.Hour) : Convert.ToString(fechaHora2.Hour)) + ":"
                            + (fechaHora2.Minute < 10 ? "0" + Convert.ToString(fechaHora2.Minute) : Convert.ToString(fechaHora2.Minute));
                    }

                    break;
            }

            lblEstado.Text = (alarma.Activa.Value == true ? "ACTIVA" : "NO ACTIVA");

            lblCantidadEnvio.Text = Convert.ToString(alarma.CantidadEnvio);
            lblIntervaloEnvio.Text = Convert.ToString(alarma.IntervaloEnvio);
            lblTiempoEnvio.Text = Convert.ToString(alarma.TiempoEnvio);

            lblUsuarioReg.Text = alarma.UsuaReg;
            lblFechaReg.Text = alarma.FechaReg.ToString();

            rptGeocercas.DataSource = alarmaCtrl.GetGeocercas(alarma.CodAlarma);
            rptGeocercas.DataBind();

            rptVehiculos.DataSource = alarmaCtrl.GetVehiculos(alarma.CodAlarma);
            rptVehiculos.DataBind();

            rptDestinatarios.DataSource = alarmaCtrl.GetDestinatarios(alarma.CodAlarma);
            rptDestinatarios.DataBind();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            alarmaCtrl.Eliminar(Convert.ToInt32(lblCodigo.Text));
            Response.Redirect("~/Vistas/Alarmas/Index");
        }
    }
}