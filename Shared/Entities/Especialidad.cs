using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Especialidad
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public TimeSpan tiempoCita { get; set; }
    }
}
