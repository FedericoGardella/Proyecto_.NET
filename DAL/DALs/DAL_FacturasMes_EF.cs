using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.IDALs;
using DAL.Models;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_FacturasMes_EF: IDAL_FacturasMes
    {
        private DBContext db;
        private string entityName = "FacturaMes";


        public DAL_FacturasMes_EF(DBContext _db)
        {
            db = _db;
        }

        public FacturaMes Get(long Id)
        {
            return db.FacturasMes.Find(Id)?.GetEntity();
        }

        public List<FacturaMes> GetAll()
        {
            return db.FacturasMes.Select(x => x.GetEntity()).ToList();
        }


        public FacturaMes Add(FacturaMes x)
        {
            FacturasMes toSave = new FacturasMes();
            toSave = FacturasMes.FromEntity(x, toSave);
            db.FacturasMes.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public FacturaMes Update(FacturaMes x)
        {
            FacturasMes toSave = db.FacturasMes.FirstOrDefault(c => c.Id == x.Id);
            toSave = FacturasMes.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            FacturasMes? toDelete = db.FacturasMes.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.FacturasMes.Remove(toDelete);
            db.SaveChanges();
        }
    }
}
