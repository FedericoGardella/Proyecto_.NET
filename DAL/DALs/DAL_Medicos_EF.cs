using DAL.IDALs;
using DAL.Models;
using Shared.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace DAL.DALs
{
    public class DAL_Medicos_EF : IDAL_Medicos
    {
        private readonly DBContext db;
        private readonly ILogger<DAL_Medicos_EF> logger;
        private readonly string entityName = "Medico";

        public DAL_Medicos_EF(DBContext _db, ILogger<DAL_Medicos_EF> _logger)
        {
            db = _db;
            logger = _logger;
        }

        public Medico Get(long Id)
        {
            return db.Medicos.Find(Id)?.GetEntity();
        }

        public List<Medico> GetAll()
        {
            return db.Medicos.Select(x => x.GetEntity()).ToList();
        }

        public Medico Add(Medico x)
        {
            Medicos toSave = Medicos.FromEntity(x, new Medicos());

            var trackedEntity = db.Medicos.Local.FirstOrDefault(p => p.Id == toSave.Id);

            if (trackedEntity != null)
            {
                db.Entry(trackedEntity).CurrentValues.SetValues(toSave);
            }
            else
            {
                db.Medicos.Add(toSave);
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al guardar el médico y sus especialidades.", ex);
            }

            // Verificar si hay especialidades asociadas
            if (x.Especialidades != null && x.Especialidades.Count > 0)
            {
                // Crear las relaciones en la tabla intermedia manualmente para no buscar las especialidades en el db
                // ya que otro servicio es responsable de ellas
                var especialidadesMedicos = x.Especialidades.Select(especialidad => new
                {
                    EspecialidadId = especialidad.Id,
                    MedicoId = toSave.Id
                }).ToList();

                // Insertar directamente en la tabla intermedia
                foreach (var rel in especialidadesMedicos)
                {
                    db.Database.ExecuteSqlRaw(
                        "INSERT INTO MedicosEspecialidades (EspecialidadesId, MedicosId) VALUES ({0}, {1})",
                        rel.EspecialidadId,
                        rel.MedicoId
                    );
                }
            }

            return Get(toSave.Id);
        }


        public Medico Update(Medico x)
        {
                Medicos toSave = db.Medicos.FirstOrDefault(c => c.Id == x.Id);
                toSave = Medicos.FromEntity(x, toSave);
                db.Update(toSave);
                db.SaveChanges();
                return Get(toSave.Id);
            }

        public void Delete(long Id)
        {
            Medicos? toDelete = db.Medicos.Find(Id);
                if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
                db.Medicos.Remove(toDelete);
                db.SaveChanges();
        }
    }
}
