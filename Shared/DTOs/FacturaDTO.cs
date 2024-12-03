using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class FacturaDTO
    {
        public DateTime Fecha { get; set; }
        public bool Pago { get; set; }
        public decimal Costo { get; set; }
        public long PacienteId { get; set; }
        public long? ContratoSeguroId { get; set; }
        public long? CitaId { get; set; }
    }
}
