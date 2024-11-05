using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_Especialidades
    {
        Especialidad Get(long Id);
        List<Especialidad> GetAll();
        Especialidad Add(Especialidad x);
        Especialidad Update(Especialidad x);
        void Delete(long Id);
    }
}
