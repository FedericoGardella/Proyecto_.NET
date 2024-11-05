using BL.IBLs;
using DAL.IDALs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_TiposSeguros : IBL_TiposSeguros
    {
        private IDAL_TiposSeguros dal;

        public BL_TiposSeguros(IDAL_TiposSeguros _dal)
        {
            dal = _dal;
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
    }
}
