using BL.IBLs;
using DAL.IDALs;
using Shared.DTOs;
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

        public List<PrecioEspecialidadNombresDTO> GetAll()
        {
            var preciosEspecialidades = dal.GetAll();

            // Convierte a DTO
            return preciosEspecialidades.Select(p => new PrecioEspecialidadNombresDTO
            {
                Id = p.Id,
                EspecialidadNombre = p.Especialidad?.Nombre,
                TipoSeguroNombre = p.TipoSeguro?.Nombre,
                Costo = p.Articulo?.Costo ?? 0
            }).ToList();
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
            try
            {
                return dal.GetCosto(especialidadId, tipoSeguroId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el costo: {ex.Message}", ex);
            }
        }


        public bool Repetido(long especialidadId, long tipoSeguroId)
        {
            return dal.Repetido(especialidadId, tipoSeguroId);
        }



    }
}
