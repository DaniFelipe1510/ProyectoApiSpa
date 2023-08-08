using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoApiSpa.Entities
{
    public class CarritoEnt
    {
        public long IdCarrito { get; set; }
        public long IdUsuario { get; set; }
        public long IdProducto { get; set; }
        public decimal Precio { get; set; }
        public decimal CantidadArticulos { get; set; }

        public DateTime FechaCarrito { get; set; }

        public string Nombre { get; set; }
    }
}