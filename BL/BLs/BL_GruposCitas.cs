using DAL.IDALs;
using DAL.Models;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<GrupoCita>  GetAll()
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
