using DAL.IDALs;
using DAL.Models;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_TiposSeguros_EF : IDAL_TiposSeguros
    {
        private DBContext db;
        private string entityName = "TipoSeguro";

        public DAL_TiposSeguros_EF(DBContext _db)
        {
            db = _db;
        }

        public TipoSeguro Get(long Id)
        {
            return db.TiposSeguros.Find(Id)?.GetEntity();
        }

        public List<TipoSeguro> GetAll()
        {
            return db.TiposSeguros.Select(x => x.GetEntity()).ToList();
        }

        public TipoSeguro Add(TipoSeguro x)
        {
            TiposSeguros toSave = new TiposSeguros();
            toSave = TiposSeguros.FromEntity(x, toSave);
            db.TiposSeguros.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public TipoSeguro Update(TipoSeguro x)
        {
            TiposSeguros toSave = db.TiposSeguros.FirstOrDefault(c => c.Id == x.Id);
            toSave = TiposSeguros.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            TiposSeguros? toDelete = db.TiposSeguros.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.TiposSeguros.Remove(toDelete);
            db.SaveChanges();
        }
    }
}
