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
                             join y in bd.Rol on x.IdRol equals y.IdRol
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
                                 x.IdRol,
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
                    res.IdRol = datos.IdRol;
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
                Usuario tabla = new Usuario();
                tabla.Correo = entidad.Correo;
                tabla.Contrasenna = entidad.Contrasenna;
                tabla.Identificacion = entidad.Identificacion;
                tabla.Nombre = entidad.Nombre;
                tabla.Estado = entidad.Estado;
                tabla.IdRol = entidad.IdRol;

                bd.Usuario.Add(tabla);
                return bd.SaveChanges();

            }
            //using (var bd = new SPADBEntities())
            //{
            //    return bd.RegistrarUsuario(entidad.Correo
            //                       , entidad.Contrasenna
            //                       , entidad.Identificacion
            //                       , entidad.Nombre
            //                       , entidad.Estado
            //                       , entidad.IdRol);
            //}
            

        }
    
        [HttpPost]
        [Route("api/RecuperarContrasenna")]
        public bool RecuperarContrasenna(UsuarioEnt entidad)
        {
             using (var bd = new SPADBEntities())
             {
                var datos = (from x in bd.Usuario
                             where x.Correo == entidad.Correo
                                && x.Estado == true
                             select x).FirstOrDefault();
                if(datos != null)
                {
                    string password = util.CreatePassword();
                   // datos.Contrasenna = util.Encrypt(password);
                   // datos.ClaveTemporal = true;
                    //datos.Caducidad = DateTime.Now.AddMinutes(30);
                    //bd.SaveChanges();

                    string mensaje = "Estimado(a) " + datos.Nombre + ". Se ha generado la siguiente contraseña temporal: " + password;
                    util.SendEmail(datos.Correo, "Recuperar Contraseña", mensaje);
                    //return true;

                }
                
                    return false;

             }
            
          

        }
        

    }
}
