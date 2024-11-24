namespace Shared.Entities
{
    public class HistoriaClinica
    {
        public long Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Comentarios { get; set; }
        public string NombreMedico { get; set; }
        public long CitaId { get; set; }
        public long PacienteId { get; set; } 
        public Paciente Paciente { get; set; }
        public List<Diagnostico> Diagnosticos { get; set; }
        public List<ResultadoEstudio> ResultadosEstudios { get; set; }
        public List<Receta> Recetas { get; set; }

    }
}
