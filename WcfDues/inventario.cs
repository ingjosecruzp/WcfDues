//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfDues
{
    using System;
    using System.Collections.Generic;
    
    public partial class inventario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public inventario()
        {
            this.detalle_inventario = new HashSet<detalle_inventario>();
        }
    
        public int idInventario { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public int UsuarioId { get; set; }
        public string UUID { get; set; }
        public string Modelo { get; set; }
        public string Estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_inventario> detalle_inventario { get; set; }
        public virtual usuarios usuarios { get; set; }
    }
}