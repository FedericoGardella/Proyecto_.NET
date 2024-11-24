using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class ArticuloDTO
    {
        public long? Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Costo { get; set; }
        public long TipoSeguroId { get; set; }
        public long EspecialidadId { get; set; }
    }
}
