namespace Shared.Entities
{
    public class Articulo
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Costo { get; set; }
        public long TipoSeguroId { get; set; }
        public TipoSeguro TipoSeguro { get; set; }
        public long PrecioEspecialidadId { get; set; }
        public PrecioEspecialidad PrecioEspecialidad { get; set; }
    }
}
