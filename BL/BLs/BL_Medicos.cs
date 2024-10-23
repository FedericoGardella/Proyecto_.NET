using BL.IBLs;
using DAL.Models;
using DAL.IDALs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BLs
{
    public class BL_Medicos : IBL_Medicos
    {
        private readonly IDAL_Medicos _medicos;

        public BL_Medicos(IDAL_Medicos medicos) 
        {
            _medicos = medicos;
        }
        public void AddMedico(Medicos medico)
        {
            _medicos.AddMedico(medico);
        }

        public void DeleteMedico(string matricula)
        {
            _medicos.DeleteMedico(matricula);
        }

        public IEnumerable<Medicos> GetAllMedicos()
        {
            return _medicos.GetAllMedicos();
        }

        public Medicos GetByMatricula(string matricula)
        {
            return _medicos.GetByMatricula(matricula);
        }

        public Medicos GetMedicoById(int id)
        {
            return _medicos.GetMedicoById(id);
        }

        public void UpdateMedico(Medicos medico)
        {
            _medicos.UpdateMedico(medico);
        }
    }
}
