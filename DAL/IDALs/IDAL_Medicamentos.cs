using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_Medicamentos
    {
        Medicamento Get(long Id);
        List<Medicamento> GetAll();
        Medicamento Add(Medicamento x);
        Medicamento Update(Medicamento x);
        void Delete(long Id);
    }
}
