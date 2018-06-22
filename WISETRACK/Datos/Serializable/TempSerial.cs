using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WISETRACK.Datos.Serializable
{
    public class TempSerial
    {
        public int Nro { get; set; }
        public int ID { get; set; }
        public int EstadoGPS { get; set; }
        public int EstadoMotor { get; set; }
        public float Velocidad { get; set; }
        public float Latitud { get; set; }
        public float Longitud { get; set; }
        public string FechaGPS { get; set; }
        public string NroPlaca { get; set; }
        public string Nombre { get; set; }
        public float Asimut { get; set; }
        public string NIT { get; set; }
        public string IDButton { get; set; }
        public float Kilometraje { get; set; }
        public float Temperatura { get; set; }
        public float VoltajeBateria { get; set; }
        public string EstadoPuerta { get; set; }
        public int tipov { get; set; }
        public string direcciones { get; set; }
    }
}