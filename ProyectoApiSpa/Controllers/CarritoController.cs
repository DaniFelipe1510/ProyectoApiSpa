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
                                 x.CantidadArticulos,
                                 y.Precio
                             }).ToList();

                //var datos = (from x in bd.Carrito
                //             join y in bd.Producto on x.IdProducto equals y.IdProducto
                //             where x.IdUsuario == q
                //             group x by new { x.IdCarrito, x.IdProducto, x.IdUsuario, x.CantidadArticulos, y.Precio } into grouped
                //             select new CarritoEnt
                //             {
                //                 IdCarrito = grouped.Key.IdCarrito,
                //                 IdProducto = grouped.Key.IdProducto,
                //                 IdUsuario = grouped.Key.IdUsuario,
                //                 CantidadArticulos = (decimal)grouped.Sum(x => x.CantidadArticulos), // Sumamos la cantidad de productos del mismo tipo
                //                 Precio = grouped.Key.Precio
                //             }).ToList();

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
                            CantidadArticulos = (decimal)item.CantidadArticulos,
                            Precio = item.Precio
                        });
                    }

                    return res;
                }

                return new List<CarritoEnt>();
            }
        }
        
        //[HttpPost]
        //[Route("api/AgregarCursoCarrito")]
        //public int AgregarCursoCarrito(CarritoEnt entidad)
        //{
        //    using (var bd = new SPADBEntities())
        //    {
        //        // Buscamos el producto en el carrito para el mismo usuario
        //        Carrito carritoExistente = bd.Carrito.FirstOrDefault(c => c.IdUsuario == entidad.IdUsuario && c.IdProducto == entidad.IdProducto);
        //        //se busca el producto para hacer el if
        //        Producto producto = bd.Producto.FirstOrDefault(p => p.IdProducto == entidad.IdProducto);

        //        if (producto != null && producto.Cantidad > 0)
        //        {
        //            Carrito tabla = new Carrito
        //            {
        //                IdUsuario = entidad.IdUsuario,
        //                IdProducto = entidad.IdProducto,
        //                FechaCarrito = entidad.FechaCarrito,
        //                CantidadArticulos = entidad.CantidadArticulos
        //            };

        //            bd.Carrito.Add(tabla);
        //            producto.Cantidad--;//le resto en la BD la cantidad al producto
        //            return bd.SaveChanges();
        //        }
        //        else
        //        {

        //            return 0;
        //        }

        //    }
        //}

        [HttpPost]
        [Route("api/AgregarCursoCarrito")]
        public int AgregarCursoCarrito(CarritoEnt entidad)
        {
            using (var bd = new SPADBEntities())
            {
                // Buscamos el producto en el carrito para el mismo usuario
                Carrito carritoExistente = bd.Carrito.FirstOrDefault(c => c.IdUsuario == entidad.IdUsuario && c.IdProducto == entidad.IdProducto);

                Producto producto = bd.Producto.FirstOrDefault(p => p.IdProducto == entidad.IdProducto);

                if (producto != null && producto.Cantidad > 0)
                {
                    if (carritoExistente != null)
                    {
                        // Si el producto ya está en el carrito, simplemente actualizamos la cantidad en lugar de agregar una nueva fila.
                        carritoExistente.CantidadArticulos++;
                    }
                    else
                    {
                        // Si el producto no está en el carrito, lo agregamos como una nueva fila.
                        Carrito tabla = new Carrito
                        {
                            IdUsuario = entidad.IdUsuario,
                            IdProducto = entidad.IdProducto,
                            FechaCarrito = entidad.FechaCarrito,
                            CantidadArticulos = entidad.CantidadArticulos
                        };

                        bd.Carrito.Add(tabla);
                    }

                    // Decrementar la cantidad del producto en 1
                    producto.Cantidad--;

                    // Guardar los cambios tanto en la tabla Carrito como en la tabla Producto
                    return bd.SaveChanges();
                }
                else
                {
                    return 0; // No se pudo agregar al carrito debido a que la cantidad es 0 o el producto no existe.
                }
            }
        }

    }
}
