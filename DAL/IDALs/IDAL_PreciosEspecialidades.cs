using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDALs
{
    public interface IDAL_PreciosEspecialidades
    {
        PrecioEspecialidad Get(long Id);
        List<PrecioEspecialidad> GetAll();
        PrecioEspecialidad Add(PrecioEspecialidad x);
        PrecioEspecialidad Update(PrecioEspecialidad x);
        void Delete(long Id);
    }
}
