using DAL.Models;
using DAL.IDALs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.DALs
{
    public class DAL_Diagnosticos_EF : IDAL_Diagnosticos
    {
        private readonly DbContext _context;

        public DAL_Diagnosticos_EF(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Diagnosticos> ObtenerDiagnosticos()
        {
            return _context.Set<Diagnosticos>().Include(d => d.HistClinId).ToList();
        }

        public Diagnosticos ObtenerDiagnosticoPorId(int id)
        {
            return _context.Set<Diagnosticos>().Include(d => d.HistClinId)
                            .FirstOrDefault(d => d.Id == id);
        }

        public void AgregarDiagnostico(Diagnosticos diagnostico)
        {
            _context.Set<Diagnosticos>().Add(diagnostico);
            _context.SaveChanges();
        }

        public void ActualizarDiagnostico(Diagnosticos diagnostico)
        {
            _context.Set<Diagnosticos>().Update(diagnostico);
            _context.SaveChanges();
        }

        public void EliminarDiagnostico(int id)
        {
            var diagnostico = _context.Set<Diagnosticos>().Find(id);
            if (diagnostico != null)
            {
                _context.Set<Diagnosticos>().Remove(diagnostico);
                _context.SaveChanges();
            }
        }
    }
}
