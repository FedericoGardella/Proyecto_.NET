using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_GruposCitas
    {
        GrupoCita Get(long Id);
        List<GrupoCita> GetAll();
        GrupoCita Add(GrupoCita x);
        GrupoCita Update(GrupoCita x);
        void Delete(long Id);
    }
}
