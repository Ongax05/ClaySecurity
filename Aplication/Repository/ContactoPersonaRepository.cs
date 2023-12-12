using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistency;

namespace Aplication.Repository
{
    public class ContactoPersonaRepository(ApiDbContext context)
        : GenericRepository<ContactoPersona>(context),
            IContactoPersona
    {
        private readonly ApiDbContext context = context;

        public async Task<(
            int totalRegisters,
            IEnumerable<ContactoPersona> registers
        )> GetContactoDeVigilantes(int pageIndex, int pageSize)
        {
            var vigilantes = await context
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
                .Select(v => v.Id)
                .ToListAsync();
            var r = await context
                .ContactoPersonas
                .Where(p => vigilantes.Contains(p.Id))
                .ToListAsync();
            var totalRegisters = r.Count;
            return (totalRegisters, r.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }
    }
}
