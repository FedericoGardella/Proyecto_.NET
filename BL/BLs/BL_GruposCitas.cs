using DAL.IDALs;
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

        public BL_GruposCitas(IDAL_Medicamentos _dal)
        {
            dal = _dal;
        }

        public Medicamento Get(long Id)
        {
            return dal.Get(Id);
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
