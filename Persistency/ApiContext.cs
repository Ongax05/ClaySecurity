using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistency;

public class ApiDbContext : DbContext
{
        public ApiDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CategoriaPersona> CategoriaPersonas { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<ContactoPersona> ContactoPersonas { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<DireccionPersona> DireccionPersonas { get; set; }
        public DbSet<EstadoContrato> EstadoContratos { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Programacion> Programaciones { get; set; }
        public DbSet<TipoContacto> TipoContactos { get; set; }
        public DbSet<TipoDireccion> TipoDireccione{ get; set; }
        public DbSet<TipoPersona> TipoPersonas { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<EmpleadoContrato> EmpleadoContratos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<EmpleadoContrato>().HasKey(n=> new {n.EmpleadoId,n.ContratoId});
        }

}
