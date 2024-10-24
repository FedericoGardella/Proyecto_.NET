using DAL.IDALs;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BLs
{
    public class BL_ResultadosEstudios: IDAL_ResultadosEstudios
    {
        private IDAL_ResultadosEstudios dal;

        public BL_ResultadosEstudios(IDAL_ResultadosEstudios _dal)
        {
            dal = _dal;
        }

        public ResultadoEstudio Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<ResultadoEstudio> GetAll()
        {
            return dal.GetAll();
        }

        public ResultadoEstudio Add(ResultadoEstudio x)
        {
            return dal.Add(x);
        }

        public ResultadoEstudio Update(ResultadoEstudio x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }
    }
}
