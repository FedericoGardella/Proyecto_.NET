namespace Shared.Entities
{
    public class Receta
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }

        public long HistoriaClinicaId { get; set; }
        public HistoriaClinica HistoriaClinica { get; set; }

        // Relación explícita con medicamentos
        public List<Medicamento> Medicamentos { get; set; } = new List<Medicamento>();

        // Propiedad para manejar los IDs de los medicamentos asociados
        public List<long> MedicamentoIds { get; set; } = new List<long>();
    }
}
