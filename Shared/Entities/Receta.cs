namespace Shared.Entities
{
    public class Receta
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public HistoriaClinica historiaClinica { get; set; }
    }
}
