using DAL.IDALs;
using DAL.Models;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_ContratosSeguros_EF : IDAL_ContratosSeguros
    {
        private DBContext db;
        private string entityName = "ContratoSeguro";

        public DAL_ContratosSeguros_EF(DBContext _db)
        {
            db = _db;
        }

        public ContratoSeguro Get(long Id)
        {
            return db.ContratosSeguros.Find(Id)?.GetEntity();
        }

        public List<ContratoSeguro> GetAll()
        {
            return db.ContratosSeguros.Select(x => x.GetEntity()).ToList();
        }


        public ContratoSeguro Add(ContratoSeguro x)
        {
            ContratosSeguros toSave = new ContratosSeguros();
            toSave = ContratosSeguros.FromEntity(x, toSave);
            db.ContratosSeguros.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public ContratoSeguro Update(ContratoSeguro x)
        {
            ContratosSeguros toSave = db.ContratosSeguros.FirstOrDefault(c => c.Id == x.Id);
            toSave = ContratosSeguros.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            ContratosSeguros? toDelete = db.ContratosSeguros.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.ContratosSeguros.Remove(toDelete);
            db.SaveChanges();
        }

        public ContratosSeguros GetContratoActivoPorPaciente(long pacienteId)
        {
            return db.ContratosSeguros
                .FirstOrDefault(cs => cs.PacientesId == pacienteId && cs.Activo);
        }

        public ContratoSeguro GetContratoActivoPaciente(long pacienteId)
        {
            var contratoActivo = db.ContratosSeguros
                .FirstOrDefault(cs => cs.PacientesId == pacienteId && cs.Activo);

            // Convertir a Shared.Entities.ContratoSeguro
            return contratoActivo?.GetEntity();
        }

    }
}
