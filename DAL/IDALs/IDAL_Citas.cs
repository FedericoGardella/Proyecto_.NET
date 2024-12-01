using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_Citas
    {
        Cita Get(long Id);
        List<Cita> GetAll();
        Cita Add(Cita x);
        Cita Update(Cita x);
        void UpdatePaciente(Cita cita);
        List<Cita> GetCitasFuturasPorPacienteYEspecialidad(long pacienteId, long especialidadId, DateTime fechaActual);
        void Delete(long Id);
    }
}
