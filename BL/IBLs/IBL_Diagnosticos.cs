using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Diagnosticos
    {
        Diagnostico Get(long Id);
        List<Diagnostico> GetAll();
        Diagnostico Add(Diagnostico x);
        Diagnostico Update(Diagnostico x);
        void Delete(long Id);
    }
}
