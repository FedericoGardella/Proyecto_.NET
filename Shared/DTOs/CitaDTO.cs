using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class CitaDTO 
    {
        public long Id { get; set; }
        public long PacienteId { get; set; }
        public TimeSpan Hora { get; set; }
    }
}
