using DAL.IDALs;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALs
{
    public class DAL_Recetas_EF : IDAL_Recetas
    {
        private DBContext db;
        private string entityName = "Receta";

        public DAL_Recetas_EF(DBContext _db)
        {
            db = _db;
        }

        public Receta Get(long Id)
        {
            return db.Recetas.Find(Id)?.GetEntity();
        }

        public List<Receta> GetAll()
        {
            return db.Recetas.Select(x => x.GetEntity()).ToList();
        }


        public Receta Add(Receta x)
        {
            Recetas toSave = new Recetas();
            toSave = Recetas.FromEntity(x, toSave);
            db.Recetas.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public Receta Update(Receta x)
        {
            Recetas toSave = db.Recetas.FirstOrDefault(c => c.Id == x.Id);
            toSave = Recetas.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            Recetas? toDelete = db.Recetas.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.Recetas.Remove(toDelete);
            db.SaveChanges();
        }
    }
}
