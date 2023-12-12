using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IContactoPersona : IGenericRepository<ContactoPersona>
    {
        Task<(int totalRegisters, IEnumerable<ContactoPersona> registers)> GetContactoDeVigilantes (int pageIndex, int pageSize );
    }
}