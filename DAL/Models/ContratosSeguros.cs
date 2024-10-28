using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ContratosSeguros
    {
        public ContratosSeguros() { }
        public long Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; } // Ej: Activo, Cancelado, etc.
        public Pacientes paciente { get; set; }
        public TiposSeguros tipoSeguro { get; set; }


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
