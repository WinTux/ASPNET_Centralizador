using Microsoft.AspNetCore.Mvc;
using System;

namespace ASPNET_campus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : ControllerBase
    {
        public HistorialController() { }
        [HttpPost]
        public ActionResult Post()
        {
            Console.WriteLine("Llegó una petición al servicio Campus");
            return Ok("Respuesta exitosa desde el controlador HitorialController");
        }
    }
}
