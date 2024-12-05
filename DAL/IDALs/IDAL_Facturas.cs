using DAL.Models;
using Shared.DTOs;
using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_Facturas
    {
        Factura Get(long Id);
        List<Factura> GetAll();
        Factura Add(Factura x);
        Factura Update(Factura x);
        void Delete(long Id);
        List<Factura> GetFacturasPorPaciente(long pacienteId);
        List<Factura> GetFacturasMensuales(long pacienteId);
        //List<CitaDTO> GetCitas(long facturaId);
    }
}
