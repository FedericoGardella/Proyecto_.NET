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
    public class DAL_Personas_EF : IDAL_Personas
    {
        private DBContext db;
        private string entityName = "Persona";

        public DAL_Personas_EF(DBContext _db)
        {
            db = _db;
        }

        public Persona Get(long Id)
        {
            return db.Personas.Find(Id)?.GetEntity();
        }

        public Persona GetXDocumento(string Documento)
        {
            return db.Personas
                     .FirstOrDefault(x => x.Documento == Documento)?
                     .GetEntity();
        }

        public List<Persona> GetAll()
        {
            return db.Personas.Select(x => x.GetEntity()).ToList();
        }

        public Persona Add(Persona x)
        {
            Personas toSave = new Personas();
            toSave = Personas.FromEntity(x, toSave);
            db.Personas.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public Persona Update(Persona x)
        {
            Personas toSave = db.Personas.FirstOrDefault(c => c.Id == x.Id);
            toSave = Personas.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            Personas? toDelete = db.Personas.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.Personas.Remove(toDelete);
            db.SaveChanges();
        }
    }
}
