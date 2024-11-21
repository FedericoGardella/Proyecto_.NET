using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Citas
    {
        Cita Get(long Id);
        List<Cita> GetAll();
        Cita Add(Cita x);
        Cita Update(Cita x);
        void Delete(long Id);
        List<Cita> GetCitasPorPacienteID(long PacienteID);
    }
}
