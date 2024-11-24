using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class HistoriaClinicaPostDTO
    {
        public long PacienteId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public string Comentarios { get; set; }
        public string NombreMedico { get; set; }
        public long CitaId { get; set; }
    }
}
