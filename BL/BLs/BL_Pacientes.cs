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
        public void AddPaciente(Pacientes paciente)
        {
            _pacientes.AddPaciente(paciente);
        }

        public void DeletePaciente(int id)
        {
            _pacientes.DeletePaciente(id);
        }

        public IEnumerable<Pacientes> GetAllPacientes()
        {
            return _pacientes.GetAllPacientes();
        }

        public Pacientes GetPacienteByDocumento(string documento)
        {
            return _pacientes.GetPacienteByDocumento(documento);
        }

        public Pacientes GetPacienteById(int id)
        {
            return _pacientes.GetPacienteById(id);
        }

        public void UpdatePaciente(Pacientes paciente)
        {
            _pacientes.UpdatePaciente(paciente);
        }
    }
}
