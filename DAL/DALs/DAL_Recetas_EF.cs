using DAL.IDALs;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALs
{
    public class DAL_Recetas_EF : IDAL_Recetas
    {
        private readonly DbContext _context;

        public DAL_Recetas_EF(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Recetas> ObtenerRecetas()
        {
            return _context.Set<Recetas>()
                           .Include(r => r.HistClinId)
                           .Include(r => r.Medicamentos)
                           .ToList();
        }

        public Recetas ObtenerRecetaPorId(int id)
        {
            return _context.Set<Recetas>()
                           .Include(r => r.HistClinId)
                           .Include(r => r.Medicamentos)
                           .FirstOrDefault(r => r.Id == id);
        }

        public void AgregarReceta(Recetas receta)
        {
            _context.Set<Recetas>().Add(receta);
            _context.SaveChanges();
        }

        public void ActualizarReceta(Recetas receta)
        {
            _context.Set<Recetas>().Update(receta);
            _context.SaveChanges();
        }

        public void EliminarReceta(int id)
        {
            var receta = _context.Set<Recetas>().Find(id);
            if (receta != null)
            {
                _context.Set<Recetas>().Remove(receta);
                _context.SaveChanges();
            }
        }
    }
}
