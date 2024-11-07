using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class PreciosEspecialidades
    {
        public PreciosEspecialidades() { }
        public long Id { get; set; }


        [ForeignKey("TiposSeguros")]
        public long TiposSegurosId { get; set; }
        public TiposSeguros TiposSeguros { get; set; }

        [ForeignKey("Especialidades")]
        public long EspecialidadesId { get; set; }
        public Especialidades Especialidades { get; set; }

        public PrecioEspecialidad GetEntity()
        {
            PrecioEspecialidad precioEspecialidad = new PrecioEspecialidad();

            precioEspecialidad.Id = Id;

            return precioEspecialidad;
        }

        public static PreciosEspecialidades FromEntity(PrecioEspecialidad precioEspecialidad, PreciosEspecialidades preciosEspecialidades)
        {
            PreciosEspecialidades precioEspecialidadToSave;
            if (precioEspecialidad == null)
                precioEspecialidadToSave = new PreciosEspecialidades();
            else
                precioEspecialidadToSave = preciosEspecialidades;

            precioEspecialidadToSave.Id = precioEspecialidad.Id;

            return precioEspecialidadToSave;
        }
    }
}
