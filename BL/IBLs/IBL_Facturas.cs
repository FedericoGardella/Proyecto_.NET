using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.IBLs
{
    public interface IBL_Facturas
    {
        Factura Get(long Id);
        List<Factura> GetAll();
        Factura Add(Factura x);
        Factura Update(Factura x);
        void Delete(long Id);
    }
}
