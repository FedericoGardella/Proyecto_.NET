using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_Citas
    {
        Cita Get(long Id);
        List<Cita> GetAll();
        Cita Add(Cita x);
        Cita Update(Cita x);
        void Delete(long Id);
        List<Cita> GetCitasPorPacienteId(long pacienteId);
    }
}
