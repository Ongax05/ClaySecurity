using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ContratoDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaContrato { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int EstadoContratoId { get; set; }
    }
}