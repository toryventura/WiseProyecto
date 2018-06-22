using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using WISETRACK.Controller;
using WISETRACK.Models;

namespace WISETRACK.Account
{
	public partial class Register : Page
	{
		RolController rolCtrl;

		protected void CreateUser_Click(object sender, EventArgs e)
		{
			var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
			var user = new ApplicationUser() { UserName = UserName.Text, Email = Email.Text, EmailConfirmed = true };


			IdentityResult result = manager.Create(user, Password.Text);

	


			if (result.Succeeded)
			{
				// Para obtener más información sobre cómo habilitar la confirmación de cuentas y el restablecimiento de contraseña, visite http://go.microsoft.com/fwlink/?LinkID=320771
				string code = manager.GenerateEmailConfirmationToken(user.Id);
				string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
				manager.SendEmail(user.Id, "Confirmar cuenta", "Para confirmar la cuenta, haga clic <a href=\"" + callbackUrl + "\">aquí</a>.");

				if (user.EmailConfirmed)
				{
					string rol = Convert.ToString(dpdRoles.SelectedItem);
					manager.AddToRole(user.Id, rol);
					Response.Redirect("~/Vistas/Usuarios/Index");

					//IdentityHelper.SignIn(manager, user, isPersistent: false);
					//IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
				}
				else
				{
					ErrorMessage.Text = "Se ha enviado un correo electrónico a su cuenta. Consulte el correo electrónico y confirme su cuenta para completar el proceso de registro.";
				}

				//IdentityHelper.SignIn(manager, user, isPersistent: false);
			}
			else
			{
				AddErrors(result);
				ErrorMessage.Text = result.Errors.FirstOrDefault();
			}
		}
		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				if (error.EndsWith("is already taken."))
					ModelState.AddModelError("", "User with the given email address already exists");
				else ModelState.AddModelError("", error);
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			rolCtrl = new RolController();
			string userName = HttpContext.Current.User.Identity.Name;

			if (!IsPostBack)
			{
				if (!SitePrincipal.IsIntruso())
				{
					dpdRoles.DataValueField = "Id";
					dpdRoles.DataTextField = "Name";
					dpdRoles.DataSource = rolCtrl.GetAll(userName);
					dpdRoles.DataBind();
				}
				else
					Response.Redirect("~/Account/Login");
			}

		}
	}
}