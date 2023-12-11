using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistency.Data.Configurations
{
    public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
    {
        public void Configure(EntityTypeBuilder<Ciudad> builder)
        {
            builder.ToTable("Ciudad");
            builder.Property(p=>p.NombreCiudad).HasColumnName("NombreCiudad").HasMaxLength(100).IsRequired();
            builder.HasOne(p=>p.Departamento).WithMany(p=>p.Ciudades).HasForeignKey(p=>p.DepartamentoId);
        }
    }
}