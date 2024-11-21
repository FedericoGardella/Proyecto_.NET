using Shared.Entities;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Medicamentos
    {
        public Medicamentos() { }
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Dosis { get; set; }

        // Relación de muchos a muchos (No se expone una lista de recetas)
        public List<Recetas> Recetas { get; set; } = new List<Recetas>();

        public Medicamento GetEntity()
        {
            Medicamento medicamento = new Medicamento
            {
                Id = Id,
                Nombre = Nombre,
                Dosis = Dosis,
            };

            return medicamento;
        }

        public static Medicamentos FromEntity(Medicamento medicamento, Medicamentos medicamentos)
        {
            Medicamentos medicamentoToSave = medicamentos ?? new Medicamentos();

            medicamentoToSave.Id = medicamento.Id;
            medicamentoToSave.Nombre = medicamento.Nombre;
            medicamentoToSave.Dosis = medicamento.Dosis;

            return medicamentoToSave;
        }
    }
}
