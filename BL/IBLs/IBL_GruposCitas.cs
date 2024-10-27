using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.IBLs
{
    public interface IBL_GruposCitas
    {
        GrupoCita Get(long Id);
        List<GrupoCita> GetAll();
        GrupoCita Add(GrupoCita x);
        GrupoCita Update(GrupoCita x);
        void Delete(long Id);
    }
}
