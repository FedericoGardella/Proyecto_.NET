using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ResultadosEstudios
    {
        public int Id { get; set; }
        public HistoriasClinicas HistClinId { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
