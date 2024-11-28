using BL.IBLs;
using DAL.IDALs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_Articulos : IBL_Articulos
    {
        private IDAL_Articulos dal;
        private readonly IDAL_TiposSeguros dalTiposSeguros;

        public BL_Articulos(IDAL_Articulos _dal, IDAL_TiposSeguros _dalTiposSeguros)
        {
            dal = _dal;
            dalTiposSeguros = _dalTiposSeguros;

        }

        public Articulo Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<Articulo> GetAll()
        {
            return dal.GetAll();
        }

        public Articulo Add(Articulo x)
        {
            return dal.Add(x);
        }

        public Articulo Update(Articulo x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }

        public Articulo UpdateCosto(long tipoSeguroId, decimal nuevoCosto)
        {
            // Validar el TipoSeguro
            var tipoSeguro = dalTiposSeguros.Get(tipoSeguroId);
            if (tipoSeguro == null)
            {
                throw new Exception("TipoSeguro no encontrado.");
            }

            // Crear un nuevo artículo con el nuevo costo
            var nuevoArticulo = new Articulo
            {
                Fecha = DateTime.UtcNow,
                Costo = nuevoCosto,
                TipoSeguroId = tipoSeguroId
            };

            var createdArticulo = dal.Add(nuevoArticulo);

            // Actualizar el TipoSeguro con el nuevo ArticuloId
            tipoSeguro.ArticuloId = createdArticulo.Id;
            dalTiposSeguros.Update(tipoSeguro);

            return createdArticulo;
        }

    }
}
