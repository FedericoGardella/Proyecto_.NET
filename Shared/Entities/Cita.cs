using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Cita
    {
        public long Id { get; set; }
        public TimeSpan Hora { get; set; }
        public Paciente paciente { get; set; }
        public GrupoCita grupoCita { get; set; }
        public PrecioEspecialidad precioEspecialidad { get; set; }
    }
}
