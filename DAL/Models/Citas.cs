using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Citas
    {
        public Citas() { }
        public long Id { get; set; }
        public TimeSpan Hora { get; set; }
        public decimal Costo { get; set; } // SE VA

        [ForeignKey("Pacientes")]
        public long? PacienteId { get; set; }
        public Pacientes? Pacientes { get; set; }

        [ForeignKey("GruposCitas")]
        public long GruposCitasId { get; set; }
        public GruposCitas GruposCitas { get; set; }

        
        public long? FacturasId { get; set; }
        public Facturas? Facturas { get; set; }

        [ForeignKey("PreciosEspecialidades")]
        public long? PreciosEspecialidadesId { get; set; }
        public PreciosEspecialidades? PreciosEspecialidades { get; set; }

        public Cita GetEntity()
        {
            return new Cita
            {
                Id = Id,
                Hora = Hora,
                Costo = Costo,
                PacienteId = PacienteId,
                GrupoCitaId = GruposCitasId,
                PrecioEspecialidadId = PreciosEspecialidadesId,
                Paciente = Pacientes?.GetEntity(), // Si Pacientes no es null, mapear a su entidad Shared
                GrupoCita = GruposCitas?.GetEntity(), // Si GruposCitas no es null, mapear a su entidad Shared
                PrecioEspecialidad = PreciosEspecialidades?.GetEntity() // Si PreciosEspecialidades no es null
            };
        }

        public static Citas FromEntity(Cita cita, Citas citas)
        {
            Citas citaToSave = citas ?? new Citas();

            citaToSave.Id = cita.Id;
            citaToSave.Hora = cita.Hora;
            citaToSave.Costo = cita.Costo;
            citaToSave.PacienteId = cita.PacienteId;
            citaToSave.GruposCitasId = cita.GrupoCitaId;
            citaToSave.PreciosEspecialidadesId = cita.PrecioEspecialidadId;

            // Relación con Pacientes (si existe)
            if (cita.Paciente != null)
            {
                citaToSave.Pacientes = Pacientes.FromEntity(cita.Paciente, citaToSave.Pacientes);
            }

            // Relación con GruposCitas (si existe)
            if (cita.GrupoCita != null)
            {
                citaToSave.GruposCitas = GruposCitas.FromEntity(cita.GrupoCita, citaToSave.GruposCitas);
            }

            // Relación con PreciosEspecialidades (si existe)
            if (cita.PrecioEspecialidad != null)
            {
                citaToSave.PreciosEspecialidades = PreciosEspecialidades.FromEntity(cita.PrecioEspecialidad, citaToSave.PreciosEspecialidades);
            }

            return citaToSave;
        }
    }
}
