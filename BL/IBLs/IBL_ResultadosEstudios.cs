using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.IBLs
{
    public interface IBL_ResultadosEstudios
    {
        ResultadoEstudio Get(long Id);
        List<ResultadoEstudio> GetAll();
        ResultadoEstudio Add(ResultadoEstudio x);
        ResultadoEstudio Update(ResultadoEstudio x);
        void Delete(long Id);
    }
}
