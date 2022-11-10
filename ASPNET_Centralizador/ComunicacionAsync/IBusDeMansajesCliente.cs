using ASPNET_Centralizador.DTO;

namespace ASPNET_Centralizador.ComunicacionAsync
{
    public interface IBusDeMansajesCliente
    {
        void PublicarNuevoEstudiante(EstudiantePublicadoDTO estudiantePublicadoDTO);
    }
}
