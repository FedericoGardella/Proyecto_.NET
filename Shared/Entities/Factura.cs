namespace Shared.Entities
{
    public class Factura
    {
        public long Id { get; set; }
        public long PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public List<Cita> Citas { get; set; }
        public List<FacturaMes> FacturasMes { get; set; }
    }
}
