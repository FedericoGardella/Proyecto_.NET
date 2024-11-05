namespace Shared.Entities
{
    public class Paciente : Persona
    {
        public HistoriaClinica historiaClinica { get; set; }
        public string Telefono { get; set; }

    }
}
