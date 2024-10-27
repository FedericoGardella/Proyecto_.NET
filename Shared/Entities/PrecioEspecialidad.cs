using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class PrecioEspecialidad
    {
        public long Id { get; set; }
        public Articulo articulo { get; set; }
        public TipoSeguro tipoSeguro { get; set; }
        public Especialidad especialidad { get; set; }
    }
}
