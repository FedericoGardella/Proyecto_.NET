using Shared.Entities;

namespace DAL.Models
{
    public class Citas
    {
        public Citas() { }
        public long Id { get; set; }
        public TimeSpan Hora { get; set; }
        public Pacientes paciente { get; set; }
        public GruposCitas grupoCita { get; set; }
        public PreciosEspecialidades precioEspecialidad { get; set; }

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
