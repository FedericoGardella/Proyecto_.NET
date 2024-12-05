using DAL.IDALs;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_Facturas_EF : IDAL_Facturas
    {
        private DBContext db;
        private string entityName = "Factura";

        public DAL_Facturas_EF(DBContext _db)
        {
            db = _db;
        }

        public Factura Get(long Id)
        {
            return db.Facturas.Find(Id)?.GetEntity();
        }

        public List<Factura> GetAll()
        {
            return db.Facturas.Select(x => x.GetEntity()).ToList();
        }

        public Factura Add(Factura x)
        {
            Facturas toSave = new Facturas();
            toSave = Facturas.FromEntity(x, toSave);
            db.Facturas.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public Factura Update(Factura x)
        {
            Facturas toSave = db.Facturas.FirstOrDefault(c => c.Id == x.Id);
            toSave = Facturas.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            Facturas? toDelete = db.Facturas.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.Facturas.Remove(toDelete);
            db.SaveChanges();
        }

        public List<Factura> GetFacturasPorPaciente(long pacienteId)
        {
            var facturas = db.Facturas
                             .Where(f => f.PacientesId == pacienteId && !f.Mensual)
                             .Include(f => f.ContratosSeguros)
                             .Include(f => f.Citas)
                             .ToList();

            return facturas.Select(f => f.GetEntity()).ToList();
        }

        public List<Factura> GetFacturasMensuales(long pacienteId)
        {
            var facturas = db.Facturas
                             .Where(f => f.PacientesId == pacienteId && f.Mensual && !f.Pago) // Filtra facturas mensuales no pagadas
                             .ToList();

            return facturas.Select(f => f.GetEntity()).ToList();
        }

    }
}

