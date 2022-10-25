using ASPNET_Centralizador.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_Centralizador.Repos
{
    public class InstitutoDbContext : DbContext
    {
        public DbSet<Estudiante> Estudiantes { get; set; }
        public InstitutoDbContext(DbContextOptions<InstitutoDbContext> opciones) : base(opciones) { 
        }
    }
}
