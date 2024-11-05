using DAL.IDALs;
using DAL.Models;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_ResultadosEstudios_EF : IDAL_ResultadosEstudios
    {
        private DBContext db;
        private string entityName = "ResultadoEstudio";

        public DAL_ResultadosEstudios_EF(DBContext _db)
        {
            db = _db;
        }

        public ResultadoEstudio Get(long Id)
        {
            return db.ResultadosEstudios.Find(Id)?.GetEntity();
        }

        public List<ResultadoEstudio> GetAll()
        {
            return db.ResultadosEstudios.Select(x => x.GetEntity()).ToList();
        }


        public ResultadoEstudio Add(ResultadoEstudio x)
        {
            ResultadosEstudios toSave = new ResultadosEstudios();
            toSave = ResultadosEstudios.FromEntity(x, toSave);
            db.ResultadosEstudios.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public ResultadoEstudio Update(ResultadoEstudio x)
        {
            ResultadosEstudios toSave = db.ResultadosEstudios.FirstOrDefault(c => c.Id == x.Id);
            toSave = ResultadosEstudios.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            ResultadosEstudios? toDelete = db.ResultadosEstudios.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.ResultadosEstudios.Remove(toDelete);
            db.SaveChanges();
        }
    }
}
