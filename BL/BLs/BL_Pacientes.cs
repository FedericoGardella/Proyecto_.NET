using BL.IBLs;
using DAL.IDALs;
using DAL.Models;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BLs
{
    public class BL_Pacientes : IBL_Pacientes
    {
        private IDAL_Pacientes dal;

        private readonly IDAL_Pacientes _pacientes;

        public BL_Pacientes(IDAL_Pacientes pacientes)
        {
            _pacientes = pacientes;
        }
        public Paciente Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<Paciente> GetAll()
        {
            return dal.GetAll();
        }

        public Paciente Add(Paciente x)
        {
            return dal.Add(x);
        }

        public Paciente Update(Paciente x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }

        public Pacientes GetPacienteByDocumento(string documento)
        {
            return _pacientes.GetPacienteByDocumento(documento);
        }
    }
}
