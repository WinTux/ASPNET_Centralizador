using ASPNET_Centralizador.DTO;
using ASPNET_Centralizador.Models;
using ASPNET_Centralizador.Repos;
using AutoMapper;
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
        private readonly IMapper mapper;
        public EstudianteController(IEstudianteRepository estRepository, IMapper mapper)
        {
            repo = estRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<EstudianteReadDTO>> getEstudiantes()
        {
            var ests = repo.GetEstudiantes();
            return Ok(mapper.Map<IEnumerable<EstudianteReadDTO>>(ests));
        }
        [HttpGet("{ci}", Name = "getestudiante")]
        public ActionResult<EstudianteReadDTO> getestudiante(int ci)
        {
            Estudiante est = repo.GetEstudianteByCi(ci);
            if (est != null)
                return Ok(mapper.Map<EstudianteReadDTO>(est));
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult<EstudianteReadDTO> setEstudiante(EstudianteCreateDTO estCreateDTO)
        {
            Estudiante estudiante = mapper.Map<Estudiante>(estCreateDTO);
            repo.CreateEstudiante(estudiante);
            repo.Guardar();

            EstudianteReadDTO estRetorno = mapper.Map<EstudianteReadDTO>(estudiante);
            return CreatedAtRoute(nameof(getestudiante), new { ci = estRetorno.ci }, estRetorno);
        }
    }
}
