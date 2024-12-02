using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities;

namespace Shared.DTOs
{
    public class CitaDTO
    {
        public long? Id { get; set; }
        public TimeSpan Hora { get; set; }
        public decimal? Costo { get; set; }
        public long? PacienteId { get; set; }
        public long? GrupoCitaId { get; set; }
        public long? PrecioEspecialidadId { get; set; }
    }
}
