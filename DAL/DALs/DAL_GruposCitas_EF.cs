using DAL.IDALs;
using DAL.Models;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_GruposCitas_EF : IDAL_GruposCitas
    {
        private DBContext db;
        private string entityName = "GrupoCita";

        public DAL_GruposCitas_EF(DBContext _db)
        {
            db = _db;
        }

        public GrupoCita Get(long Id)
        {
            return db.GruposCitas.Find(Id)?.GetEntity();
        }

        public List<GrupoCita> GetAll()
        {
            return db.GruposCitas.Select(x => x.GetEntity()).ToList();
        }


        public GrupoCita Add(GrupoCita x)
        {
            GruposCitas toSave = new GruposCitas();
            toSave = GruposCitas.FromEntity(x, toSave);
            db.GruposCitas.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public GrupoCita Update(GrupoCita x)
        {
            GruposCitas toSave = db.GruposCitas.FirstOrDefault(c => c.Id == x.Id);
            toSave = GruposCitas.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            GruposCitas? toDelete = db.GruposCitas.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.GruposCitas.Remove(toDelete);
            db.SaveChanges();
        }
    }
}
