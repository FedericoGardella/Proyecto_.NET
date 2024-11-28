using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Articulos
    {
        public Articulos() { }
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Costo { get; set; }


        public long? TiposSegurosId { get; set; }
        public TiposSeguros? TiposSeguros { get; set; }


        public long? PreciosEspecialidadesId { get; set; }
        public PreciosEspecialidades? PreciosEspecialidades { get; set; }

        public Articulo GetEntity()
        {
            return new Articulo
            {
                Id = Id,
                Fecha = Fecha,
                Costo = Costo,
                TipoSeguroId = TiposSegurosId.HasValue ? TiposSegurosId.Value : 0,
                //TipoSeguro = TiposSeguros?.GetEntity(),
                TipoSeguro = TiposSeguros != null ? new TipoSeguro
                {
                    Id = TiposSeguros.Id,
                    Nombre = TiposSeguros.Nombre,
                    Descripcion = TiposSeguros.Descripcion
                    // Nota: No incluyas aquí el Articulo relacionado para evitar la recursión
                } : null,
                PrecioEspecialidadId = PreciosEspecialidadesId.HasValue ? PreciosEspecialidadesId.Value : 0,
                PrecioEspecialidad = PreciosEspecialidades?.GetEntity()
            };
        }

        public static Articulos FromEntity(Articulo articulo, Articulos articulos)
        {
            Articulos articuloToSave;
            if (articulos == null)
                articuloToSave = new Articulos();
            else
                articuloToSave = articulos;

            articuloToSave.Id = articulo.Id;
            articuloToSave.Fecha = articulo.Fecha;
            articuloToSave.Costo = articulo.Costo;
            articuloToSave.TiposSegurosId = articulo.TipoSeguroId;
            articuloToSave.PreciosEspecialidadesId = articulo.PrecioEspecialidadId;

            return articuloToSave;
        }
    }
}
