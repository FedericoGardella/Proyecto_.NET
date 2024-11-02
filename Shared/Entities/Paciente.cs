using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Shared.Entities
{
    public class Paciente : Persona
    {
        public string Telefono { get; set; }
        public string Cedula { get; set; }
    }
}
