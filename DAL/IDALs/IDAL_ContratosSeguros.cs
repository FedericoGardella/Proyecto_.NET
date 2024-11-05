using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_ContratosSeguros
    {
        ContratoSeguro Get(long Id);
        List<ContratoSeguro> GetAll();
        ContratoSeguro Add(ContratoSeguro x);
        ContratoSeguro Update(ContratoSeguro x);
        void Delete(long Id);
    }
}
