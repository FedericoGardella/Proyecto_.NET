using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDALs
{
    public class IDAL_HistoriasClinicas
    {
        IEnumerable<HistoriasClinicas> ObtenerHistoriasClinicas();

        HistoriasClinicas ObtenerHistoriaClinicaPorId(int id);

        void AgregarHistoriaClinica(HistoriasClinicas historiaClinica);

        void ActualizarHistoriaClinica(HistoriasClinicas historiaClinica);

        void EliminarHistoriaClinica(int id);
    }
}
