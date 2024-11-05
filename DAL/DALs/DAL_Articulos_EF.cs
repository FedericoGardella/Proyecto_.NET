using DAL.IDALs;
using DAL.Models;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_Articulos_EF : IDAL_Articulos
    {
        private DBContext db;
        private string entityName = "Articulo";

        public DAL_Articulos_EF(DBContext _db)
        {
            db = _db;
        }

        public Articulo Get(long Id)
        {
            return db.Articulos.Find(Id)?.GetEntity();
        }

        public List<Articulo> GetAll()
        {
            return db.Articulos.Select(x => x.GetEntity()).ToList();
        }


        public Articulo Add(Articulo x)
        {
            Articulos toSave = new Articulos();
            toSave = Articulos.FromEntity(x, toSave);
            db.Articulos.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public Articulo Update(Articulo x)
        {
            Articulos toSave = db.Articulos.FirstOrDefault(c => c.Id == x.Id);
            toSave = Articulos.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            Articulos? toDelete = db.Articulos.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.Articulos.Remove(toDelete);
            db.SaveChanges();
        }
    }
}
