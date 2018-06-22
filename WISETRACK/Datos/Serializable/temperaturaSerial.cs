using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WISETRACK.Datos.Serializable
{
    public class temperaturaSerial
    {
        public DateTime FechaGPS { get; set; }
        public double Temperatura { get; set; }
        public string NroPlaca { get; set; }
        public float Latitud { get; set; }
        public float Longitud { get; set; }
        public string IDButton { get; set; }
        public string Nombre { get; set; }
        public string EstadoPuerta { get; set; }
        public double Velocidad { get; set; }
        public string IMEI { get; set; }
        public string direcciones { get; set; }

    }

}