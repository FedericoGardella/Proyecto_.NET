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
    public class DAL_ResultadosEstudios_EF : IDAL_ResultadosEstudios
    {
        private readonly DbContext _context;

        public DAL_ResultadosEstudios_EF(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<ResultadosEstudios> ObtenerResultadosEstudios()
        {
            return _context.Set<ResultadosEstudios>()
                           .Include(r => r.HistClinId)
                           .ToList();
        }

        public ResultadosEstudios ObtenerResultadoEstudioPorId(int id)
        {
            return _context.Set<ResultadosEstudios>()
                           .Include(r => r.HistClinId)
                           .FirstOrDefault(r => r.Id == id);
        }

        public void AgregarResultadoEstudio(ResultadosEstudios resultadoEstudio)
        {
            _context.Set<ResultadosEstudios>().Add(resultadoEstudio);
            _context.SaveChanges();
        }

        public void ActualizarResultadoEstudio(ResultadosEstudios resultadoEstudio)
        {
            _context.Set<ResultadosEstudios>().Update(resultadoEstudio);
            _context.SaveChanges();
        }

        public void EliminarResultadoEstudio(int id)
        {
            var resultadoEstudio = _context.Set<ResultadosEstudios>().Find(id);
            if (resultadoEstudio != null)
            {
                _context.Set<ResultadosEstudios>().Remove(resultadoEstudio);
                _context.SaveChanges();
            }
        }
    }
}
