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
    public class DAL_Medicos_EF : IDAL_Medicos
    {
        private readonly DbContext _context;

        public DAL_Medicos_EF(DbContext context)
        {
            _context = context;
        }
        public Medicos GetMedicoById(int id)
        {
            return _context.Set<Medicos>()
                           .Include(m => m.Especialidad)
                           .FirstOrDefault(m => m.Id == id);
        }

        public Medicos GetByMatricula(string matricula)
        {
            return _context.Set<Medicos>()
                           .Include(m => m.Especialidad)
                           .FirstOrDefault(m => m.Matricula == matricula);
        }

        public IEnumerable<Medicos> GetAllMedicos()
        {
            return _context.Set<Medicos>()
                           .Include(m => m.Especialidad)
                           .ToList();
        }

        public void AddMedico(Medicos medico)
        {
            _context.Set<Medicos>().Add(medico);
            _context.SaveChanges();
        }

        public void UpdateMedico(Medicos medico)
        {
            _context.Entry(medico).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteMedico(int id) // Cambiado a eliminar por ID
        {
            var medico = _context.Set<Medicos>().Find(id);
            if (medico != null)
            {
                _context.Set<Medicos>().Remove(medico);
                _context.SaveChanges();
            }
        }
    }
}
