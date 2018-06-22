using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;

namespace WISETRACK.Vistas.Usuarios
{
    public partial class AsignarVehiculo : System.Web.UI.Page
    {
        private PersonaController personaCtrl;
        private VehiculoController vehiculoCtrl;
        private HomeController homeCtrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            personaCtrl = new PersonaController();
            vehiculoCtrl = new VehiculoController();
            homeCtrl = new HomeController();

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    if (SitePrincipal.ExisteActiva())
                    {
                        CargarComboUsuario();
                        CargarGrillaVehiculo();
                    }
                    else
                    {
                        SitePrincipal.pagRedireccion = "~/Vistas/Usuarios/AsignarVehiculo";
                        SitePrincipal.countRedireccion = 0;
                        Response.Redirect("~/Vistas/Empresas/Panel");
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        public void CargarComboUsuario()
        {
            cbopersona.DataSource = personaCtrl.ListarUsuarioSupervisor();
            cbopersona.DataTextField = "Apellido";
            cbopersona.DataValueField = "CI";
            cbopersona.DataBind();
            cbopersona.Items.Insert(0, "Seleccione");
        }

        public void CargarGrillaVehiculo()
        {
            string nci = cbopersona.SelectedValue;
            var user = HttpContext.Current.User.Identity.Name;
            var nit = homeCtrl.obtenerNit(user);
            gdvvehiculo.DataSource = vehiculoCtrl.ListarVehiculoSupervisor(nci, nit);
            gdvvehiculo.DataBind();
        }

        public void CargarGrillaVehiculoAsignado()
        {
            string nci1 = cbopersona.SelectedValue;
            var user1 = HttpContext.Current.User.Identity.Name;
            var nit1 = homeCtrl.obtenerNit(user1);
            gdvvehiculoasignado.DataSource = vehiculoCtrl.ListarVehiculoAsignado(nci1, nit1);
            gdvvehiculoasignado.DataBind();
        }

        protected void cbopersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrillaVehiculo();
            uptodovehiculo.Update();
            CargarGrillaVehiculoAsignado();
            upvehiculoasig.Update();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string rcbopersona = cbopersona.SelectedValue;
            if (!rcbopersona.Equals("Seleccione"))
            {
                foreach (GridViewRow gvr in gdvvehiculo.Rows)
                {
                    bool selec = ((CheckBox)gvr.FindControl("SelecPriv")).Checked;
                    if (selec)
                    {
                        try
                        {
                            var user = HttpContext.Current.User.Identity.Name;
                            var nit = homeCtrl.obtenerNit(user);

                            string ci = cbopersona.SelectedValue;
                            string placa = gvr.Cells[1].Text;

                            bool sw = vehiculoCtrl.GuardarAsignacionVehiculo(ci, placa, nit);
                            if (sw)
                            {
                                MensajeAlerta("Datos guardados correctamente");
                            }
                            else
                            {
                                MensajeAlerta("Favor, Intente de nuevo");
                            }
                        }
                        catch (Exception ex)
                        {
                            InfoMessage.Text = ex.Message;
                            return;
                        }
                    }
                }
                CargarGrillaVehiculo();
                uptodovehiculo.Update();
                CargarGrillaVehiculoAsignado();
                upvehiculoasig.Update();
            }
            else
            {
                MensajeAlerta("Por Favor, Seleccione una persona.");
            }
        }

        private void MensajeAlerta(string mensaje)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(mensaje);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            cbopersona.Enabled = false;
            gdvvehiculo.Enabled = false;
            btnGuardar.Enabled = false;

            HabilitarCheckVehiculoAsignado();
            btnEditar.Enabled = false;
            lnkquitar.Visible = true;
            btnCancel.Visible = true;
        }

        private void HabilitarCheckVehiculoAsignado()
        {
            foreach (GridViewRow gvr in gdvvehiculoasignado.Rows)
            {
                var selec = ((CheckBox)gvr.FindControl("SelecPriv"));
                if (selec.Enabled == false)
                {
                    selec.Enabled = true;
                }
            }
        }

        private void DeshabilitarCheckVehiculoAsignado()
        {
            foreach (GridViewRow gvr in gdvvehiculoasignado.Rows)
            {
                var selec = ((CheckBox)gvr.FindControl("SelecPriv"));
                if (selec.Enabled == true)
                {
                    selec.Enabled = false;
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            bool sw = false;
            foreach (GridViewRow gvr in gdvvehiculoasignado.Rows)
            {
                bool selec = ((CheckBox)gvr.FindControl("SelecPriv")).Checked;
                if (selec)
                {
                    try
                    {
                        int id = Convert.ToInt32(gvr.Cells[1].Text);
                        vehiculoCtrl.RemoverAsignacionVehiculo(id);
                        sw = true;
                    }
                    catch (Exception ex)
                    {
                        sw = false;
                        InfoMessage.Text = ex.Message;
                        return;
                    }
                }
            }

            if (sw)
            {
                MensajeAlerta("Datos eliminados correctamente");
            }
            else
            {
                MensajeAlerta("Favor, Intente de nuevo");
            }

            cbopersona.Enabled = true;
            gdvvehiculo.Enabled = true;
            btnGuardar.Enabled = true;
            btnEditar.Enabled = true;
            lnkquitar.Visible = false;
            btnCancel.Visible = false;
            DeshabilitarCheckVehiculoAsignado();
            CargarGrillaVehiculo();
            uptodovehiculo.Update();
            CargarGrillaVehiculoAsignado();
            upvehiculoasig.Update();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            cbopersona.Enabled = true;
            gdvvehiculo.Enabled = true;
            btnGuardar.Enabled = true;
            btnEditar.Enabled = true;
            lnkquitar.Visible = false;
            btnCancel.Visible = false;
            DeshabilitarCheckVehiculoAsignado();
            CargarGrillaVehiculo();
            uptodovehiculo.Update();
            CargarGrillaVehiculoAsignado();
            upvehiculoasig.Update();
        }

    }
}