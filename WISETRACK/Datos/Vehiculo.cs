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
    
    public partial class Vehiculo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehiculo()
        {
            this.AlarmaVehiculo = new HashSet<AlarmaVehiculo>();
            this.Alerta = new HashSet<Alerta>();
            this.EmpresaVehiculo = new HashSet<EmpresaVehiculo>();
            this.Seguimiento = new HashSet<Seguimiento>();
            this.UsuarioVehiculo = new HashSet<UsuarioVehiculo>();
            this.VehiculoConductor = new HashSet<VehiculoConductor>();
        }
    
        public string NroPlaca { get; set; }
        public string Patente { get; set; }
        public string NroChasis { get; set; }
        public string NroMotor { get; set; }
        public string Modelo { get; set; }
        public int CodTipoV { get; set; }
        public int CodMarca { get; set; }
        public byte[] Foto { get; set; }
        public Nullable<bool> Estado { get; set; }
        public string UsuaReg { get; set; }
        public System.DateTime FechaReg { get; set; }
        public string UsuaModif { get; set; }
        public Nullable<System.DateTime> FechaModif { get; set; }
        public Nullable<int> Año { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlarmaVehiculo> AlarmaVehiculo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alerta> Alerta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmpresaVehiculo> EmpresaVehiculo { get; set; }
        public virtual Marca Marca { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seguimiento> Seguimiento { get; set; }
        public virtual TipoVehiculo TipoVehiculo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioVehiculo> UsuarioVehiculo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehiculoConductor> VehiculoConductor { get; set; }
    }
}
