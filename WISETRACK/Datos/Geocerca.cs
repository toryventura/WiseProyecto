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
    
    public partial class Geocerca
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Geocerca()
        {
            this.AlarmaGeocerca = new HashSet<AlarmaGeocerca>();
            this.PuntosGeocerca = new HashSet<PuntosGeocerca>();
        }
    
        public int CodigoGEO { get; set; }
        public string Descripcion { get; set; }
        public string ColorLimite { get; set; }
        public string ColorRelleno { get; set; }
        public int CodTipoGEO { get; set; }
        public string UsuaReg { get; set; }
        public System.DateTime FechaReg { get; set; }
        public string UsuaModif { get; set; }
        public Nullable<System.DateTime> FechaModif { get; set; }
        public string NIT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlarmaGeocerca> AlarmaGeocerca { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual TipoGeocerca TipoGeocerca { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PuntosGeocerca> PuntosGeocerca { get; set; }
    }
}
