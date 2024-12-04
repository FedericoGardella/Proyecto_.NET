using DAL.IDALs;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_PreciosEspecialidades_EF : IDAL_PreciosEspecialidades
    {
        private DBContext db;
        private string entityName = "PrecioEspecialidad";

        public DAL_PreciosEspecialidades_EF(DBContext _db)
        {
            db = _db;
        }

        public PrecioEspecialidad Get(long Id)
        {
            return db.PreciosEspecialidades.Find(Id)?.GetEntity();
        }

        public List<PrecioEspecialidad> GetAll()
        {
            var preciosEspecialidades = db.PreciosEspecialidades
                                        .Include(p => p.Especialidades) // Incluye la relación con Especialidad
                                        .Include(p => p.TiposSeguros)  // Incluye la relación con TipoSeguro
                                        .Include(p => p.Articulos)    // Incluye el Artículo para obtener el costo
                                        .ToList();

            return preciosEspecialidades.Select(pe => pe.GetEntity()).ToList();
        }

        public PrecioEspecialidad Add(PrecioEspecialidad x)
        {
            PreciosEspecialidades toSave = new PreciosEspecialidades();
            toSave = PreciosEspecialidades.FromEntity(x, toSave);

            if (!db.Articulos.Any(a => a.Id == toSave.ArticulosId))
            {
                throw new Exception($"El artículo con ID {toSave.ArticulosId} no existe en la base de datos.");
            }


            db.PreciosEspecialidades.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public PrecioEspecialidad Update(PrecioEspecialidad x)
        {
            PreciosEspecialidades toSave = db.PreciosEspecialidades.FirstOrDefault(c => c.Id == x.Id);
            toSave = PreciosEspecialidades.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            PreciosEspecialidades? toDelete = db.PreciosEspecialidades.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.PreciosEspecialidades.Remove(toDelete);
            db.SaveChanges();
        }

        public PrecioEspecialidad GetByEspecialidadAndTipoSeguro(long especialidadId, long tipoSeguroId)
        {
            // Busca el registro en la base de datos
            var precioEspecialidad = db.PreciosEspecialidades
                .FirstOrDefault(pe => pe.EspecialidadesId == especialidadId && pe.TiposSegurosId == tipoSeguroId);

            return precioEspecialidad?.GetEntity();
        }

        public bool Repetido(long especialidadId, long tipoSeguroId)
        {
            return db.PreciosEspecialidades
                .Any(pe => pe.EspecialidadesId == especialidadId && pe.TiposSegurosId == tipoSeguroId);
        }


    }
}
