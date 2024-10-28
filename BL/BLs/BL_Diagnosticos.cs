using BL.IBLs;
using DAL.IDALs;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BLs
{
    public class BL_Diagnosticos : IBL_Diagnosticos
    {
        private IDAL_Diagnosticos dal;

        public BL_Diagnosticos(IDAL_Diagnosticos _dal)
        {
            dal = _dal;
        }

        public Diagnostico Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<Diagnostico> GetAll()
        {
            return dal.GetAll();
        }

        public Diagnostico Add(Diagnostico x)
        {
            return dal.Add(x);
        }

        public Diagnostico Update(Diagnostico x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }
    }
}
