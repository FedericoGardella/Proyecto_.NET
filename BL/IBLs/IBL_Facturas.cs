using Shared.DTOs;
using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Facturas
    {
        Factura Get(long Id);
        List<Factura> GetAll();
        Factura Add(Factura x);
        Factura Update(Factura x);
        void Delete(long Id);
        List<FacturaDTO> GetFacturasPorPaciente(long pacienteId);
        public void GenerarFacturasMensuales(string token);
        List<FacturaDTO> GetFacturasMensuales(long pacienteId);
        //List<CitaDTO> GetCitas(long facturaId);
    }
}
