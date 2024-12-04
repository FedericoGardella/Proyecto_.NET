using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class PrecioEspecialidadNombresDTO
    {
        public long Id { get; set; }
        public string EspecialidadNombre { get; set; }
        public string TipoSeguroNombre { get; set; }
        public decimal Costo { get; set; }
    }
}
