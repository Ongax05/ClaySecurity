using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistency;

namespace Aplication.Repository
{
    public class ContratoRepository(ApiDbContext context)
        : GenericRepository<Contrato>(context),
            IContrato
    {
        private readonly ApiDbContext context = context;

        public async Task<(
            int totalRegisters,
            IEnumerable<Contrato> registers
        )> GetContratosActivos(int pageIndex, int pageSize)
        {
            var r = await context
                .Contratos
                .Include(p => p.EstadoContrato)
                .Include(p=>p.Cliente)
                .Where(
                    p =>
                        p.EstadoContrato
                            .Descripcion
                            .Equals("activo", StringComparison.OrdinalIgnoreCase)
                )
                .ToListAsync();
            var totalRegisters = r.Count;
            return (totalRegisters, r.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        public async Task<string> GetNombreEmpleadoPorIdDeContrato(int Id)
        {
            var obj = await context.EmpleadoContratos.Where(p=>p.ContratoId == Id).Include(p=>p.Persona).FirstOrDefaultAsync();
            return obj.Persona.Nombre;
        }
    }
}
