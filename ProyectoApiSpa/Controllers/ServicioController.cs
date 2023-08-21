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
    }
}
