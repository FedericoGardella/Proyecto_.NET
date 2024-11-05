namespace Shared.Entities
{
    public class Cita
    {
        public long Id { get; set; }
        public TimeSpan Hora { get; set; }
        public Paciente paciente { get; set; }
        public GrupoCita grupoCita { get; set; }
        public PrecioEspecialidad precioEspecialidad { get; set; }
    }
}
