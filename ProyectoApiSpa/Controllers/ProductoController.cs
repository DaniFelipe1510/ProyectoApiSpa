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
    public class ProductoController : ApiController
    {
        [HttpGet]
        [Route("api/ConsultarProductos")]
        public List<ProductoEnt> ConsultarProductos()
        {
            using (var bd = new SPADBEntities())
            {
                var datos = (from x in bd.Producto
                             select x).ToList();

                if (datos.Count > 0)
                {
                    List<ProductoEnt> res = new List<ProductoEnt>();
                    foreach (var item in datos)
                    {
                        res.Add(new ProductoEnt
                        {
                            IdProducto = item.IdProducto,
                            Nombre = item.Nombre,
                            Descripcion = item.Descripcion,
                            Cantidad = item.Cantidad,
                            Precio = item.Precio,
                            Imagen = item.Imagen
                        });
                    }

                    return res;
                }

                return new List<ProductoEnt>();
            }
        }

        [HttpPost]
        [Route("api/RegistrarProducto")]
        public long RegistrarProducto(ProductoEnt entidad)
        {
            using (var bd = new SPADBEntities())
            {
                Producto tabla = new Producto();
                tabla.Nombre = entidad.Nombre;
                tabla.Descripcion = entidad.Descripcion;
                tabla.Cantidad = entidad.Cantidad;
                tabla.Precio = entidad.Precio;
                tabla.Imagen = entidad.Imagen;

                bd.Producto.Add(tabla);
                bd.SaveChanges();

                return tabla.IdProducto;

            }


        }

        [HttpPut]
        [Route("api/ActualizarRuta")]
        public void ActualizarRuta(ProductoEnt entidad)
        {
            using (var bd = new SPADBEntities())
            {
                var datos = (from x in bd.Producto
                             where x.IdProducto == entidad.IdProducto
                             select x).FirstOrDefault();
                if (datos != null)
                {
                    datos.Imagen = entidad.Imagen;
                    bd.SaveChanges();
                }

            }


        }

        [HttpGet]
        [Route("api/ConsultarProducto")]
        public ProductoEnt ConsultarProducto(long q)
        {
            using (var bd = new SPADBEntities())
            {
                var datos = (from x in bd.Producto
                             where x.IdProducto == q
                             select x).FirstOrDefault();
                if (datos != null)
                {
                    ProductoEnt res = new ProductoEnt();
                    res.IdProducto = datos.IdProducto;
                    res.Nombre = datos.Nombre;
                    res.Descripcion = datos.Descripcion;
                    res.Cantidad = datos.Cantidad;
                    res.Precio = datos.Precio;
                    res.Imagen = datos.Imagen;
                    return res;
                }

                return null;
            }
        }

        [HttpPut]
        [Route("api/ActualizarProducto")]
        public int ActualizarCurso(ProductoEnt entidad)
        {
            using (var bd = new SPADBEntities())
            {
                var datos = (from x in bd.Producto
                             where x.IdProducto == entidad.IdProducto
                             select x).FirstOrDefault();
                if (datos != null)
                {
                    datos.Nombre = entidad.Nombre;
                    datos.Descripcion = entidad.Descripcion;
                    datos.Cantidad = entidad.Cantidad;
                    datos.Precio = entidad.Precio;
                    datos.Imagen = entidad.Imagen;
                    return bd.SaveChanges();
                }

                return 0;

            }


        }
    }
}
