using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WISETRACK.Datos.Serializable
{
    [Serializable]
    public class seguimientoSerial 
    {
        public int ID { get; set; }
        public int EstadoGPS { get; set; }
        public int EstadoMotor { get; set; }
        public double Velocidad { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public System.DateTime FechaGPS { get; set; }
        public string NroPlaca { get; set; }
        public string Nombre { get; set; }
    }
}