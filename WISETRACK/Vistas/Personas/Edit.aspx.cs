using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK.Vistas.Personas
{
    public partial class Edit : System.Web.UI.Page
    {
        PersonaController personaCtrl;
        RolController rolCtrl;
        UsuarioController usuarioCtrl;

        Persona persona;
        string ci = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            personaCtrl = new PersonaController();
            rolCtrl = new RolController();
            usuarioCtrl = new UsuarioController();

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    if (SitePrincipal.ExisteActiva())
                    {
                        string ci = Request.QueryString["ci"];
                        if (!String.IsNullOrEmpty(ci))
                        {
                            var resultd = personaCtrl.listar(ci);
                            if (resultd.Estado)
                            {
                                cargarTipoPersona();
                                CargarPersona();
                                CargarRoles();
                            }
                            else
                            {
                                MensajeAlerta("El personal ya ha sido dado de baja");
                                Response.Redirect("~/Vistas/Personas/Index");
                            }
                        }
                        else
                        {
                            Response.Redirect("~/Vistas/Personas/Index");
                        }
                    }
                    else
                    {
                        SitePrincipal.pagRedireccion = "~/Vistas/Personas/Edit";
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

        public void CargarPersona()
        {
            string ci = Request.QueryString["ci"];
            txtCI.Text = ci;
            persona = personaCtrl.listar(ci);

            txtNombre.Text = persona.Nombre;
            txtApellidop.Text = persona.ApellidoP;
            txtApellidom.Text = persona.ApellidoM;
            txtdireccion.Text = persona.Direccion;
            txttelefono.Text = persona.Telefono;
            txtEmail.Text = persona.Email;
            cboTipop.SelectedValue = persona.CodTipo.ToString();

            switch (persona.CodTipo)
            {
                case 1:
                    divUser.Visible = false;
                    divPass.Visible = false;
                    divConfPass.Visible = false;
                    divRoles.Visible = false;
                    divContacto.Visible = false;
                    divTelfContacto.Visible = false;
                    divLicencia.Visible = false;
                    divFechaVigL.Visible = false;
                    divFechaVigDefL.Visible = false;
                    break;
                case 2:
                    cboTipop.Enabled = false;
                    divLicencia.Visible = false;
                    divFechaVigL.Visible = false;
                    divFechaVigDefL.Visible = false;

                    if (persona.AspNetUsers != null)
                    {
                        UserName.Text = persona.AspNetUsers.UserName;
                        txtContacto.Text = persona.Contacto;
                        txttelefonoc.Text = persona.TelfContacto;
                    }
                    break;
                case 3:
                    divUser.Visible = false;
                    divPass.Visible = false;
                    divConfPass.Visible = false;
                    divRoles.Visible = false;
                    divContacto.Visible = false;
                    divTelfContacto.Visible = false;

                    txtContacto.Text = persona.Contacto;
                    txttelefonoc.Text = persona.TelfContacto;
                    txtlicencia.Text = persona.CategoriaL;
                    txtfechavigl.Text = persona.FechaVigL.ToString();
                    txtfechavigdefl.Text = persona.FechaVigDefL.ToString();
                    break;
            }
        }

        public void cargarTipoPersona()
        {
            cboTipop.DataSource = personaCtrl.comboTipoPersona();
            cboTipop.DataTextField = "Descripcion";
            cboTipop.DataValueField = "CodTipo";
            cboTipop.DataBind();

            //int codTipo = persona.CodTipo;
            //cboTipop.SelectedValue = Convert.ToString(codTipo);

            //switch (codTipo)
            //{
            //    case 1:
            //        divUser.Visible = false;
            //        divRoles.Visible = false;
            //        divContacto.Visible = false;
            //        divTelfContacto.Visible = false;
            //        divLicencia.Visible = false;
            //        divFechaVigL.Visible = false;
            //        divFechaVigDefL.Visible = false;
            //        break;
            //    case 2:
            //        divLicencia.Visible = false;
            //        divFechaVigL.Visible = false;
            //        divFechaVigDefL.Visible = false;

            //        UserName.Text = persona.AspNetUsers.UserName;
            //        txtContacto.Text = persona.Contacto;
            //        txttelefonoc.Text = persona.TelfContacto;
            //        break;
            //    case 3:
            //        divUser.Visible = false;
            //        divRoles.Visible = false;

            //        txtContacto.Text = persona.Contacto;
            //        txttelefonoc.Text = persona.TelfContacto;
            //        txtlicencia.Text = persona.CategoriaL;
            //        txtfechavigl.Text = persona.FechaVigL.ToString();
            //        txtfechavigdefl.Text = persona.FechaVigDefL.ToString();
            //        break;
            //}
        }

        public void CargarRoles()
        {
            string userName = HttpContext.Current.User.Identity.Name;

            dpdRoles.DataValueField = "Id";
            dpdRoles.DataTextField = "Name";
            dpdRoles.DataSource = rolCtrl.GetAll(userName);
            dpdRoles.DataBind();

            if (persona.IdUser != null)
                dpdRoles.SelectedValue = persona.AspNetUsers.AspNetRoles.FirstOrDefault().Id;
            else
                dpdRoles.SelectedIndex = 0;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            var user = HttpContext.Current.User.Identity.Name;
            int codTipo = Convert.ToInt32(cboTipop.SelectedValue);

            Persona p = new Persona
            {
                CI = txtCI.Text,
                Nombre = txtNombre.Text,
                ApellidoP = txtApellidop.Text,
                ApellidoM = txtApellidom.Text,
                Direccion = txtdireccion.Text,
                Telefono = txttelefono.Text,
                Email = txtEmail.Text,
                Contacto = "",
                TelfContacto = "",
                CategoriaL = "",
                CodTipo = codTipo,
                UsuaModif = user,
                FechaModif = DateTime.Now
            };

            switch (codTipo)
            {
                case 1:
                    break;
                case 2:
                    p.Contacto = txtContacto.Text;
                    p.TelfContacto = txttelefonoc.Text;

                    persona = personaCtrl.listar(p.CI);
                    p.IdUser = persona.IdUser;

                    var usuario = usuarioCtrl.Get(user);
                    usuarioCtrl.Actualizar2(usuario, p.Email, p.Telefono);
                    break;
                case 3:
                    p.Contacto = txtContacto.Text;
                    p.TelfContacto = txttelefonoc.Text;
                    p.CategoriaL = txtlicencia.Text;
                    p.FechaVigL = Convert.ToDateTime(txtfechavigl.Text);
                    p.FechaVigDefL = Convert.ToDateTime(txtfechavigdefl.Text);
                    break;
            }

            bool sx = personaCtrl.update(p);
            if (sx == true)
            {
                MensajeAlerta("Se modifico correctamente");
                Response.Redirect("/Vistas/Personas/Index");
            }
            else
            {
                MensajeAlerta("Datos invalidos");
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

        protected void cboTipop_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int codtipo = Convert.ToInt32(cboTipop.SelectedValue);
            switch (codtipo)
            {
                case 1:
                    divUser.Visible = false;
                    divPass.Visible = false;
                    divConfPass.Visible = false;
                    divRoles.Visible = false;
                    divContacto.Visible = false;
                    divTelfContacto.Visible = false;
                    divLicencia.Visible = false;
                    divFechaVigL.Visible = false;
                    divFechaVigDefL.Visible = false;
                    break;
                case 2:
                    divUser.Visible = false;
                    divPass.Visible = false;
                    divConfPass.Visible = false;
                    divRoles.Visible = false;
                    divContacto.Visible = false;
                    divTelfContacto.Visible = false;

                    divLicencia.Visible = false;
                    divFechaVigL.Visible = false;
                    divFechaVigDefL.Visible = false;
                    break;
                case 3:
                    divUser.Visible = false;
                    divPass.Visible = false;
                    divConfPass.Visible = false;
                    divRoles.Visible = false;
                    divContacto.Visible = false;
                    divTelfContacto.Visible = false;

                    divLicencia.Visible = true;
                    divFechaVigL.Visible = true;
                    divFechaVigDefL.Visible = true;
                    break;
            }
            up2.Update();
        }
    }
}