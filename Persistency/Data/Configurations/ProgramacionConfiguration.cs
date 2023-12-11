using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistency.Data.Configurations
{
    public class ProgramacionConfiguration : IEntityTypeConfiguration<Programacion>
    {
        public void Configure(EntityTypeBuilder<Programacion> builder)
        {
            builder.ToTable("Programacion");
            builder.HasOne(p=>p.Contrato).WithMany(p=>p.Programaciones).HasForeignKey(p=>p.ContratoId);
            builder.HasOne(p=>p.Turno).WithMany(p=>p.Programaciones).HasForeignKey(p=>p.TurnoId);
            builder.HasOne(p=>p.Empleado).WithMany(p=>p.Programaciones).HasForeignKey(p=>p.EmpleadoId);
        }
    }
}