using ASPNET_Centralizador.Models;
using System.Collections.Generic;

namespace ASPNET_Centralizador.Repos
{
    public interface IEstudianteRepository
    {
        IEnumerable<Estudiante> GetEstudiantes();
        Estudiante GetEstudianteByCi(int ci);
        void CreateEstudiante(Estudiante est);
        void UpdateEstudiante(Estudiante est);
        void DeleteEstudiante(Estudiante est);
        bool Guardar();
    }
}
