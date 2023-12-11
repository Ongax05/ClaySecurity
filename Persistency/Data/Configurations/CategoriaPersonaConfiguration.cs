using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistency.Data.Configurations
{
    public class CategoriaPersonaConfiguration : IEntityTypeConfiguration<CategoriaPersona>
    {
        public void Configure(EntityTypeBuilder<CategoriaPersona> builder)
        {
            builder.ToTable("CategoriaPersona");
            builder.Property(p=>p.Descripcion).HasColumnName("Descripcion").HasMaxLength(100).IsRequired();
        }
    }
}