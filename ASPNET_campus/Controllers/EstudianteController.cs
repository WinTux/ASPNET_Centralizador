using ASPNET_campus.Conexion;
using ASPNET_campus.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ASPNET_campus.Controllers
{
    [Route("api/estudiantes")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IPerfilRepository repositorio;
        private readonly IMapper mapper;
        public EstudianteController(IPerfilRepository repository, IMapper mapper) {
            repositorio = repository;
            this.mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<EstudianteReadDTO>> GetEstudiantes()
        {
            Console.WriteLine("Obteniendo estudiantes mediante EstudianteController del servicio Campus");
            var estudiantes = repositorio.GetEstudiantes();
            return Ok(mapper.Map<IEnumerable<EstudianteReadDTO>>(estudiantes));
        }
    }
}
