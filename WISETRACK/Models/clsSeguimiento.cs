using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WISETRACK.Models
{
    public class clsSeguimiento
    {
        public int CodSeguimiento { get; set; }
        public string IMEI { get; set; }
        public string NroPlaca { get; set; }
        public string RazonSocial { get; set; }
        public string Modelo { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string estado { get; set; }
    }
}