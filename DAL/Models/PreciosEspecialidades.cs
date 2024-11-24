using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class PreciosEspecialidades
    {
        public PreciosEspecialidades() { }
        public long Id { get; set; }

        [ForeignKey("Articulos")]
        public long ArticulosId { get; set; }
        public Articulos Articulos { get; set; }


        public PrecioEspecialidad GetEntity()
        {
            return new PrecioEspecialidad
            {
                Id = Id,
                ArticuloId = ArticulosId,
                Articulo = Articulos?.GetEntity()
            };
        }

        public static PreciosEspecialidades FromEntity(PrecioEspecialidad precioEspecialidad, PreciosEspecialidades preciosEspecialidades)
        {
            PreciosEspecialidades precioEspecialidadToSave = preciosEspecialidades ?? new PreciosEspecialidades();

            precioEspecialidadToSave.Id = precioEspecialidad.Id;
            precioEspecialidadToSave.ArticulosId = precioEspecialidad.ArticuloId;

            return precioEspecialidadToSave;
        }
    }
}
