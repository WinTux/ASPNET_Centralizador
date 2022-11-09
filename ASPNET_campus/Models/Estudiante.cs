using System.ComponentModel.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ASPNET_campus.Models
{
    public class Estudiante
    {
        [Key]
        public int ci { get; set; }
        public int fci { get; set; }//de fireign ci
        [Required]
        [MaxLength(50)]
        public string nombre { get; set; }
        [Required]
        [MaxLength(50)]
        public string apellido { get; set; }
        [Required]
        public DateTime fecha_nac { get; set; }
        [MaxLength(100)]
        public string email { get; set; }
        [MaxLength(60)]
        public string direccion { get; set; }
        public ICollection<Perfil> perfiles { get; set; } = new List<Perfil>();
    }
}
