using ASPNET_campus.Conexion;
using ASPNET_campus.DTO;
using ASPNET_campus.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ASPNET_campus.Controllers
{
    [Route("api/Perfil/estudiante/{estudianteci}")]
    [ApiController]
    public class PerfilController  : ControllerBase
    {
        private readonly IPerfilRepository repositorio;
        private readonly IMapper mapper;
        public PerfilController(IPerfilRepository repository, IMapper mapper)
        {
            repositorio = repository;
            this.mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PerfilReadDTO>> GetPerfilesDeEstudiante(int estudianteci) {
            Console.WriteLine($"Se obtienen perfiles de estudiantes con carnet: {estudianteci}");
            if (!repositorio.ExisteEstudiante(estudianteci))
                return NotFound();
            var perfiles = repositorio.GetPerfilesDeEstudiante(estudianteci);
            return Ok(mapper.Map<IEnumerable<PerfilReadDTO>>(perfiles));
        }

        [HttpGet("{perfilid}", Name = "GetPerfilDeEstudiante")]
        //Alcanzable con: http://localhost:1234/api/Perfil/estudiante/123/321
        public ActionResult<PerfilReadDTO> GetPerfilDeEstudiante(int estudianteci, int perfilid)
        {
            Console.WriteLine($"Se obtiene el perfil {perfilid} del estudiante con carnet: {estudianteci}");
            if (!repositorio.ExisteEstudiante(estudianteci))
                return NotFound();
            var perfil = repositorio.GetPerfil(estudianteci, perfilid);
            return Ok(mapper.Map<PerfilReadDTO>(perfil));
        }
        [HttpPost]
        //Alcanzable con: http://localhost:1234/api/Perfil/estudiante/123
        public ActionResult<PerfilReadDTO> CrearPerfilParaEstudiante(int estudianteci, PerfilCreateDTO perfilDTO)
        {
            Console.WriteLine($"Se crea un perfil para el estudiante con carnet: {estudianteci}");
            if (!repositorio.ExisteEstudiante(estudianteci))
                return NotFound();
            Perfil perfil = mapper.Map<Perfil>(perfilDTO);
            repositorio.CrearPerfil(estudianteci, perfil);
            repositorio.Guardar();

            var perfilReadDTO = mapper.Map<PerfilReadDTO>(perfil);
            return CreatedAtRoute(nameof(GetPerfilDeEstudiante),new { estudianteci = estudianteci, perfilid = perfilReadDTO.id}, perfilReadDTO);
        }
    }
}
