namespace Shared.Entities
{
    public class ContratoSeguro
    {
        public long Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public bool Activo { get; set; }
        public long PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public long TipoSeguroId { get; set; }
        public TipoSeguro TipoSeguro { get; set; }
    }
}
