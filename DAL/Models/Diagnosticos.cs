using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Diagnosticos
    {
        public Diagnosticos() { }

        public long Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey("HistoriasClinicas")]
        public long HistoriasClinicasId { get; set; }
        public HistoriasClinicas HistoriasClinicas { get; set; }

        public Diagnostico GetEntity()
        {
            return new Diagnostico
            {
                Id = Id,
                Descripcion = Descripcion,
                Fecha = Fecha,
                HistoriaClinicaId = HistoriasClinicasId,
                HistoriaClinica = HistoriasClinicas?.GetEntity()
            };
        }

        public static Diagnosticos FromEntity(Diagnostico diagnostico, Diagnosticos diagnosticos)
        {
            Diagnosticos diagnosticoToSave = diagnosticos ?? new Diagnosticos();

            diagnosticoToSave.Id = diagnostico.Id;
            diagnosticoToSave.Descripcion = diagnostico.Descripcion;
            diagnosticoToSave.Fecha = diagnostico.Fecha;
            diagnosticoToSave.HistoriasClinicasId = diagnostico.HistoriaClinicaId;

            return diagnosticoToSave;
        }
    }
}
