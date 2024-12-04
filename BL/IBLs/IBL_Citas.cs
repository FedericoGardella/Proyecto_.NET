using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Citas
    {
        Cita Get(long Id);
        List<Cita> GetAll();
        Cita Add(Cita x);
        Cita Update(Cita x);
        void UpdatePaciente(long citaId, long pacienteId, long tipoSeguroId);
        void Delete(long Id);
    }
}
