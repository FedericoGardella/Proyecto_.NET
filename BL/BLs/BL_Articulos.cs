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

    }
}
