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
        public string Correo { get; set; }
        public string Contrasenna { get; set; }
        public string Confirmarcontrasenna { get; set; }
        public string NombreRol { get; set; }
        public bool Estado { get; set; }
        public int IdRol { get; set; }

        public string Token { get; set; }

        public string ContrasennaNueva { get; set; }
    }
}