using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Recetas
    {
        public Recetas() { }
        public long Id { get; set; }
        public HistoriasClinicas historiaClinica { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } // Tipo de receta, por ejemplo, "Receta controlada"

        public List<Medicamentos> Medicamentos { get; set; }


        public Receta GetEntity()
        {
            Receta receta = new Receta();

            receta.Id = Id;
            receta.Fecha = Fecha;
            receta.Tipo = Tipo;
            // HistClinId va?

            return receta;
        }

        public static Recetas FromEntity(Receta receta, Recetas recetas)
        {
            Recetas recetaToSave;
            if (receta == null)
                recetaToSave = new Recetas();
            else
                recetaToSave  = recetas;

            recetaToSave.Id = receta.Id;
            recetaToSave.Fecha = receta.Fecha;
            recetaToSave.Tipo = receta.Tipo;

            return recetaToSave;
        }
    }
}
