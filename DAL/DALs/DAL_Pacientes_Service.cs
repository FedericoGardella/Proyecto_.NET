using DAL.IDALs;
using DAL.Models;
using Shared.DTOs;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_Pacientes_Service : IDAL_Pacientes
    {
        public Paciente Add(Paciente x)
        {
            throw new NotImplementedException();
        }

        public void Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public Paciente Get(long Id)
        {
            throw new NotImplementedException();
        }

        public List<Paciente> GetAll()
        {
            throw new NotImplementedException();
        }

        public Paciente GetPacienteByDocumento(string documento)
        {
            throw new NotImplementedException();
        }

        public Paciente Update(Paciente x)
        {
            throw new NotImplementedException();
        }

        public List<ContratoSeguroDTO> GetContratosSeguros(long PacienteId) 
        { 
            throw new NotImplementedException(); 
        }
    }
}
