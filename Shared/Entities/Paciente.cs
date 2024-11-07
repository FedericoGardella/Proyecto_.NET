using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.Entities
{
    public class Paciente : Persona
    {
        public string Telefono { get; set; }
        public List<Cita> Citas { get; set; }

        public long? HistoriaClinicaId { get; set; }
        public HistoriaClinica? HistoriaClinica { get; set; }
        public long? ContratoSeguroId { get; set; }
        public ContratoSeguro? ContratoSeguro { get; set; }

    }
}
