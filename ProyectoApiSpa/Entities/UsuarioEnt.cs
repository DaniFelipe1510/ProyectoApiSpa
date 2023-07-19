using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoApiSpa.Entities
{
    public class UsuarioEnt
    {
        public long IdUsuario { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasenna { get; set; }
        public string Confirmarcontrasenna { get; set; }
    }
}