using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoApiSpa.Entities
{
    public class ServicioEnt
    {
        public long IdServicio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Duracion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
    }
}