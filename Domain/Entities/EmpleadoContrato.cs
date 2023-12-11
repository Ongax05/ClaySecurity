using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EmpleadoContrato
    {
        public int EmpleadoId { get; set; }
        public Persona Persona { get; set; }
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }
    }
}