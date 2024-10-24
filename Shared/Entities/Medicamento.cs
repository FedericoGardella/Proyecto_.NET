using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Medicamento
    {
        public long Id { get; set; }
        public int RecetasId { get; set; } // Esta va?
        public string Nombre { get; set; }
        public string Dosis { get; set; }
    }
}
