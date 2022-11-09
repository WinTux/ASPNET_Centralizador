using System;

namespace ASPNET_campus.DTO
{
    public class EstudianteReadDTO
    {
        public int ci { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fecha_nac { get; set; }
        public string direccion { get; set; }
    }
}
