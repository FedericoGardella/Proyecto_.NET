using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Medicos
    {
        Medico Get(long Id);
        List<Medico> GetAll();
        Medico Add(Medico x);
        Medico Update(Medico x);
        void Delete(long Id);
        Medico GetByMatricula(string matricula);
    }
}
