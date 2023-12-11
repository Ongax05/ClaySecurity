using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistency;

namespace Aplication.Repository
{
    public class PersonaRepository : GenericRepository<Persona>,IPersona
    {
        public PersonaRepository(ApiDbContext context) : base(context)
        {
        }
    }
}