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
                                 y.Precio,
                                 y.Nombre,
                                 y.Descripcion
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
                            Precio = item.Precio,
                            Nombre = item.Nombre,
                            Descripcion = item.Descripcion
                        });
                    }

                    return res;
                }

                return new List<CarritoEnt>();
            }
        }

        [HttpGet]
        [Route("api/ConsultarProductosUsuario")]
        public List<CarritoEnt> ConsultarProductosUsuario(long q)
        {
            using (var bd = new SPADBEntities())
            {
                var datos = (from x in bd.ProductoUsuario
                             join y in bd.Producto on x.IdProducto equals y.IdProducto
                             where x.IdUsuario == q
                             select new
                             {
                                 x.IdProductoUsuario,
                                 x.IdProducto,
                                 x.IdUsuario,
                                 x.PrecioPago,
                                 x.FechaPago,
                                 x.CantidadProductos,
                                 y.Nombre,
                                 y.Descripcion
                             }).ToList();

                if (datos.Count > 0)
                {
                    List<CarritoEnt> res = new List<CarritoEnt>();
                    foreach (var item in datos)
                    {
                        res.Add(new CarritoEnt
                        {
                            IdCarrito = item.IdProductoUsuario,
                            IdProducto = item.IdProducto,
                            IdUsuario = item.IdUsuario,
                            Precio = item.PrecioPago,
                            Nombre = item.Nombre,
                            CantidadArticulos = (decimal)item.CantidadProductos,
                            FechaCarrito = item.FechaPago,
                            Descripcion = item.Descripcion
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

                   
                    //producto.Cantidad--;

                    // Guardar los cambios tanto en la tabla Carrito como en la tabla Producto
                    return bd.SaveChanges();
                }
                else
                {
                    return 0; // No se pudo agregar al carrito debido a que la cantidad es 0 o el producto no existe.
                }
            }
        }


        [HttpPost]
        [Route("api/PagarCursosCarrito")]
        public int PagarCursosCarrito(CarritoEnt entidad)
        {
            using (var bd = new SPADBEntities())
            {

                //Busco el carrito para pasarlo a la tabla de usuarios
                var datos = (from cc in bd.Carrito
                             join c in bd.Producto on cc.IdProducto equals c.IdProducto
                             where cc.IdUsuario == entidad.IdUsuario
                             select new
                             {
                                 cc.IdProducto,
                                 cc.IdUsuario,
                                 cc.CantidadArticulos,
                                 c.Precio
                             }).ToList();

                if (datos.Count > 0)
                {
                    foreach (var item in datos)
                    {
                        ProductoUsuario cu = new ProductoUsuario();
                        cu.IdProducto = item.IdProducto;
                        cu.IdUsuario = item.IdUsuario;
                        cu.FechaPago = DateTime.Now;
                        cu.CantidadProductos = item.CantidadArticulos;
                        cu.PrecioPago = (decimal)(item.Precio * item.CantidadArticulos);
                        bd.ProductoUsuario.Add(cu);

                        Producto producto = bd.Producto.FirstOrDefault(p => p.IdProducto == cu.IdProducto);
                        if (producto != null)
                        {
                            producto.Cantidad -= (int)cu.CantidadProductos;
                        }
                    }

                    //Busco el carrito para limpiarlo
                    var carritoActual = (from cc in bd.Carrito
                                         where cc.IdUsuario == entidad.IdUsuario
                                         select cc).ToList();

                    foreach (var item in carritoActual)
                    {
                        bd.Carrito.Remove(item);
                    }

                    return bd.SaveChanges();
                }

                return 0;
            }
        }
        [HttpDelete]
        [Route("api/RemoverCursoCarrito")]
        public int RemoverCursoCarrito(long q)
        {
            using (var bd = new SPADBEntities())
            {
                var carrito = (from cc in bd.Carrito
                               where cc.IdCarrito == q
                               select cc).FirstOrDefault();



                if (carrito != null)
                {
                    bd.Carrito.Remove(carrito);
                    return bd.SaveChanges();
                }



                return 0;
            }
        }

    }
}
