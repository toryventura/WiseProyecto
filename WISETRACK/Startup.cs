using Microsoft.Owin;
using Owin;
using System;
using System.Threading;
using WS.DATA;

[assembly: OwinStartupAttribute(typeof(WISETRACK.Startup))]
namespace WISETRACK
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{

			ConfigureAuth(app);
		}
		
	}
}
