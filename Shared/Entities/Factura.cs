namespace Shared.Entities
{
    public class Factura
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public bool Pago { get; set; }
        public decimal Costo { get; set; }
        public bool Mensual { get; set; }
        public long PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public long? ContratoSeguroId { get; set; }
        public ContratoSeguro ContratoSeguro { get; set; }
        public long? CitaId { get; set; }
        public Cita Cita { get; set; }
    }
}
