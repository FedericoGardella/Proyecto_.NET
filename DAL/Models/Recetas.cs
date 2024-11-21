using Shared.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Recetas
    {
        public Recetas() { }
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }

        [ForeignKey("HistoriasClinicas")]
        public long HistoriasClinicasId { get; set; }
        public HistoriasClinicas HistoriasClinicas { get; set; }

        // Relación de muchos a muchos
        public List<Medicamentos> Medicamentos { get; set; } = new List<Medicamentos>();

        public Receta GetEntity()
        {
            Receta receta = new Receta
            {
                Id = Id,
                Fecha = Fecha,
                Tipo = Tipo,
                Descripcion = Descripcion,
                HistoriaClinicaId = HistoriasClinicasId,
                HistoriaClinica = HistoriasClinicas?.GetEntity(),
            };

            return receta;
        }

        public static Recetas FromEntity(Receta receta, Recetas recetas)
        {
            Recetas recetaToSave = recetas ?? new Recetas();

            recetaToSave.Id = receta.Id;
            recetaToSave.Fecha = receta.Fecha;
            recetaToSave.Descripcion = receta.Descripcion;
            recetaToSave.Tipo = receta.Tipo;
            recetaToSave.HistoriasClinicasId = receta.HistoriaClinicaId;

            return recetaToSave;
        }
    }
}
