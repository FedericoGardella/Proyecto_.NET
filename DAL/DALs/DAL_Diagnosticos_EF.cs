using DAL.Models;
using DAL.IDALs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_Diagnosticos_EF : IDAL_Diagnosticos
    {
        private DBContext db;
        private string entityName = "Diagnostico";

        public DAL_Diagnosticos_EF(DBContext _db)
        {
            db = _db;
        }

        public Diagnostico Get(long Id)
        {
            return db.Diagnosticos.Find(Id)?.GetEntity();
        }

        public List<Diagnostico> GetAll()
        {
            return db.Diagnosticos.Select(x => x.GetEntity()).ToList();
        }

        public Diagnostico Add(Diagnostico x)
        {
            Diagnosticos toSave = new Diagnosticos();
            toSave = Diagnosticos.FromEntity(x, toSave);
            db.Diagnosticos.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public Diagnostico Update(Diagnostico x)
        {
            Diagnosticos toSave = db.Diagnosticos.FirstOrDefault(c => c.Id == x.Id);
            toSave = Diagnosticos.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            Diagnosticos? toDelete = db.Diagnosticos.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.Diagnosticos.Remove(toDelete);
            db.SaveChanges();
        }
    }
}
