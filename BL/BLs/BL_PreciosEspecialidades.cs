using BL.IBLs;
using DAL.IDALs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_PreciosEspecialidades : IBL_PreciosEspecialidades
    {
        private IDAL_PreciosEspecialidades dal;
        private readonly IDAL_Articulos dalArticulos;

        public BL_PreciosEspecialidades(IDAL_PreciosEspecialidades _dal, IDAL_Articulos _dalArticulos)
        {
            dal = _dal;
            dalArticulos = _dalArticulos;
        }

        public PrecioEspecialidad Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<PrecioEspecialidad> GetAll()
        {
            return dal.GetAll();
        }

        public PrecioEspecialidad Add(PrecioEspecialidad x)
        {
            return dal.Add(x);
        }

        public PrecioEspecialidad Update(PrecioEspecialidad x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }

        public Articulo UpdateCosto(long precioEspecialidadId, decimal nuevoCosto)
        {
            // Validar la existencia del PrecioEspecialidad
            var precioEspecialidad = dal.Get(precioEspecialidadId);
            if (precioEspecialidad == null)
            {
                throw new Exception("PrecioEspecialidad no encontrado.");
            }

            // Crear un nuevo artículo con el nuevo costo
            var nuevoArticulo = new Articulo
            {
                Fecha = DateTime.UtcNow,
                Costo = nuevoCosto,
                PrecioEspecialidadId = precioEspecialidadId
            };

            var createdArticulo = dalArticulos.Add(nuevoArticulo);

            // Actualizar el PrecioEspecialidad con el nuevo ArticuloId
            precioEspecialidad.ArticuloId = createdArticulo.Id;
            dal.Update(precioEspecialidad);

            return createdArticulo;
        }


        public decimal GetCosto(long especialidadId, long tipoSeguroId)
        {
            // Busca el registro de PrecioEspecialidad con los IDs proporcionados
            var precioEspecialidad = dal.GetByEspecialidadAndTipoSeguro(especialidadId, tipoSeguroId);

            if (precioEspecialidad == null)
            {
                throw new Exception($"No se encontró un precio de especialidad para EspecialidadId {especialidadId} y TipoSeguroId {tipoSeguroId}.");
            }

            // Validar que tenga un ArticuloId
            if (precioEspecialidad.ArticuloId == 0)
            {
                throw new Exception($"El PrecioEspecialidad con ID {precioEspecialidad.Id} no tiene un ArticuloId válido asociado.");
            }

            // Buscar el artículo relacionado
            var articulo = dalArticulos.Get(precioEspecialidad.ArticuloId);
            if (articulo == null)
            {
                throw new Exception($"No se encontró un artículo con ID {precioEspecialidad.ArticuloId} asociado al PrecioEspecialidad.");
            }

            // Devuelve el costo del artículo
            return articulo.Costo;
        }

        public bool Repetido(long especialidadId, long tipoSeguroId)
        {
            return dal.Repetido(especialidadId, tipoSeguroId);
        }



    }
}
