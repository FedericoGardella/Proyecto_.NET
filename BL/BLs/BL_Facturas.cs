using BL.IBLs;
using DAL.IDALs;
using Shared.DTOs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_Facturas : IBL_Facturas
    {
        private IDAL_Facturas dal;

        public BL_Facturas(IDAL_Facturas _dal)
        {
            dal = _dal;
        }

        public Factura Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<Factura> GetAll()
        {
            return dal.GetAll();
        }

        public Factura Add(Factura x)
        {
            return dal.Add(x);
        }

        public Factura Update(Factura x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }

        public List<CitaDTO> GetCitas(long facturaId)
        {
            return dal.GetCitas(facturaId);
        }

        public List<FacturaMesDTO> GetFacturasMes(long facturaId)
        {
            return dal.GetFacturasMes(facturaId);
        }
    }
}
