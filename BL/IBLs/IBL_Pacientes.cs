using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.IBLs
{
    public interface IBL_Pacientes
    {
        Pacientes GetPacienteById(int id);
        IEnumerable<Pacientes> GetAllPacientes();
        void AddPaciente(Pacientes paciente);
        void UpdatePaciente(Pacientes paciente);
        void DeletePaciente(int id);
        Pacientes GetPacienteByDocumento(string documento);
    }
}