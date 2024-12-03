using BL.IBLs;
using DAL.IDALs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_Citas : IBL_Citas
    {
        private IDAL_Citas dal;

        public BL_Citas(IDAL_Citas _dal)
        {
            dal = _dal;
        }

        public Cita Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<Cita> GetAll()
        {
            return dal.GetAll();
        }

        public Cita Add(Cita x)
        {
            return dal.Add(x);
        }

        public Cita Update(Cita x)
        {
            return dal.Update(x);
        }

        public void UpdatePaciente(long citaId, long pacienteId)
        {
            var cita = dal.Get(citaId);

            if (cita == null)
            {
                throw new Exception($"No se encontró la cita con ID {citaId}");
            }

            if (cita.PacienteId != null)
            {
                throw new Exception("La cita ya está asignada a otro paciente.");
            }

            cita.PacienteId = pacienteId;
            dal.UpdatePaciente(cita);
        }

        public List<Cita> GetCitasFuturasPorPacienteYEspecialidad(long pacienteId, long especialidadId, DateTime fechaActual)
        {

            var citasFuturas = dal.GetCitasFuturasPorPacienteYEspecialidad(pacienteId, especialidadId, fechaActual);


            return citasFuturas;
        }

        public List<(long PacienteId, string Hora)> GetTomorrowAppointments()
        {
            var tomorrow = DateTime.Now.AddDays(1).Date;

            // Obtener la lista de CitaDTO desde el DAL
            var citasDTO = dal.GetCitasDTOByDate(tomorrow);

            // Transformar la lista de CitaDTO en el formato deseado
            return citasDTO
                .Where(c => c.PacienteId.HasValue)
                .Select(c => (PacienteId: c.PacienteId.Value, Hora: c.Hora.ToString(@"hh\:mm")))
                .ToList();
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }
    }
}
