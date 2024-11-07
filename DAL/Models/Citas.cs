using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Citas
    {
        public Citas() { }
        public long Id { get; set; }
        public TimeSpan Hora { get; set; }

        [ForeignKey("Pacientes")]
        public long PacienteId { get; set; }
        public Pacientes Pacientes { get; set; }

        [ForeignKey("GruposCitas")]
        public long GruposCitasId { get; set; }
        public GruposCitas GruposCitas { get; set; }

        [ForeignKey("Facturas")]
        public long FacturasId { get; set; }
        public Facturas Facturas { get; set; }

        [ForeignKey("PreciosEspecialidades")]
        public long PreciosEspecialidadesId { get; set; }
        public PreciosEspecialidades PreciosEspecialidades { get; set; }

        public Cita GetEntity()
        {
            Cita cita = new Cita();

            cita.Id = Id;
            cita.Hora = Hora;

            return cita;
        }

        public static Citas FromEntity(Cita cita, Citas citas)
        {
            Citas citaToSave;
            if (citas == null)
                citaToSave = new Citas();
            else
                citaToSave = citas;

            citaToSave.Id = cita.Id;
            citaToSave.Hora = cita.Hora;

            return citaToSave;
        }
    }
}
