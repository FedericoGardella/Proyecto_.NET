using DAL.IDALs;
using DAL.Models;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_HistoriasClinicas_EF : IDAL_HistoriasClinicas
    {
        private DBContext db;
        private string entityName = "HistoriaClinica";

        public DAL_HistoriasClinicas_EF(DBContext _db)
        {
            db = _db;
        }

        public HistoriaClinica Get(long Id)
        {
            return db.HistoriasClinicas.Find(Id)?.GetEntity();
        }

        public List<HistoriaClinica> GetAll()
        {
            return db.HistoriasClinicas.Select(x => x.GetEntity()).ToList();
        }


        public HistoriaClinica Add(HistoriaClinica x)
        {
            HistoriasClinicas toSave = new HistoriasClinicas();
            toSave = HistoriasClinicas.FromEntity(x, toSave);
            db.HistoriasClinicas.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public HistoriaClinica Update(HistoriaClinica x)
        {
            HistoriasClinicas toSave = db.HistoriasClinicas.FirstOrDefault(c => c.Id == x.Id);
            toSave = HistoriasClinicas.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            HistoriasClinicas? toDelete = db.HistoriasClinicas.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.HistoriasClinicas.Remove(toDelete);
            db.SaveChanges();
        }
    }
}
