using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WISETRACK.Datos.Serializable
{
    public class tramaSerial
    {
        //USADO PARA AUDITORIA
        public int ID { get; set; }
        public string IMEI { get; set; }
        public int EstadoGPS { get; set; }
        public float Velocidad { get; set; }
        public float Asimut { get; set; }
        public float Altitud { get; set; }
        public float Longitud { get; set; }
        public float Latitud { get; set; }
        public string TipoMensaje { get; set; }
        public string TipoRespuesta { get; set; }
        public int EstadoMotor { get; set; }
        public string FechaGPS { get; set; }
        public string NroPlaca { get; set; }
        public string IDButton {get;set;}
        public float Kilometraje {get;set;}
        public float Temperatura {get;set;}
        public float VoltajeBateria {get;set;}
        public string EstadoPuerta { get; set; }
        public string direcciones { get; set; }
    }
}