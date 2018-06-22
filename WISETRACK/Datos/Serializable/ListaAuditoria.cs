using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WISETRACK.Datos.Serializable
{
    public class ListaAuditoria
    {
        public string Patente { get; set; }
        public string NroPlaca { get; set; }
        public string FechaIni { get; set; }
        public string HoraIni { get; set; }
        public string FechaFin { get; set; }
        public string HoraFin { get; set; }
        public string tipo { get; set; }
        public string valor {get;set;}


    }
}