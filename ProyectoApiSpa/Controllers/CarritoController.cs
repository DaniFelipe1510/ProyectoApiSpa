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
    public class CarritoController : ApiController
    {
        [HttpGet]
        [Route("api/ConsultarCursoCarrito")]
        public List<CarritoEnt> ConsultarCursoCarrito(long q)
        {
            using (var bd = new SPADBEntities())
            {
                var datos = (from x in bd.Carrito
                             join y in bd.Producto on x.IdProducto equals y.IdProducto
                             where x.IdUsuario == q
                             select new
                             {
                                 x.IdCarrito,
                                 x.IdProducto,
                                 x.IdUsuario,
                                 y.Precio
                             }).ToList();

                if (datos.Count > 0)
                {
                    List<CarritoEnt> res = new List<CarritoEnt>();
                    foreach (var item in datos)
                    {
                        res.Add(new CarritoEnt
                        {
                            IdCarrito = item.IdCarrito,
                            IdProducto = item.IdProducto,
                            IdUsuario = item.IdUsuario,
                            Precio = item.Precio
                        });
                    }

                    return res;
                }

                return new List<CarritoEnt>();
            }
        }

        [HttpPost]
        [Route("api/AgregarCursoCarrito")]
        public int AgregarCursoCarrito(CarritoEnt entidad)
        {
            using (var bd = new SPADBEntities())
            {
                Carrito tabla = new Carrito();
                tabla.IdUsuario = entidad.IdUsuario;
                tabla.IdProducto = entidad.IdProducto;
                tabla.FechaCarrito = entidad.FechaCarrito;

                bd.Carrito.Add(tabla);
                return bd.SaveChanges();
            }
        }
       
    }
}
