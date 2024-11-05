namespace Shared.Entities
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime FechaEmision { get; set; }
        public bool Pago { get; set; }
        public ContratoSeguro contrato { get; set; }
    }
}
