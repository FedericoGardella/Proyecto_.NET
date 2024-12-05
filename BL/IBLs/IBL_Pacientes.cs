using DAL.Models;
using Shared.DTOs;
using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Pacientes
    {
        Paciente Get(long Id, string token);
        List<Paciente> GetAll();
        Paciente Add(Paciente x);
        PacienteDTO Update(PacienteDTO x);
        void Delete(long Id);
        Paciente GetPacienteByDocumento(string documento);
        string GetPacienteEmail(long id);
        List<ContratoSeguroDTO> GetContratosSeguros(long PacienteId);
    }
}