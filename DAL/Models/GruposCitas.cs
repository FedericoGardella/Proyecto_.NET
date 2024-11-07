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

        public List<Citas> Citas { get; set; }


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
