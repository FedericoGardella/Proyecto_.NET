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
    public class DAL_Pacientes_EF : IDAL_Pacientes
    {
        private readonly DbContext _context;

        public DAL_Pacientes_EF(DbContext context)
        { 
            _context = context; 
        }

        public void AddPaciente(Pacientes paciente)
        {
            _context.Set<Pacientes>().Add(paciente);
            _context.SaveChanges();
        }

        public void DeletePaciente(int id)
        {
            var paciente = _context.Set<Pacientes>().Find(id);
            if (paciente != null)
            {
                _context.Set<Pacientes>().Remove(paciente);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Pacientes> GetAllPacientes()
        {
            return _context.Set<Pacientes>().ToList();
        }

        public Pacientes GetPacienteByDocumento(string documento)
        {
            return _context.Set<Pacientes>().FirstOrDefault(p => p.Documento == documento);
        }

        public Pacientes GetPacienteById(int id)
        {
            return _context.Set<Pacientes>().Find(id);
        }
        public void UpdatePaciente(Pacientes paciente)
        {
            _context.Entry(paciente).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }


}

