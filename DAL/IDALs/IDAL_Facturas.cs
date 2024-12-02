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
        //List<CitaDTO> GetCitas(long facturaId);
    }
}
