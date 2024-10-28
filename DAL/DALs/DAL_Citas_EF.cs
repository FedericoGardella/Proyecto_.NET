using DAL.IDALs;
using DAL.Models;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
