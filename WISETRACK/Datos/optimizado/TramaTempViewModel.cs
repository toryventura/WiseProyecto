using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WISETRACK.Datos.optimizado
{
    public class TramaTempViewModel
    {
        public int Nro { get; set; }
        public long ID { get; set; }
        public string EstadoGPS { get; set; }
        public int EstadoMotor { get; set; }
        public double Velocidad { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public DateTime FechaGPS { get; set; }
        public string NroPlaca { get; set; }
        public string Nombre { get; set; }
        public double Asimut { get; set; }
        public string NIT { get; set; }
        public string IDButton { get; set; }
        public double  Kilometraje { get; set; }
        public double Temperatura { get; set; }
        public double  VoltajeBateria { get; set; }
        public string EstadoPuerta { get; set; }
        public int tipov { get; set; }
        public string IMEI { get; set; }
        public string direcciones { get; set; }
        public string Patente { get; set; }
    }
}