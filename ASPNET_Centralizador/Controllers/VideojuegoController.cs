using ASPNET_Centralizador.Models;
using ASPNET_Centralizador.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ASPNET_Centralizador.Controllers
{
    [ApiController]
    [Route("api/videojuego")]
    public class VideojuegoController : ControllerBase
    {
        private readonly IVideojuegoRepository repo;
        public VideojuegoController(IVideojuegoRepository videojuegoRepository) {
            repo = videojuegoRepository;
        }

        [HttpGet]
        public ActionResult <IEnumerable<Videojuego>> GetVideojuegos()
        {
            var videojuegos = repo.GetVideojuegos();
            return Ok(videojuegos);
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Videojuego>> GetVideojuegoById(int id)
        {
            var videojuego = repo.GetVideojuego(id);
            return Ok(videojuego);
        }
    }
}
