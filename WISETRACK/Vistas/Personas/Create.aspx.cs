using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;
using WISETRACK.Models;
using Microsoft.AspNet.Identity.Owin;
using Owin;

namespace WISETRACK.Vistas.Personas
{
    public partial class Create : System.Web.UI.Page
    {
        PersonaController pc;
        HomeController homeCtrl;
        RolController rolCtrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            homeCtrl = new HomeController();
            pc = new PersonaController();
            rolCtrl = new RolController();

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    if (SitePrincipal.ExisteActiva())
                    {
                        cargarTipoPersona();
                        CargarRoles();
                    }
                    else
                    {
                        SitePrincipal.pagRedireccion = "~/Vistas/Personas/Create";
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

        public void cargarTipoPersona()
        {
            cboTipop.DataSource = pc.comboTipoPersona();
            cboTipop.DataTextField = "Descripcion";
            cboTipop.DataValueField = "CodTipo";
            cboTipop.DataBind();
        }

        public void CargarRoles()
        {
            string userName = HttpContext.Current.User.Identity.Name;

            dpdRoles.DataValueField = "Id";
            dpdRoles.DataTextField = "Name";
            dpdRoles.DataSource = rolCtrl.GetAll(userName);
            dpdRoles.DataBind();
            dpdRoles.SelectedIndex = 1;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Add();
        }

        public void Add()
        {
            string userName = HttpContext.Current.User.Identity.Name;
            var resultpc = pc.BuscarSiExistePersona(txtCI.Text); //Si existe y esta activo
            if (resultpc == null)
            {
                //si no esta activo
                if (!pc.BuscarSiEstaRegistradoPersona(txtCI.Text))
                {
                    Persona p = new Persona()
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
                        Estado = true,
                        CodTipo = Convert.ToInt32(cboTipop.SelectedValue.ToString()),
                        CategoriaL = "",
                        IdUser = null,
                        UsuaReg = userName,
                        FechaReg = DateTime.Now
                    };

                    bool ok = false;
                    int tipoPersona = Convert.ToInt32(cboTipop.SelectedValue);

                    switch (tipoPersona)
                    {
                        case 1:
                            ok = true;
                            break;

                        case 2:
                            try
                            {
                                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                                var user = new ApplicationUser() { UserName = UserName.Text, Email = txtEmail.Text, EmailConfirmed = true };

                                IdentityResult result = manager.Create(user, Password.Text);

                                if (result.Succeeded)
                                {
                                    string rol = Convert.ToString(dpdRoles.SelectedItem);
                                    manager.AddToRole(user.Id, rol);

                                    p.IdUser = user.Id;
                                    p.Contacto = txtContacto.Text;
                                    p.TelfContacto = txttelefonoc.Text;
                                    ok = true;
                                }
                                else
                                {
                                    ErrorMessage.Text = result.Errors.FirstOrDefault();
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorMessage.Text = ex.Message;
                            }
                            break;

                        case 3:
                            p.Contacto = txtContacto.Text;
                            p.TelfContacto = txttelefonoc.Text;
                            p.CategoriaL = txtlicencia.Text;
                            p.FechaVigL = Convert.ToDateTime(txtfechavigl.Text);
                            p.FechaVigDefL = Convert.ToDateTime(txtfechavigdefl.Text);
                            ok = true;
                            break;
                    }

                    if (ok)
                    {
                        ok = pc.add(p);

                        if (ok == true)
                        {
                            string rol = Convert.ToString(dpdRoles.SelectedItem);

                            if (!rol.Equals("SA"))
                            {
								UsuarioEmpresa ue = new UsuarioEmpresa
								{
									NIT = homeCtrl.obtenerNit(userName),
									CI = txtCI.Text,
									UsuaReg = userName,
									FechaReg = DateTime.Now,
									Activo = false,
									Estado = true
								};

								bool sw = pc.addUsuarioEmpresa(ue);

                                if (sw == true)
                                {
                                    MensajeAlerta("Se registro correctamente");
                                    Response.Redirect("~/Vistas/Personas/Index");
                                }
                            }
                            else
                            {
                                pc.AddAllUsuarioEmpresa(p.CI, userName);
                                Response.Redirect("~/Vistas/Personas/Index");
                            }
                        }
                        else
                        {
                            MensajeAlerta("Datos invalidos");
                        }
                    }

                }
                else
                {
                    //**********************************GM**********************************************
                    Persona p = new Persona()
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
                        Estado = true,
                        CodTipo = Convert.ToInt32(cboTipop.SelectedValue.ToString()),
                        CategoriaL = "",
                        IdUser = null,
                        UsuaReg = userName,
                        FechaReg = DateTime.Now
                    };

                    bool ok = false;
                    int tipoPersona = Convert.ToInt32(cboTipop.SelectedValue);

                    switch (tipoPersona)
                    {
                        case 1:
                            ok = true;
                            break;

                        case 2:
                            try
                            {
                                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                                var user = new ApplicationUser() { UserName = UserName.Text, Email = txtEmail.Text, EmailConfirmed = true };

                                IdentityResult result = manager.Create(user, Password.Text);

                                if (result.Succeeded)
                                {
                                    string rol = Convert.ToString(dpdRoles.SelectedItem);
                                    manager.AddToRole(user.Id, rol);

                                    p.IdUser = user.Id;
                                    p.Contacto = txtContacto.Text;
                                    p.TelfContacto = txttelefonoc.Text;
                                    ok = true;
                                }
                                else
                                {
                                    ErrorMessage.Text = result.Errors.FirstOrDefault();
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorMessage.Text = ex.Message;
                            }
                            break;

                        case 3:
                            p.Contacto = txtContacto.Text;
                            p.TelfContacto = txttelefonoc.Text;
                            p.CategoriaL = txtlicencia.Text;
                            p.FechaVigL = Convert.ToDateTime(txtfechavigl.Text);
                            p.FechaVigDefL = Convert.ToDateTime(txtfechavigdefl.Text);
                            ok = true;
                            break;
                    }

                    if (ok)
                    {
                        ok = pc.ActualizarDatosPersona(p);

                        if (ok == true)
                        {
                            string rol = Convert.ToString(dpdRoles.SelectedItem);

                            if (!rol.Equals("SA"))
                            {
								UsuarioEmpresa ue = new UsuarioEmpresa
								{
									NIT = homeCtrl.obtenerNit(userName),
									CI = txtCI.Text,
									UsuaReg = userName,
									FechaReg = DateTime.Now,
									Activo = false,
									Estado = true
								};

								bool sw = pc.addUsuarioEmpresa(ue);

                                if (sw == true)
                                {
                                    MensajeAlerta("Se registro correctamente");
                                    Response.Redirect("~/Vistas/Personas/Index");
                                }
                            }
                            else
                            {
                                pc.AddAllUsuarioEmpresa(p.CI, userName);
                                Response.Redirect("~/Vistas/Personas/Index");
                            }
                        }
                        else
                        {
                            MensajeAlerta("Datos invalidos");
                        }
                    }

                }
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
    }
}