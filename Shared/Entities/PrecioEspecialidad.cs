namespace Shared.Entities
{
    public class PrecioEspecialidad
    {
        public long Id { get; set; }
        public Articulo articulo { get; set; }
        public TipoSeguro tipoSeguro { get; set; }

        public Especialidad especialidad { get; set; }
    }
}
