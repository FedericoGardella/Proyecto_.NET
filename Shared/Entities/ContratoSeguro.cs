namespace Shared.Entities
{
    public class ContratoSeguro
    {
        public long Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; }
        public Paciente paciente { get; set; }
        public TipoSeguro tipoSeguro { get; set; }
    }
}
