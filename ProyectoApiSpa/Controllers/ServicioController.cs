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
    public class ServicioController : ApiController
    {
        [HttpGet]
        [Route("api/ConsultarServicios")]
        public List<ServicioEnt> ConsultarServicios()
        {
            using (var bd = new SPADBEntities())
            {
                var datos = (from x in bd.Servicio
                             select x).ToList();

                if (datos.Count > 0)
                {
                    List<ServicioEnt> res = new List<ServicioEnt>();
                    foreach (var item in datos)
                    {
                        res.Add(new ServicioEnt
                        {
                            IdServicio = item.IdServicio,
                            Nombre = item.Nombre,
                            Descripcion = item.Descripcion,
                            Duracion = item.Duracion,
                            Precio = item.Precio,
                            Imagen = item.Imagen
                        });
                    }

                    return res;
                }

                return new List<ServicioEnt>();
            }
        }

        [HttpGet]
        [Route("api/ConsultarServicio")]
        public ServicioEnt ConsultarServicio(long q)
        {
            using (var bd = new SPADBEntities())
            {
                var datos = (from x in bd.Servicio
                             where x.IdServicio == q
                             select x).FirstOrDefault();
                if (datos != null)
                {
                    ServicioEnt res = new ServicioEnt();
                    res.IdServicio = datos.IdServicio;
                    res.Nombre = datos.Nombre;
                    res.Descripcion = datos.Descripcion;
                    res.Duracion = datos.Duracion;
                    res.Precio = datos.Precio;
                    res.Imagen = datos.Imagen;
                    return res;
                }

                return null;
            }
        }

        [HttpPost]
        [Route("api/RegistrarServicio")]
        public long RegistrarServicios(ReservaEnt entidad)
        {
            using (var bd = new SPADBEntities())
            {
                Reserva tabla = new Reserva();
                tabla.IdServicio = entidad.IdServicio;
                tabla.IdUsuario = entidad.IdUsuario;
                tabla.Fecha = entidad.Fecha;
                tabla.Hora = entidad.Hora;
                tabla.PrecioPago = entidad.PrecioPago;
                tabla.Cliente = entidad.Cliente;

                bd.Reserva.Add(tabla);
                bd.SaveChanges();

                return tabla.IdReserva;

            }


        }

        [HttpGet]
        [Route("api/ConsultarMisReservas")]
        public List<ReservaEnt> ConsultarMisReservas(long q)
        {
            using (var bd = new SPADBEntities())
            {
                var datos = (from x in bd.Reserva
                             join y in bd.Servicio on x.IdServicio equals y.IdServicio
                             where x.IdUsuario == q
                             select new
                             {
                                 x.IdReserva,
                                 x.IdServicio,
                                 x.IdUsuario,
                                 x.PrecioPago,
                                 x.Fecha,
                                 x.Hora,
                                 x.Cliente,
                                 y.Nombre,
                                 y.Duracion
                             }).ToList();

                if (datos.Count > 0)
                {
                    List<ReservaEnt> res = new List<ReservaEnt>();
                    foreach (var item in datos)
                    {
                        res.Add(new ReservaEnt
                        {
                            IdReserva = item.IdReserva,
                            IdServicio = item.IdServicio,
                            IdUsuario = item.IdUsuario,
                            PrecioPago = item.PrecioPago,
                            Fecha = item.Fecha,
                            Hora = item.Hora,
                            Cliente = item.Cliente,
                            Nombre = item.Nombre,
                            Duracion = item.Duracion,
                            
                        });
                    }

                    return res;
                }

                return new List<ReservaEnt>();
            }
        }
    }
}
