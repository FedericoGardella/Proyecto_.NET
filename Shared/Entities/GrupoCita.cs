namespace Shared.Entities
{
    public class GrupoCita
    {
        public long Id { get; set; }
        public string Lugar { get; set; }
        public DateTime Fecha { get; set; }
        public Medico medico { get; set; }
        public List<Cita> Citas { get; set; }
        public Especialidad especialidad { get; set; }
    }
}
