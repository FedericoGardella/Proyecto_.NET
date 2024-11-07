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
            Diagnostico diagnostico = new Diagnostico();

            diagnostico.Id = Id;
            diagnostico.Descripcion = Descripcion;
            diagnostico.Fecha = Fecha;

            return diagnostico;
        }

        public static Diagnosticos FromEntity(Diagnostico diagnostico, Diagnosticos diagnosticos)
        {
            Diagnosticos diagnosticoToSave;
            if (diagnosticos == null)
                diagnosticoToSave = new Diagnosticos();
            else
                diagnosticoToSave = diagnosticos;

            diagnosticoToSave.Id = diagnostico.Id;
            diagnosticoToSave.Descripcion = diagnostico.Descripcion;
            diagnosticoToSave.Fecha = diagnostico.Fecha;

            return diagnosticoToSave;
        }
    }
}
