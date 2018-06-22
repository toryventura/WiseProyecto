using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WISETRACK.Controller;
using WISETRACK.Datos;
using WISETRACK.Models;

namespace WISETRACK
{
	public class Global : HttpApplication
	{
		RolController rolCtrl;
		PersonaController personaCtrl;

		void Application_Start(object sender, EventArgs e)
		{
			// Código que se ejecuta al iniciar la aplicación
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

			var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

			var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
			um.UserValidator = new UserValidator<ApplicationUser>(um)
			{
				AllowOnlyAlphanumericUserNames = false
			};

			if (!rm.RoleExists("SA"))
			{
				rolCtrl = new RolController();
				personaCtrl = new PersonaController();

				rm.Create(new IdentityRole("SA"));
				var rol = rm.FindByName("SA");

				rolCtrl.AddNivel(rol.Id, 1, true);
				rolCtrl.CargarPrivilegiosSA(rol.Id);

				var user = new ApplicationUser() { UserName = "sistemas", Email = "contacto@e-tech.com.bo" };

				um.Create(user, "Elimelec1*");
				um.AddToRole(user.Id, "SA");

				Persona p = new Persona
				{
					CI = "1",
					Nombre = "Sistemas",
					ApellidoP = "Etech",
					ApellidoM = "Group",
					Direccion = "Av. San Martin 14 Edif. Fragata Piso 3 Of. 1",
					Telefono = "+59133390306",
					Email = "contacto@e-tech.com.bo",
					Contacto = "",
					TelfContacto = "",
					Estado = true,
					CodTipo = 2,
					CategoriaL = "",
					IdUser = user.Id,
					UsuaReg = "sistemas",
					FechaReg = DateTime.Now
				};

				personaCtrl.add(p);
			}

		}
		protected void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = WS.DATA.Formato.Formato1;
		}

	}
}