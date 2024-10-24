using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Medico
    {
        public long Id { get; set; }

        public string Matricula { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }
    }
}
