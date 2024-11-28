using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class ContratoSeguroDTO
    {
        public long? Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; }
        public long PacienteId { get; set; }
        public long TipoSeguroId { get; set; }
    }
}
