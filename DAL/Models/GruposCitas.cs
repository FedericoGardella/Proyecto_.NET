using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class GruposCitas
    {
        public GruposCitas() { }
        public long Id { get; set; }
        public string Lugar { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey("Medicos")]
        public long MedicosId { get; set; }
        public Medicos Medicos { get; set; }

        [ForeignKey("Especialidades")]
        public long EspecialidadesId { get; set; }
        public Especialidades Especialidades { get; set; }

        public List<Citas> Citas { get; set; } = new List<Citas>();


        public GrupoCita GetEntity()
        {
            return new GrupoCita
            {
                Id = Id,
                Lugar = Lugar,
                Fecha = Fecha,
                MedicoId = MedicosId,
                EspecialidadId = EspecialidadesId,
                Medico = Medicos == null ? null : new Medico
                {
                    Id = Medicos.Id,
                    Nombres = Medicos.Nombres,
                    Apellidos = Medicos.Apellidos,
                    Documento = Medicos.Documento,
                    Matricula = Medicos.Matricula
                },
                Especialidad = Especialidades == null ? null : new Especialidad
                {
                    Id = Especialidades.Id,
                    Nombre = Especialidades.Nombre
                },
                Citas = Citas?.Select(cita => new Cita
                {
                    Id = cita.Id,
                    Hora = cita.Hora,
                    PacienteId = cita.PacienteId,
                    GrupoCitaId = cita.GruposCitasId
                }).ToList()
            };
        }


        public static GruposCitas FromEntity(GrupoCita grupoCita, GruposCitas gruposCitas)
        {
            GruposCitas grupoCitaToSave = gruposCitas ?? new GruposCitas();

            grupoCitaToSave.Id = grupoCita.Id;
            grupoCitaToSave.Lugar = grupoCita.Lugar;
            grupoCitaToSave.Fecha = grupoCita.Fecha;
            grupoCitaToSave.MedicosId = grupoCita.MedicoId;
            grupoCitaToSave.EspecialidadesId = grupoCita.EspecialidadId;


            return grupoCitaToSave;
        }
    }
}
