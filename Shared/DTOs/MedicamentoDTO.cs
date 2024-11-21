using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class MedicamentoDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Dosis { get; set; }
        public long RecetaId { get; set; }
    }
}
