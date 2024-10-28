using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.IBLs
{
    public interface IBL_HistoriasClinicas
    {
        HistoriaClinica Get(long Id);
        List<HistoriaClinica> GetAll();
        HistoriaClinica Add(HistoriaClinica x);
        HistoriaClinica Update(HistoriaClinica x);
        void Delete(long Id);
    }
}
