using DAL.Models;
using Shared.DTOs;
using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_Pacientes
    {
        Paciente Get(long Id, string token);
        List<Paciente> GetAll();
        Paciente Add(Paciente x);
        PacienteDTO Update(PacienteDTO x);
        public void Delete(long Id);
        Paciente GetPacienteByDocumento(string documento);
        List<ContratoSeguroDTO> GetContratosSeguros(long PacienteId);
        string GetEmail(long pacienteId);
    }
}
