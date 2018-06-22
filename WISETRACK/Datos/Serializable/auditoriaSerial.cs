using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WISETRACK.Datos.Serializable
{
    [Serializable]
    public class auditoriaSerial
    {
        public int ID { get; set; }
        public string IMEI { get; set; }
        public int EstadoGPS { get; set; }
        public double Velocidad { get; set; }
        public double Asimut { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public double Altitud { get; set; }
        public string TipoMensaje { get; set; }
        public string TipoRespuesta { get; set; }
        public int EstadoMotor { get; set; }
        public System.DateTime FechaGPS { get; set; }
        //public System.DateTime FechaEnvio { get; set; }
        //public System.DateTime FechaReg { get; set; }
    }
}