using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class ContratosSeguros
    {
        public ContratosSeguros() {
        }
        public long Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public bool Activo { get; set; }

        [ForeignKey("Pacientes")]
        public long PacientesId { get; set; } // Clave foránea hacia Pacientes
        public Pacientes Pacientes { get; set; }

        [ForeignKey("TiposSeguros")]
        public long TiposSegurosId { get; set; } // Clave foránea hacia TiposSeguros
        public TiposSeguros TiposSeguros { get; set; }


        public ContratoSeguro GetEntity()
        {
            return new ContratoSeguro
            {
                Id = Id,
                FechaInicio = FechaInicio,
                Activo = Activo,
                PacienteId = PacientesId,
                //Paciente = Pacientes?.GetEntity(),
                TipoSeguroId = TiposSegurosId,
                TipoSeguro = TiposSeguros?.GetEntity()
            };
        }

        public static ContratosSeguros FromEntity(ContratoSeguro contratoSeguro, ContratosSeguros contratosSeguros)
        {
            ContratosSeguros contratoSeguroToSave = contratosSeguros ?? new ContratosSeguros();

            contratoSeguroToSave.Id = contratoSeguro.Id;
            contratoSeguroToSave.FechaInicio = contratoSeguro.FechaInicio;
            contratoSeguroToSave.Activo = contratoSeguro.Activo;
            contratoSeguroToSave.PacientesId = contratoSeguro.PacienteId;
            contratoSeguroToSave.TiposSegurosId = contratoSeguro.TipoSeguroId;

            return contratoSeguroToSave;
        }
    }
}
