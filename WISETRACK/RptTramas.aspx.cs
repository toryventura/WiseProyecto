using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WISETRACK.Controller;
using WISETRACK.Datos;

namespace WISETRACK
{
    public partial class RptTramas : System.Web.UI.Page
    {
        PruebaController pruebaCtrl;

        //public static List<Prueba> pruebas;
        //public static List<int> nros;

        public static List<string> mensajes;

        //public static int contador;

        protected void Page_Load(object sender, EventArgs e)
        {
            pruebaCtrl = new PruebaController();

            if (!IsPostBack)
            {
                //if (!SitePrincipal.IsIntruso())
                //{
                    CargarPrueba();
                //}
                //else
                //    Response.Redirect("~/Account/Login");
            }
        }

        private void CargarPrueba()
        {
            //contador = 0;
            //pruebas = pruebaCtrl.GetAllSA();
            //nros = new List<int>();

            mensajes = new List<string>();

            //foreach (var item in pruebas)
            //    nros.Add(item.Nro);

            //gdvPrueba.DataSource = pruebas;
            //gdvPrueba.DataBind();

            mensajes = pruebaCtrl.GetMensajes();

            foreach (var mensaje in mensajes)
                lstMensajes.Items.Add(mensaje);

        }

        protected void tmrPrincipal_Tick(object sender, EventArgs e)
        {
            //contador++;
            //lblPrincipal.Text = "Tiempo en Ejecucíon: " + GetFormatoTiempo(contador);

            //if (contador <= 60)
            //{
            /*List<int> nros2 = pruebaCtrl.GetNros();

            for (int index = nros2.Count - 1; index >= 0; index--)
            {
                int nro2 = nros2.ElementAt(index);

                if (!nros.Contains(nro2))
                {
                    Prueba prueba = pruebaCtrl.Get(nro2);
                    pruebas.Insert(0, prueba);

                    nros.Insert(0, nro2);
                }

            }*/

            //gdvPrueba.DataSource = pruebas;
            //gdvPrueba.DataBind();

            mensajes = pruebaCtrl.GetMensajes();

            for (int pos = mensajes.Count - 1; pos >= 0; pos--)
            {
                string mensaje = mensajes.ElementAt(pos);
                ListItem item = new ListItem(mensaje);

                if (!lstMensajes.Items.Contains(item))
                    lstMensajes.Items.Insert(0, mensaje);
            }
            //}
            //else
            //    Response.Redirect("/RptTramas");

        }

        //private string GetFormatoTiempo(int tiempo)
        //{
        //    string ret = String.Empty;

        //    if (tiempo < 60)
        //    {
        //        if (tiempo < 10)
        //            ret = "00:00:0" + Convert.ToString(tiempo);
        //        else
        //            ret = "00:00:" + Convert.ToString(tiempo);
        //    }
        //    else
        //    {
        //        int min = tiempo / 60;
        //        int seg = tiempo - (min * 60);

        //        if (min < 60)
        //        {
        //            if (min < 10)
        //                ret = "00:0" + Convert.ToString(min);
        //            else
        //                ret = "00:" + Convert.ToString(min);

        //            if (seg < 10)
        //                ret = ret + ":0" + Convert.ToString(seg);
        //            else
        //                ret = ret + ":" + Convert.ToString(seg);
        //        }
        //        else
        //        {
        //            int hora = min / 60;
        //            int min2 = min - (hora * 60);

        //            if (hora < 10)
        //                ret = "0" + Convert.ToString(hora);
        //            else
        //                ret = Convert.ToString(hora);

        //            if (min2 < 10)
        //                ret = ret + ":0" + Convert.ToString(min2);
        //            else
        //                ret = ret + ":" + Convert.ToString(min2);

        //            if (seg < 10)
        //                ret = ret + ":0" + Convert.ToString(seg);
        //            else
        //                ret = ret + ":" + Convert.ToString(seg);
        //        }
        //    }

        //    return ret;
        //}
    }
}