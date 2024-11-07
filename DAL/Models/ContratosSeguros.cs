using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class ContratosSeguros
    {
        public ContratosSeguros() { }
        public long Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; } // Ej: Activo, Cancelado, etc.
        public Pacientes Pacientes { get; set; }

        [ForeignKey("TiposSeguros")]
        public long TiposSegurosId { get; set; }
        public TiposSeguros TiposSeguros { get; set; }


        public ContratoSeguro GetEntity()
        {
            ContratoSeguro contratoSeguro = new ContratoSeguro();

            contratoSeguro.Id = Id;
            contratoSeguro.FechaInicio = FechaInicio;
            contratoSeguro.Estado = Estado;
            // Paciente y tipoSeguro van?

            return contratoSeguro;
        }

        public static ContratosSeguros FromEntity(ContratoSeguro contratoSeguro, ContratosSeguros contratosSeguros)
        {
            ContratosSeguros contratoSeguroToSave;
            if (contratosSeguros == null)
                contratoSeguroToSave = new ContratosSeguros();
            else
                contratoSeguroToSave = contratosSeguros;

            contratoSeguroToSave.Id = contratoSeguro.Id;
            contratoSeguroToSave.FechaInicio = contratoSeguro.FechaInicio;
            contratoSeguroToSave.Estado = contratoSeguro.Estado;

            return contratoSeguroToSave;
        }
    }
}
