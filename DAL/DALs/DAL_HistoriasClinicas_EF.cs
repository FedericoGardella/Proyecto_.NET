using DAL.Models;
using DAL.IDALs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_HistoriasClinicas_EF : IDAL_HistoriasClinicas
    {
        private readonly DbContext _context;

        public DAL_HistoriasClinicas_EF(DbContext context)
        {
            _context = context;
        }

        // Método para obtener todas las historias clínicas con detalles completos
        public List<HistoriaClinica> ObtenerHistoriasClinicas()
        {
            return _context.HistoriasClinicas.Select(x => x.GetEntity()).ToList();
        }

        // Método para obtener una historia clínica por su Id
        public HistoriaClinica ObtenerHistoriaClinicaPorId(int id)
        {
            return _context.Set<HistoriaClinicas>()
                           .Include(h => h.PacienteId)
                           .Include(h => h.Diagnosticos)
                           .Include(h => h.Recetas)
                           .Include(h => h.ResultadosEstudios)
                           .FirstOrDefault(h => h.Id == id);
        }

        // Método para agregar una historia clínica
        public void AgregarHistoriaClinica(HistoriasClinicas historiaClinica)
        {
            _context.Set<HistoriasClinicas>().Add(historiaClinica);
            _context.SaveChanges();
        }

        // Método para actualizar una historia clínica
        public void ActualizarHistoriaClinica(HistoriasClinicas historiaClinica)
        {
            _context.Set<HistoriasClinicas>().Update(historiaClinica);
            _context.SaveChanges();
        }

        // Método para eliminar una historia clínica
        public void EliminarHistoriaClinica(int id)
        {
            var historiaClinica = _context.Set<HistoriasClinicas>().Find(id);
            if (historiaClinica != null)
            {
                _context.Set<HistoriasClinicas>().Remove(historiaClinica);
                _context.SaveChanges();
            }
        }
    }
}
