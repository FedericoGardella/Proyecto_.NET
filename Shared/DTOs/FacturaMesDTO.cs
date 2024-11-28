using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class FacturaMesDTO
    {
        public long Id { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal GastosMes { get; set; }
        public bool Pagada { get; set; }
        public decimal CostoContrato { get; set; }
        public long ContratoSeguroId { get; set; }
        public long FacturaId { get; set; }
    }
}
