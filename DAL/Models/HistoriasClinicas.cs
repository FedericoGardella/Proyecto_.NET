using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class HistoriasClinicas
    {
        public int Id { get; set; }
        public Pacientes PacienteId { get; set; }
        public DateTime FechaCreacion { get; set; }

        public List<Diagnosticos> Diagnosticos { get; set; }
        public List<Recetas> Recetas { get; set; }
        public List<ResultadosEstudios> ResultadosEstudios { get; set; }

        // Constructor para inicializar las listas
        public HistoriasClinicas()
        {
            Diagnosticos = new List<Diagnosticos>();
            Recetas = new List<Recetas>();
            ResultadosEstudios = new List<ResultadosEstudios>();
        }
    }
}
