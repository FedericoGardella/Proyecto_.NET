using BL.IBLs;
using DAL.IDALs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_Medicamentos : IBL_Medicamentos
    {
        private IDAL_Medicamentos dal;

        public BL_Medicamentos(IDAL_Medicamentos _dal)
        {
            dal = _dal;
        }

        public Medicamento Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<Medicamento> GetByIds(List<long> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return new List<Medicamento>();
            }

            return dal.GetByIds(ids);
        }


        public List<Medicamento> GetAll()
        {
            return dal.GetAll();
        }

        public Medicamento Add(Medicamento x)
        {
            return dal.Add(x);
        }

        public Medicamento Update(Medicamento x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }
    }
}
