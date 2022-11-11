using ASPNET_campus.DTO;
using ASPNET_campus.Models;
using AutoMapper;

namespace ASPNET_campus.DTO_perfiles
{
    public class EstudiantePerfil : Profile
    {
        public EstudiantePerfil()
        {
            CreateMap<Estudiante, EstudianteReadDTO>(); // -->
            CreateMap<Perfil, PerfilReadDTO>(); // -->
            CreateMap<PerfilCreateDTO, Perfil>(); // -->
            CreateMap<EstudiantePublicadoDTO, Estudiante>()
                .ForMember(
                    destino => destino.fci,
                    opcion => opcion.MapFrom(
                        origen => origen.ci
                    )
                );
        }
    }
}
