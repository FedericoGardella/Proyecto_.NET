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
    public class DAL_Medicamentos_EF : IDAL_Medicamentos
    {
        private readonly DbContext _context;

        public DAL_Medicamentos_EF(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Medicamentos> ObtenerMedicamentos()
        {
            return _context.Set<Medicamentos>().ToList();
        }

        public Medicamentos ObtenerMedicamentoPorId(int id)
        {
            return _context.Set<Medicamentos>()
                           .FirstOrDefault(m => m.Id == id);
        }

        public void AgregarMedicamento(Medicamentos medicamento)
        {
            _context.Set<Medicamentos>().Add(medicamento);
            _context.SaveChanges();
        }

        public void ActualizarMedicamento(Medicamentos medicamento)
        {
            _context.Set<Medicamentos>().Update(medicamento);
            _context.SaveChanges();
        }

        public void EliminarMedicamento(int id)
        {
            var medicamento = _context.Set<Medicamentos>().Find(id);
            if (medicamento != null)
            {
                _context.Set<Medicamentos>().Remove(medicamento);
                _context.SaveChanges();
            }
        }
    }
}
