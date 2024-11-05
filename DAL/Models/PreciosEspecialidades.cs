using Shared.Entities;

namespace DAL.Models
{
    public class PreciosEspecialidades
    {
        public PreciosEspecialidades() { }
        public long Id { get; set; }
        public Articulos articulo { get; set; }
        public TiposSeguros tipoSeguro { get; set; }
        public Especialidades especialidad { get; set; }

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
