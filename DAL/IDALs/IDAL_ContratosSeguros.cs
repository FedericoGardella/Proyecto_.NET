using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDALs
{
    public interface IDAL_ContratosSeguros
    {
        ContratoSeguro Get(long Id);
        List<ContratoSeguro> GetAll();
        ContratoSeguro Add(ContratoSeguro x);
        ContratoSeguro Update(ContratoSeguro x);
        void Delete(long Id);
    }
}
