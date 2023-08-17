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
        [Route("api/ConsultarCursos")]
        public List<ProductoEnt> ConsultarCursos()
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
    }
}
