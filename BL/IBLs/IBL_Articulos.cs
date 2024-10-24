using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.IBLs
{
    public interface IBL_Articulos
    {
        Articulo Get(long Id);
        List<Articulo> GetAll();
        Articulo Add(Articulo x);
        Articulo Update(Articulo x);
        void Delete(long Id);
    }
}
