using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Turno : BaseEntity
    {
        public string CodigoTurno { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFinal { get; set; }
        public ICollection<Programacion> Programaciones { get; set; }
    }
}