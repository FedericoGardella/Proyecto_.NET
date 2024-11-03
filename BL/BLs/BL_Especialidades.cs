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