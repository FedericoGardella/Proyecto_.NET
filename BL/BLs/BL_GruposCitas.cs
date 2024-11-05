using DAL.IDALs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_GruposCitas
    {
        private IDAL_GruposCitas dal;

        public BL_GruposCitas(IDAL_GruposCitas _dal)
        {
            dal = _dal;
        }

        public GrupoCita Get(long Id)
        {
            return dal.Get(Id);
        }


        public List<GrupoCita> GetAll()
        {
            return dal.GetAll();
        }

        public GrupoCita Add(GrupoCita x)
        {
            return dal.Add(x);
        }

        public GrupoCita Update(GrupoCita x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }
    }
}
