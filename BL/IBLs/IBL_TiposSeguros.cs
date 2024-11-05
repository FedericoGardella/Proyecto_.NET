using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_TiposSeguros
    {
        TipoSeguro Get(long Id);
        List<TipoSeguro> GetAll();
        TipoSeguro Add(TipoSeguro x);
        TipoSeguro Update(TipoSeguro x);
        void Delete(long Id);
    }
}
