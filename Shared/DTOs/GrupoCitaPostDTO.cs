using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class GrupoCitaPostDTO
    {
        public string Lugar { get; set; }
        public DateTime Fecha { get; set; }
        public long MedicoId { get; set; }
        public long EspecialidadId { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public int CantidadCitas { get; set; }
        public int IntervaloMinutos { get; set; }
    }
}
