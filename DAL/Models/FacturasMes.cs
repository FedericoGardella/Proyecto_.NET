using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Shared.Entities;

namespace DAL.Models
{
    public class FacturasMes
    {
        public long Id { get; set; }
        public decimal GastosMes { get; set; } // Total de gastos del mes
        public DateTime FechaEmision { get; set; } // Fecha de emisión
        public bool Pagada { get; set; } // Indica si fue pagada

        // Contrato seguro activo
        [ForeignKey("ContratosSeguros")]
        public long ContratosSegurosId { get; set; }
        public ContratosSeguros ContratosSeguros { get; set; }
        public decimal CostoContrato { get; set; } // Guardamos el costo del contrato ya que puede cambiar con el tiempo

        // Relación con Facturas
        [ForeignKey("Facturas")]
        public long FacturasId { get; set; }
        public Facturas Facturas { get; set; }



        public FacturaMes GetEntity()
        {
            return new FacturaMes
            {
                Id = Id,
                FechaEmision = FechaEmision,
                Pagada = Pagada,
                GastosMes = GastosMes,
                CostoContrato = CostoContrato,
                ContratoSeguroId = ContratosSegurosId,
                ContratoSeguro = ContratosSeguros?.GetEntity(),
                FacturaId = FacturasId,
                Factura = Facturas?.GetEntity()
            };
        }

        public static FacturasMes FromEntity(FacturaMes facturaMes, FacturasMes facturasMes)
        {
            FacturasMes facturaMesToSave;
            if (facturasMes == null)
                facturaMesToSave = new FacturasMes();
            else
                facturaMesToSave = facturasMes;

            facturaMesToSave.Id = facturaMes.Id;
            facturaMesToSave.FechaEmision = facturaMes.FechaEmision;
            facturaMesToSave.Pagada = facturaMes.Pagada;
            facturaMesToSave.GastosMes = facturaMes.GastosMes;
            facturaMesToSave.CostoContrato = facturaMes.CostoContrato;
            facturaMesToSave.ContratosSegurosId = facturaMes.ContratoSeguroId;
            facturaMesToSave.FacturasId = facturaMes.FacturaId;

            return facturaMesToSave;
        }
    }
}
