namespace Shared.Entities
{
    public class GrupoCita
    {
        public long Id { get; set; }
        public string Lugar { get; set; }
        public DateTime Fecha { get; set; }
        public long MedicoId { get; set; }
        public long EspecialidadId { get; set; }
        public List<Cita> Citas { get; set; } = new List<Cita>();
    }
}
