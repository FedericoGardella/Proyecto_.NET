using BL.IBLs;
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
    public class BL_Recetas: IBL_Recetas
    {
        private IDAL_Recetas dal;

        public BL_Recetas(IDAL_Recetas _dal)
        {
            dal = _dal;
        }

        public Receta Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<Receta> GetAll()
        {
            return dal.GetAll();
        }

        public Receta Add(Receta x)
        {
            return dal.Add(x);
        }

        public Receta Update(Receta x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }
    }
}
