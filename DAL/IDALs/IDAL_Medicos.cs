using Shared.DTOs;
using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_Medicos
    {
        Medico Get(long Id);
        List<Medico> GetAll();
        Medico Add(Medico x);
        MedicoDTO Update(MedicoDTO x);
        void Delete(long Id);
    }
}
