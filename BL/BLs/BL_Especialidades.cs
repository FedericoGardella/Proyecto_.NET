using BL.IBLs;
using DAL.IDALs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_Especialidades : IBL_Especialidades
    {
        private IDAL_Especialidades dal;

        public BL_Especialidades(IDAL_Especialidades _dal)
        {
            dal = _dal;
        }

        public Especialidad Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<Especialidad> GetByIds(List<long> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return new List<Especialidad>();
            }

            return dal.GetByIds(ids);
        }

        public List<Especialidad> GetAll()
        {
            return dal.GetAll();
        }

        public Especialidad Add(Especialidad x)
        {
            return dal.Add(x);
        }

        public Especialidad Update(Especialidad x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }
    }
}
