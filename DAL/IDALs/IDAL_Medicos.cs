using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDALs
{
    public interface IDAL_Medicos
    {
        Medico Get(long Id);
        List<Medico> GetAll();
        Medico Add(Medico x);
        Medico Update(Medico x);
        void Delete(long Id);
    }
}
