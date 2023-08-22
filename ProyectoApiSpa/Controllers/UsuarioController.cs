using ProyectoApiSpa.App_Start;
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
    [Authorize]
    public class UsuarioController : ApiController
    {

        UtilitariosModel util = new UtilitariosModel();
        TokenGenerator tokGenerator = new TokenGenerator();

        [HttpPost]
        [AllowAnonymous]
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
                                 x.ClaveTemporal,
                                 x.Caducidad,
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
                    if(datos.Caducidad != null && datos.Caducidad != null) { 
                    if (datos.ClaveTemporal.Value && datos.Caducidad < DateTime.Now)
                      {
                          return null;
                      }
                    }

                    UsuarioEnt res = new UsuarioEnt();
                    res.Correo = datos.Correo;
                    res.Identificacion = datos.Identificacion;
                    res.Nombre = datos.Nombre;
                    res.Estado = datos.Estado;
                    res.IdRol = datos.IdRol;
                    res.IdUsuario = datos.IdUsuario;
                    res.NombreRol = datos.NombreRol;
                    res.Token = tokGenerator.GenerateTokenJwt(datos.IdUsuario);

                    return res;
                }

                return null;
            }
        }

        [HttpPost]
        [AllowAnonymous]
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


        [HttpGet]
        [Route("api/ConsultarUsuarios")]
        public List<UsuarioEnt> ConsultarUsuarios()
        {
            using (var bd = new SPADBEntities())
            {
                var datos = (from x in bd.Usuario
                             select x).ToList();
                if (datos.Count > 0)
                {
                    List<UsuarioEnt> res = new List<UsuarioEnt>();
                    foreach (var item in datos)
                    {
                        res.Add(new UsuarioEnt

                        {
                            Correo = item.Correo,
                            Identificacion = item.Identificacion,
                            Nombre = item.Nombre,
                            Estado = item.Estado,
                            IdRol = item.IdRol,
                            IdUsuario = item.IdUsuario
                        });
                    }
                    return res;
                }

                return new List<UsuarioEnt>();
            }

            /*using (var bd = new KN_ProyectoEntities())
            {
                return bd.IniciarSesion(entidad.CorreoElectronico, entidad.Contrasenna).FirstOrDefault();
            }
            */
        }

        [HttpGet]
        [Route("api/ConsultarUsuario")]
        public UsuarioEnt ConsultarUsuario(long q)
        {
            using (var bd = new SPADBEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.IdUsuario == q
                             select x).FirstOrDefault();
                if (datos != null)
                {
                    UsuarioEnt res = new UsuarioEnt();
                    res.Correo = datos.Correo;
                    res.Identificacion = datos.Identificacion;
                    res.Nombre = datos.Nombre;
                    res.Estado = datos.Estado;
                    res.IdRol = datos.IdRol;
                    res.IdUsuario = datos.IdUsuario;

                    return res;

                }

                return null;

            }

            /*using (var bd = new KN_ProyectoEntities())
            {
                return bd.IniciarSesion(entidad.CorreoElectronico, entidad.Contrasenna).FirstOrDefault();
            }
            */
        }

        [HttpGet]
        [Route("api/ConsultarRoles")]
        public List<RolEnt> ConsultarRoles()
        {
            using (var bd = new SPADBEntities())
            {
                var datos = (from x in bd.Rol
                             select x).ToList();
                if (datos.Count > 0)
                {
                    List<RolEnt> res = new List<RolEnt>();
                    foreach (var item in datos)
                    {
                        res.Add(new RolEnt

                        {
                            IdRol = item.IdRol,
                            NombreRol = item.NombreRol,

                        });
                    }
                    return res;
                }

                return new List<RolEnt>();
            }

            /*using (var bd = new KN_ProyectoEntities())
            {
                return bd.IniciarSesion(entidad.CorreoElectronico, entidad.Contrasenna).FirstOrDefault();
            }
            */
        }

        [HttpPut]
        [Route("api/EditarUsuario")]
        public int EditarUsuario(UsuarioEnt entidad)
        {

            using (var bd = new SPADBEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.IdUsuario == entidad.IdUsuario
                             select x).FirstOrDefault();
                if (datos != null)
                {
                    datos.Correo = entidad.Correo;
                    datos.IdRol = entidad.IdRol;
                    return bd.SaveChanges();
                }
                return 0;
            }
        }

        [HttpPut]
        [Route("api/CambiarEstado")]
        public int CambiarEstado(UsuarioEnt entidad)
        {

            using (var bd = new SPADBEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.IdUsuario == entidad.IdUsuario
                             select x).FirstOrDefault();
                if (datos != null)
                {
                    var EstadoActual = datos.Estado;
                    datos.Estado = (EstadoActual == true ? false : true);
                    return bd.SaveChanges();
                }
                return 0;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/RecuperarContrasenna")]
        public bool RecuperarContrasenna(UsuarioEnt entidad)
        {
            using (var bd = new SPADBEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.Correo == entidad.Correo
                                && x.Estado == true
                             select x).FirstOrDefault();
                if (datos != null)
                {
                    string password = util.CreatePassword();
                    datos.Contrasenna = util.Encrypt(password);
                    datos.ClaveTemporal = true;
                    datos.Caducidad = DateTime.Now.AddMinutes(30);
                    bd.SaveChanges();

                    string mensaje = "Estimado(a) " + datos.Nombre + ". Se ha generado la siguiente contraseña temporal: " + password;
                    util.SendEmail(datos.Correo, "Recuperar Contraseña", mensaje);
                    return true;

                }

                return false;

            }



        }

        [HttpPut]
        [AllowAnonymous]
        [Route("api/CambiarContrasenna")]
        public int CambiarContrasenna(UsuarioEnt entidad)
        {
            
                using (var bd = new SPADBEntities())
                {
                    var datos = (from x in bd.Usuario
                                 where x.IdUsuario == entidad.IdUsuario
                                    && x.Estado == true
                                 select x).FirstOrDefault();
                    if (datos != null)
                    {
                        datos.Contrasenna = entidad.ContrasennaNueva;
                        datos.ClaveTemporal = false;
                        datos.Caducidad = DateTime.Now;
                        return bd.SaveChanges();

                    }
                    return 0;
                }

            }
        }
}
