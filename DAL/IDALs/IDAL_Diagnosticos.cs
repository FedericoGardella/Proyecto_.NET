using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDALs
{
    public class IDAL_Diagnosticos
    {
        IEnumerable<Diagnosticos> ObtenerDiagnosticos();

        Diagnosticos ObtenerDiagnosticoPorId(int id);

        void AgregarDiagnostico(Diagnosticos diagnostico);

        void ActualizarDiagnostico(Diagnosticos diagnostico);

        void EliminarDiagnostico(int id);
    }
}
