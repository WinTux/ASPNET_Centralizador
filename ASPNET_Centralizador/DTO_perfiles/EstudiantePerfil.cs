using ASPNET_Centralizador.DTO;
using ASPNET_Centralizador.Models;
using AutoMapper;
namespace ASPNET_Centralizador.DTO_perfiles
{
    public class EstudiantePerfil : Profile
    {
        public EstudiantePerfil(){
            CreateMap<Estudiante, EstudianteReadDTO>(); // -->
            CreateMap<EstudianteCreateDTO, Estudiante>(); // -->
            CreateMap<EstudianteUpdateDTO, Estudiante>(); // -->
            CreateMap<Estudiante, EstudianteUpdateDTO>(); // -->
            CreateMap<EstudianteReadDTO, EstudiantePublicadoDTO>(); // -->
        }
    }
}
