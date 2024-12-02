using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class GrupoCitaDetalleDTO
    {
        public long Id { get; set; }
        public string Lugar { get; set; }
        public DateTime Fecha { get; set; }
        public string MedicoNombre { get; set; }
        public string EspecialidadNombre { get; set; }
        public List<CitaDTO> Citas { get; set; } = new List<CitaDTO>();
    }
}
