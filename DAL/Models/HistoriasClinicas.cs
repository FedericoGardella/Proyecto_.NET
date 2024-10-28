using Shared.Entities;



namespace DAL.Models
{
    public class HistoriasClinicas
    {

        public HistoriasClinicas() { }
        public long Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Pacientes paciente { get; set; }

        public List<Diagnosticos> Diagnosticos { get; set; }
        public List<ResultadosEstudios> ResultadosEstudios { get; set; }
        public List<Recetas> Recetas { get; set; }

        public HistoriaClinica GetEntity()
        {
            HistoriaClinica historiaClinica = new HistoriaClinica();

            historiaClinica.Id = Id;
            historiaClinica.FechaCreacion = FechaCreacion;

            return historiaClinica;
        }

        public static HistoriasClinicas FromEntity(HistoriaClinica historiaClinica, HistoriasClinicas historiasClinicas)
        {
            HistoriasClinicas historiaToSave;
            if (historiasClinicas == null)
                historiaToSave = new HistoriasClinicas();
            else
                historiaToSave = historiasClinicas;

            historiaToSave.Id = historiaClinica.Id;
            historiaToSave.FechaCreacion = historiaClinica.FechaCreacion;

            return historiaToSave;
        }
    }
}
