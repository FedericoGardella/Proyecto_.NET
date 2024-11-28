using Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Pacientes : Personas
    {
        [Required]
        [MaxLength(20), MinLength(4)]
        public string Telefono { get; set; }

        public List<Citas> Citas { get; set; }

        // Relación de uno a muchos con ContratosSeguros
        public List<ContratosSeguros> ContratosSeguros { get; set; }

        public List<HistoriasClinicas> HistoriasClinicas { get; set; } // Relación uno a muchos
        


        public long? FacturasId { get; set; }
        public Facturas? Facturas { get; set; }

        public Paciente GetEntity()
        {
            return new Paciente
            {
                Id = Id,
                Nombres = Nombres,
                Apellidos = Apellidos,
                Telefono = Telefono,
                Documento = Documento,
                HistoriasClinicas = HistoriasClinicas?.Select(h => h.GetEntity()).ToList(),
                FacturaId = FacturasId
            };
        }

        public static Pacientes FromEntity(Paciente paciente, Pacientes pacientes = null)
        {
            // Inicializar la instancia de Pacientes si no existe
            Pacientes pacienteToSave = pacientes ?? new Pacientes();

            // Mapear propiedades simples
            pacienteToSave.Id = paciente.Id;
            pacienteToSave.Nombres = paciente.Nombres;
            pacienteToSave.Apellidos = paciente.Apellidos;
            pacienteToSave.Telefono = paciente.Telefono;
            pacienteToSave.Documento = paciente.Documento;
            pacienteToSave.FacturasId = paciente.FacturaId;

            return pacienteToSave;
        }
    }
}
