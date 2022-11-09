using ASPNET_campus.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_campus.Conexion
{
    public class CampusDbContext : DbContext
    {
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public CampusDbContext(DbContextOptions<CampusDbContext> opciones) : base(opciones){ }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiante>()
                .HasMany(es => es.perfiles)
                .WithOne(per => per.estudiante)
                .HasForeignKey(p => p.estudianteCI);
            modelBuilder.Entity<Perfil>()
                .HasOne(p => p.estudiante)
                .WithMany(es => es.perfiles)
                .HasForeignKey(p => p.estudianteCI);
        }
    }
}
