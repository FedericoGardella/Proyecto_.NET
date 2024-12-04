using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class PreciosEspecialidades
    {
        public PreciosEspecialidades() { }
        public long Id { get; set; }


        public long ArticulosId { get; set; }
        public Articulos Articulos { get; set; }


        [ForeignKey("TiposSeguros")]
        public long TiposSegurosId { get; set; }
        public TiposSeguros TiposSeguros { get; set; }


        [ForeignKey("Especialidades")]
        public long EspecialidadesId { get; set; }
        public Especialidades Especialidades { get; set; }


        public PrecioEspecialidad GetEntity()
        {
            return new PrecioEspecialidad
            {
                Id = Id,
                ArticuloId = ArticulosId,
                Articulo = Articulos?.GetEntity(),
                EspecialidadId = EspecialidadesId,
                Especialidad = Especialidades?.GetEntity(),
                TipoSeguroId = TiposSegurosId,
                TipoSeguro = TiposSeguros?.GetEntity()
            };
        }

        public static PreciosEspecialidades FromEntity(PrecioEspecialidad precioEspecialidad, PreciosEspecialidades preciosEspecialidades)
        {
            PreciosEspecialidades precioEspecialidadToSave = preciosEspecialidades ?? new PreciosEspecialidades();

            precioEspecialidadToSave.Id = precioEspecialidad.Id;
            precioEspecialidadToSave.ArticulosId = precioEspecialidad.ArticuloId;
            precioEspecialidadToSave.EspecialidadesId = precioEspecialidad.EspecialidadId;
            precioEspecialidadToSave.TiposSegurosId = precioEspecialidad.TipoSeguroId;

            return precioEspecialidadToSave;
        }
    }
}
