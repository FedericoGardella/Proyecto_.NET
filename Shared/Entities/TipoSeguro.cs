namespace Shared.Entities
{
    public class TipoSeguro
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public long ArticuloId { get; set; }
        public Articulo Articulo { get; set; }
    }
}
