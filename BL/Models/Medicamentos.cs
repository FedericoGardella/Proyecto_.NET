using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Medicamentos
    {
        public int Id { get; set; }
        public int RecetasId { get; set; }
        public string Nombre { get; set; }
        public string Dosis { get; set; }
    }
}
