using DAL.IDALs;
using DAL.Models;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_Especialidades_EF : IDAL_Especialidades
    {
        private DBContext db;
        private string entityName = "Especialidad";

        public DAL_Especialidades_EF(DBContext _db)
        {
            db = _db;
        }

        public Especialidad Get(long Id)
        {
            var especialidad = db.Especialidades.Find(Id)?.GetEntity();
            if (especialidad == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            return especialidad;
        }

        public List<Especialidad> GetByIds(List<long> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return new List<Especialidad>();
            }

            return db.Set<Especialidades>()
                     .Where(m => ids.Contains(m.Id))
                     .Select(m => m.GetEntity()) // Convierte cada entidad en su modelo correspondiente
                     .ToList();
        }

        public List<Especialidad> GetAll()
        {
            return db.Especialidades.Select(x => x.GetEntity()).ToList();
        }


        public Especialidad Add(Especialidad x)
        {
            Especialidades toSave = new Especialidades();
            toSave = Especialidades.FromEntity(x, toSave);
            db.Especialidades.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public Especialidad Update(Especialidad x)
        {
            Especialidades toSave = db.Especialidades.FirstOrDefault(c => c.Id == x.Id);
            toSave = Especialidades.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            Especialidades? toDelete = db.Especialidades.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.Especialidades.Remove(toDelete);
            db.SaveChanges();
        }
    }
}
