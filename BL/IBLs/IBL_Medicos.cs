using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Medicos
    {
        Medico Get(long Id);
        List<Medico> GetAll();
        Medico Add(Medico x);
        Medico Update(Medico x);
        void Delete(long Id);
    }
}
