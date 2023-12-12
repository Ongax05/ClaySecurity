using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPersona : IGenericRepository<Persona>
    {
        Task<(int totalRegisters, IEnumerable<Persona> registers)> GetEmpleados (int pageIndex, int pageSize );
        Task<(int totalRegisters, IEnumerable<Persona> registers)> GetVigilantes (int pageIndex, int pageSize );
        Task<(int totalRegisters, IEnumerable<Persona> registers)> GetBumangueses (int pageIndex, int pageSize );
        Task<(int totalRegisters, IEnumerable<Persona> registers)> GetEmpleadosDePiedecuestaYGiron (int pageIndex, int pageSize );
        Task<(int totalRegisters, IEnumerable<Persona> registers)> GetClientesCon5AñosDeAntiguedad (int pageIndex, int pageSize );
    }
}