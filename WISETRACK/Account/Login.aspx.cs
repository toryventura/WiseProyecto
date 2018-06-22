
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.UI;
using WISETRACK.Controller;
namespace WISETRACK.Account
{
	public partial class Login : Page
	{
		UsuarioController usuarioCtrl;


		protected void Page_Load(object sender, EventArgs e)
		{
			usuarioCtrl = new UsuarioController();

			RegisterHyperLink.NavigateUrl = "Register";
			// Habilite esta opción una vez tenga la confirmación de la cuenta habilitada para la funcionalidad de restablecimiento de contraseña
			ForgotPasswordHyperLink.NavigateUrl = "Forgot";
			//OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
			var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
			if (!String.IsNullOrEmpty(returnUrl))
			{
				RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
			}
		}

		protected void SendEmailConfirmationToken(object sender, EventArgs e)
		{
			var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

			var user = manager.FindByName(UserName.Text);
			if (user != null)
			{
				if (!user.EmailConfirmed)
				{
					string code = manager.GenerateEmailConfirmationToken(user.Id);
					//string code = manager.GenerateEmailConfirmationToken(user.Id);
					string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
					manager.SendEmailAsync(user.Id, "Confirme su cuenta", "Confirme su cuenta haciendo clic en <a href=\"" + callbackUrl + "\">aqui</a>.");

					FailureText.Text = "Correo de confirmacion enviado. Consulte el correo electronico y confirme su cuenta.";
					ErrorMessage.Visible = true;
					ResendConfirm.Visible = false;
				}
			}
		}
		public string base64Decode(string sData) //Decode    
		{
			try
			{
				var encoder = new System.Text.UTF8Encoding();
				System.Text.Decoder utf8Decode = encoder.GetDecoder();
				byte[] todecodeByte = Convert.FromBase64String(sData);
				int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
				char[] decodedChar = new char[charCount];
				utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
				string result = new String(decodedChar);
				return result;
			}
			catch (Exception ex)
			{
				throw new Exception("Error in base64Decode" + ex.Message);
			}
		}

		protected void LogIn(object sender, EventArgs e)
		{
			if (IsValid)
			{
				// Validar la contraseña del usuario
				var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

				var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

				// Esto no cuenta los errores de inicio de sesión hacia el bloqueo de cuenta
				// Para habilitar los errores de contraseña para desencadenar el bloqueo, cambie a shouldLockout: true
				// Require the user to have a confirmed email before they can log on.

				var user = manager.FindByName(UserName.Text);
				//var user = manager.FindByName(UserName.Text);
				if (user != null)
				{

					if (!user.EmailConfirmed)
					{
						FailureText.Text = "Intentos de inicio de sesión no válidos. Debe tener una cuenta de correo electrónico confirmada.";
						ErrorMessage.Visible = true;
						ResendConfirm.Visible = true;
					}
					else
					{
						if (usuarioCtrl.AsignPersona(UserName.Text))
						{
							var result = signinManager.PasswordSignIn(UserName.Text, Password.Text, RememberMe.Checked, shouldLockout: false);
							//var passwordHash = manager.PasswordHasher.HashPassword(Password.Text);
							//var pas = Utilsss.HashPassword(Password.Text);
							///"AAKshMpqyvk8UVfleZtH9hNzxau5nZGb01P3qVuaMcsP3v//CjSpPzKhPb51V8G2eQ==")

							switch (result)
							{
								case SignInStatus.Success:
									Response.Redirect("~/Vistas/Empresas/Index");
									break;
								case SignInStatus.LockedOut:
									Response.Redirect("/Account/Lockout");
									break;
								case SignInStatus.RequiresVerification:
									Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
																	Request.QueryString["ReturnUrl"],
																	RememberMe.Checked),
													  true);
									break;
								case SignInStatus.Failure:
								default:
									FailureText.Text = "Intento de inicio de sesión no válido";
									ErrorMessage.Visible = true;
									break;
							}
						}
						else
						{
							FailureText.Text = "Usuario sin Persona Asignada. Contáctese con el Administrador del Sistema";
							ErrorMessage.Visible = true;
						}
					}
				}
			}

		}
	}
}
