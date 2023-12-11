using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PersonaDto
    {
        public int Id { get; set; }
        public string CodigoInterno { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int TipoPersonaId { get; set; }
        public int CategoriaPersonaId { get; set; }
        public int CiudadId { get; set; }
    }
}