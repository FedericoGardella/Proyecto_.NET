using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class RecetaDTO
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public long HistoriaClinicaId { get; set; }
        public List<long> MedicamentoIds { get; set; } = new List<long>();
    }
}
