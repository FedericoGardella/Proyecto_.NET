using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Medicamentos
    {
        public Medicamentos() { }
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Dosis { get; set; }

        [ForeignKey("Recetas")]
        public long RecetasId { get; set; }
        public Recetas Recetas { get; set; }

        public Medicamento GetEntity()
        {
            Medicamento medicamento = new Medicamento();

            medicamento.Id = Id;
            medicamento.Nombre = Nombre;
            medicamento.Dosis = Dosis;
            // RecetasId va?

            return medicamento;
        }

        public static Medicamentos FromEntity(Medicamento medicamento, Medicamentos medicamentos)
        {
            Medicamentos medicamentoToSave;
            if (medicamentos == null)
                medicamentoToSave = new Medicamentos();
            else
                medicamentoToSave = medicamentos;

            medicamentoToSave.Id = medicamento.Id;
            medicamentoToSave.Nombre = medicamento.Nombre;
            medicamentoToSave.Dosis = medicamento.Dosis;
            // RecetasId va?

            return medicamentoToSave;
        }
    }
}
