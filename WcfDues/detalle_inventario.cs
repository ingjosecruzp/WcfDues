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
    
    public partial class detalle_inventario
    {
        public int idDetalle_Inventario { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Codebars { get; set; }
        public decimal Cantidad { get; set; }
        public string NombreLote { get; set; }
        public string PicturName { get; set; }
        public System.DateTime FechaHora { get; set; }
        public int InventarioId { get; set; }
        public int UsuarioId { get; set; }
    
        public virtual inventario inventario { get; set; }
        public virtual usuarios usuarios { get; set; }
    }
}
