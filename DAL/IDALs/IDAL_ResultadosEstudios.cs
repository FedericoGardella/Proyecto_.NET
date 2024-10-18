using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDALs
{
    public class IDAL_ResultadosEstudios
    {
        IEnumerable<ResultadosEstudios> ObtenerResultadosEstudios();

        ResultadosEstudios ObtenerResultadoEstudioPorId(int id);

        void AgregarResultadoEstudio(ResultadosEstudios resultadoEstudio);

        void ActualizarResultadoEstudio(ResultadosEstudios resultadoEstudio);

        void EliminarResultadoEstudio(int id);
    }
}
