using DAL.IDALs;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_Citas_EF : IDAL_Citas
    {
        private DBContext db;
        private string entityName = "Cita";

        public DAL_Citas_EF(DBContext _db)
        {
            db = _db;
        }

        public Cita Get(long Id)
        {
            return db.Citas.Find(Id)?.GetEntity();
        }

        public List<Cita> GetAll()
        {
            return db.Citas.Select(x => x.GetEntity()).ToList();
        }


        public Cita Add(Cita x)
        {
            Citas toSave = new Citas();
            toSave = Citas.FromEntity(x, toSave);
            db.Citas.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public Cita Update(Cita x)
        {
            Citas toSave = db.Citas.FirstOrDefault(c => c.Id == x.Id);
            toSave = Citas.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void UpdatePaciente(Cita cita)
        {
            var existingCita = db.Citas.Find(cita.Id);

            if (existingCita == null)
            {
                throw new Exception($"No se encontró la cita con ID {cita.Id}");
            }

            existingCita.PacienteId = cita.PacienteId;

            db.SaveChanges();
        }

        public List<Cita> GetCitasFuturasPorPacienteYEspecialidad(long pacienteId, long especialidadId, DateTime fechaActual)
        {
            return db.Citas
                     .Include(c => c.GruposCitas) // Incluir la relación con el grupo de citas
                     .ThenInclude(g => g.Especialidades) // Incluir la relación con especialidades
                     .Where(c => c.PacienteId == pacienteId &&
                                 c.GruposCitas.EspecialidadesId == especialidadId &&
                                 c.GruposCitas.Fecha > fechaActual)
                     .Select(c => c.GetEntity()) // Convertir a la entidad compartida
                     .ToList(); // Materializar la consulta como una lista
        }

        public void Delete(long Id)
        {
            Citas? toDelete = db.Citas.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.Citas.Remove(toDelete);
            db.SaveChanges();
        }
    }
}
