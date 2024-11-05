using DAL.IDALs;
using DAL.Models;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_Pacientes_EF : IDAL_Pacientes
    {
        private DBContext db;
        private string entityName = "Paciente";

        public DAL_Pacientes_EF(DBContext _db)
        {
            db = _db;
        }

        public Paciente Get(long Id)
        {
            return db.Pacientes.Find(Id)?.GetEntity();
        }

        public List<Paciente> GetAll()
        {
            return db.Pacientes.Select(x => x.GetEntity()).ToList();
        }

        public Paciente Add(Paciente x)
        {
            Pacientes toSave = new Pacientes();
            toSave = Pacientes.FromEntity(x, toSave);
            db.Pacientes.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public Paciente Update(Paciente x)
        {
            Pacientes toSave = db.Pacientes.FirstOrDefault(c => c.Id == x.Id);
            toSave = Pacientes.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            Pacientes? toDelete = db.Pacientes.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.Pacientes.Remove(toDelete);
            db.SaveChanges();
        }

        public Pacientes GetPacienteByDocumento(string documento)
        {
            return db.Pacientes.FirstOrDefault(p => p.Documento == documento);
        }


    }
}
