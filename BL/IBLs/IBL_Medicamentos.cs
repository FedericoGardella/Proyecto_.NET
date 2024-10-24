using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.IBLs
{
    public interface IBL_Medicamentos
    {
        Medicamento Get(long Id);
        List<Medicamento> GetAll();
        Medicamento Add(Medicamento x);
        Medicamento Update(Medicamento x);
        void Delete(long Id);
    }
}
