using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.IBLs
{
    public interface IBL_Diagnosticos
    {
        Diagnostico Get(long Id);
        List<Diagnostico> GetAll();
        Diagnostico Add(Diagnostico x);
        Diagnostico Update(Diagnostico x);
        void Delete(long Id);
    }
}
