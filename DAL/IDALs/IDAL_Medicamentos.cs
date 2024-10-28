using DAL.Models;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDALs
{
    public interface IDAL_Medicamentos
    {
        Medicamento Get(long Id);
        List<Medicamento> GetAll();
        Medicamento Add(Medicamento x);
        Medicamento Update(Medicamento x);
        void Delete(long Id);
    }
}
