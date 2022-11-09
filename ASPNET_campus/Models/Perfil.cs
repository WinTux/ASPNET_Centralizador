using System.ComponentModel.DataAnnotations;

namespace ASPNET_campus.Models
{
    public class Perfil
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string nick { get; set; }
        [Required]
        public string descripcion { get; set; }
        public string lenguajes { get; set; }
        [Required]
        public int estudianteCI { get; set; }//Para relacioarse con fci
        public Estudiante estudiante { get; set; }//Para navegar entre entidades

    }
}
