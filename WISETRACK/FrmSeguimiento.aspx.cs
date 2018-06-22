using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WISETRACK.Controller;
using WISETRACK.Datos.optimizado;

namespace WISETRACK
{
    public partial class FrmSeguimiento2 : System.Web.UI.Page
    {
        private SeguimientoController controler;
        private HomeController homeControl;
        private VehiculoController vehiculoCtrl;
        private PersonaController personaCtrl;

        [WebMethod]
        public static string ListarSeguimiento(string placa)
        {
            SeguimientoController seguimientoControl = new SeguimientoController();
            HomeController homeControl = new HomeController();
            var user = HttpContext.Current.User.Identity.Name;
            string nit = homeControl.obtenerNit(user);
            string data = String.Empty;
            switch (placa)
            {
                case "TODAS":
                    data = ListarSeguimientoTodos(nit, data);
                    break;
                case "todas":
                    data = ListarSeguimientoTodos(nit, data);
                    break;
                case "":
                    data = ListarSeguimientoTodos(nit, data);
                    break;
                default:
                    List<TramaTempViewModel> lista = seguimientoControl.ListarSeguimientoByPlaca(placa);
                    data = JsonConvert.SerializeObject(lista, Formatting.Indented);
                    break;
            }
            return data;
        }
        private static string ListarSeguimientoTodos(string nit, string data)
        {
            SeguimientoController seguimientoControl = new SeguimientoController();
            
            if (HttpContext.Current.User.IsInRole("SA"))
            {
                if (!SitePrincipal.ExisteActiva())
                {
                    List<TramaTempViewModel> lista = seguimientoControl.ListarSeguimientoSistema();
                    data = JsonConvert.SerializeObject(lista, Formatting.Indented);
                }
                else
                {
                    List<TramaTempViewModel> lista = seguimientoControl.ListarSeguimientoByNit(nit);
                    data = JsonConvert.SerializeObject(lista, Formatting.Indented);
                }
            }
            else
            {
                if (HttpContext.Current.User.IsInRole("SUPERVISOR"))
                {
                    PersonaController personaCtrl = new PersonaController();
                    var user =HttpContext.Current.User.Identity.Name;
                    var collection = seguimientoControl.ListarSeguimientoByNit(nit);
                    var resultvehiculo = personaCtrl.ObtenerVehiculosAsociadosPersonal(user);
                    List<TramaTempViewModel> listaopc = new List<TramaTempViewModel>();
                    listaopc = (from c in collection
                                join rv in resultvehiculo on
                                    c.NroPlaca equals rv.NroPlaca
                                select c).ToList();
                    data = JsonConvert.SerializeObject(listaopc, Formatting.Indented);

                }
                else
                {
                    List<TramaTempViewModel> lista = seguimientoControl.ListarSeguimientoByNit(nit);
                    data = JsonConvert.SerializeObject(lista, Formatting.Indented);
                }
            }
            return data;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            controler = new SeguimientoController();
            homeControl = new HomeController();
            vehiculoCtrl = new VehiculoController();
            personaCtrl = new PersonaController();

            if (!IsPostBack)
            {
                if (!SitePrincipal.IsIntruso())
                {
                    cargarVehiculo();
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        public void cargarVehiculo()
        {
            var user = HttpContext.Current.User.Identity.Name;
            string nit = homeControl.obtenerNit(user);
            if (HttpContext.Current.User.IsInRole("SA"))
            {
                if (SitePrincipal.ExisteActiva())
                {
                    //var user = HttpContext.Current.User.Identity.Name;
                    //string nit = homeControl.obtenerNit(user);
                    cboplaca.DataSource = controler.comboVehiculo(nit);
                    cboplaca.DataTextField = "Patente";
                    cboplaca.DataValueField = "NroPlaca";
                    cboplaca.DataBind();
                    cboplaca.Items.Insert(0, "todas");
                }
                else
                {
                    cboplaca.DataSource = vehiculoCtrl.cargarDetalleVehiculosSA();
                    cboplaca.DataTextField = "Patente";
                    cboplaca.DataValueField = "NroPlaca";
                    cboplaca.DataBind();
                    cboplaca.Items.Insert(0, "todas");
                }
            }
            else
            {
                if (HttpContext.Current.User.IsInRole("SUPERVISOR"))
                {
                    //var user = HttpContext.Current.User.Identity.Name;
                    cboplaca.DataSource = personaCtrl.ObtenerVehiculosAsociadosPersonal(user);
                    cboplaca.DataTextField = "Patente";
                    cboplaca.DataValueField = "NroPlaca";
                    cboplaca.DataBind();
                    cboplaca.Items.Insert(0, "todas");
                }
                else
                {
                    //var user = HttpContext.Current.User.Identity.Name;
                    //string nit = homeControl.obtenerNit(user);
                    cboplaca.DataSource = controler.comboVehiculo(nit);
                    cboplaca.DataTextField = "Patente";
                    cboplaca.DataValueField = "NroPlaca";
                    cboplaca.DataBind();
                    cboplaca.Items.Insert(0, "todas");
                }
            }
        }

        //protected void cboplaca_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        //{
        //    e.Item.Text = string.Concat(e.Item.Text.Split(' ')[0], "");
        //}


    }
}