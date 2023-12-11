using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Programacion : BaseEntity
    {
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }
        public int TurnoId { get; set; }
        public Turno Turno { get; set; }
        public int EmpleadoId { get; set; }
        public Persona Empleado { get; set; }
    }
}