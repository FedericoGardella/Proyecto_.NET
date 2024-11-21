namespace Shared.Entities
{
    public class Medicamento
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Dosis { get; set; }

        // Relación muchos a muchos con Recetas
        public List<Receta> Recetas { get; set; } = new List<Receta>();

    }
}
