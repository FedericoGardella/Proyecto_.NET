using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Articulos
    {
        public Articulos() { }
        public long Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Costo { get; set; }


        [ForeignKey("TiposSeguros")]
        public long TiposSegurosId { get; set; }
        public TiposSeguros? TiposSeguros { get; set; }


        [ForeignKey("Especialidades")]
        public long EspecialidadesId { get; set; }
        public Especialidades? Especialidades { get; set; }

        public Articulo GetEntity()
        {
            return new Articulo
            {
                Id = Id,
                Nombre = Nombre,
                Fecha = Fecha,
                Costo = Costo,
                TipoSeguroId = TiposSegurosId,
                TipoSeguro = TiposSeguros?.GetEntity(),
                EspecialidadId = EspecialidadesId,
                Especialidad = Especialidades?.GetEntity()
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
            articuloToSave.Nombre = articulo.Nombre;
            articuloToSave.Fecha = articulo.Fecha;
            articuloToSave.Costo = articulo.Costo;
            articuloToSave.TiposSegurosId = articulo.TipoSeguroId;
            articuloToSave.EspecialidadesId = articulo.EspecialidadId;

            return articuloToSave;
        }
    }
}
