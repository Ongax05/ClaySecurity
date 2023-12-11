using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistency.Data.Configurations
{
    public class TurnoConfiguration : IEntityTypeConfiguration<Turno>
    {
        public void Configure(EntityTypeBuilder<Turno> builder)
        {
            builder.ToTable("Turno");
            builder.Property(p=>p.CodigoTurno).HasColumnName("CodigoTurno").HasMaxLength(100).IsRequired();
            builder.Property(p=>p.HoraFinal).HasColumnName("HoraFinal").HasColumnType("datetime").IsRequired();
            builder.Property(p=>p.HoraInicio).HasColumnName("HoraInicio").HasColumnType("datetime").IsRequired();
        }
    }
}