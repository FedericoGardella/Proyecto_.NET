using DAL.Models;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDALs
{
    public interface IDAL_Recetas
    {
        Receta Get(long Id);
        List<Receta> GetAll();
        Receta Add(Receta x);
        Receta Update(Receta x);
        void Delete(long Id);
    }
}
