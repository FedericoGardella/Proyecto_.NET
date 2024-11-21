using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Medicamentos
    {
        Medicamento Get(long Id);
        List<Medicamento> GetByIds(List<long> ids);
        List<Medicamento> GetAll();
        Medicamento Add(Medicamento x);
        Medicamento Update(Medicamento x);
        void Delete(long Id);
    }
}
