using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class HistoriasClinicas
    {
        public HistoriasClinicas() { }
        public long Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Comentarios { get; set; }
        public string NombreMedico { get; set; }
        public long CitasId { get; set; }

        [ForeignKey("Pacientes")]
        public long PacientesId { get; set; }
        public Pacientes Pacientes { get; set; }

        public List<Diagnosticos> Diagnosticos { get; set; }
        public List<ResultadosEstudios> ResultadosEstudios { get; set; }
        public List<Recetas> Recetas { get; set; }

        public HistoriaClinica GetEntity()
        {
            return new HistoriaClinica
            {
                Id = Id,
                FechaCreacion = FechaCreacion,
                Comentarios = Comentarios,
                NombreMedico = NombreMedico,
                CitaId = CitasId,
                PacienteId = PacientesId,
                ResultadosEstudios = ResultadosEstudios?.Select(re => new Shared.Entities.ResultadoEstudio
                {
                    Id = re.Id,
                    Descripcion = re.Descripcion,
                    Fecha = re.Fecha
                }).ToList(),
                Diagnosticos = Diagnosticos?.Select(d => new Shared.Entities.Diagnostico
                {
                    Id = d.Id,
                    Descripcion = d.Descripcion,
                    Fecha = d.Fecha
                }).ToList(),
                Recetas = Recetas?.Select(r => new Shared.Entities.Receta
                {
                    Id = r.Id,
                    Fecha = r.Fecha,
                    Descripcion = r.Descripcion,
                    Tipo = r.Tipo,
                    Medicamentos = r.Medicamentos?.Select(m => new Shared.Entities.Medicamento
                    {
                        Id = m.Id,
                        Nombre = m.Nombre,
                        Dosis = m.Dosis
                    }).ToList()
                }).ToList()
            };
        }

        public static HistoriasClinicas FromEntity(HistoriaClinica historiaClinica, HistoriasClinicas historiasClinicas)
        {
            HistoriasClinicas historiaToSave = historiasClinicas ?? new HistoriasClinicas();

            historiaToSave.Id = historiaClinica.Id;
            historiaToSave.FechaCreacion = historiaClinica.FechaCreacion;
            historiaToSave.Comentarios = historiaClinica.Comentarios;
            historiaToSave.NombreMedico = historiaClinica.NombreMedico;
            historiaToSave.CitasId = historiaClinica.CitaId;
            historiaToSave.PacientesId = historiaClinica.PacienteId;

            return historiaToSave;
        }
    }
}
