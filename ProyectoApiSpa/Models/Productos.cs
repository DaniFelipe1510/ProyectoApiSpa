//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoApiSpa.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Productos
    {
        public long IDProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
    
        public virtual Empleado Empleado { get; set; }
        public virtual Inventario Inventario { get; set; }
    }
}
