using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Citas
    {
        Cita Get(long Id);
        List<Cita> GetAll();
        Cita Add(Cita x);
        Cita Update(Cita x);
        void UpdatePaciente(long citaId, long pacienteId);
        List<Cita> GetCitasFuturasPorPacienteYEspecialidad(long pacienteId, long especialidadId, DateTime fechaActual);
        List<(long PacienteId, string Hora)> GetTomorrowAppointments();
        void Delete(long Id);
    }
}
