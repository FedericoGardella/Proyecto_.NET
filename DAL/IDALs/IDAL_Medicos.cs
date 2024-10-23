using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDALs
{
    public interface IDAL_Medicos
    {
        public Medicos GetMedicoById(int id);
        Medicos GetByMatricula(string matricula);
        IEnumerable<Medicos> GetAllMedicos();
        void AddMedico(Medicos medico);
        void UpdateMedico(Medicos medico);
        void DeleteMedico(string matricula);
    }
}
