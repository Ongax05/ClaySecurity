using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class TurnoDto
    {
        public int Id { get; set; }
        public string CodigoTurno { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFinal { get; set; }

    }
}