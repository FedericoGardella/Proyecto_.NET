namespace Shared.Entities
{
    public class Paciente : Persona
    {
        public HistoriaClinica HistoriaClinica { get; set; }
        public string Telefono { get; set; }

        public List<Cita> Citas { get; set; }
    }
}
