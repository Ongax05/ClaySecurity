using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProgramacionDto
    {
        public int Id { get; set; }
        public int ContratoId { get; set; }
        public int TurnoId { get; set; }
        public int EmpleadoId { get; set; }
    }
}