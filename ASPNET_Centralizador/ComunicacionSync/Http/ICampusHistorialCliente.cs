using ASPNET_Centralizador.DTO;
using System.Threading.Tasks;

namespace ASPNET_Centralizador.ComunicacionSync.Http
{
    public interface ICampusHistorialCliente
    {
        Task ComunicarseConCampus(EstudianteReadDTO est);
    }
}
