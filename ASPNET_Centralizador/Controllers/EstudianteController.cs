using ASPNET_Centralizador.ComunicacionAsync;
using ASPNET_Centralizador.ComunicacionSync.Http;
using ASPNET_Centralizador.DTO;
using ASPNET_Centralizador.Models;
using ASPNET_Centralizador.Repos;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNET_Centralizador.Controllers
{
    [ApiController]
    [Route("api/estudiante")]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteRepository repo;
        private readonly IMapper mapper;
        private readonly ICampusHistorialCliente campusHistorialCliente;
        private readonly IBusDeMansajesCliente busDeMansajesCliente;
        public EstudianteController(IEstudianteRepository estRepository, IMapper mapper, ICampusHistorialCliente campusHistorialCliente, IBusDeMansajesCliente busDeMansajesCliente)
        {
            repo = estRepository;
            this.mapper = mapper;
            this.campusHistorialCliente = campusHistorialCliente;
            this.busDeMansajesCliente = busDeMansajesCliente;
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
        public async Task<ActionResult<EstudianteReadDTO>> setEstudiante(EstudianteCreateDTO estCreateDTO)
        {
            Estudiante estudiante = mapper.Map<Estudiante>(estCreateDTO);
            repo.CreateEstudiante(estudiante);
            repo.Guardar();

            EstudianteReadDTO estRetorno = mapper.Map<EstudianteReadDTO>(estudiante);

            try {
                await campusHistorialCliente.ComunicarseConCampus(estRetorno);
            } catch (Exception e) {
                Console.WriteLine("Ocurrió un erro al intentar comunicarse con el servicio Campus " +
                    "(forma sincronizada)");
                Console.WriteLine(e.Message);
            }
            try
            {
                var estudiantePublicadoDTO = mapper.Map<EstudiantePublicadoDTO>(estRetorno);
                estudiantePublicadoDTO.tipoEvento = "estudiante_publicado";
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrió un erro al intentar comunicarse con el servicio Campus " +
                    "(forma sincronizada)");
                Console.WriteLine(e.Message);
            }

            return CreatedAtRoute(nameof(getestudiante), new { ci = estRetorno.ci }, estRetorno);
        }

        [HttpPut("{ci}")]
        public ActionResult updateestudiante(int ci, EstudianteUpdateDTO estUpdateDTO)
        {
            Estudiante estudiante = repo.GetEstudianteByCi(ci);
            if (estudiante == null)
                return NotFound();
            //estudiante (original)
            //estUpdateDTO (modificado)
            mapper.Map(estUpdateDTO, estudiante);//va a reemplazar atributos diferentes --->
            repo.UpdateEstudiante(estudiante);//Sabemos que no hace nada pero podría hacerlo
            repo.Guardar();
            return NoContent();
        }
        [HttpPatch("{ci}")]
        public ActionResult updateparcialestudiante(int ci, 
            JsonPatchDocument<EstudianteUpdateDTO> estPatch)
        {
            Estudiante estudiante = repo.GetEstudianteByCi(ci);
            if (estudiante == null)
                return NotFound();

            EstudianteUpdateDTO estParaPatch = mapper.Map<EstudianteUpdateDTO>(estudiante);
            estPatch.ApplyTo(estParaPatch, ModelState);//ModelState es agregado para que realice las validaciones
            if (!TryValidateModel(estParaPatch))
                return ValidationProblem(ModelState);
            mapper.Map(estParaPatch, estudiante);//va a reemplazar atributos diferentes --->
            repo.UpdateEstudiante(estudiante);//Sabemos que no hace nada pero podría hacerlo
            repo.Guardar();
            return NoContent();
        }
        [HttpDelete("{ci}")]
        public ActionResult eliminarestudiante(int ci)
        {
            Estudiante estudiante = repo.GetEstudianteByCi(ci);
            if (estudiante == null)
                return NotFound();

            repo.DeleteEstudiante(estudiante);
            repo.Guardar();
            return NoContent();
        }
    }
}
