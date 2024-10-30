using DAL.IDALs;
using DAL.Models;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALs
{
    public class DAL_Paciente_Service : IDAL_Pacientes
    {
        Pacientes IDAL_Pacientes.GetPacienteById(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Pacientes> IDAL_Pacientes.GetAllPacientes()
        {
            throw new NotImplementedException();
        }

        void IDAL_Pacientes.AddPaciente(Pacientes paciente)
        {
            throw new NotImplementedException();
        }

        void IDAL_Pacientes.UpdatePaciente(Pacientes paciente)
        {
            throw new NotImplementedException();
        }

        void IDAL_Pacientes.DeletePaciente(int id)
        {
            throw new NotImplementedException();
        }

        Pacientes IDAL_Pacientes.GetPacienteByDocumento(string documento)
        {
            throw new NotImplementedException();
        }
    }
}
