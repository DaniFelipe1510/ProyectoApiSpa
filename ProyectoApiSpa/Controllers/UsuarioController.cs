using ProyectoApiSpa.Entities;
using ProyectoApiSpa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProyectoApiSpa.Controllers
{
    public class UsuarioController : ApiController
    {
        
        UtilitariosModel util = new UtilitariosModel();
        [HttpPost]
        [Route("api/IniciarSesion")]
        public UsuarioEnt IniciarSesion(UsuarioEnt entidad)
        {
            using (var bd = new SPADBEntities())
            {
                var datos = (from x in bd.Usuario
                             join y in bd.Rol on x.Rol equals y.IdRol
                             where x.Correo == entidad.Correo
                                && x.Contrasenna == entidad.Contrasenna
                                && x.Estado == true
                             select new
                             {
                                 //x.ClaveTemporal,
                                 //x.Caducidad,
                                 x.Correo,
                                 x.Identificacion,
                                 x.Nombre,
                                 x.Estado,
                                 x.Rol,
                                 x.IdUsuario,
                                 y.NombreRol
                             }).FirstOrDefault();

                if (datos != null)
                {
                    // if (datos.ClaveTemporal.Value && datos.Caducidad < DateTime.Now)
                    // {
                    //    return null;
                    // }

                    UsuarioEnt res = new UsuarioEnt();
                    res.Correo = datos.Correo;
                    res.Identificacion = datos.Identificacion;
                    res.Nombre = datos.Nombre;
                    res.Estado = datos.Estado;
                    res.Rol = datos.Rol;
                    res.IdUsuario = datos.IdUsuario;
                    res.NombreRol = datos.NombreRol;
                    return res;
                }

                return null;
            }
        }


             
        

        [HttpPost]
        [Route("api/RegistrarUsuario")]
        public int RegistrarUsuario(UsuarioEnt entidad)
        {
            using (var bd = new SPADBEntities())
            {
                return bd.RegistrarUsuario(entidad.Correo
                                   , entidad.Contrasenna
                                   , entidad.Identificacion
                                   , entidad.Nombre
                                   , entidad.Estado
                                   , entidad.Rol);
            }


        }
    /*
        [HttpPost]
        [Route("api/RecuperarContrasenna")]
        public bool RecuperarContrasenna(UsuarioEnt entidad)
        {
             using (var bd = new KN_ProyectoEntities())
             {
                var datos = (from x in bd.Usuario
                             where x.CorreoElectronico == entidad.CorreoElectronico
                                && x.Estado == true
                             select x).FirstOrDefault();
                if(datos != null)
                {
                    string password = util.CreatePassword();
                    datos.Contrasenna = util.Encrypt(password);
                    datos.ClaveTemporal = true;
                    datos.Caducidad = DateTime.Now.AddMinutes(30);
                    bd.SaveChanges();

                    string mensaje = "Estimado(a) " + datos.Nombre + ". Se ha generado la siguiente contraseña temporal: " + password;
                    util.SendEmail(datos.CorreoElectronico, "Recuperar Contraseña", mensaje);
                    return true;

                }
                
                    return false;

             }
            
          

        }
        */

    }
}
