using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Diagnosticos
    {
<<<<<<< HEAD
        public Diagnosticos() { }
        public long Id { get; set; }
=======

        public int Id { get; set; }
        public HistoriasClinicas HistClinId { get; set; }
>>>>>>> origin/MarianoOK
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }

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
