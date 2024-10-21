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
    public class DAL_Especialidades_EF : IDAL_Especialidades
    {

        private readonly DbContext _context;

        public DAL_Especialidades_EF(DbContext context)
        {
            _context = context;
        }
        public Especialidades GetEspecialidadById(int id)
        {
            return _context.Set<Especialidades>().Find(id); // Usando Set<T>() para encontrar por ID
        }

        public IEnumerable<Especialidades> GetAllEspecialidades()
        {
            return _context.Set<Especialidades>().ToList(); // Usando Set<T>() para obtener todas las especialidades
        }

        public void AddEspecialidad(Especialidades especialidad)
        {
            _context.Set<Especialidades>().Add(especialidad); // Usando Set<T>() para añadir una nueva especialidad
            _context.SaveChanges(); // Guarda los cambios en la base de datos
        }

        public void UpdateEspecialidad(Especialidades especialidad)
        {
            _context.Entry(especialidad).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteEspecialidad(int id)
        {
            var especialidad = _context.Set<Especialidades>().Find(id);
            if (especialidad != null)
            {
                _context.Set<Especialidades>().Remove(especialidad);
                _context.SaveChanges();
            }
        }
    }
}
