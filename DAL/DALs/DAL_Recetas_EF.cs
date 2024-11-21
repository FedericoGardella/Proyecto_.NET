using DAL.IDALs;
using DAL.Models;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_Recetas_EF : IDAL_Recetas
    {
        private DBContext db;
        private string entityName = "Receta";

        public DAL_Recetas_EF(DBContext _db)
        {
            db = _db;
        }

        public Receta Get(long Id)
        {
            return db.Recetas.Find(Id)?.GetEntity();
        }

        public List<Receta> GetAll()
        {
            return db.Recetas.Select(x => x.GetEntity()).ToList();
        }


        public Receta Add(Receta x)
        {
            // Convertir la receta a su modelo de base de datos
            Recetas toSave = Recetas.FromEntity(x, new Recetas());

            // Verificar si hay medicamentos asociados
            if (x.Medicamentos != null && x.Medicamentos.Count > 0)
            {
                // Obtener los medicamentos por sus IDs y asociarlos
                var medicamentos = db.Medicamentos
                                     .Where(m => x.Medicamentos.Select(med => med.Id).Contains(m.Id))
                                     .ToList();

                toSave.Medicamentos.AddRange(medicamentos);
            }

            // Agregar y guardar la receta con sus relaciones
            db.Recetas.Add(toSave);
            db.SaveChanges();

            // Retornar la receta recién creada
            return Get(toSave.Id);
        }



        public Receta Update(Receta x)
        {
            Recetas toSave = db.Recetas.FirstOrDefault(c => c.Id == x.Id);
            toSave = Recetas.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            Recetas? toDelete = db.Recetas.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.Recetas.Remove(toDelete);
            db.SaveChanges();
        }
    }
}
