using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoApiSpa.Entities
{
    public class ProductoEnt
    {
        public long IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
    }
}