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
    public class BL_Personas : IBL_Personas
    {
        private IDAL_Personas dal;

        public BL_Personas(IDAL_Personas _dal)
        {
            dal = _dal;
        }

        public Persona Get(long Id)
        {
            return dal.Get(Id);
        }
        public Persona GetXDocumento(string Documento)
        {
            return dal.GetXDocumento(Documento);
        }
        public List<Persona> GetAll()
        {
            return dal.GetAll();
        }

        public Persona Add(Persona x)
        {
            return dal.Add(x);
        }

        public Persona Update(Persona x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }
    }
}
