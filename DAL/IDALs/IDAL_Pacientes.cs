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
        Paciente Get(long Id);
        List<Paciente> GetAll();
        Paciente Add(Paciente x);
        Paciente Update(Paciente x);
        public void Delete(long Id);
        Pacientes GetPacienteByDocumento(string documento);

    }
}
