using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOs;
using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_FacturasMes
    {
        FacturaMes Get(long Id);
        List<FacturaMes> GetAll();
        FacturaMes Add(FacturaMes x);
        FacturaMes Update(FacturaMes x);
        void Delete(long Id);
    }
}
