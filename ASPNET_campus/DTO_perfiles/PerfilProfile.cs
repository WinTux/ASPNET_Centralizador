using ASPNET_campus.DTO;
using ASPNET_campus.Models;
using AutoMapper;

namespace ASPNET_Centralizador.DTO_perfiles
{
    public class PerfilProfile : Profile
    {
        public PerfilProfile()
        {
            CreateMap<Perfil, PerfilReadDTO>(); // -->
            CreateMap<PerfilCreateDTO, Perfil>(); // -->
        }
    }
}
