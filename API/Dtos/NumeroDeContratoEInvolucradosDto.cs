using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class NumeroDeContratoEInvolucradosDto
    {
        public int ContratoId { get; set; }
        public string NombreCliente { get; set; }
        public string NombreEmpleado { get; set; }
    }
}