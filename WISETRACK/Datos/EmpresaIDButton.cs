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
    
    public partial class EmpresaIDButton
    {
        public int ID { get; set; }
        public int CodButton { get; set; }
        public string Nit { get; set; }
        public Nullable<System.DateTime> FechaReg { get; set; }
        public string UsuarioReg { get; set; }
        public Nullable<System.DateTime> FechaMod { get; set; }
        public string UsuarioMod { get; set; }
        public Nullable<bool> Estado { get; set; }
    
        public virtual Empresa Empresa { get; set; }
        public virtual IdButton IdButton { get; set; }
    }
}
