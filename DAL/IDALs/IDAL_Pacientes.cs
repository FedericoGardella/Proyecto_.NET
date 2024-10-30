using DAL.Models;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDALs
{
    public interface IDAL_Pacientes
    {
        Pacientes GetPacienteById(int id);
        IEnumerable<Pacientes> GetAllPacientes();
        void AddPaciente(Pacientes paciente);
        void UpdatePaciente(Pacientes paciente);
        void DeletePaciente(int id);
        Pacientes GetPacienteByDocumento(string documento);

    }
}
