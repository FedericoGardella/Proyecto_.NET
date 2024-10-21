using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Recetas
    {
        public int Id { get; set; }
        public HistoriasClinicas HistClinId { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } // Tipo de receta, por ejemplo, "Receta controlada"

        public List<Medicamentos> Medicamentos { get; set; }

        // Constructor para inicializar la lista
        public Recetas()
        {
            Medicamentos = new List<Medicamentos>();
        }
    }
}
