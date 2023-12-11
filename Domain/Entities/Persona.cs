using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Persona : BaseEntity
    {
        public string CodigoInterno { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int TipoPersonaId { get; set; }
        public TipoPersona TipoPersona { get; set; }
        public int CategoriaPersonaId { get; set; }
        public CategoriaPersona CategoriaPersona { get; set; }
        public int CiudadId { get; set; }
        public Ciudad Ciudad { get; set; }
        public ICollection<ContactoPersona> ContactoPersonas { get; set; }
        public ICollection<DireccionPersona> DireccionPersonas { get; set; }
        public ICollection<Contrato> Contratos { get; set; }
        public ICollection<Programacion> Programaciones { get; set; }
        public ICollection<EmpleadoContrato> EmpleadoContratos { get; set; }
    }
}