using ASPNET_Centralizador.Models;
using ASPNET_Centralizador.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace ASPNET_Centralizador.Controllers
{
    [ApiController]
    [Route("api/estudiante")]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteRepository repo;
        public EstudianteController(IEstudianteRepository estRepository)
        {
            repo = estRepository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Estudiante>> getEstudiantes()
        {
            var ests = repo.GetEstudiantes();
            return Ok(ests);
        }
        [HttpGet("{ci}")]
        public ActionResult<IEnumerable<Estudiante>> getEstudiante(int ci)
        {
            Estudiante est = repo.GetEstudianteByCi(ci);
            if (est != null)
                return Ok(est);
            else
                return NotFound();
        }
    }
}
