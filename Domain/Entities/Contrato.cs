using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Contrato : BaseEntity
    {
        public int ClienteId { get; set; }
        public Persona Cliente { get; set; }
        public DateTime FechaContrato { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int EstadoContratoId { get; set; }
        public EstadoContrato EstadoContrato { get; set; }
        public ICollection<Programacion> Programaciones { get; set; }
        public ICollection<EmpleadoContrato> EmpleadoContratos { get; set; }
    }
}