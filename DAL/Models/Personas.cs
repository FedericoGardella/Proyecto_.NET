using Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Personas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombres { get; set; }

        [Required]
        [MaxLength(100)]
        public string Apellidos { get; set; }

        public string Documento { get; set; }

        public Persona GetEntity()
        {
            Persona persona = new Persona
            {
                Id = Id,
                Nombres = Nombres,
                Apellidos = Apellidos,
                Documento = Documento
            };

            if (this is Pacientes pacientes)
            {
                persona = new Paciente
                {
                    Id = pacientes.Id,
                    Nombres = pacientes.Nombres,
                    Apellidos = pacientes.Apellidos,
                    Documento = pacientes.Documento,
                    Telefono = pacientes.Telefono,
                    ContratoSeguroId = pacientes.ContratosSegurosId,
                    HistoriasClinicas = pacientes.HistoriasClinicas?.Select(h => h.GetEntity()).ToList()
                };
            }

            return persona;
        }

        public static Personas FromEntity(Persona persona, Personas personas = null)
        {
            Personas personaToSave = personas ?? new Personas
            {
                Id = persona.Id,
                Nombres = persona.Nombres,
                Apellidos = persona.Apellidos,
                Documento = persona.Documento
            };

            if (persona is Paciente paciente)
            {
                if (personaToSave is Pacientes pacientes)
                {
                    pacientes.Telefono = paciente.Telefono;
                    pacientes.HistoriasClinicas = paciente.HistoriasClinicas?.Select(h => HistoriasClinicas.FromEntity(h, null)).ToList();
                    pacientes.ContratosSegurosId = paciente.ContratoSeguroId;
                }
            }

            return personaToSave;
        }
    }
}
