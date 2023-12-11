using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistency.Data.Configurations
{
    public class EstadoContratoConfiguration : IEntityTypeConfiguration<EstadoContrato>
    {
        public void Configure(EntityTypeBuilder<EstadoContrato> builder)
        {
            builder.ToTable("EstadoContrato");
            builder.Property(p=>p.Descripcion).HasColumnName("Descripcion").HasMaxLength(100).IsRequired();
        }
    }
}