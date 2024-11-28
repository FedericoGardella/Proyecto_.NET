namespace Shared.Entities
{
    public class Medico : Persona
    {
        public string Matricula { get; set; }
        public List<Especialidad> Especialidades { get; set; } = new List<Especialidad>();
    }
}

