using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistency.Data.Configurations
{
    public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("Persona");
            builder.Property(p=>p.CodigoInterno).HasColumnName("CodigoInterno").HasMaxLength(250).IsRequired();
            builder.Property(p=>p.Nombre).HasColumnName("Nombre").HasMaxLength(100).IsRequired();
            builder.Property(p=>p.FechaRegistro).HasColumnName("FechaRegistro").HasColumnType("datetime").IsRequired();
            builder.HasOne(p=>p.TipoPersona).WithMany(p=>p.Personas).HasForeignKey(p=>p.TipoPersonaId);
            builder.HasOne(p=>p.CategoriaPersona).WithMany(p=>p.Personas).HasForeignKey(p=>p.CategoriaPersonaId);
            builder.HasOne(p=>p.Ciudad).WithMany(p=>p.Personas).HasForeignKey(p=>p.CiudadId);
        }
    }
}