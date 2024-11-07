using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Facturas
    {
        public Facturas() { }
        public int Id { get; set; }
        public DateTime FechaEmision { get; set; }
        public bool Pago { get; set; }

        [ForeignKey("ContratosSeguros")]
        public long ContratosSegurosId { get; set; }
        public ContratosSeguros ContratosSeguros { get; set; }

        public List<Citas> Citas { get; set; }


        public Factura GetEntity()
        {
            Factura factura = new Factura();

            factura.Id = Id;
            factura.FechaEmision = FechaEmision;
            factura.Pago = Pago;

            return factura;
        }

        public static Facturas FromEntity(Factura factura, Facturas facturas)
        {
            Facturas facturaToSave;
            if (facturas == null)
                facturaToSave = new Facturas();
            else
                facturaToSave = facturas;

            facturaToSave.Id = factura.Id;
            facturaToSave.FechaEmision = factura.FechaEmision;
            facturaToSave.Pago = factura.Pago;

            return facturaToSave;
        }

    }
}
