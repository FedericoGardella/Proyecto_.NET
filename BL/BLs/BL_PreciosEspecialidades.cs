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
    public class BL_PreciosEspecialidades: IBL_PreciosEspecialidades
    {
        private IDAL_PreciosEspecialidades dal;

        public BL_PreciosEspecialidades(IDAL_PreciosEspecialidades _dal)
        {
            dal = _dal;
        }

        public PrecioEspecialidad Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<PrecioEspecialidad> GetAll()
        {
            return dal.GetAll();
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
    }
}
