using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class HistoriaClinica
    {
        public long Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Paciente paciente { get; set; }

    }
}
