﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.IBLs;
using DAL.IDALs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_FacturasMes: IBL_FacturasMes
    {
        private IDAL_FacturasMes dal;

        public BL_FacturasMes(IDAL_FacturasMes _dal)
        {
            dal = _dal;
        }

        public FacturaMes Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<FacturaMes> GetAll()
        {
            return dal.GetAll();
        }

        public FacturaMes Add(FacturaMes x)
        {
            return dal.Add(x);
        }

        public FacturaMes Update(FacturaMes x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }
    }
}
