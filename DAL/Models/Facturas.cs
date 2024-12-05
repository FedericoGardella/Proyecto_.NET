using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Facturas
    {
        public Facturas() { }
        public long Id { get; set; }
        public DateTime Fecha { get; set; } // Fecha de emisión de la factura
        public bool Pago { get; set; } // Indicador de si está pagada
        public decimal Costo { get; set; } // Costo total de la factura
        public bool Mensual { get; set; } // Indica si es una factura mensual


        [ForeignKey("Pacientes")]
        public long PacientesId { get; set; }
        public Pacientes Pacientes { get; set; }

        [ForeignKey("ContratosSeguros")]
        public long? ContratosSegurosId { get; set; }
        public ContratosSeguros? ContratosSeguros { get; set; }

        [ForeignKey("Citas")]
        public long? CitasId { get; set; }
        public Citas? Citas { get; set; }


        public Factura GetEntity()
        {
            return new Factura
            {
                Id = Id,
                Fecha = Fecha,
                Pago = Pago,
                Costo = Costo,
                Mensual = Mensual,
                PacienteId = PacientesId,
                Paciente = Pacientes?.GetEntity(),
                ContratoSeguroId = ContratosSegurosId,
                ContratoSeguro = ContratosSeguros?.GetEntity(),
                CitaId = CitasId,
                Cita = Citas?.GetEntity()
            };
        }

        public static Facturas FromEntity(Factura factura, Facturas facturas)
        {
            Facturas facturaToSave;
            if (facturas == null)
                facturaToSave = new Facturas();
            else
                facturaToSave = facturas;

            facturaToSave.Id = factura.Id;
            facturaToSave.Fecha = factura.Fecha;
            facturaToSave.Pago = factura.Pago;
            facturaToSave.Costo = factura.Costo;
            facturaToSave.Mensual = factura.Mensual;
            facturaToSave.PacientesId = factura.PacienteId;
            facturaToSave.ContratosSegurosId = factura.ContratoSeguroId;
            facturaToSave.CitasId = factura.CitaId;

            return facturaToSave;
        }

    }
}
