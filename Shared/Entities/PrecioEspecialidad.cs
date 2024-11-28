namespace Shared.Entities
{
    public class PrecioEspecialidad
    {
        public long Id { get; set; }
        public long ArticuloId { get; set; }
        public Articulo Articulo { get; set; }
        public long TipoSeguroId { get; set; }
        public TipoSeguro TipoSeguro { get; set; }
        public long EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; }

    }
}
