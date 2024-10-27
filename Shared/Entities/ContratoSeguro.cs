using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class ContratoSeguro
    {
        public long Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; }
        public Paciente paciente { get; set; }
        public TipoSeguro tipoSeguro { get; set; }
    }
}
