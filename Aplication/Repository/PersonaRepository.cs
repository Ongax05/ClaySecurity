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
    public class PersonaRepository(ApiDbContext context)
        : GenericRepository<Persona>(context),
            IPersona
    {
        private readonly ApiDbContext context = context;

        public async Task<(int totalRegisters, IEnumerable<Persona> registers)> GetBumangueses(
            int pageIndex,
            int pageSize
        )
        {
            var r = await context
                .Personas
                .Include(p => p.Ciudad)
                .Where(
                    p =>
                        p.Ciudad
                            .NombreCiudad
                            .Equals("bucaramanga", StringComparison.CurrentCultureIgnoreCase)
                )
                .ToListAsync();
            var totalRegisters = r.Count;
            return (totalRegisters, r.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        public async Task<(int totalRegisters, IEnumerable<Persona> registers)> GetEmpleados(
            int pageIndex,
            int pageSize
        )
        {
            var r = await context
                .Personas
                .Include(p => p.CategoriaPersona)
                .Where(
                    p =>
                        p.CategoriaPersona
                            .Descripcion
                            .Equals("empleado", StringComparison.CurrentCultureIgnoreCase)
                )
                .ToListAsync();
            var totalRegisters = r.Count;
            return (totalRegisters, r.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        public async Task<(
            int totalRegisters,
            IEnumerable<Persona> registers
        )> GetClientesCon5AñosDeAntiguedad(int pageIndex, int pageSize)
        {
            var r = await context
                .Personas
                .Include(p => p.CategoriaPersona)
                .Where(
                    p =>
                        p.CategoriaPersona
                            .Descripcion
                            .Equals("cliente", StringComparison.CurrentCultureIgnoreCase)
                        && p.FechaRegistro < DateTime.Now.AddYears(-5)
                )
                .ToListAsync();
            var totalRegisters = r.Count;
            return (totalRegisters, r.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        public async Task<(
            int totalRegisters,
            IEnumerable<Persona> registers
        )> GetEmpleadosDePiedecuestaYGiron(int pageIndex, int pageSize)
        {
            var r = await context
                .Personas
                .Include(p => p.CategoriaPersona)
                .Include(p => p.Ciudad)
                .Where(
                    p =>
                        (
                            p.Ciudad
                                .NombreCiudad
                                .Equals("giron", StringComparison.CurrentCultureIgnoreCase)
                            || p.Ciudad
                                .NombreCiudad
                                .Equals("piedecuesta", StringComparison.CurrentCultureIgnoreCase)
                        )
                        && p.CategoriaPersona
                            .Descripcion
                            .Equals("empleado", StringComparison.CurrentCultureIgnoreCase)
                )
                .ToListAsync();
            var totalRegisters = r.Count;
            return (totalRegisters, r.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        public async Task<(int totalRegisters, IEnumerable<Persona> registers)> GetVigilantes(
            int pageIndex,
            int pageSize
        )
        {
            var r = await context
                .Personas
                .Include(p => p.CategoriaPersona)
                .Include(p => p.TipoPersona)
                .Where(
                    p =>
                        p.CategoriaPersona
                            .Descripcion
                            .Equals("empleado", StringComparison.CurrentCultureIgnoreCase)
                        && p.TipoPersona
                            .Descripcion
                            .Equals("vigilante", StringComparison.CurrentCultureIgnoreCase)
                )
                .ToListAsync();
            var totalRegisters = r.Count;
            return (totalRegisters, r.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }
    }
}
