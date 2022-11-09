using ASPNET_campus.Models;
using System.Collections;
using System.Collections.Generic;

namespace ASPNET_campus.Conexion
{
    public interface IPerfilRepository
    {
        //PAra estudiantes
        IEnumerable<Estudiante> GetEstudiantes();
        void CrearEstudiante(Estudiante est);
        bool ExisteEstudiante(int ci);
        //Para perfiles
        Perfil GetPerfil(int idperfil, int ci);
        IEnumerable<Perfil> GetPerfilesDeEstudiante(int ci);
        void CrearPerfil(int ci, Perfil perfil);

        bool Guardar();
    }
}
