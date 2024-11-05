namespace Shared.Entities
{
    public class HistoriaClinica
    {
        public long Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Paciente paciente { get; set; }

    }
}
