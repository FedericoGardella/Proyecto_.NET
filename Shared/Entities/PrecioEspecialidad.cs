namespace Shared.Entities
{
    public class PrecioEspecialidad
    {
        public long Id { get; set; }
        public long ArticuloId { get; set; }
        public Articulo Articulo { get; set; }
    }
}
