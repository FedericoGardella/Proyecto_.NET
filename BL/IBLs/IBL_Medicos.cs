using Shared.DTOs;
using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Medicos
    {
        Medico Get(long Id);
        List<Medico> GetAll();
        Medico Add(Medico x);
        MedicoDTO Update(MedicoDTO x);
        void Delete(long Id);
    }
}
