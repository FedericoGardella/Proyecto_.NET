using DAL.Models;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDALs
{
    public interface IDAL_Diagnosticos
    {
        Diagnostico Get(long Id);
        List<Diagnostico> GetAll();
        Diagnostico Add(Diagnostico x);
        Diagnostico Update(Diagnostico x);
        void Delete(long Id);
    }
}
