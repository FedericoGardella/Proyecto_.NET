using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_TiposSeguros
    {
        TipoSeguro Get(long Id);
        List<TipoSeguro> GetAll();
        TipoSeguro Add(TipoSeguro x);
        TipoSeguro Update(TipoSeguro x);
        void Delete(long Id);
    }
}
