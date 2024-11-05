using Shared.Entities;

namespace DAL.Models
{
    public class GruposCitas
    {
        public GruposCitas() { }
        public long Id { get; set; }
        public string Lugar { get; set; }
        public DateTime Fecha { get; set; }
        public Medicos medico { get; set; }
        public Especialidades especialidad { get; set; }


        public GrupoCita GetEntity()
        {
            GrupoCita grupoCita = new GrupoCita();

            grupoCita.Id = Id;
            grupoCita.Lugar = Lugar;
            grupoCita.Fecha = Fecha;

            return grupoCita;
        }

        public static GruposCitas FromEntity(GrupoCita grupoCita, GruposCitas gruposCitas)
        {
            GruposCitas grupoCitaToSave;
            if (gruposCitas == null)
                grupoCitaToSave = new GruposCitas();
            else
                grupoCitaToSave = gruposCitas;

            grupoCitaToSave.Id = grupoCita.Id;
            grupoCitaToSave.Lugar = grupoCita.Lugar;
            grupoCitaToSave.Fecha = grupoCita.Fecha;

            return grupoCitaToSave;
        }
    }
}
