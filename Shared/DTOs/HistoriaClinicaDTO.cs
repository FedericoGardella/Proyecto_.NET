using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class HistoriaClinicaDTO
    {
        public long PacienteId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}
