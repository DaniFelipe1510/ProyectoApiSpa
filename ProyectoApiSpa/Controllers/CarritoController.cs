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
                //se busca el producto para hacer el if
                Producto producto = bd.Producto.FirstOrDefault(p => p.IdProducto == entidad.IdProducto);

                if (producto != null && producto.Cantidad > 0)
                {
                    Carrito tabla = new Carrito
                    {
                        IdUsuario = entidad.IdUsuario,
                        IdProducto = entidad.IdProducto,
                        FechaCarrito = entidad.FechaCarrito
                    };

                    bd.Carrito.Add(tabla);
                    producto.Cantidad--;//le resto en la BD la cantidad al producto
                    return bd.SaveChanges();
                }
                else
                {
                    
                    return 0;
                }
              
            }
        }
       
    }
}
