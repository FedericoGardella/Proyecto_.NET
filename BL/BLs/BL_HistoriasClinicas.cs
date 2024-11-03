﻿using BL.IBLs;
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
    public class BL_HistoriasClinicas: IBL_HistoriasClinicas
    {
        private IDAL_HistoriasClinicas dal;

        public BL_HistoriasClinicas(IDAL_HistoriasClinicas _dal)
        {
            dal = _dal;
        }

        public HistoriaClinica Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<HistoriaClinica> GetAll()
        {
            return dal.GetAll();
        }

        public HistoriaClinica Add(HistoriaClinica x)
        {
            return dal.Add(x);
        }

        public HistoriaClinica Update(HistoriaClinica x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }
    }
}