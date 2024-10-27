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
    public class DAL_Medicos_EF : IDAL_Medicos
    {
        private DBContext db;
        private string entityName = "Medico";

        public DAL_Medicos_EF(DBContext _db)
        {
            db = _db;
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
            Medicos toSave = new Medicos();
            toSave = Medicos.FromEntity(x, toSave);
            db.Medicos.Add(toSave);
            db.SaveChanges();
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
