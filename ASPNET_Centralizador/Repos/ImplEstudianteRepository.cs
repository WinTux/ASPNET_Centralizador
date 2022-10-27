using ASPNET_Centralizador.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASPNET_Centralizador.Repos
{
    public class ImplEstudianteRepository : IEstudianteRepository
    {
        private readonly InstitutoDbContext ddbb;
        public ImplEstudianteRepository(InstitutoDbContext institutoDbContext) {
            ddbb = institutoDbContext;
        }

        public void CreateEstudiante(Estudiante est)
        {
            if (est == null)
                throw new ArgumentNullException(nameof(est));
            ddbb.Estudiantes.Add(est);
        }

        public void DeleteEstudiante(Estudiante est)
        {
            if(est == null)
                throw new ArgumentNullException(nameof(est));
            ddbb.Estudiantes.Remove(est);
        }

        public Estudiante GetEstudianteByCi(int ci)
        {
            return ddbb.Estudiantes.FirstOrDefault(est => est.ci == ci);
        }

        public IEnumerable<Estudiante> GetEstudiantes()
        {
            return ddbb.Estudiantes.ToList();
        }

        public bool Guardar()
        {
            return (ddbb.SaveChanges() > -1);
        }

        public void UpdateEstudiante(Estudiante est)
        {
            //No hacemos nada, pero está abierto a modificaciones (escalable)
        }
    }
}
