﻿using BL.IBLs;
using DAL.IDALs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_ContratosSeguros : IBL_ContratosSeguros
    {
        private IDAL_ContratosSeguros dal;

        public BL_ContratosSeguros(IDAL_ContratosSeguros _dal)
        {
            dal = _dal;
        }

        public ContratoSeguro Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<ContratoSeguro> GetAll()
        {
            return dal.GetAll();
        }

        public ContratoSeguro Add(ContratoSeguro x)
        {
            return dal.Add(x);
        }

        public ContratoSeguro Update(ContratoSeguro x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }
    }
}
