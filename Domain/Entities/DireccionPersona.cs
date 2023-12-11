using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DireccionPersona : BaseEntity
    {
        public string Direccion { get; set; }
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }
        public int TipoDireccionId { get; set; }
        public TipoDireccion TipoDireccion { get; set; }
    }
}