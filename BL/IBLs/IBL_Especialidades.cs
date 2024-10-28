using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Especialidades
    {
        Especialidad Get(long Id);
        List<Especialidad> GetAll();
        Especialidad Add(Especialidad x);
        Especialidad Update(Especialidad x);
        void Delete(long Id);
    }
}
