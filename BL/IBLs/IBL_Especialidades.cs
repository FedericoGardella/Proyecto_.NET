using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Especialidades
    {
        Especialidad Get(long Id);
        List<Especialidad> GetAll();
        Especialidad Add(Especialidad x);
        Especialidad Update(Especialidad x);
        void Delete(long Id);
    }
}
