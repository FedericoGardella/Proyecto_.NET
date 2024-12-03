using DAL.Models;
using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_ContratosSeguros
    {
        ContratoSeguro Get(long Id);
        List<ContratoSeguro> GetAll();
        ContratoSeguro Add(ContratoSeguro x);
        ContratoSeguro Update(ContratoSeguro x);
        void Delete(long Id);
        ContratosSeguros GetContratoActivoPorPaciente(long pacienteId);
        ContratoSeguro GetContratoActivoPaciente(long pacienteId);
    }
}
