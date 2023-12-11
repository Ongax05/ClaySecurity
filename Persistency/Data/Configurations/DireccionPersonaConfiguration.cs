using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistency.Data.Configurations
{
    public class DireccionPersonaConfiguration : IEntityTypeConfiguration<DireccionPersona>
    {
        public void Configure(EntityTypeBuilder<DireccionPersona> builder)
        {
            builder.ToTable("DireccionPersona");
            builder.Property(p=>p.Direccion).HasColumnName("Direccion").HasMaxLength(100).IsRequired();
            builder.HasOne(p=>p.Persona).WithMany(p=>p.DireccionPersonas).HasForeignKey(p=>p.PersonaId);
            builder.HasOne(p=>p.TipoDireccion).WithMany(p=>p.DireccionPersonas).HasForeignKey(p=>p.TipoDireccionId);
        }
    }
}