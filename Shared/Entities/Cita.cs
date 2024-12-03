using System.Net.Sockets;

namespace Shared.Entities
{
    public class Cita
    {
        public long Id { get; set; }
        public TimeSpan Hora { get; set; }
        public long? PacienteId { get; set; }
        public Paciente? Paciente { get; set; }
        public long GrupoCitaId { get; set; }
        public GrupoCita GrupoCita { get; set; }
        public long? PrecioEspecialidadId { get; set; }
        public PrecioEspecialidad? PrecioEspecialidad { get; set; }
    }
}
