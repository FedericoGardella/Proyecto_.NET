using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.IBLs
{
    public interface IBL_Medicos
    {
        public Medicos GetMedicoById(int id);
        Medicos GetByMatricula(string matricula);
        IEnumerable<Medicos> GetAllMedicos();
        void AddMedico(Medicos medico);
        void UpdateMedico(Medicos medico);
        void DeleteMedico(string matricula);
    }
}
