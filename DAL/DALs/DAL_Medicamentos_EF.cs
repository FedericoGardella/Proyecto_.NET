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
    public class DAL_Medicamentos_EF : IDAL_Medicamentos
    {
        private DBContext db;
        private string entityName = "Medicamento";

        public DAL_Medicamentos_EF(DBContext _db)
        {
            db = _db;
        }

        public Medicamento Get(long Id)
        {
            return db.Medicamentos.Find(Id)?.GetEntity();
        }

        public List<Medicamento> GetAll()
        {
            return db.Medicamentos.Select(x => x.GetEntity()).ToList();
        }


        public Medicamento Add(Medicamento x)
        {
            Medicamentos toSave = new Medicamentos();
            toSave = Medicamentos.FromEntity(x, toSave);
            db.Medicamentos.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public Medicamento Update(Medicamento x)
        {
            Medicamentos toSave = db.Medicamentos.FirstOrDefault(c => c.Id == x.Id);
            toSave = Medicamentos.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            Medicamentos? toDelete = db.Medicamentos.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.Medicamentos.Remove(toDelete);
            db.SaveChanges();
        }
    }
}
