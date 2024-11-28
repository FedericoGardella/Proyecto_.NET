using DAL.IDALs;
using DAL.Models;
using Microsoft.Extensions.Logging;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_PreciosEspecialidades_EF : IDAL_PreciosEspecialidades
    {
        private DBContext db;
        private string entityName = "PrecioEspecialidad";

        public DAL_PreciosEspecialidades_EF(DBContext _db)
        {
            db = _db;
        }

        public PrecioEspecialidad Get(long Id)
        {
            return db.PreciosEspecialidades.Find(Id)?.GetEntity();
        }

        public List<PrecioEspecialidad> GetAll()
        {
            return db.PreciosEspecialidades.Select(x => x.GetEntity()).ToList();
        }

        public PrecioEspecialidad Add(PrecioEspecialidad x)
        {
            PreciosEspecialidades toSave = new PreciosEspecialidades();
            toSave = PreciosEspecialidades.FromEntity(x, toSave);

            if (!db.Articulos.Any(a => a.Id == toSave.ArticulosId))
            {
                throw new Exception($"El artículo con ID {toSave.ArticulosId} no existe en la base de datos.");
            }


            db.PreciosEspecialidades.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public PrecioEspecialidad Update(PrecioEspecialidad x)
        {
            PreciosEspecialidades toSave = db.PreciosEspecialidades.FirstOrDefault(c => c.Id == x.Id);
            toSave = PreciosEspecialidades.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            PreciosEspecialidades? toDelete = db.PreciosEspecialidades.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.PreciosEspecialidades.Remove(toDelete);
            db.SaveChanges();
        }
    }
}
