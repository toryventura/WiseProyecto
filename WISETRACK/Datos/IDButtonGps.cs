//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WISETRACK.Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class IDButtonGps
    {
        public int ID { get; set; }
        public string IMEI { get; set; }
        public int CODIDButton { get; set; }
        public Nullable<bool> Estado { get; set; }
        public Nullable<System.DateTime> FechaReg { get; set; }
        public string UsuReg { get; set; }
    
        public virtual GPS GPS { get; set; }
        public virtual IdButton IdButton { get; set; }
    }
}
