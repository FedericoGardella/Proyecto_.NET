using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_GruposCitas
    {
        GrupoCita Get(long Id);
        List<GrupoCita> GetAll();
        GrupoCita Add(GrupoCita x);
        GrupoCita Update(GrupoCita x);
        void Delete(long Id);
    }
}
