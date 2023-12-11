using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistency.Data.Configurations
{
    public class ContratoConfiguration : IEntityTypeConfiguration<Contrato>
    {
        public void Configure(EntityTypeBuilder<Contrato> builder)
        {
            builder.ToTable("Contrato");
            builder.HasOne(p=>p.Cliente).WithMany(p=>p.Contratos).HasForeignKey(p=>p.ClienteId);
            builder.HasOne(p=>p.Empleado).WithMany(p=>p.Contratos).HasForeignKey(p=>p.EmpleadoId);
            builder.Property(p=>p.FechaContrato).HasColumnName("FechaContrato").HasColumnType("datetime").IsRequired();
            builder.Property(p=>p.FechaVencimiento).HasColumnName("FechaVencimiento").HasColumnType("datetime").IsRequired();
            builder.HasOne(p=>p.EstadoContrato).WithMany(p=>p.Contratos).HasForeignKey(p=>p.EstadoContratoId);
            
        }
    }
}