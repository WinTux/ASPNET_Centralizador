using ASPNET_campus.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASPNET_campus.Conexion
{
    public class ImplPerfilRepository : IPerfilRepository
    {
        private readonly CampusDbContext context;
        public ImplPerfilRepository(CampusDbContext context)
        {
            this.context = context;
        }
        //Estudiantes
        public void CrearEstudiante(Estudiante est)
        {
            if (est == null)
                throw new ArgumentNullException(nameof(est));
            else
                context.Estudiantes.Add(est);
        }
        public bool ExisteEstudiante(int ci)
        {
            return context.Estudiantes.Any(est => est.ci == ci);
        }
        
        public IEnumerable<Estudiante> GetEstudiantes()
        {
            return context.Estudiantes.ToList();
        }
        //Perfiles
        public void CrearPerfil(int ci, Perfil perfil)
        {
            if (perfil == null)
                throw new ArgumentNullException(nameof(perfil));
            else {
                perfil.estudianteCI = ci;
                context.Perfiles.Add(perfil);
            }
        }

        

        public Perfil GetPerfil(int idperfil, int ci)
        {
            return context.Perfiles
                .Where(per => per.id == idperfil && per.estudianteCI == ci).FirstOrDefault();
        }

        public IEnumerable<Perfil> GetPerfilesDeEstudiante(int ci)
        {
            return context.Perfiles.Where(p => p.estudianteCI == ci)
                .OrderBy(p => p.estudiante.apellido);
        }

        public bool Guardar()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
