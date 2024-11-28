using System.ComponentModel.DataAnnotations.Schema;
using Shared.Entities;

namespace DAL.Models
{
    public class TiposSeguros
    {
        public TiposSeguros() { }
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public long ArticulosId { get; set; }
        public Articulos Articulos { get; set; }


        public TipoSeguro GetEntity()
        {
            return new TipoSeguro
            {
                Id = Id,
                Nombre = Nombre,
                Descripcion = Descripcion,
                ArticuloId = ArticulosId,
                //Articulo = Articulos?.GetEntity()
            };

        }

        public static TiposSeguros FromEntity(TipoSeguro tipoSeguro, TiposSeguros tiposSeguros)
        {
            TiposSeguros tipoSeguroToSave = tiposSeguros ?? new TiposSeguros();

            tipoSeguroToSave.Id = tipoSeguro.Id;
            tipoSeguroToSave.Nombre = tipoSeguro.Nombre;
            tipoSeguroToSave.Descripcion = tipoSeguro.Descripcion;
            tipoSeguroToSave.ArticulosId = tipoSeguro.ArticuloId;

            return tipoSeguroToSave;
        }
    }
}
