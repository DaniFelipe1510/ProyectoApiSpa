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
    
    public partial class Reserva
    {
        public long IdReserva { get; set; }
        public long IdServicio { get; set; }
        public long IdUsuario { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.TimeSpan Hora { get; set; }
        public decimal PrecioPago { get; set; }
    
        public virtual Servicio Servicio { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
