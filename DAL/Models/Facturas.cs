using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Facturas
    {
        public Facturas() { }
        public long Id { get; set; }


        public long PacientesId { get; set; }
        public Pacientes Pacientes { get; set; }


        public List<Citas> Citas { get; set; } // SE VA
        public List<FacturasMes> FacturasMes { get; set; } // SE VA

        // Le agrego costostotales?

        public Factura GetEntity()
        {
            return new Factura
            {
                Id = Id,
                PacienteId = PacientesId,
                Paciente = Pacientes?.GetEntity()
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
            facturaToSave.PacientesId = factura.PacienteId;

            return facturaToSave;
        }

    }
}
