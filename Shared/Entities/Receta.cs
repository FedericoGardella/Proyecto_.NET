using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Receta
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
    }
}
