//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoApiSpa.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Carrito
    {
        public long IdCarrito { get; set; }
        public long IdProducto { get; set; }
        public long IdUsuario { get; set; }
        public System.DateTime FechaCarrito { get; set; }
        public Nullable<decimal> CantidadArticulos { get; set; }
    
        public virtual Producto Producto { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
