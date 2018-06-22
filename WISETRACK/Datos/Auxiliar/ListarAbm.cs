using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WISETRACK.Datos.Auxiliar
{
    public class DataGPS
    {
        public string IMEI { get; set; }
        public string ID { get; set; }
        public string NroTelefono { get; set; }
        public string Modelo { get; set; }
    }

    public class DataPersona
    {
        public string CI { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
    }

    public class ListarAbmGPS
    {
        public string nit { get; set; }
        public string razon_social { get; set; }
        public string IMEI { get; set; }
        public string ID { get; set; }
        public decimal NroTelefono { get; set; }
        public string Modelo { get; set; }
        public string estado { get; set; }
    }
    public class ListarGPSPlaca
    {
        public string nit { get; set; }
        public string IMEI { get; set; }
        public string ID { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
    }

    public class ListarAbmPersona
    {
        public string nit { get; set; }
        public string razon_social { get; set; }
        public string CI { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string estado { get; set; }
    }

    public class ListarUsuarioVehiculo
    {
        public int id { get; set; }
        public string ci { get; set; }
        public string nroplaca { get; set; }
        public string nit { get; set; }
        public string usuario { get; set; }
        public string empresa { get; set; }
    }

    public class ListarAbmUsuario
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public string Persona { get; set; }
    }


}