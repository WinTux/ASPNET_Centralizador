using System;

namespace ASPNET_campus.DTO
{
    public class EstudiantePublicadoDTO
    {
        public int ci { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fecha_nac { get; set; }
        public string tipoEvento { get; set; }
    }
}
