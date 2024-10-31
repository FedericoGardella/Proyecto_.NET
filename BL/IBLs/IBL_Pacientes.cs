using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Pacientes
    {
        Paciente Get(long Id);
        List<Paciente> GetAll();
        Paciente Add(Paciente x);
        Paciente Update(Paciente x);
        void Delete(long Id);
        Pacientes GetPacienteByDocumento(string documento);
    }
}