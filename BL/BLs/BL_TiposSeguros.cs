using BL.IBLs;
using DAL.IDALs;
using Shared.DTOs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_TiposSeguros : IBL_TiposSeguros
    {
        private IDAL_TiposSeguros dal;
        private readonly IDAL_Articulos dalArticulos;

        public BL_TiposSeguros(IDAL_TiposSeguros _dal, IDAL_Articulos _dalArticulos)
        {
            dal = _dal;
            dalArticulos = _dalArticulos;
        }

        public TipoSeguro Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<TipoSeguro> GetAll()
        {
            return dal.GetAll();
        }

        public TipoSeguro Add(TipoSeguro x)
        {
            return dal.Add(x);
        }

        public TipoSeguro Update(TipoSeguro x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }

        public Articulo UpdateCosto(long tipoSeguroId, decimal nuevoCosto)
        {
            // Validar la existencia del TipoSeguro
            var tipoSeguro = dal.Get(tipoSeguroId);
            if (tipoSeguro == null)
            {
                throw new Exception($"TipoSeguro con ID {tipoSeguroId} no encontrado.");
            }

            // Crear un nuevo artículo con el nuevo costo
            var nuevoArticulo = new Articulo
            {
                Fecha = DateTime.UtcNow,
                Costo = nuevoCosto,
                TipoSeguroId = tipoSeguroId // Asociar el nuevo artículo al TipoSeguro
            };

            var createdArticulo = dalArticulos.Add(nuevoArticulo);

            // Actualizar el TipoSeguro con el nuevo ArticuloId
            tipoSeguro.ArticuloId = createdArticulo.Id;
            dal.Update(tipoSeguro);

            return createdArticulo;
        }

        //public List<ContratoSeguroDTO> GetContratosSeguros(long TipoSeguroId)
        //{            return dal.GetContratosSeguros(TipoSeguroId);}
    }
}
