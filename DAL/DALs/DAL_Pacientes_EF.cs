using DAL.IDALs;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
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

        public Paciente Get(long Id, string token)
        {
            return db.Pacientes.Find(Id)?.GetEntity();
        }

        public List<Paciente> GetAll()
        {
            return db.Pacientes.Select(x => x.GetEntity()).ToList();
        }

        public Paciente Add(Paciente x)
        {
            // Convertimos `Paciente` a `Pacientes` para almacenarlo en la base de datos.
            Pacientes toSave = Pacientes.FromEntity(x, new Pacientes());

            // Verificamos si el `dbContext` ya está rastreando esta entidad.
            var trackedEntity = db.Pacientes.Local.FirstOrDefault(p => p.Id == toSave.Id);

            if (trackedEntity != null)
            {
                // Si ya existe una instancia en el contexto, actualizamos los valores
                db.Entry(trackedEntity).CurrentValues.SetValues(toSave);
            }
            else
            {
                // Si es una entidad nueva, la añadimos al contexto
                db.Pacientes.Add(toSave);
            }

            db.SaveChanges();

            // Retornamos la entidad recién agregada, utilizando el método `Get` para obtener su versión más actualizada.
            return Get(toSave.Id, null);
        }

        public Paciente Update(Paciente x)
        {
            Pacientes toSave = db.Pacientes.FirstOrDefault(c => c.Id == x.Id);
            toSave = Pacientes.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id,null);
        }

        public void Delete(long Id)
        {
            Pacientes? toDelete = db.Pacientes.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.Pacientes.Remove(toDelete);
            db.SaveChanges();
        }

        public Paciente GetPacienteByDocumento(string documento)
        {
            var paciente = db.Pacientes.AsNoTracking().FirstOrDefault(p => p.Documento == documento);
            return paciente?.GetEntity();
        }

    }
}
