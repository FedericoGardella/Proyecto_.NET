using BL.IBLs;
using DAL.Models;
using DAL.IDALs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_Medicos : IBL_Medicos
    {
        private IDAL_Medicos dal;

        public BL_Medicos(IDAL_Medicos _dal)
        {
            dal = _dal;
        }

        public Medico Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<Medico> GetAll()
        {
            return dal.GetAll();
        }

        public Medico Add(Medico x)
        {
            return dal.Add(x);
        }

        public Medico Update(Medico x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }
    }
}
