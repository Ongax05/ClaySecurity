using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class DireccionPersonaDto
    {
        public int Id { get; set; }
        public string Direccion { get; set; }
        public int PersonaId { get; set; }
        public int TipoDireccionId { get; set; }
    }
}