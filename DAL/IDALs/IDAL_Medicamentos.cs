using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDALs
{
    public class IDAL_Medicamentos
    {
        IEnumerable<Medicamentos> ObtenerMedicamentos();

        Medicamentos ObtenerMedicamentoPorId(int id);

        void AgregarMedicamento(Medicamentos medicamento);

        void ActualizarMedicamento(Medicamentos medicamento);

        void EliminarMedicamento(int id);
    }
}
