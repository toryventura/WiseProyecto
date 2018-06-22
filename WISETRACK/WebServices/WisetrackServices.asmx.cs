using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using WISETRACK.Controller;
using WISETRACK.Datos;
using WISETRACK.Datos.Auxiliar;
using WISETRACK.Datos.optimizado;
using WISETRACK.Datos.Serializable;

namespace WISETRACK.WebServices
{
    /// <summary>
    /// Descripción breve de WisetrackServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WisetrackServices : System.Web.Services.WebService
    {
        private SeguimientoController seguimientoControl = new SeguimientoController();
        WISETRACKEntities bd = new WISETRACKEntities();
        private ZonasController zonasControl = new ZonasController();
        private HomeController homeControl = new HomeController();
        private VehiculoController vehiculoCtrl = new VehiculoController();
        private ReporteController reporteCtrl = new ReporteController();
        private PersonaController personaCtrl = new PersonaController();
        private GpsController gpsCtrl = new GpsController();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

         //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public string ListarSeguimiento(string placa)
        {
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
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json ) ]
        public string ListarSeguimiento1()
        {
            var user = HttpContext.Current.User.Identity.Name;
            string nit = homeControl.obtenerNit(user);
            string data = String.Empty;
            string placa = "TODAS";
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
        //public string Tramatemp()
        //{
        //    var 
        //}

        private string ListarSeguimientoTodos(string nit, string data)
        {
            if (User.IsInRole("SA"))
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
                if (User.IsInRole("SUPERVISOR"))
                {
                    var user = User.Identity.Name;
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



        [WebMethod]
        public string obtenerAuditoriaOptimizado(string fini, string hini, string ffin, string hfin, string placa, string cod, string velocidad)
        {
            AuditoriaController auditoriaControl = new AuditoriaController();
            List<sp_obtenerAuditoriaOptimizado_Result> list = auditoriaControl.obtenerAuditoriaOptimizada(fini, hini, ffin, hfin, placa).ToList();

            //foreach (var item in collection)
            //{
            //    TramaTempViewModel tr = new TramaTempViewModel();
            //    tr.Asimut = item.Asimut;
            //    tr.direcciones = item.direcciones;
            //    tr.EstadoGPS = item.EstadoGPS.ToString();
            //    tr.EstadoMotor = item.EstadoMotor;
            //    tr.EstadoPuerta = item.EstadoPuerta;
            //    tr.FechaGPS = item.FechaGPS;
            //    tr.ID = item.ID;
            //    tr.IDButton = item.IDButton;
            //    tr.IMEI = item.IMEI;
            //    tr.Kilometraje = item.Kilometraje;
            //    tr.Latitud = Convert.ToDouble(item.Latitud);
            //    tr.Longitud = Convert.ToDouble(item.Longitud);
            //    tr.NIT = 
            //}

            if (!String.IsNullOrEmpty(velocidad))
            {
                switch (cod)
                {
                    case "1":
                        //igual
                        list = list.Where(det => det.Velocidad == Convert.ToDouble(velocidad)).ToList();
                        break;
                    case "2":
                        //mayor
                        list = list.Where(det => det.Velocidad > Convert.ToDouble(velocidad)).ToList();
                        break;
                    case "3":
                        //mayor igual
                        list = list.Where(det => det.Velocidad >= Convert.ToDouble(velocidad)).ToList();
                        break;
                    //default:
                    //    break;
                }
            }
            string data = JsonConvert.SerializeObject(list, Formatting.Indented);
            return data;
        }

        [WebMethod]
        public string enviarGeocerca(string geocerca, string puntosgeo)
        {
            List<geocercaSerial> lista = JsonConvert.DeserializeObject<List<geocercaSerial>>(geocerca);
            List<puntosgeoSerial> listag = JsonConvert.DeserializeObject<List<puntosgeoSerial>>(puntosgeo);
            zonasControl.guardarGeocerca(lista, listag);
            return "Datos guardados con éxito";
        }

        [WebMethod]
        public string pintarGeocerca(string id)
        {
            string data = String.Empty;
            if (id.Equals("")) return "Por favor, Seleccione una Geocerca";

            int cod = Convert.ToInt32(id);
            if (cod > 0)
            {
                List<sp_reporteGeocerca_Result> list = zonasControl.exportarGeocerca(cod);
                data = JsonConvert.SerializeObject(list, Formatting.Indented);

            }
            else
            {
                data = "Por favor, Seleccione una Geocerca";
            }

            return data;
        }

        [WebMethod]
        [HttpPost]
        public string verFoto(string placa)
        {
            string data = String.Empty;
            if (!String.IsNullOrEmpty(placa))
            {
                List<sp_ListarFoto_Result> se = bd.sp_ListarFoto(placa).ToList();
                data = JsonConvert.SerializeObject(se, Formatting.Indented);
            }
            return data;
        }

        [WebMethod]
        public string ActualizarGeocerca(string geocercaID, string puntosgeo)
        {
            List<puntosgeoSerial> listag = JsonConvert.DeserializeObject<List<puntosgeoSerial>>(puntosgeo);
            zonasControl.ModificarGeocerca(geocercaID, listag);
            return "Datos modificados con éxito";
        }

        [WebMethod]
        public string ListarReporteTemperatura()
        {
            List<temperaturaSerial> lista = reporteCtrl.listarReporteTemperatura("20/07/2016", "00:00", "21/07/2016", "16:55", "6735VG2");
            string data = JsonConvert.SerializeObject(lista, Formatting.Indented);
            return data;
        }

        [WebMethod]
        public string VisualizarTodasGeocercas()
        {
            var user = HttpContext.Current.User.Identity.Name;
            string nit = homeControl.obtenerNit(user);
            string data = String.Empty;

            if (!string.IsNullOrEmpty(nit))
            {
                List<sp_ListarGeocercaAll_Result> lista = zonasControl.VisualizarTodasGeocercas(nit);
                data = JsonConvert.SerializeObject(lista, Formatting.Indented);
            }
            else
            {
                if (User.IsInRole("SA"))
                {
                    //Si es SA le envio nit 0 
                    List<sp_ListarGeocercaAll_Result> lista = zonasControl.VisualizarTodasGeocercas("0");
                    data = JsonConvert.SerializeObject(lista, Formatting.Indented);
                }
            }
            return data;
        }

        [WebMethod]
        public string LimpiarAuditoria()
        {
            FrmAuditoria.listaAudit = new List<ListaAuditoria>();
            return "OK";
        }

        [WebMethod]
        public string BuscarSiExisteGPS(string imei)
        {
            string data = string.Empty;
            DataGPS g = gpsCtrl.BuscarSiExisteGps(imei);
            if (g != null)
            {
                data = "1";
            }
            else
            {
                data = "0";
            }
            return data;
        }

        [WebMethod]
        public string BuscarSiExistePersona(string ci)
        {
            string data = string.Empty;
            DataPersona p = personaCtrl.BuscarSiExistePersona(ci);
            if (p != null)
            {
                data = "1";
            }
            else
            {
                data = "0";
            }
            return data;
        }
    }
}
