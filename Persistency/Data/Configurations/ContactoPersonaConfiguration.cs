using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistency.Data.Configurations
{
    public class ContactoPersonaConfiguration : IEntityTypeConfiguration<ContactoPersona>
    {
        public void Configure(EntityTypeBuilder<ContactoPersona> builder)
        {
            builder.ToTable("ContactoPersona");
            builder.Property(p=>p.Descripcion).HasColumnName("Descripcion").HasMaxLength(100).IsRequired();
            builder.HasOne(p=>p.Persona).WithMany(p=>p.ContactoPersonas).HasForeignKey(p=>p.PersonaId);
            builder.HasOne(p=>p.TipoContacto).WithMany(p=>p.ContactoPersonas).HasForeignKey(p=>p.TipoContactoId);
        }
    }
}